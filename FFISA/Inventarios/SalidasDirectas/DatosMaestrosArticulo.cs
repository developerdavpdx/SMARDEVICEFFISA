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

namespace FFISA.Inventarios.SalidasDirectas
{
    public partial class DatosMaestrosArticulo : BaseForm
    {
        //Para generar entrada de mercancia
        LogicaGlobal Logic = new LogicaGlobal();
        Dictionary<string, string> CurrentData = new Dictionary<string, string>();
        public string EstadoInicializacion { get; set; }

        #region events
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            ListadoArticulos();
        }
        private void AñadirArticulo_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            ListadoArticulos();
        }
        private void MenuEntradas_Click(object sender, EventArgs e)
        {

            try
            {
                FormHelper.ClickEvent();
                Cursor.Current = Cursors.WaitCursor;
                //Validar si no se guardo nada para eliminar el documento
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("Folio", CurrentData["FolioSD"]);
                Logic.AD.RequestParameters.Add("Usuario", FormHelper.Usuario);
                List<Dictionary<string, string>> Details = Logic.ExecGetRequest("/InventariosMovil/GetLinesDocumentosSalidasHHEMD", Logic.AD.RequestParameters, true, false);

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
                        question = Logic.ShowException(null, "El documento generado con folio: " + CurrentData["FolioSD"] + " no cuenta con ningún escaneo, si regresas al menú inicial el documento sera eliminado, ¿Deseas Continuar?", 250, "AVISO", "Aviso.wav", true);

                        if (question == "Yes")
                        {
                            string resultdel = EliminarDocumento(CurrentData["FolioSD"]);
                            if (resultdel == "OK")
                            {
                                MenuSalidasDirectas MenuSalidasDirectas = new MenuSalidasDirectas();
                                FormHelper.AbrirFormulario(this, MenuSalidasDirectas, false);
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
                    MenuSalidasDirectas MenuSalidasDirectas = new MenuSalidasDirectas();
                    FormHelper.AbrirFormulario(this, MenuSalidasDirectas, false);
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
                    if (TxtLoteCLN.Text == string.Empty)
                    {
                        Logic.ShowException(null, "Es necesario escanear el numero de lote.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }
                    else
                    {
                        ValidarLoteInventariosHH();
                    }
                }
                catch (Exception ex)
                {
                    Logic.ShowException(ex, "No fue posible realizar validaciones para el lote: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    Cursor.Current = Cursors.Default;
                    this.Enabled = true;
                }
            }
        }
        private void ValidarAlmacen_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                ValidaAlmacenHHEMD();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible validar el almacén, intenta de nuevo más tarde: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        private void ModificarCuentaContable_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();

                //Seleccionar cuenta contable
                CuentasContables CC = new CuentasContables();

                if (CC.EstadoInicializacion == "OK")
                {
                    Cursor.Current = Cursors.Default;
                    // Restaura el cursor al estado normal antes de abrir el formulario de diálogo.
                    // Es buena práctica para que el usuario no vea el ícono de "espera" mientras interactúa con la ventana modal.

                    CC.ShowDialog();
                    // Muestra el formulario de selección de fecha (DFE) como un cuadro de diálogo modal.
                    // La ejecución se detiene aquí hasta que el usuario cierre esa ventana con "Continuar" o "Cancelar".

                    this.BringToFront();
                    // Asegura que el formulario principal se muestre por encima de cualquier otra ventana que pudo quedarse "flotando".
                    // A veces necesario en CF si el sistema no lo hace automáticamente.

                    this.Refresh();
                    // Fuerza el repintado inmediato del formulario principal (redibuja los controles visibles).
                    // Muy útil porque CF no siempre actualiza la interfaz gráfica al volver de un modal.

                    Application.DoEvents();
                    // Procesa todos los mensajes pendientes del sistema (como repintado, eventos de foco, etc).
                    // Esto es **crucial en Compact Framework 3.5**, donde el redibujado puede retrasarse si no se fuerza este ciclo.
                    // Asegura que la UI esté completamente actualizada antes de continuar con la lógica del programa.

                    //Continuar con el flujo , la opcion preeliminar es opcional
                    if (CC.CuentaSeleccionada.Count > 0)
                    {
                        TxtCuentaContableVAL.Text = CC.CuentaSeleccionada["CuentaContable"];
                        Cursor.Current = Cursors.Default;
                        //Actualizar datos de cuenta contable seleccionada
                        CurrentData["CodigoCuentaContable"] = CC.CuentaSeleccionada["CodigoCuentaContable"];
                    }
                    else
                    {
                        Logic.ShowException(null, "debes confirmar la nueva cuenta contable en la ventana de selección utilizando el icono de color verde.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }
                }
                else
                {
                    Logic.ShowException(null, CC.EstadoInicializacion, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible mostrar el listado de cuentas contables disponibles, intenta de nuevo más tarde: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        private void Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                bool validacion = validaCapturaArticulo(this.Body);
                if (validacion)
                    InsertaLineasSalidasHHEMD();
                else
                    Logic.ShowException(null, "Es necesario llenar toda la información.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
            catch (Exception ex)
            {

                Logic.ShowException(ex, "No fue posible guardar el registro para el artículo: " + CurrentData["Articulo"] + ", intenta de nuevo más tarde: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        private void TxtCantidadMetrosCLNVAL_KeyPress(object sender, KeyPressEventArgs e)
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
        public DatosMaestrosArticulo(Dictionary<string, string> CurrentData)
        {
            InitializeComponent();
            this.CurrentData = CurrentData;
            TxtArticulo.Text = this.CurrentData["CodigoArticulo"];
            this.TxtCuentaContableVAL.Text = this.CurrentData["CuentaContable"];
            TxtLoteCLN.Focus();
            EstadoInicializacion = "OK";
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
        private void ValidaAlmacenHHEMD()
        {
            Dictionary<string, string> resultado = new Dictionary<string, string>();
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            Logic.AD.RequestParameters.Add("Almacen", TxtAlmacenVAL.Text);
            //Listado dinamico
            List<Dictionary<string, string>> result = Logic.ExecGetRequest("/InventariosMovil/ValidaAlmacenHHEMD", Logic.AD.RequestParameters, false, false);

            if (result[0]["Status"] == "NO" || result[0]["Status"] == "ERROR")
            {
                string Exception = result[0]["Message"].ToString();
                Logic.ShowException(null, Exception, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                TxtAlmacenVAL.Text = string.Empty;
            }
            else
            {
                SoundPlayer.ReproducirSonido("Inicio.wav");
                resultado.Add("CodigoAlmacen", result[0]["CodigoAlmacen"]);
                resultado.Add("NombreAlmacen", result[0]["NombreAlmacen"]);
                TxtAlmacenVAL.Text = resultado["CodigoAlmacen"] + "/" + resultado["NombreAlmacen"];
                //Actualizar datos de almacen seleccionado
                CurrentData["CodigoAlmacen"] = resultado["CodigoAlmacen"];
                CurrentData["NombreAlmacen"] = resultado["NombreAlmacen"];
            }
        }
        private void ValidarLoteInventariosHH()
        {
            string estatus = string.Empty;
            string Excepcion = string.Empty;
            Dictionary<string, string> DatosLote = new Dictionary<string, string>();
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            Logic.AD.RequestParameters.Add("ItemCode", CurrentData["CodigoArticulo"]);
            Logic.AD.RequestParameters.Add("Lote", TxtLoteCLN.Text);


            //Listado dinamico
            List<Dictionary<string, string>> result = Logic.ExecGetRequest("/InventariosMovil/ValidarLoteInventariosHH", Logic.AD.RequestParameters, false, false);

            if (result[0]["Status"] == "NO" || result[0]["Status"] == "ERROR")
            {
                Excepcion = result[0]["Message"].ToString();
                Logic.ShowException(null, Excepcion, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                TxtLoteCLN.Text = string.Empty;
            }
            else
            {
                DatosLote.Add("Codigo", result[0]["Codigo"]);
                DatosLote.Add("Mensaje", result[0]["Mensaje"]);
                if (result[0].ContainsKey("Cantidad"))
                    DatosLote.Add("Cantidad", result[0]["Cantidad"]);
                if (result[0].ContainsKey("Cantidadenmetros"))
                    DatosLote.Add("Cantidadenmetros", result[0]["Cantidadenmetros"]);
                if (result[0].ContainsKey("Comentarios"))
                    DatosLote.Add("Comentarios", result[0]["Comentarios"]);
                if (result[0].ContainsKey("CodigoAlmacen"))
                    DatosLote.Add("CodigoAlmacen", result[0]["CodigoAlmacen"]);
                if (result[0].ContainsKey("NombreAlmacen"))
                    DatosLote.Add("NombreAlmacen", result[0]["NombreAlmacen"]);

                //Actualizar codigo de almacen
                if (CurrentData.ContainsKey("CodigoAlmacen"))
                {
                    if (result[0].ContainsKey("CodigoAlmacen"))
                        CurrentData["CodigoAlmacen"] = result[0]["CodigoAlmacen"];
                }
                else
                {
                    if (result[0].ContainsKey("CodigoAlmacen"))
                        CurrentData.Add("CodigoAlmacen", result[0]["CodigoAlmacen"]);
                }

                if (DatosLote["Codigo"] != "OK" && DatosLote["Codigo"] != "LOTE_EXISTE_EN_SAP")
                {
                    Logic.ShowException(null, DatosLote["Mensaje"], "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    TxtLoteCLN.Text = string.Empty;
                    TxtAlmacenVAL.Text = string.Empty;
                    TxtCantidadCLNVAL.Text = string.Empty;
                    TxtCantidadMetrosCLN.Text = string.Empty;
                    TxtComentariosCLN.Text = string.Empty;
                }
                else
                {
                    TxtAlmacenVAL.Text = DatosLote["CodigoAlmacen"] + "/" + DatosLote["NombreAlmacen"];
                    TxtCantidadCLNVAL.Text = DatosLote["Cantidad"];
                    TxtCantidadMetrosCLN.Text = DatosLote["Cantidadenmetros"];
                    TxtComentariosCLN.Text = DatosLote["Comentarios"];
                }
            }
            Cursor.Current = Cursors.Default;
        }
        private void InsertaLineasSalidasHHEMD()
        {
            string resultado = string.Empty;
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            AccesoDatosInventarios.LineasSalidasHHEMD LED = new AccesoDatosInventarios.LineasSalidasHHEMD();
            LED.Folio = CurrentData["FolioSD"];
            LED.Articulo = CurrentData["CodigoArticulo"];
            LED.Almacen = CurrentData["CodigoAlmacen"];
            LED.Lote = TxtLoteCLN.Text; ;
            LED.Cantidad = TxtCantidadCLNVAL.Text;
            LED.CantidadMetros = TxtCantidadMetrosCLN.Text;
            LED.Comentarios = TxtComentariosCLN.Text;
            LED.CuentaContable = TxtCuentaContableVAL.Text;
            LED.CodigoCuentaContable = CurrentData["CodigoCuentaContable"];


            StringWriter sw = new StringWriter();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = false, // Evita saltos de línea
                OmitXmlDeclaration = false, // Incluye la declaración <?xml version="1.0" encoding="utf-8"?>
                Encoding = System.Text.Encoding.UTF8
            };

            string Xml = Logic.ConvertToXml(LED, "LineasSalidasHHEMD");

            //Listado dinamico
            List<Dictionary<string, string>> resultadoApi = Logic.ExecPostRequest("/InventariosMovil/InsertaLineasSalidasHHEMD", Xml, false, string.Empty);
            if (resultadoApi[0].ContainsKey("Folio"))
            {
                resultado = resultadoApi[0]["Folio"];
                if (!resultado.Contains("Duplicado"))
                {
                    resultado = resultadoApi[0]["Message"] + " para el folio: " + resultadoApi[0]["Folio"];
                    Logic.LimpiaFormulario(this.Body);
                }
                else
                {
                    resultado = "Ya has realizado un registro para el lote: " + LED.Lote;
                }

                Logic.ShowException(null, resultado, "AVISO", MessageBoxButtons.OK, (resultado.Contains("Ya has realizado") ? MessageBoxIcon.Exclamation : MessageBoxIcon.Asterisk), MessageBoxDefaultButton.Button1);
            }
            else
            {
                resultado = "ERROR";
                string Mensaje = resultadoApi[0]["Message"].ToString();
                Logic.ShowException(null, Mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        private void ListadoArticulos()
        {

            //Conservar solo lo necesario
            var NeedData = new[] { "FolioSD" };

            CurrentData = CurrentData
                .Where(kvp => NeedData.Contains(kvp.Key))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            Articulos Articulos = new Articulos(CurrentData);
            FormHelper.AbrirFormulario(this, Articulos, false);
        }
        private string EliminarDocumento(string Folio)
        {
            string result = string.Empty;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

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
                    writer.WriteStartElement("DocumentosSalidasHHEMD");
                    writer.WriteElementString("Folio", Folio);
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }

                string Xml = sw.ToString();

                List<Dictionary<string, string>> borrado = Logic.ExecPostRequest("/InventariosMovil/EliminarDocumentosSalidasHHEMD", Xml, false, string.Empty);
                if (borrado.Count > 0)
                {
                    string Message = borrado[0]["Message"].ToString();
                    if (Message.Contains("No fue posible"))
                    {
                        result = "ERROR";
                    }

                    else
                    {
                        result = "OK";
                    }
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
        #endregion
    }
}