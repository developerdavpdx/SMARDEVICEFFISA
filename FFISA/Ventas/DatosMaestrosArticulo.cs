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
    public partial class DatosMaestrosArticulo : BaseForm
    {
        //Para generar entrada de mercancia
        LogicaGlobal Logic = new LogicaGlobal();
        LogicaVentas LogicVentas = new LogicaVentas();
        private Dictionary<string, string> CurrentData { get; set; }
        public List<Dictionary<string, string>> DetallesArticulo { get; set; }
        public string EstadoInicializacion { get; set; }
        private bool LoteValidado { get; set; }

        #region events
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            ArticulosOVF();
        }
        private void MenuEntregas_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuEntregas MenuEntregas = new MenuEntregas();
            FormHelper.AbrirFormulario(this, MenuEntregas, false);
        }
        private void TxtLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    if (TxtLoteCLN.Text == string.Empty)
                    {
                        Logic.ShowException(null, "Es necesario escanear el numero de lote: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }
                    else
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        ValidarLoteVentasHHEMOV();
                        TxtLoteCLN.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Logic.ShowException(ex, "No fue posible realizar validaciones para el lote: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    Cursor.Current = Cursors.Default;
                }
            }
        }
        private void FinalizarEscaneo_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                if (FormHelper.EscaneosVentas > 0)
                {
                    string question = string.Empty;
                    string HasRollos = this.ObtenerNumeroRollos();
                    if (HasRollos == "NO")
                    {
                        Logic.ShowException(null, "No fue posible obtener el número de rollos de la orden de venta debido a que no hay conexión con el servidor, por favor intenta de nuevo para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }
                    else
                    {
                        question = Logic.ShowException(null, "El total de rollos escaneados es de: " + FormHelper.EscaneosVentas + ", ¿deseas continuar? en caso contrario puedes seguir agregando escaneos.", 250, "AVISO", "Aviso.wav", true);

                        if (question == "Yes")
                        {
                            try
                            {
                                FormHelper.AbrirFormularioConValidacion(
                                this,
                                () => new EntregasPendientes(),
                                (form) =>
                                {
                                    var ir = form as EntregasPendientes;
                                    if (ir.EstadoInicializacion == "OK")
                                    {
                                        ir.Modificar.Visible = false; //Desactivar Modificar
                                        ir.Eliminar.Visible = false; //Desactivar eliminar
                                        ir.Detalle.Location = new Point(8, 3); //Mover el boton de detalle a la localizacion del boton eliminar

                                        ir.Guardar.Location = new Point(87, 3);  //Agregar nuevo boton con leyenda (Guardar)
                                        ir.Footer.Controls.Add(ir.Guardar); //Agregar al panel
                                        ir.Guardar.Visible = true; //Mostrar boton con leyenda guardar
                                        ir.Guardar.Click += new EventHandler(ir.MenuEntregas_Click); //Asignar evento click

                                        ir.RequiresPreeliminar = true; //Indicar que se necesita la opcion de preeliminar
                                        ir.FromDatosMaestros = true; //Indicar que venemos de datos maestros para mostrar diferentes botones
                                        ir.Regresar.Visible = false; //ocultar boton regresar
                                        ir.LblTitleDocPend.Location = new Point(28, 8);

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
                                Logic.ShowException(ex, "No fue posible mostrar los datos maestros del artículo seleccionado.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                            }
                        }
                    }
                }
                else
                {
                    Logic.ShowException(null, "No se ha generado ningún escaneo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible mostrar la vista de entregas pendientes, intenta de nuevo más tarde: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }

        } //Generar encabezado de entrega de mercancia
        private void ChkCantidadReferencia_Click(object sender, EventArgs e)
        {
            bool ischecked = this.ChkCantidadReferenciaCLN.Checked;
            switch (ischecked)
            {
                case true:
                    TxtCantidadReferenciaCLN.Enabled = true;
                    break;

                case false:
                    TxtCantidadReferenciaCLN.Text = string.Empty;
                    TxtCantidadReferenciaCLN.Enabled = false;
                    break;
            }
        }
        private void TxtCantidadReferenciaCLN_KeyPress(object sender, KeyPressEventArgs e)
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
            List<Dictionary<string, string>> DatosArticulo = ObtenerDatosMaestrosArticulo(CurrentData["DocEntry"], CurrentData["CodigoArticulo"]);
            //Validar si hay registros antes de intentar enlistarlos
            if (DatosArticulo[0]["Status"] == "NO" || DatosArticulo[0]["Status"] == "ERROR")
            {
                EstadoInicializacion = DatosArticulo[0]["Message"];
            }
            else
            {
                DetallesArticulo = DatosArticulo; //Guardar los detalles para mas tarde
                this.TxtArticulo.Text = DatosArticulo[0]["CodigoArticulo"];
                this.TxtAlmacen.Text = DatosArticulo[0]["NombreAlmacen"];
                this.TxtUMS.Text = DatosArticulo[0]["DescripcionUnidadSolicitada"];
                this.TxtUMI.Text = DatosArticulo[0]["UnidadMedidaInventario"];
                //Obtener el numero de rollos
                ObtenerNumeroRollos();
                TxtLoteCLN.Focus();
                LoteValidado = false;
                this.EstadoInicializacion = "OK";
            }
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
        private void ValidarLoteVentasHHEMOV()
        {
            string estatus = string.Empty;
            string stock = "0";
            string Excepcion = string.Empty;

            try
            {
                // ✅ VALIDAR CurrentData primero
                if (CurrentData == null ||
                    !CurrentData.ContainsKey("DocEntry") ||
                    !CurrentData.ContainsKey("CodigoArticulo"))
                {
                    Logic.ShowException(null,
                        "No se encontraron los datos del artículo.\n\nIntenta escanear nuevamente.",
                        "AVISO",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2);
                    return;
                }

                //Actualizar los DATOS MAESTROS DE ARTICULOS
                List<Dictionary<string, string>> DatosArticulo = ObtenerDatosMaestrosArticulo(CurrentData["DocEntry"], CurrentData["CodigoArticulo"]);

                // ✅ VALIDAR que DatosArticulo no esté vacío
                if (DatosArticulo == null || DatosArticulo.Count == 0)
                {
                    Logic.ShowException(null,
                        "No se pudo obtener la información del artículo.\n\nVerifica tu conexión e intenta de nuevo.",
                        "AVISO",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2);
                    return;
                }

                DetallesArticulo = DatosArticulo;


                if (DetallesArticulo[0]["Status"] == "NO" || DetallesArticulo[0]["Status"] == "ERROR")
                {
                    Excepcion = DetallesArticulo[0].ContainsKey("Message") ? DetallesArticulo[0]["Message"].ToString() : "Error desconocido";
                    Logic.ShowException(null, Excepcion, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }

                else
                {
                    Dictionary<string, string> DatosLote = new Dictionary<string, string>();
                    Logic.AD.RequestParameters = new Dictionary<string, string>();
                    Logic.AD.RequestParameters.Add("ItemCode", DetallesArticulo[0]["CodigoArticulo"]);
                    Logic.AD.RequestParameters.Add("Lote", TxtLoteCLN.Text);
                    Logic.AD.RequestParameters.Add("WhsOV", DetallesArticulo[0]["CodigoAlmacen"]);
                    Logic.AD.RequestParameters.Add("UMS", DetallesArticulo[0]["DescripcionUnidadSolicitada"]);

                    //Listado dinamico
                    List<Dictionary<string, string>> result = Logic.ExecGetRequest("/VentasMovil/ValidarLoteVentasHHEMOV", Logic.AD.RequestParameters, false, false);

                    // ✅ VALIDAR que result no esté vacío
                    if (result == null || result.Count == 0)
                    {
                        Logic.ShowException(null,
                            "No fue posible obtener respuesta del servidor.\n\nVerifica tu conexión e intenta de nuevo más tarde.",
                            "AVISO",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button2);
                        return;
                    }

                    if (result[0]["Status"] == "NO" || result[0]["Status"] == "ERROR")
                    {
                        Excepcion = result[0].ContainsKey("Message") ? result[0]["Message"].ToString() : "Error desconocido";
                        Logic.ShowException(null, Excepcion, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }
                    else
                    {
                        // ✅ VALIDAR QUE TODAS LAS CLAVES EXISTAN ANTES DE ACCEDER
                        if (!result[0].ContainsKey("Codigo") ||
                            !result[0].ContainsKey("Mensaje") ||
                            !result[0].ContainsKey("StockLoteOV") ||
                            !result[0].ContainsKey("StockLoteOVConversion") ||
                            !result[0].ContainsKey("CodigoAlmacenAdicionalConStock"))
                        {
                            Logic.ShowException(null,
                                "No fue posible obtener los datos del lote del servidor.\n\nVerifica tu conexión e intenta de nuevo más tarde.",
                                "AVISO",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button2);
                            return;
                        }

                        DatosLote.Add("Codigo", result[0]["Codigo"]);
                        DatosLote.Add("Mensaje", result[0]["Mensaje"]);
                        DatosLote.Add("StockLoteOV", result[0]["StockLoteOV"]); //Cantidad Nativa
                        DatosLote.Add("StockLoteOVConversion", result[0]["StockLoteOVConversion"]); //Cantidad Conversion
                        DatosLote.Add("CodigoAlmacenAdicionalConStock", result[0]["CodigoAlmacenAdicionalConStock"]);

                        //Validar resultado y continuar segun el caso
                        switch (DatosLote["Codigo"])
                        {
                            case "OK": //guardar automaticamente y avanzar a la siguiente pantalla
                                stock = DatosLote["StockLoteOVConversion"];
                                string UMS = DetallesArticulo[0]["DescripcionUnidadSolicitada"];
                                //Mostrar Cantidad de acuerdo a unidad de medida solicitada
                                switch (UMS)
                                {
                                    case "Yardas":
                                        TxtCantidadCLN.Text = stock;
                                        break;
                                    default:
                                        TxtCantidadCLN.Text = stock;
                                        break;
                                }
                                if ((stock == "0" || stock == "" || stock == null) && (UMS.ToUpper() == "YARDAS" || UMS.ToUpper() == "METROS"))
                                    Logic.ShowException(null, "El lote escaneado no cuenta con cantidad en metros.",
                                   "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                else
                                {
                                    if (LoteValidado)
                                    {
                                        InsertaLineasVentasHHEMOV(DatosLote["StockLoteOV"], DatosLote["StockLoteOVConversion"]);
                                        LoteValidado = false;
                                        Logic.LimpiaFormulario(this.Body);
                                    }
                                    else
                                        LoteValidado = true;
                                }
                                break;

                            default:
                                Logic.ShowException(null, (DatosLote["Mensaje"]),
                                    "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                Cursor.Current = Cursors.WaitCursor;
                                stock = DatosLote["StockLoteOV"];
                                TxtCantidadCLN.Text = stock;
                                TxtLoteCLN.Text = string.Empty;
                                break;
                        }
                    }

                    ObtenerNumeroRollos(); //Obtener numero de rollos siempre
                }
            }
            catch (KeyNotFoundException ex)
            {
                // ✅ Capturar específicamente el error de clave no encontrada
                Logic.ShowException(null,
                    "No fue posible obtener los datos necesarios.\n\nVerifica tu conexión e intenta de nuevo más tarde.",
                    "AVISO",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2);
            }
            catch (IndexOutOfRangeException ex)
            {
                // ✅ Capturar error de índice fuera de rango
                Logic.ShowException(null,
                    "Los datos recibidos están incompletos.\n\nVerifica tu conexión e intenta de nuevo más tarde.",
                    "AVISO",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2);
            }
            catch (Exception ex)
            {
                // ✅ Capturar cualquier otro error inesperado
                Logic.ShowException(null,
                    "Ocurrió un error al validar el lote. comprueba tu conexión al servidor, \n\nIntenta de nuevo más tarde.",
                    "ERROR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void InsertaLineasVentasHHEMOV(string CantidadNativa, string CantidadConversion)
        {
            string resultado = string.Empty;
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            AccesoDatosVentas.LineasVentasHHEMOV LVEM = new AccesoDatosVentas.LineasVentasHHEMOV();
            LVEM.Folio = CurrentData["FolioEM"];
            LVEM.Articulo = DetallesArticulo[0]["CodigoArticulo"];
            LVEM.Linea = DetallesArticulo[0]["Linea"];
            LVEM.Almacen = DetallesArticulo[0]["CodigoAlmacen"];
            LVEM.UMV = DetallesArticulo[0]["UnidadMedidaVenta"];
            LVEM.UMI = DetallesArticulo[0]["UnidadMedidaInventario"];
            LVEM.UMS = DetallesArticulo[0]["DescripcionUnidadSolicitada"];
            LVEM.Lote = TxtLoteCLN.Text;
            LVEM.Cantidad = CantidadNativa; //Cantidad nativa en kilos
            LVEM.CantidadConversion = CantidadConversion; //Cantidad en metros o yardas
            LVEM.Referencia = ChkCantidadReferenciaCLN.Checked ? "SI" : "NO";
            LVEM.CantidadReferencia = TxtCantidadReferenciaCLN.Text;
            LVEM.Usuario = FormHelper.Usuario;


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
                writer.WriteStartElement("LineasVentasHHEMOV");

                // Escribimos cada propiedad como elemento XML
                writer.WriteElementString("Folio", LVEM.Folio);
                writer.WriteElementString("Articulo", LVEM.Articulo);
                writer.WriteElementString("Linea", LVEM.Linea);
                writer.WriteElementString("Almacen", LVEM.Almacen);
                writer.WriteElementString("UMV", LVEM.UMV);
                writer.WriteElementString("UMI", LVEM.UMI);
                writer.WriteElementString("UMS", LVEM.UMS);
                writer.WriteElementString("Lote", LVEM.Lote);
                writer.WriteElementString("Cantidad", LVEM.Cantidad);
                writer.WriteElementString("CantidadConversion", LVEM.CantidadConversion);
                writer.WriteElementString("Referencia", LVEM.Referencia);
                writer.WriteElementString("CantidadReferencia", LVEM.CantidadReferencia);
                writer.WriteElementString("Usuario", LVEM.Usuario);

                writer.WriteEndElement(); // Cierra LineasVentasHHEMOV
                writer.WriteEndDocument();
            }

            string Xml = sw.ToString();

            //Listado dinamico
            List<Dictionary<string, string>> resultadoApi = Logic.ExecPostRequest("/VentasMovil/InsertaLineasVentasHHEMOV", Xml, false, string.Empty);
            if (resultadoApi[0].ContainsKey("Folio"))
            {
                resultado = resultadoApi[0]["Folio"];
                if (!resultado.Contains("Duplicado"))
                {
                    resultado = resultadoApi[0]["Message"] + " para el folio: " + resultadoApi[0]["Folio"];
                }
                else
                {
                    resultado = "Ya has realizado un registro para el lote: " + LVEM.Lote;
                    Logic.ShowException(null, resultado, "AVISO", MessageBoxButtons.OK, (resultado.Contains("Ya has realizado") ? MessageBoxIcon.Exclamation : MessageBoxIcon.Asterisk), MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                resultado = "ERROR";
                string Mensaje = resultadoApi[0]["Message"].ToString();
                Logic.ShowException(null, Mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }

            ObtenerNumeroRollos(); //Obtener numero de rollos siempre
        }
        private string ObtenerNumeroRollos()
        {
            //Obtener el numero de rollos
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            Logic.AD.RequestParameters.Add("Folio", CurrentData["FolioEM"]);
            List<Dictionary<string, string>> Rollos = Logic.ExecGetRequest("/VentasMovil/GetRollosHHEMOV", Logic.AD.RequestParameters, false, false);

            if (Rollos[0]["Status"] == "NO" || Rollos[0]["Status"] == "ERROR")
            {
                string result = Rollos[0]["Message"];
                return "NO";
            }
            else
            {
                //Asignar numero de rollos escaneados
                string NumeroRollos = Rollos[0]["NumeroRollos"];
                LblNoRollos.Text = "No. rollos: " + NumeroRollos;
                FormHelper.EscaneosVentas = int.Parse(NumeroRollos);
                return "SI";
            }
        }
        private List<Dictionary<string, string>> ObtenerDatosMaestrosArticulo(string DocEntry, string CodigoArticulo)
        {
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            Logic.AD.RequestParameters.Add("OV", DocEntry);
            Logic.AD.RequestParameters.Add("Articulo", CodigoArticulo);
            List<Dictionary<string, string>> DatosArticulo = Logic.ExecGetRequest("/VentasMovil/GetArticulosPorOVHH", Logic.AD.RequestParameters, true, true);
            return DatosArticulo;
        }
        private void ArticulosOVF()
        {
            try
            {
                //Conservar solo lo necesario
                var NeedData = new[] { "DocEntry", "OrdenVenta", "FolioEM", "FromNuevoDocumento" };

                CurrentData = CurrentData
                    .Where(kvp => NeedData.Contains(kvp.Key))
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);


                FormHelper.AbrirFormularioConValidacion(
                this,
                () => new ArticulosOV(CurrentData),
                (form) =>
                {
                    var ir = form as ArticulosOV;
                    if (ir.EstadoInicializacion == "OK")
                    {
                        if (CurrentData["FromNuevoDocumento"] == "SI")
                        {
                            ir.Regresar.Visible = false;
                            ir.MenuEntregas.Location = new Point(8, 1);
                            ir.MenuEntregas.Visible = true;
                        }
                        else
                        {
                            ir.MenuEntregas.Visible = false; //Ocultar menu para regresar a menu ventas
                            ir.Regresar.Visible = true;
                            ir.Regresar.Location = new Point(8, 3);
                            ir.lblOV.Text = "Folio: " + CurrentData["FolioEM"] + "";
                        }

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
                Logic.ShowException(ex, "No es posible abrir la edición del documento ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        #endregion
    }
}