using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FFISA.Model;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

namespace FFISA.Compras
{
    public partial class AutorizacionOC : BaseForm
    {
        //Para generar entrada de mercancia
        LogicaGlobal Logic = new LogicaGlobal();
        private Dictionary<string, string> CurrentData { get; set; }
        Dictionary<string, string> Documentacion { get; set; }
        private string MensajeAutorizacion { get; set; }


        #region events
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            CheckListOC CheckListOC = new CheckListOC(CurrentData);
            FormHelper.AbrirFormulario(this, CheckListOC, false);
        }
        private void Enviar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                if (TxtMensaje.Text == string.Empty)
                {
                    Logic.ShowException(null, "Por favor describe la solicitud.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    //Marcar en sap con los documentos que si cuenta antes de solicitar autorizacion.
                    bool MarcarDocumentos = MarcaDocumentosOC(Documentacion);
                    if (MarcarDocumentos)
                        SolicitarAutorizacion();//Solicitar autorización

                    Cursor.Current = Cursors.Default;
                    CheckListOC CheckListOC = new CheckListOC(CurrentData);
                    FormHelper.AbrirFormulario(this, CheckListOC, false);
                }
            }

            catch (Exception ex)
            {
                Logic.ShowException(ex, "No es posible solicitar autorización", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
                this.Enabled = true;
            }
        }
        #endregion

        #region methods
        public AutorizacionOC(Dictionary<string, string> CurrentData, Dictionary<string, string> Documentos)
        {
            try
            {
                InitializeComponent();
                this.CurrentData = CurrentData;
                this.Documentacion = Documentos;
                this.MensajeAutorizacion = GeneraMensajeAutorizacion(Documentos);
                this.TxtMensaje.Text = this.MensajeAutorizacion;
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible solicitar autorización para la OC: " + CurrentData["DocNum"] + " ", 230, "ERROR", "Error.wav", this, false);
                this.Close();
            }
        }
        //Generar nuevo documento para escaneos de EM
        private void SolicitarAutorizacion()
        {

            Logic.AD.RequestParameters = new Dictionary<string, string>();
            //Datos de OC checklist
            AccesoDatosGlobal.EntradaMercanciaByOC SA = new AccesoDatosGlobal.EntradaMercanciaByOC();
            SA.OrdenCompra = CurrentData["DocEntry"];
            SA.SapDocument = CurrentData["DocNum"];
            SA.Mensaje = TxtMensaje.Text;

            StringWriter sw = new StringWriter();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = false, // Evita saltos de línea
                OmitXmlDeclaration = false, // Incluye la declaración <?xml version="1.0" encoding="utf-8"?>
                Encoding = System.Text.Encoding.UTF8
            };

            using (XmlWriter writer = XmlWriter.Create(sw, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("EntradaMercanciaByOC");
                writer.WriteElementString("OrdenCompra", SA.OrdenCompra);
                writer.WriteElementString("SapDocument", SA.SapDocument);
                writer.WriteElementString("Mensaje", SA.Mensaje);
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            string Xml = sw.ToString();

            //Listado dinamico
            List<Dictionary<string, string>> items = Logic.ExecPostRequest("/Compras/SolicitarAutorizacion", Xml, false, string.Empty);


            string Message = string.Empty;
            if (items.Count > 0)
            {

                if (items[0]["Status"] == "NO" || items[0]["Status"] == "ERROR")
                {
                    Message = items[0]["Message"].ToString();
                    Logic.ShowException(null, "Se actualizó el estatus del documento en sap pero no pudo ser enviada la solicitud de autorización: " + Message, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    Message = items[0]["Message"].ToString();
                    Logic.ShowException(null, Message, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                }
            }

            Cursor.Current = Cursors.Default;
        }
        private string GeneraMensajeAutorizacion(Dictionary<string, string> Documentos)
        {
            StringBuilder Message = new StringBuilder();
            Message.Append("Se solicita autorización para el ingreso correspondiente a la OC: " + CurrentData["DocNum"]);
            Message.Append(" debido a que no se cuenta con los siguientes documentos: ");
            Message.Append(Environment.NewLine);
            Message.Append(Environment.NewLine);
            foreach (var item in Documentos)
            {
                if (item.Value == "NO" || item.Value == string.Empty)
                {
                    string field = Regex.Replace(item.Key, "(?<!^)([A-Z])", " $1");
                    field = (field == "Num Ped" ? "Número de pedimento" : field);
                    Message.Append("*" + field);
                    Message.Append(Environment.NewLine);
                }
            }
            Message.Append(Environment.NewLine);
            Message.Append("con el fin de poder realizar la entrada de mercancía correspondiente.");
            return Message.ToString();
        }
        //Marcar en SAP con los documentos que si se cuenta
        private bool MarcaDocumentosOC(Dictionary<string, string> Documentos)
        {
            bool updated = false;//Resultado Final

            //Datos de OC checklist
            AccesoDatosGlobal.OcCheckList OC = new AccesoDatosGlobal.OcCheckList();
            OC.DocEntry = CurrentData["DocEntry"];
            OC.U_CertificadoCalidad = Documentos["CertificadoCalidad"];
            OC.U_OrdenFisica = Documentos["OrdenFisica"];
            OC.U_PackingList = Documentos["PackingList"];
            OC.U_NumPed = Documentos["NumPed"];

            StringWriter sw = new StringWriter();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = false, // Evita saltos de línea
                OmitXmlDeclaration = false, // Incluye la declaración <?xml version="1.0" encoding="utf-8"?>
                Encoding = System.Text.Encoding.UTF8
            };

            using (XmlWriter writer = XmlWriter.Create(sw, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("OcCheckList");

                writer.WriteElementString("DocEntry", OC.DocEntry.ToString());
                writer.WriteElementString("CertificadoCalidad", OC.U_CertificadoCalidad);
                writer.WriteElementString("OrdenFisica", OC.U_OrdenFisica);
                writer.WriteElementString("PackingList", OC.U_PackingList);
                writer.WriteElementString("Pedimento", OC.U_NumPed);

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            string Xml = sw.ToString();
            List<Dictionary<string, string>> result = Logic.ExecPostRequest("/Compras/UpdateDetailsOrdenesCompra", Xml, false, string.Empty);
            //Si no ocurrio alguna excepcion
            if (result != null)
            {
                string Message = string.Empty;
                if (result.Count > 0)
                {

                    if (result[0]["Status"] == "NO" || result[0]["Status"] == "ERROR")
                    {
                        Message = result[0]["Message"].ToString();
                        Logic.ShowException(null, Message, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        updated = false;
                    }
                    else
                    {
                        updated = true;
                    }
                }
            }
            else
            {
                updated = false;
            }


            return updated;

        }
        #endregion

    }
}