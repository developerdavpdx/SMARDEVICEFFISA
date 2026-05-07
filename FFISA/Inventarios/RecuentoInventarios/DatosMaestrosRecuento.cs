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
using System.Reflection;

namespace FFISA.Inventarios.RecuentoInventarios
{
    public partial class DatosMaestrosRecuento : BaseForm
    {
        //Para generar entrada de mercancia
        LogicaGlobal Logic = new LogicaGlobal();
        Dictionary<string, string> CurrentData = new Dictionary<string, string>();
        Dictionary<string, string> DatosLote = new Dictionary<string, string>();
        Dictionary<string, string> DatosEnvio = new Dictionary<string, string>();
        public string EstadoInicializacion { get; set; }
        public bool LoteValidado = false;

        #region events
        private void MenuRecuentos_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                //Validar si no se guardo nada para eliminar el documento
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("Folio", CurrentData["FolioRI"]);
                Logic.AD.RequestParameters.Add("Usuario", FormHelper.Usuario);
                List<Dictionary<string, string>> Details = Logic.ExecGetRequest("/InventariosMovil/GetLinesDocumentosRecuentosHHRI", Logic.AD.RequestParameters, true, false);

                //Validar si hay registros antes de intentar enlistarlos
                if (Details[0]["Status"] == "NO" || Details[0]["Status"] == "ERROR")
                {
                    if (Details[0]["Status"] == "ERROR")
                    {
                        Logic.ShowException(null, "No fue posible continuar verifica tu conexión a los servidores e intenta de nuevo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }
                    else
                    {
                        string question = string.Empty;
                        question = Logic.ShowException(null, "El documento generado con folio: " + CurrentData["FolioRI"] + " no cuenta con ningún escaneo, si regresas al menú inicial el documento sera eliminado, ¿Deseas Continuar?", 250, "AVISO", "Aviso.wav", true);

                        if (question == "Yes")
                        {
                            string resultdel = EliminarDocumento(CurrentData["FolioRI"]);
                            if (resultdel == "OK")
                            {
                                MenuRecuentoInventarios MenuRecuentoInventarios = new MenuRecuentoInventarios();
                                FormHelper.AbrirFormulario(this, MenuRecuentoInventarios, false);
                            }
                            else
                            {
                                Logic.ShowException(null, "No fue posible continuar verifica tu conexión a los servidores e intenta de nuevo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                Cursor.Current = Cursors.Default;
                            }
                        }
                    }
                }
                else
                {
                    MenuRecuentoInventarios MenuRecuentoInventarios = new MenuRecuentoInventarios();
                    FormHelper.AbrirFormulario(this, MenuRecuentoInventarios, false);
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible continuar verifica tu conexión a los servidores: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        private void TxtLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    if (TxtLoteCLNVAL.Text == string.Empty)
                    {
                        Logic.ShowException(null,
                            "Es necesario escanear el numero de lote:",
                            "AVISO",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button2);
                        return;
                    }

                    if (ProcesarLoteYPrepararDatos())
                    {
                        if (LoteValidado)
                        {
                            GuardarRecuento();
                            LoteValidado = false;
                        }
                        else
                        {
                            LoteValidado = true;
                        }
                    }
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }
        private void TxtLoteCLNVAL_TextChanged(object sender, EventArgs e)
        {
            LoteValidado = false;
        }
        private void Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();

                if (ProcesarLoteYPrepararDatos())
                {
                    GuardarRecuento();
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex,
                    "No fue posible guardar el registro para el recuento: "
                    + CurrentData["FolioRI"],
                    "AVISO",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2);
            }
        }
        private void TxtCantidadCLNVAL_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números (0-9), punto (.) y la tecla de retroceso (Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Bloquear otros caracteres
            }
            // Evitar más de un punto decimal
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true; // Bloquear si ya hay un punto
            }
        }
        #endregion

