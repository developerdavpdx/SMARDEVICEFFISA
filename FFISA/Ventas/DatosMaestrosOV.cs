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
using FFISA.Main;
using System.Threading;

namespace FFISA.Ventas
{
    public partial class DatosMaestrosOV : BaseForm
    {
        //Para generar entrada de mercancia
        LogicaGlobal Logic = new LogicaGlobal();
        LogicaVentas LogicVentas = new LogicaVentas();
        public Dictionary<string, string> CurrentData { get; set; }
        public string EstadoInicializacion { get; set; }
        private int reintentos = 0;

        #region events
        private void Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                bool result = validaCapturaOC(this);
                if (!result)
                {
                    Logic.ShowException(null, "Es necesario llenar toda la información.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    //Generar registro automatico con folio consecutivo para orden de venta
                    string Resultado = InsertaEncabezadoVentasHHEMOV();
                    if (Resultado == "OK" || Resultado == "SI") //Avanzar a la siguiente pantalla
                    {
                        Logic.ShowException(null, ("\u25CF Registro generado correctamente con folio: " + CurrentData["FolioEM"] + "."
                     + Environment.NewLine + Environment.NewLine),
                     "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);

                        IntentaObtenerListado(); //Avanzar a la siguiente pantalla
                    }
                    else if (Resultado == "EMAIL_MISSING")
                    {
                        Logic.ShowException(null, ("\u25CF Se generó el registro correctamente con folio: " + CurrentData["FolioEM"] + ", pero la notificación a facturación no pudo ser enviada."
                     + Environment.NewLine + Environment.NewLine),
                     "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        Cursor.Current = Cursors.WaitCursor;
                        IntentaObtenerListado(); //Avanzar a la siguiente pantalla
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible generar el documento consecutivo para la orden de venta: " + CurrentData["OrdenVenta"], "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            OrdenesVenta();
        }
        private void MenuEntregas_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuEntregas MenuEntregas = new MenuEntregas();
            FormHelper.AbrirFormulario(this, MenuEntregas, false);
        }
        #endregion

        #region methods
        public DatosMaestrosOV(Dictionary<string, string> CurrentData)
        {
            InitializeComponent();
            this.CurrentData = CurrentData;
            this.CurrentData.Add("FromNuevoDocumento", "SI");
            this.TxtOrdenVentaVAL.Text = this.CurrentData["OrdenVenta"];
            this.TxtOrdenVentaVAL.Enabled = false;
            this.TxtClienteVAL.Text = this.CurrentData["Cliente"];
            this.TxtClienteVAL.Enabled = false;
            this.TxtUsuarioVAL.Text = FormHelper.Usuario;
            this.TxtUsuarioVAL.Enabled = false;
            this.EstadoInicializacion = "OK";
        }
        private bool validaCapturaOC(Form PnlControls)
        {
            foreach (Control ctrl in PnlControls.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox txt = (TextBox)ctrl;
                    //Solo a los txt que contienen en su nombre VAL deben ser validados
                    if (txt.Name.Contains("VAL"))
                    {
                        if (txt.Text == string.Empty)
                            return false;
                        else
                            continue;
                    }

                }
            }

            return true;
        }
        private string InsertaEncabezadoVentasHHEMOV()
        {
            string resultado = string.Empty;
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            AccesoDatosVentas.EncabezadoVentasHHEMOV EEMOV = new AccesoDatosVentas.EncabezadoVentasHHEMOV();
            EEMOV.OrdenVenta = CurrentData["DocEntry"];
            EEMOV.SapDocument = CurrentData["OrdenVenta"];
            EEMOV.Cliente = CurrentData["CodigoCliente"];
            EEMOV.Usuario = FormHelper.Usuario;
            EEMOV.Comentarios = TxtComentariosVALCLN.Text;
            EEMOV.Estatus = "Pendiente";

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
                writer.WriteStartElement("EncabezadoVentasHHEMOV");
                writer.WriteElementString("OrdenVenta", EEMOV.OrdenVenta);
                writer.WriteElementString("SapDocument", EEMOV.SapDocument);
                writer.WriteElementString("Cliente", EEMOV.Cliente);
                writer.WriteElementString("Usuario", EEMOV.Usuario);
                writer.WriteElementString("Comentarios", EEMOV.Comentarios);
                writer.WriteElementString("Estatus", EEMOV.Estatus);


                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            string Xml = sw.ToString();

            //Listado dinamico
            List<Dictionary<string, string>> items = Logic.ExecPostRequest("/VentasMovil/InsertaEncabezadoVentasHHEMOV", Xml, false, string.Empty);

            if (items[0].ContainsKey("Folio"))
            {
                //Guardar el folio obtenido de la cabecera de la entrega de mercancia
                string Folio = items[0]["Folio"].ToString();
                string Estatus = items[0]["Status"].ToString();
                if (CurrentData.ContainsKey("FolioEM"))
                    CurrentData.Remove("FolioEM");
                CurrentData.Add("FolioEM", Folio);
                resultado = Estatus;
            }
            else
            {
                resultado = "ERROR";
                string Mensaje = items[0]["Message"].ToString();
                Logic.ShowException(null, Mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            return resultado;
        } //Generar encabezado de entrega de mercancia
        private void IntentaObtenerListado()
        {
            try
            {
                bool isok = false;
                FormHelper.AbrirFormularioConValidacion(
                this,
                () => new ArticulosOV(CurrentData),
                (form) =>
                {
                    var ir = form as ArticulosOV;
                    if (ir.EstadoInicializacion == "OK")
                    {
                        isok = true;
                        ir.Regresar.Visible = false;
                        ir.MenuEntregas.Location = new Point(8, 1);
                        return true;
                    }
                    else
                    {
                        isok = false;
                        return false;
                    }
                }, false);

                //Si no fue posible mostrar el listado de articulos
                if (!isok) {
                    string question = string.Empty;
                    if (reintentos > 0)
                        question = Logic.ShowException(null, "No fue posible obtener el listado de artículos por orden de venta debido a que no hay conexión con el servidor, ¿deseas reintentar? en caso contrario serás redirigido al menu de entregas.", 250, "AVISO", "Aviso.wav", true);
                    else
                        question = Logic.ShowException(null, "Se guardo el folio pero no fue posible obtener el listado de artículos por orden de venta debido a que no hay conexión con el servidor, ¿deseas reintentar? en caso contrario serás redirigido al menu de entregas.", 250, "AVISO", "Aviso.wav", true);

                    if (question == "Yes")
                    {
                        reintentos++;
                        this.IntentaObtenerListado();
                    }
                    else
                    {
                        reintentos = 0;
                        MenuEntregas MenuEntregas = new MenuEntregas();
                        FormHelper.AbrirFormulario(this, MenuEntregas, false);
                    }
                }

            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible mostrar el listado de artículos de la orden de venta.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        private void OrdenesVenta()
        {
            try
            {
                FormHelper.AbrirFormularioConValidacion(
                this,
                () => new OrdenesVenta(),
                (form) =>
                {
                    var ir = form as OrdenesVenta;
                    if (ir.EstadoInicializacion == "OK")
                    {
                        return true;
                    }
                    else
                    {
                        Logic.ShowException(null, ir.EstadoInicializacion, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
                        return false;
                    }
                }, false);
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible obtener el listado de ordenes de venta.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        #endregion
    }
}