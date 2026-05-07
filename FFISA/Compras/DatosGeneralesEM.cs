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

namespace FFISA.Compras
{
    public partial class DatosGeneralesEM : BaseForm
    {
        //Para generar entrada de mercancia
        LogicaGlobal Logic = new LogicaGlobal();
        Dictionary<string, string> CurrentData = new Dictionary<string, string>();
        string Etiqueta = string.Empty;
        string OrdenCompra = string.Empty;
        string Articulo = string.Empty;
        string CantidadIngresar = string.Empty;

        #region events
        private void Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                this.Enabled = false;
                bool result = validaCapturaOC(this);
                if (!result)
                {
                    Logic.ShowException(null, "Es necesario llenar toda la información.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    //Validar si requiere rango de kilos y obtener etiqueta asignada
                    Dictionary<string,string> RequiereKilosData = Requierekilos(TxtArticuloVAL.Text);
                    Etiqueta = RequiereKilosData["Etiqueta"];
                    CurrentData["Etiqueta"] = Etiqueta;
                    if (!RequiereKilosData["RequiereKilos"].Contains("Error"))
                    {
                        //Validar cantidad ingresada
                        if (RequiereKilosData["RequiereKilos"] != string.Empty && RequiereKilosData != null && RequiereKilosData["RequiereKilos"] == "Si")
                        {

                            decimal qty = decimal.Parse(TxtCantidadCLNVAL.Text);
                            if (qty < 200 || qty > 400)
                            {
                                Logic.ShowException(null, "\u25CF La cantidad ingresada debe ser mínimo 200 y hasta 400.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                            }
                            else
                                ContinuarGuardado();
                        }

                        else
                            ContinuarGuardado();
                    }
                    else
                        Logic.ShowException(null, RequiereKilosData["RequiereKilos"], "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2); //No fue posible obtener la restriccion de kilos
                }

                Cursor.Current = Cursors.Default;
                this.Enabled = true;
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible generar el registro para la entrada de mercancía en la OC: " + CurrentData["DocNum"], "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
                this.Enabled = true;
            }
        }
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            //Conservar solo lo necesario
            var NeedData = new[] { "DocEntry", "DocNum","FolioEM","Series" };

            CurrentData = CurrentData
                .Where(kvp => NeedData.Contains(kvp.Key))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            SeleccionArticuloOC SeleccionArticuloOC = new SeleccionArticuloOC(CurrentData);
            FormHelper.AbrirFormulario(this, SeleccionArticuloOC, false);
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
        private void MenuCompras_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuCompras MenuCompras = new MenuCompras();
            FormHelper.AbrirFormulario(this, MenuCompras, false);
        }
        #endregion

        #region methods
        public DatosGeneralesEM(Dictionary<string, string> CurrentData)
        {
            InitializeComponent();
            this.CurrentData = CurrentData;
            this.TxtOrdenCompraVAL.Text = this.CurrentData["DocNum"];
            this.TxtOrdenCompraVAL.Enabled = false;
            this.TxtArticuloVAL.Text = this.CurrentData["Articulo"];
            this.TxtArticuloVAL.Enabled = false;
            this.TxtloteCLN.Enabled = false;
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
        private string InsertaDocumentosHHOC(string Impresion)
        {
            string resultado = string.Empty;

            TxtloteCLN.Text = Impresion; //Mostrar el codigo de barras

            Logic.AD.RequestParameters = new Dictionary<string, string>();
            AccesoDatosGlobal.EntradaMercanciaByOC EMOC = new AccesoDatosGlobal.EntradaMercanciaByOC();
            EMOC.OrdenCompra = CurrentData["DocEntry"];
            EMOC.SapDocument = CurrentData["DocNum"];
            EMOC.Articulo = TxtArticuloVAL.Text;
            EMOC.Linea = CurrentData["Linea"];
            EMOC.Lote = TxtloteCLN.Text;
            EMOC.Cantidad = TxtCantidadCLNVAL.Text;
            EMOC.Folio = (CurrentData.ContainsKey("FolioEM") ? CurrentData["FolioEM"] : string.Empty);

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
                writer.WriteElementString("OrdenCompra", EMOC.OrdenCompra);
                writer.WriteElementString("SapDocument", EMOC.SapDocument);
                writer.WriteElementString("Articulo", EMOC.Articulo);
                writer.WriteElementString("Linea", EMOC.Linea);
                writer.WriteElementString("Lote", EMOC.Lote);
                writer.WriteElementString("Cantidad", EMOC.Cantidad.ToString());
                writer.WriteElementString("Folio", EMOC.Folio);

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            string Xml = sw.ToString();

            //Listado dinamico
            List<Dictionary<string, string>> items = Logic.ExecPostRequest("/Compras/InsertaDocumentosHHOC", Xml, false, string.Empty);
            if (items.Count > 0)
            {
                if (items[0].ContainsKey("Folio"))
                {
                    string Folio = items[0]["Folio"].ToString();
                    if (CurrentData.ContainsKey("FolioEM"))
                        CurrentData.Remove("FolioEM");
                    CurrentData.Add("FolioEM", Folio);

                    resultado = "OK";
                }
                else
                {
                    resultado = "ERROR";
                    string Mensaje = items[0]["Message"].ToString();
                    Logic.ShowException(null, Mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }

            }
            return resultado;
        } //Generar nuevo documento para escaneos de EM
        private string RealizarImpresion()
        {
            try
            {

                string impresion = string.Empty;
                string Kilos = TxtCantidadCLNVAL.Text;
                if (Kilos == string.Empty)
                {
                    Logic.ShowException(null, "Se debe colocar la cantidad para poder imprimir la etiqueta.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    impresion = "Imcompleto";
                }
                else
                {
                    string EtiquetaAsignada = (CurrentData["Etiqueta"].ToString() != string.Empty ? CurrentData["Etiqueta"].ToString() : Etiqueta);
                    //Obtener el tipo de etiqueta
                    switch (EtiquetaAsignada)
                    {
                        case "104x83 mm": //ETIQUETA PACAS
                            impresion = ImprimeEtiquetaPacas(CurrentData["Articulo"], CurrentData["Descripcion"], Kilos);
                            break;

                        case "104x23 mm": //ETIQUETA TUBOS
                            impresion = ImprimeEtiquetaTubos(CurrentData["Articulo"], Kilos);
                            break;

                        default:
                            impresion = "Error: El artículo no cuenta con tamaño de etiqueta asignada, considera actualizar el dato maestro en sap.";
                            break;
                    }
                }

                return impresion;

            }
            catch (Exception ex)
            {
                StringBuilder Message = new StringBuilder();
                Message.Append(ex.Message != null ? ex.Message.ToString() : string.Empty);
                Message.Append(Environment.NewLine);
                Message.Append(Environment.NewLine);
                Message.Append(ex.InnerException != null ? ex.InnerException.ToString() : string.Empty);
                return "Error: No fue posible generar la etiqueta para el artículo: " + CurrentData["Articulo"] + " " + " No hay conexión con el servidor, intenta de nuevo más tarde.";
            }
        }
        private string ImprimeEtiquetaPacas(string ItemCode, string Descripcion, string Kilos)
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
                writer.WriteStartElement("ImpresionesPacas");
                writer.WriteElementString("Fecha_ingreso", DateTime.Now.ToShortDateString());
                writer.WriteElementString("Producto", ItemCode);
                writer.WriteElementString("Descripcion", Descripcion);
                writer.WriteElementString("Kilos", Kilos);
                writer.WriteElementString("Usuario", FormHelper.Usuario);
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            string Xml = sw.ToString();

            //Listado dinamico
            List<Dictionary<string, string>> items = Logic.ExecPostRequest("/ImpresionEtiquetas/Crear_ZPL_Pacas", Xml, true, "No fue posible generar la etiqueta para el artículo: ");

            string CodigoBarras = string.Empty;

            //Si hay items no se logro imprimir de acuerdo a la logica del rest api
            if (items.Count > 0)
            {
                string Message = string.Empty;

                if (items[0]["Status"] == "NO" || items[0]["Status"] == "ERROR")
                {
                    Message = items[0]["Message"].ToString();
                    Logic.ShowException(null, Message, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    CodigoBarras = "Error";
                }
                else
                {
                    CodigoBarras = items[0]["Message"].ToString();
                }
            }
            //todo ok
            else
            {
                CodigoBarras = "Error: No fue posible imprimir la etiqueta, por favor intentar de nuevo más tarde.";
            }

            return CodigoBarras;

        } //ETIQUETA PACAS
        private string ImprimeEtiquetaTubos(string ItemCode,string Kilos)
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
                writer.WriteStartElement("ImpresionesTubos");
                writer.WriteElementString("Producto", ItemCode);
                writer.WriteElementString("Kilos", Kilos);
                writer.WriteElementString("Usuario", FormHelper.Usuario);
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            string Xml = sw.ToString();

            //Listado dinamico
            List<Dictionary<string, string>> items = Logic.ExecPostRequest("/ImpresionEtiquetas/Crear_ZPL_Tubos", Xml, true, "No fue posible generar la etiqueta para el artículo: ");
            string CodigoBarras = string.Empty;
            //Si hay items no se logro imprimir de acuerdo a la logica del rest api
            if (items.Count > 0)
            {
                string Message = string.Empty;

                if (items[0]["Status"] == "NO" || items[0]["Status"] == "ERROR")
                {
                    Message = items[0]["Message"].ToString();
                    Logic.ShowException(null, Message, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    CodigoBarras = string.Empty;
                }
                else
                {
                    CodigoBarras = items[0]["Message"].ToString();
                }
            }
            //todo ok
            else
            {
                CodigoBarras = "Error: No fue posible imprimir la etiqueta, por favor intentar de nuevo más tarde.";
            }

            return CodigoBarras;
        }
        private string ValidarCantidadEMvsOC(string Impresion)
        {
            string resultado = string.Empty;
            // URL de la API o aplicación MVC a la que se quiere hacer la solicitud
            string webServiceUrl = "/Compras/ValidarCantidadEMvsOC";
            OrdenCompra = CurrentData["DocEntry"];
            Articulo = TxtArticuloVAL.Text;
            CantidadIngresar = TxtCantidadCLNVAL.Text;

            Logic.AD.RequestParameters = new Dictionary<string, string>();
            Logic.AD.RequestParameters.Add("OrdenCompra", OrdenCompra);
            Logic.AD.RequestParameters.Add("Articulo", Articulo);
            Logic.AD.RequestParameters.Add("CantidadIngresar", CantidadIngresar);
            Logic.AD.RequestParameters.Add("FolioOC", CurrentData["FolioEM"].ToString());
            List<Dictionary<string, string>> result = Logic.ExecGetRequest(webServiceUrl, Logic.AD.RequestParameters, false, false);
            if (result.Count > 0)
            {
                string EstadoCantidad = string.Empty;
                string Mensaje = string.Empty;
                bool ShowAditionalIcon = false;
                string AditionalIcon = string.Empty;

                if (result[0]["Status"] == "NO" || result[0]["Status"] == "ERROR")
                {
                    resultado = "ERROR";
                    Mensaje = result[0]["Message"].ToString();
                    Logic.ShowException(null, Mensaje, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    resultado = "OK";
                    EstadoCantidad = result[0]["EstadoCantidad"].ToString();
                    AditionalIcon = (EstadoCantidad.Contains("Considera") ? "\u25BA " : "\u25BA ");
                    Logic.ShowException(null, ("\u25CF Registro generado correctamente con folio: " + CurrentData["FolioEM"] + "."
                    + Environment.NewLine + Environment.NewLine
                    + "\u25CF Etiqueta generada: " + Impresion + ". "
                    + Environment.NewLine + Environment.NewLine + Environment.NewLine
                    + AditionalIcon + EstadoCantidad),
                    "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);

                }
            }
            TxtloteCLN.Text = string.Empty;
            TxtCantidadCLNVAL.Text = string.Empty;
            Cursor.Current = Cursors.WaitCursor;
            return resultado;
        }
        private Dictionary<string,string> Requierekilos(string Articulo)
        {
            string Mensaje = string.Empty;
            Dictionary<string, string> RequiereKilos = new Dictionary<string, string>();
            string Etiqueta = string.Empty;

            // URL de la API o aplicación MVC a la que se quiere hacer la solicitud
            string webServiceUrl = "/Compras/Requierekilos";

            Logic.AD.RequestParameters = new Dictionary<string, string>();
            Logic.AD.RequestParameters.Add("Articulo", Articulo);
            List<Dictionary<string, string>> result = Logic.ExecGetRequest(webServiceUrl, Logic.AD.RequestParameters, false, false);
            if (result.Count > 0)
            {


                if (result[0]["Status"] == "NO" || result[0]["Status"] == "ERROR")
                {
                    RequiereKilos.Add("RequiereKilos", "Error");
                    Mensaje = result[0]["Message"].ToString();
                    Logic.ShowException(null, Mensaje, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    RequiereKilos.Add("RequiereKilos", result[0]["U_RangoKilos"].ToString());
                    RequiereKilos.Add("Etiqueta", result[0]["Etiqueta"].ToString());
                }
            }
            return RequiereKilos;
        }
        private void ContinuarGuardado()
        {
            //Impresion
            string Impresion = RealizarImpresion();

            if (!Impresion.Contains("Error"))
            {
                //PASO 3 Insertar el registro del escaneo
                if (!Impresion.Contains("Imcompleto"))
                {
                    string resultado = InsertaDocumentosHHOC(Impresion);
                    if (resultado == "OK")
                    {
                        ValidarCantidadEMvsOC(Impresion);
                    }
                }
            }
            else
                Logic.ShowException(null, Impresion, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2); //No fue posible realizar la impresión
        }
        #endregion

    }
}