        #region methods
        public DatosMaestrosRecuento(Dictionary<string, string> CurrentData)
        {
            InitializeComponent();
            this.CurrentData = CurrentData;
            this.TxtLoteCLNVAL.Focus();
            this.lblRecuento.Text = "Recuento: " + CurrentData["Recuento"];
            this.LblFolio.Text = "Folio: " + CurrentData["FolioRI"];

            //Guardar folio de documento para envio de lineas
            if (this.DatosEnvio.ContainsKey("FolioRI"))
                this.DatosEnvio["FolioRI"] = CurrentData["FolioRI"];
            else
                this.DatosEnvio.Add("FolioRI", CurrentData["FolioRI"]);

            ObtenerTotalRecuentoHHRI();

            this.EstadoInicializacion = "OK";
        }
        private bool validaCapturaArticulo(Panel PnlControls)
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
        private bool ValidarLoteRecuentoHHRI(string Lote)
        {
            string Excepcion = string.Empty;
            DatosLote.Clear();
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            Logic.AD.RequestParameters.Add("Lote", Lote);
            Logic.AD.RequestParameters.Add("DocEntry", CurrentData["DocEntry"]);
            Logic.AD.RequestParameters.Add("DocNum", CurrentData["Recuento"]);

            //Listado dinamico
            List<Dictionary<string, string>> result = Logic.ExecGetRequest(
                "/InventariosMovil/ValidarLoteRecuentoHHRI",
                Logic.AD.RequestParameters, false, false);

            if (result[0]["Status"] == "NO" || result[0]["Status"] == "ERROR")
            {
                Excepcion = result[0]["Message"].ToString();
                if (Excepcion.Contains("No fue posible"))
                    Excepcion = "No es posible realizar validaciones correspondientes del lote, comprueba tu conexión con el servidor e intenta de nuevo para poder continuar.";

                Logic.ShowException(null, Excepcion, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                return false;
            }

            DatosLote.Clear();
            // Copiar todas las claves dinámicamente
            foreach (var kvp in result[0])
            {
                DatosLote[kvp.Key] = kvp.Value;
            }

            return true;
        }
        private void InsertaLineasRecuentosHHRI()
        {
            string resultado = string.Empty;
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            AccesoDatosInventarios.LineasRecuentosHHRI LRI = new AccesoDatosInventarios.LineasRecuentosHHRI();
            LRI.FolioRI = DatosEnvio["FolioRI"];
            LRI.Lote = DatosEnvio["Lote"];
            LRI.Cantidad = DatosEnvio["Cantidad"];
            LRI.Articulo = DatosEnvio["Articulo"];
            LRI.Almacen = DatosEnvio["Almacen"];
            LRI.ExisteEnRecuento = DatosEnvio["ExisteEnRecuento"];
            LRI.Usuario = FormHelper.Usuario;

            StringWriter sw = new StringWriter();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = false, // Evita saltos de línea
                OmitXmlDeclaration = false, // Incluye la declaración <?xml version="1.0" encoding="utf-8"?>
                Encoding = System.Text.Encoding.UTF8
            };

            string Xml = Logic.ConvertToXml(LRI, "LineasRecuentosHHRI");

            //Listado dinamico
            List<Dictionary<string, string>> resultadoApi = Logic.ExecPostRequest("/InventariosMovil/InsertaLineasRecuentosHHRI", Xml, false, string.Empty);
            if (resultadoApi[0].ContainsKey("Folio"))
            {
                resultado = resultadoApi[0]["Folio"];
                if (!resultado.Contains("Duplicado") && !resultado.Contains("EN_USO"))
                {
                    resultado = resultadoApi[0]["Message"] + " para el folio: " + resultadoApi[0]["Folio"];
                    ObtenerTotalRecuentoHHRI();
                }
                else if(resultado.Contains("Duplicado"))
                {
                    resultado = "Ya has realizado un registro para el lote: " + LRI.Lote;
                }
                else if (resultado.Contains("EN_USO"))
                {
                    resultado = "El lote " + LRI.Lote + " ya ha sido registrado anteriormente en otro documento pendiente.";
                }

                Logic.ShowException(null, resultado, "AVISO", MessageBoxButtons.OK, (resultado.Contains("Ya has realizado") || resultado.Contains("ya ha sido registrado") ? MessageBoxIcon.Exclamation : MessageBoxIcon.Asterisk), MessageBoxDefaultButton.Button1);
                Logic.LimpiaFormulario(this.Body);
                TxtLoteCLNVAL.Focus();
            }
            else
            {
                resultado = "ERROR";
                string Mensaje = resultadoApi[0]["Message"].ToString();
                Logic.ShowException(null, Mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        private void ListadoDocumentosSAP()
        {
            try
            {
                FormHelper.AbrirFormularioConValidacion(
                this,
                () => new DocumentosSap(CurrentData),
                (form) =>
                {
                    var ir = form as DocumentosSap;
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
                Logic.ShowException(ex, "No fue posible obtener el listado de recuentos SAP.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        private void ObtenerTotalRecuentoHHRI()
        {
            //Obtener el numero de rollos
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            Logic.AD.RequestParameters.Add("FolioRI", CurrentData["FolioRI"]);
            Logic.AD.RequestParameters.Add("Usuario", FormHelper.Usuario);
            List<Dictionary<string, string>> TotalRecuento = Logic.ExecGetRequest("/InventariosMovil/GetTotalRecuentoHHRI", Logic.AD.RequestParameters, false, false);

            if (TotalRecuento[0]["Status"] == "NO" || TotalRecuento[0]["Status"] == "ERROR")
            {
                string result = TotalRecuento[0]["Message"];
                LblTotalRecuento.Text = "No fue posible obtener el número de rollos del recuento.";
            }
            else
            {
                //Asignar numero de rollos escaneados
                string Total = TotalRecuento[0]["TotalRecuento"];
                LblTotalRecuento.Text = "No. rollos: " + Total;
            }
        }
        private string EliminarDocumento(string FolioRI)
        {
            string result = string.Empty;

            try
            {
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
                    writer.WriteStartElement("DocumentosRecuentosHHRI");
                    writer.WriteElementString("Folio", FolioRI);
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }

                string Xml = sw.ToString();

                List<Dictionary<string, string>> borrado = Logic.ExecPostRequest("/InventariosMovil/EliminarDocumentosRecuentosHHRI", Xml, false, string.Empty);
                if (borrado.Count > 0)
                {
                    string Message = borrado[0]["Message"].ToString();
                    if (Message.Contains("No fue posible"))
                    {
                        result = "ERROR";
                    }
                    else
                        result = "OK";
                }
                return result;
            }

            catch (Exception ex)
            {
                StringBuilder Error = new StringBuilder();
                Error.Append(ex.Message != null ? ex.Message.ToString() : string.Empty);
                Error.Append(Environment.NewLine);
                Error.Append(Environment.NewLine);
                Error.Append(ex.InnerException != null ? ex.InnerException.ToString() : string.Empty);
                result = "No fue posible eliminar el documento: " + Error.ToString();
                return result;
            }
        }
        private bool ProcesarLoteYPrepararDatos()
        {
            bool result = ValidarLoteRecuentoHHRI(TxtLoteCLNVAL.Text);

            if (!result)
                return false;

            if (DatosLote["Codigo"] != "OK" &&
                DatosLote["Codigo"] != "LOTE_EXISTE_EN_SAP" &&
                !DatosLote["Codigo"].Contains("PUEDE_AGREGARSE"))
            {
                Logic.ShowException(null,
                    DatosLote["Mensaje"],
                    "AVISO",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2);

                LimpiarCampos();
                LoteValidado = false;
                return false;
            }

            // Cargar DatosEnvio
            SetDatoEnvio("Lote", DatosLote["Lote"]);
            SetDatoEnvio("Articulo", DatosLote["Articulo"]);
            SetDatoEnvio("Almacen", DatosLote["CodigoAlmacen"]);
            SetDatoEnvio("ExisteEnRecuento",
                DatosLote["Codigo"].Contains("NO_EN_RECUENTO,PUEDE_AGREGARSE") ? "NO" : "SI");

            // Reflejar en UI
            TxtCantidadCLNVAL.Text = DatosLote["Cantidad"];
            TxtCantidadMetrosCLNVAL.Text = DatosLote["Cantidadenmetros"];
            TxtArticuloCLNVAL.Text = DatosLote["Articulo"];
            TxtAlmacenCLNVAL.Text =
                DatosLote["CodigoAlmacen"] + "/" + DatosLote["NombreAlmacen"];

            return true;
        }
        private void SetDatoEnvio(string key, string value)
        {
            if (DatosEnvio.ContainsKey(key))
                DatosEnvio[key] = value;
            else
                DatosEnvio.Add(key, value);
        }
        private void LimpiarCampos()
        {
            TxtArticuloCLNVAL.Text = string.Empty;
            TxtAlmacenCLNVAL.Text = string.Empty;
            TxtCantidadCLNVAL.Text = string.Empty;
            TxtCantidadMetrosCLNVAL.Text = string.Empty;
            TxtLoteCLNVAL.Text = string.Empty;
        }
        private void GuardarRecuento()
        {
            if (!validaCapturaArticulo(this.Body))
            {
                Logic.ShowException(null,
                    "Es necesario llenar toda la información.",
                    "AVISO",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2);
                return;
            }

            SetDatoEnvio("Cantidad", TxtCantidadCLNVAL.Text);

            InsertaLineasRecuentosHHRI();
        }
        #endregion
    }
}