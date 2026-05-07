using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FFISA.Model;
using System.Net;
using System.IO;
using System.Xml;

namespace FFISA.Inventarios.TransferenciaStock
{
    public partial class MenuTransferencias : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal();
        Dictionary<string, string> CurrentData = new Dictionary<string, string>();
        public string EstadoInicializacion { get; set; }

        #region events
        private void PbNuevoDocumento_Click(object sender, EventArgs e)
        {
            string Resultado = string.Empty;

            try
            {
                FormHelper.ClickEvent();
                //Generar registro automatico con folio consecutivo para entregas
                Resultado = InsertaEncabezadoTransferencias();

                if (Resultado == "OK" || Resultado == "SI")
                {
                    DatosMaestrosTraspaso DatosMaestrosTraspaso = new DatosMaestrosTraspaso(CurrentData);
                    FormHelper.AbrirFormulario(this, DatosMaestrosTraspaso, false);
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                if (Resultado == "OK" || Resultado == "SI")
                    Logic.ShowException(ex, "Se guardo el folio de documento, pero no fue posible mostrar la vista de artículos, intenta de nuevo más tarde: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                else
                Logic.ShowException(ex, "No fue posible mostrar la vista de artículos, intenta de nuevo más tarde: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                Cursor.Current = Cursors.Default;
            }
        }
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuInventarios MenuInventarios = new MenuInventarios();
            FormHelper.AbrirFormulario(this, MenuInventarios, false);
        }
        private void PbEMPendientes_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();

                FormHelper.AbrirFormularioConValidacion(
                this,
                () => new TransferenciasPendientes(),
                (form) =>
                {
                    var ir = form as TransferenciasPendientes;
                    if (ir.EstadoInicializacion == "OK")
                    {
                        return true;
                    }
                    else
                    {
                        Logic.ShowException(null, ir.EstadoInicializacion, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        return false;
                    }
                }, false);
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible mostrar el listado de documentos pendientes .", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        #endregion

        #region methods
        public MenuTransferencias()
        {
            InitializeComponent();
        }
        private string InsertaEncabezadoTransferencias()
        {
            string resultado = string.Empty;
            AccesoDatosInventarios.EncabezadoTransferenciasHHTS TS = new AccesoDatosInventarios.EncabezadoTransferenciasHHTS();
            TS.Usuario = FormHelper.Usuario;

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
                writer.WriteStartElement("EncabezadoTransferenciasHHTS");
                writer.WriteElementString("Usuario", TS.Usuario);
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            string Xml = sw.ToString();

            //Listado dinamico
            List<Dictionary<string, string>> items = Logic.ExecPostRequest("/InventariosMovil/InsertaEncabezadoTransferenciasHHTS", Xml, false, string.Empty);

            if (items[0].ContainsKey("Folio"))
            {
                //Guardar el folio obtenido de la cabecera de la entrega de mercancia
                CurrentData = new Dictionary<string, string>();
                string Folio = items[0]["Folio"].ToString();
                string Estatus = items[0]["Status"].ToString();
                if (CurrentData.ContainsKey("FolioTS"))
                    CurrentData.Remove("FolioTS");
                CurrentData.Add("FolioTS", Folio);
                resultado = Estatus;
            }
            else
            {
                resultado = "ERROR";
                string Mensaje = items[0]["Message"].ToString();
                Logic.ShowException(null, Mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            return resultado;
        }
        #endregion
    }
}