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

namespace FFISA.Inventarios.EntradasDirectas
{
    public partial class DatosMaestrosArticulo : BaseForm
    {
        //Para generar entrada de mercancia
        LogicaGlobal Logic = new LogicaGlobal();
        Dictionary<string, string> CurrentData = new Dictionary<string, string>();
        public string EstadoInicializacion { get; set; }
        private bool AlmacenValidado = true;
        private bool MensajeAvisoREGISTRADO_SIN_STOCK = false;
        private const string TIPO_MATERIA_PRIMA = "MATERIA PRIMA";
        private const string TIPO_MAQUILAS = "MAQUILAS";
        private const string CAMPO_SELECTED = "SI";
        private List<string> _plantillasSeleccionadas;
        private bool _esMateriaPrimaOMaquilas;
        private bool _plantillasCargadas;

        #region events
        private void Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                ProcesarGuardado();
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex,
                    "No fue posible guardar el registro para el artículo: " + CurrentData["Articulo"],
                    "AVISO",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2);
            }
        }
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
            FormHelper.ClickEvent();
            Cursor.Current = Cursors.WaitCursor;
            //Validar si no se guardo nada para eliminar el documento
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            Logic.AD.RequestParameters.Add("Folio", CurrentData["FolioEM"]);
            Logic.AD.RequestParameters.Add("Usuario", FormHelper.Usuario);
            List<Dictionary<string, string>> Details = Logic.ExecGetRequest("/InventariosMovil/GetLinesDocumentosEntradasHHEMD", Logic.AD.RequestParameters, true, false);

            //Validar si hay registros antes de intentar enlistarlos
            if (Details[0]["Status"] == "NO")
            {
                string question = string.Empty;
                question = Logic.ShowException(null, "El documento generado con folio: " + CurrentData["FolioEM"] + " no cuenta con ningún escaneo, si regresas al menú inicial el documento sera eliminado, ¿Deseas Continuar?", 250, "AVISO", "Aviso.wav", true);

                if (question == "Yes")
                {
                    string resultdel = EliminarDocumento(CurrentData["FolioEM"]);
                    if (resultdel == "OK")
                    {
                        MenuEntradasDirectas MenuEntradasDirectas = new MenuEntradasDirectas();
                        FormHelper.AbrirFormulario(this, MenuEntradasDirectas, false);
                    }
                    else
                    {
                        Logic.ShowException(null, "No fue posible continuar verifica tu conexión a los servidores e intenta de nuevo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            else if (Details[0]["Status"] == "ERROR")
            {
                Logic.ShowException(null, "No fue posible validar el estado del documento, comprueba tu conexión con el servidor e intenta de nuevo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
            else
            {
                MenuEntradasDirectas MenuEntradasDirectas = new MenuEntradasDirectas();
                FormHelper.AbrirFormulario(this, MenuEntradasDirectas, false);
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
        private void TxtLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (TxtLoteCLN.Text == string.Empty)
                    {
                        Logic.ShowException(null,
                            "Es necesario escanear el numero de lote:",
                            "AVISO",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button2);
                        return;
                    }

                    var lote = ValidarLoteInventariosHHEMD("EscaneoLote");

                    if (lote["LoteValidado"] == "SI" && (TxtCantidadCLNVAL.Text != string.Empty || TxtCantidadMetrosCLNVAL.Text != string.Empty))
                    {
                        ProcesarGuardado(); // 👈 MISMA lógica
                    }

                    Cursor.Current = Cursors.Default;
                }
                catch (Exception ex)
                {
                    Logic.ShowException(ex,
                        "No fue posible realizar validaciones para el lote:",
                        "AVISO",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2);
                    Cursor.Current = Cursors.Default;
                }
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
            if (e.KeyChar == (char)Keys.Enter)
            {
                ProcesarGuardado(); // 👈 MISMA lógica
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
            if (e.KeyChar == (char)Keys.Enter)
            {
                ProcesarGuardado(); // 👈 MISMA lógica
            }
        }
        private void TxtAlmacenVAL_KeyPress(object sender, KeyPressEventArgs e)
        {
            AlmacenValidado = false;
        }
        #endregion

        #region methods
        public DatosMaestrosArticulo(Dictionary<string, string> CurrentData)
        {
            InitializeComponent();
            this.CurrentData = CurrentData;
            this.TxtArticulo.Text = this.CurrentData["CodigoArticulo"];
            this.TxtAlmacenVAL.Text = this.CurrentData["CodigoAlmacen"] + "/" + this.CurrentData["Almacen"];
            if (this.CurrentData["Etiqueta"] == "SI")
            {
                this.TxtLoteCLN.BackColor = Color.White;
                this.TxtLoteCLN.Enabled = true;
                this.TxtLoteCLN.Focus();
            }
            else
            {
                this.TxtLoteCLN.BackColor = Color.LightGray;
                this.TxtLoteCLN.Enabled = false;
                TxtCantidadCLNVAL.Focus();
            }
            this.TxtCuentaContableVAL.Text = this.CurrentData["CuentaContable"];
            this.EstadoInicializacion = "OK";
        }
        private bool validaCapturaArticulo(Panel PnlControls, string RequiereCM)
        {
            // Verificar que las plantillas se hayan cargado correctamente
            if (!_plantillasCargadas)
            {
                return false;
            }

            foreach (Control ctrl in PnlControls.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox txt = (TextBox)ctrl;

                    // Solo a los txt que contienen en su nombre VAL deben ser validados
                    if (txt.Name.Contains("VAL"))
                    {
                        // Si es MATERIA PRIMA o MAQUILAS, saltar la validación de TxtCantidadMetrosCLNVAL
                        if (RequiereCM == "NO" && txt.Name == "TxtCantidadMetrosCLNVAL")
                        {
                            continue; // No validar este campo
                        }

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
                AlmacenValidado = false;
                TxtAlmacenVAL.Text = string.Empty;
            }
            else
            {
                SoundPlayer.ReproducirSonido("Inicio.wav");
                resultado.Add("CodigoAlmacen", result[0]["CodigoAlmacen"]);
                resultado.Add("NombreAlmacen", result[0]["NombreAlmacen"]);
                AlmacenValidado = true;
                TxtAlmacenVAL.Text = resultado["CodigoAlmacen"] + "/" + resultado["NombreAlmacen"];
                //Actualizar datos de almacen seleccionado
                CurrentData["CodigoAlmacen"] = resultado["CodigoAlmacen"];
                CurrentData["NombreAlmacen"] = resultado["NombreAlmacen"];
            }
        }
        private Dictionary<string, string> ValidarLoteInventariosHHEMD(string Origen)
        {
            string estatus = string.Empty;
            string Excepcion = string.Empty;
            Dictionary<string, string> ResultLote = new Dictionary<string, string>();
            //Validar si ya cuentan con la etiqueta del lote
            if (CurrentData["Etiqueta"] == "NO")
            {
                ResultLote.Add("LoteValidado", "SI");
                ResultLote.Add("LoteExistente", string.Empty);
            }
            else
            {
                Dictionary<string, string> DatosLote = new Dictionary<string, string>();
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("ItemCode", CurrentData["CodigoArticulo"]);
                Logic.AD.RequestParameters.Add("Lote", TxtLoteCLN.Text);
                Logic.AD.RequestParameters.Add("Folio", CurrentData["FolioEM"]);


                //Listado dinamico
                List<Dictionary<string, string>> result = Logic.ExecGetRequest("/InventariosMovil/ValidarLoteInventariosHH", Logic.AD.RequestParameters, false, false);

                if (result[0]["Status"] == "NO" || result[0]["Status"] == "ERROR")
                {
                    Excepcion = result[0]["Message"].ToString();
                    Logic.ShowException(null, Excepcion, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    TxtCantidadCLNVAL.Text = string.Empty;
                    TxtCantidadMetrosCLNVAL.Text = string.Empty;
                    TxtComentariosCLN.Text = string.Empty;
                    ResultLote.Add("LoteValidado", "NO");
                    ResultLote.Add("LoteExistente", string.Empty);
                }
                else
                {
                    DatosLote.Add("Codigo", result[0]["Codigo"]);
                    DatosLote.Add("Mensaje", result[0]["Mensaje"]);
                    if (DatosLote["Codigo"] != "OK" && DatosLote["Codigo"] != "REGISTRADO_SIN_STOCK" && DatosLote["Codigo"] != "NO_EXISTE")
                    {
                        Logic.ShowException(null, DatosLote["Mensaje"], "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        TxtCantidadCLNVAL.Text = string.Empty;
                        TxtCantidadMetrosCLNVAL.Text = string.Empty;
                        TxtComentariosCLN.Text = string.Empty;
                        TxtLoteCLN.Focus();
                        ResultLote.Add("LoteValidado", "NO");
                        ResultLote.Add("LoteExistente", string.Empty);
                    }
                    //EL LOTE NO EXISTE PERO SE REGISTRARA
                    else
                    {
                        if (DatosLote["Codigo"] == "REGISTRADO_SIN_STOCK")
                        {
                            Logic.ShowException(null, (Origen.Contains("Guardado") ? "● Se procedera con el guardado pero considera lo siguiente: " + Environment.NewLine + Environment.NewLine + DatosLote["Mensaje"] : "● " + DatosLote["Mensaje"]), "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                        }

                        ResultLote.Add("LoteValidado", "SI");
                        ResultLote.Add("LoteExistente", (DatosLote["Codigo"] == "OK" ? "SI" : "NO"));
                    }
                }
            }

            Cursor.Current = Cursors.Default;
            return ResultLote;
        }
        private void InsertaLineasEntradasHHEMD(string RequiereCM, string LoteExistente)
        {

            string resultado = string.Empty;
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            AccesoDatosInventarios.LineasEntradasHHEMD LED = new AccesoDatosInventarios.LineasEntradasHHEMD();
            LED.Folio = CurrentData["FolioEM"];
            LED.Articulo = CurrentData["CodigoArticulo"];
            //Si el lote no existe que se imprima etiqueta
            LED.Etiqueta = CurrentData["Etiqueta"];//(LoteExistente == "NO" ? "NO" : CurrentData["Etiqueta"]); 
            LED.Almacen = CurrentData["CodigoAlmacen"];
            LED.Lote = TxtLoteCLN.Text;
            LED.Cantidad = TxtCantidadCLNVAL.Text;

            // Usar la variable de clase
            LED.CantidadMetros = TxtCantidadMetrosCLNVAL.Text;

            LED.Comentarios = TxtComentariosCLN.Text;
            LED.CuentaContable = TxtCuentaContableVAL.Text;
            LED.CodigoCuentaContable = CurrentData["CodigoCuentaContable"];

            // Validar cantidad
            try
            {
                decimal cantidad = decimal.Parse(LED.Cantidad);
                if (cantidad <= 0)
                {
                    string Mensaje = "La cantidad a ingresar debe ser mayor a 0.";
                    Logic.ShowException(null, Mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            catch
            {
                string Mensaje = "La cantidad ingresada no es válida.";
                Logic.ShowException(null, Mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            // Solo validar metros si NO es MATERIA PRIMA o MAQUILAS
            if (RequiereCM == "SI")
            {
                try
                {
                    decimal cantidadMetros = decimal.Parse(LED.CantidadMetros);
                    if (cantidadMetros <= 0)
                    {
                        string Mensaje = "La cantidad de metros a ingresar debe ser mayor a 0.";
                        Logic.ShowException(null, Mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }
                catch
                {
                    string Mensaje = "La cantidad de metros ingresada no es válida.";
                    Logic.ShowException(null, Mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
            }

            // Resto del código igual...
            StringWriter sw = new StringWriter();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = false,
                OmitXmlDeclaration = false,
                Encoding = System.Text.Encoding.UTF8
            };
            string Xml = Logic.ConvertToXml(LED, "LineasEntradasHHEMD");

            List<Dictionary<string, string>> resultadoApi = Logic.ExecPostRequest("/InventariosMovil/InsertaLineasEntradasHHEMD", Xml, false, string.Empty);
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
                    TxtCantidadCLNVAL.Text = string.Empty;
                    TxtCantidadMetrosCLNVAL.Text = string.Empty;
                    TxtComentariosCLN.Text = string.Empty;
                    TxtLoteCLN.Focus();
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
            var NeedData = new[] { "FolioEM" };

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
                    writer.WriteStartElement("DocumentosEntradasHHEMD");
                    writer.WriteElementString("Folio", Folio);
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }

                string Xml = sw.ToString();

                List<Dictionary<string, string>> borrado = Logic.ExecPostRequest("/InventariosMovil/EliminarDocumentosEntradasHHEMD", Xml, false, string.Empty);
                if (borrado.Count > 0)
                {
                    string Message = borrado[0]["Message"].ToString();
                    if (Message.Contains("No fue posible"))
                    {
                        result = "ERROR";
                        Logic.ShowException(null, Message, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
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
        private List<string> ListaPlantillas()
        {
            try
            {
                // Obtener series de numeración
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("Usuario", FormHelper.Usuario);
                List<Dictionary<string, string>> plantillas = Logic.ExecGetRequest("/InventariosMovil/GetPlantillas", Logic.AD.RequestParameters, false, false);

                //Validar si hay registros antes de intentar enlistarlos
                if (plantillas == null || plantillas.Count == 0)
                {
                    // No hay datos, retornar null para indicar error
                    return null;
                }

                if (plantillas[0]["Status"] == "NO" || plantillas[0]["Status"] == "ERROR")
                {
                    // Error de API/conexión, retornar null para indicar error
                    return null;
                }
                else
                {
                    // Filtrar plantillas - si no hay coincidencias, retorna lista vacía (NO es error)
                    List<string> nombresPlantillas = plantillas
                        .Where(p => p.ContainsKey("plantilla") &&
                                    p.ContainsKey("Selected") &&
                                    (p["plantilla"].ToUpper().Contains(TIPO_MATERIA_PRIMA) ||
                                     p["plantilla"].ToUpper().Contains(TIPO_MAQUILAS)) &&
                                    p["Selected"] == CAMPO_SELECTED)
                        .Select(p => p["plantilla"])
                        .ToList();

                    // Retornar la lista (puede estar vacía si no hay coincidencias, y eso está bien)
                    return nombresPlantillas;
                }
            }
            catch (Exception ex)
            {
                StringBuilder Error = new StringBuilder();
                Error.Append(ex.Message != null ? ex.Message.ToString() : string.Empty);
                Error.Append(Environment.NewLine);
                Error.Append(Environment.NewLine);
                Error.Append(ex.InnerException != null ? ex.InnerException.ToString() : string.Empty);
                string mensajeError = "No fue posible obtener el listado de plantillas: " +
                    (Error.ToString().Contains("SocketException") ?
                    "No es posible establecer conexión con el servidor, intenta de nuevo más tarde." :
                    Error.ToString());
                MessageBox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                // Retornar null en caso de excepción (error real)
                return null;
            }
        }
        private bool CargarPlantillas()
        {
            try
            {
                _plantillasSeleccionadas = ListaPlantillas();

                // Si es null, hubo un error de conexión o API
                if (_plantillasSeleccionadas == null)
                {
                    _plantillasCargadas = false;
                    _esMateriaPrimaOMaquilas = false;
                    return false;
                }

                // Si está vacía (Count == 0), no hubo error, solo no hay plantillas que coincidan
                // En este caso, la plantilla NO es de MATERIA PRIMA ni MAQUILAS
                _esMateriaPrimaOMaquilas = _plantillasSeleccionadas.Count > 0;

                _plantillasCargadas = true;
                return true;
            }
            catch (Exception ex)
            {
                _plantillasCargadas = false;
                _esMateriaPrimaOMaquilas = false;
                Logic.ShowException(ex, "No fue posible cargar las plantillas. Verifica tu conexión e intenta de nuevo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return false;
            }
        }
        private Dictionary<string, string> RequiereCantidadMetros(string ItemCode)
        {
            string Mensaje = string.Empty;
            Dictionary<string, string> NeedCantidadMetros = new Dictionary<string, string>();
            string Etiqueta = string.Empty;

            // URL de la API o aplicación MVC a la que se quiere hacer la solicitud
            string webServiceUrl = "/InventariosMovil/RequiereMetros";

            Logic.AD.RequestParameters = new Dictionary<string, string>();
            Logic.AD.RequestParameters.Add("ItemCode", ItemCode);
            List<Dictionary<string, string>> result = Logic.ExecGetRequest(webServiceUrl, Logic.AD.RequestParameters, false, false);
            if (result.Count > 0)
            {
                if (result[0]["Status"] == "NO" || result[0]["Status"] == "ERROR")
                {
                    NeedCantidadMetros.Add("RequiereMetros", "Error");
                    Mensaje = result[0]["Message"].ToString();
                    Logic.ShowException(null, "No fue posible validar si el artículo requiere cantidad en metros, por favor intente de nuevo para continuar.: " + Mensaje, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    NeedCantidadMetros.Add("RequiereMetros", result[0]["RequiereMetros"].ToString());
                }
            }
            return NeedCantidadMetros;
        }
        private void ProcesarGuardado()
        {
            if (!AlmacenValidado)
            {
                Logic.ShowException(null,
                    "Es necesario que valides el almacén ingresado con el icono de color verde.",
                    "AVISO",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2);
                return;
            }

            if (!CargarPlantillas())
            {
                Logic.ShowException(null,
                    "No fue posible obtener la información de plantillas. Verifica tu conexión e intenta de nuevo.",
                    "ERROR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                return;
            }

            Dictionary<string, string> RequiereMetros = RequiereCantidadMetros(TxtArticulo.Text);
            string RCM = RequiereMetros["RequiereMetros"];

            if (RCM.Contains("Error"))
                return;

            string RequiereCM = (RCM == "No") ? "NO" : "SI";

            if (!validaCapturaArticulo(this.Body, RequiereCM))
            {
                Logic.ShowException(null,
                    "Es necesario llenar toda la información.",
                    "AVISO",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2);
                return;
            }

            Dictionary<string, string> Lote = ValidarLoteInventariosHHEMD("GuardadoLote");

            if (Lote["LoteValidado"] == "SI")
                InsertaLineasEntradasHHEMD(RequiereCM, Lote["LoteExistente"]);
        }
        #endregion
    }
}