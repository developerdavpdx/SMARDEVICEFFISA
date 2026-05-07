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

namespace FFISA.Compras
{
    public partial class ListadoEMPendientes : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal();
        public string EstadoInicializacion { get; private set; }
        public bool Headers = false;

        #region events
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuCompras MenuCompras = new MenuCompras();
            FormHelper.AbrirFormulario(this, MenuCompras, false);
        }
        private void Generar_Click(object sender, EventArgs e)
        {
            try
            {

                FormHelper.ClickEvent();
                string OrdenCompra = string.Empty;
                string Folio = string.Empty;
                if (LvDocumentosPendientes.SelectedIndices.Count > 0)
                {
                    int index = LvDocumentosPendientes.SelectedIndices[0]; // Obtiene el índice de la fila seleccionada
                    ListViewItem item = LvDocumentosPendientes.Items[index]; // Obtiene el ítem (fila)
                    OrdenCompra = item.SubItems[2].Text;
                    Folio = item.SubItems[3].Text;


                    //Seleccionar fecha de entrega
                    DialogoFechaEntrada DFE = new DialogoFechaEntrada(Folio);
                    if (DFE.EstadoInicializacion == "OK")
                    {
                        DFE.LblTitleModule.Text = "FECHA ENTRADA MERCANCÍA";
                        DFE.lblDescriptionModule.Text = "SELECCIONE LA FECHA DE ENTRADA";
                        //Ocultar opcion de preeliminar
                        DFE.LblPreeliminar.Visible = false;
                        DFE.Preeliminar.Visible = false;

                        //Acomodar titulos
                        DFE.TxtFechaEntrada.Location = new Point(69, 61);
                        DFE.lblDescriptionModule.Location = new Point(13, 37);

                        Cursor.Current = Cursors.Default;
                        // Restaura el cursor al estado normal antes de abrir el formulario de diálogo.
                        // Es buena práctica para que el usuario no vea el ícono de "espera" mientras interactúa con la ventana modal.
                        this.Enabled = true;

                        DFE.ShowDialog();
                        // Muestra el formulario de selección de fecha (DFE) como un cuadro de diálogo modal.
                        // La ejecución se detiene aquí hasta que el usuario cierre esa ventana con "Continuar" o "Cancelar".
                        Cursor.Current = Cursors.WaitCursor;
                        this.Enabled = false;
                        // Vuelve a habilitar el formulario principal, por si fue deshabilitado antes de abrir el modal.

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



                        this.Enabled = false;

                        //Continuar con el flujo
                        if (DFE.FechaEntrada != string.Empty)
                        {

                            string resultEM = string.Empty;

                            if (LvDocumentosPendientes.SelectedIndices.Count > 0)
                            {
                                //Antes de generar la entrada de mercancia, validar que los articulos no hayan excedido la cantidad en los escaneos
                                Logic.AD.RequestParameters = new Dictionary<string, string>();
                                Logic.AD.RequestParameters.Add("OrdenCompra", OrdenCompra);
                                Logic.AD.RequestParameters.Add("FolioOC", Folio);
                                List<Dictionary<string, string>> Exceso = Logic.ExecGetRequest("/Compras/ValidarExcesoCantidadEMvsOC", Logic.AD.RequestParameters, false, true);
                                string EstadoCantidad = string.Empty;
                                if (Exceso.Count > 0)
                                {

                                    string Mensaje = string.Empty;

                                    if (Exceso[0]["Status"] == "NO" || Exceso[0]["Status"] == "ERROR")
                                    {
                                        Mensaje = Exceso[0]["Message"].ToString();
                                        Logic.ShowException(null, Mensaje, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                    }
                                    else
                                    {
                                        EstadoCantidad = Exceso[0]["EstadoCantidad"].ToString();
                                        if (EstadoCantidad.Contains("excede"))
                                            Logic.ShowException(null, "\u25CF " + EstadoCantidad, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                        else
                                            resultEM = CreateEMByOC(OrdenCompra, Folio, DFE.FechaEntrada);
                                    }
                                }
                            }
                            else
                                Logic.ShowException(null, "Selecciona un documento para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                            if (resultEM.Contains("Error"))
                            {
                                Logic.ShowException(null, "Por favor valida tu conexión a internet: " + resultEM, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                this.Close();
                            }
                            else
                            {
                                this.Enabled = true;
                                Cursor.Current = Cursors.Default;
                            }
                        }
                        else
                        {
                            Logic.ShowException(null, "debes confirmar una fecha para realizar la entrada de mercancía en la ventana de selección utilizando el icono de color verde.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                            this.Enabled = true;
                        }
                    }
                    else
                    {
                        Logic.ShowException(null, DFE.EstadoInicializacion, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        this.Enabled = true;
                    }

                }
                else
                {
                    Logic.ShowException(null, "Selecciona un documento para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    this.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No es posible generar la entrada de mercancía para el documento: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
                this.Enabled = true;
            }
        }
        private void LvOrdenesCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    ShowOCDetail();
                }
                catch (Exception ex)
                {
                    Logic.ShowException(ex, "No es posible mostrar el detalle del documento: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
                    this.Enabled = true;
                }
            }
        }
        private void ptbBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                this.Enabled = false;
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                //FI
                Logic.AD.RequestParameters.Add("FI", FI.Text);
                //FF
                Logic.AD.RequestParameters.Add("FF", FF.Text);
                //Obtener listado de entradas pendientes
                List<Dictionary<string, string>> OC = Logic.ExecGetRequest("/Compras/GetDocumentosHHOC", Logic.AD.RequestParameters, false, true);
                string result = DocumentosPendientes(OC);
                if (result.Contains("No se encontraron"))
                    Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                this.Enabled = true;
                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible consultar el listado de OC: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                this.Enabled = true;
                Cursor.Current = Cursors.Default;

            }
        }
        private void Detalle_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                ShowOCDetail();
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No es posible mostrar el detalle del documento: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        private void PtbEliminar_Click(object sender, EventArgs e)
        {
            string result = string.Empty;
            try
            {
                FormHelper.ClickEvent();
                this.Enabled = false;
                if (LvDocumentosPendientes.SelectedIndices.Count > 0) // Verifica que haya una fila seleccionada
                {
                    result = Logic.ShowException(null, "El documento con todos sus registros será eliminado ¿Deseas Continuar?", 250, "AVISO", "Aviso.wav", true);
                    //Si esta de acuerdo
                    if (result == "Yes")
                    {
                        result = EliminarDocumento();
                        if (result != "OK")
                        {
                            Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        }
                        else
                        {
                            result = "Documento eliminado.";
                            Logic.AD.RequestParameters = new Dictionary<string, string>();
                            List<Dictionary<string, string>> OC = Logic.ExecGetRequest("/Compras/GetDocumentosHHOC", Logic.AD.RequestParameters, false, true);
                            result = DocumentosPendientes(OC);
                            if (result != "OK")
                            {
                                if (result != "No se encontraron documentos nuevos.")
                                {
                                    Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                }
                                else
                                    Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                            }
                        }
                    }
                }
                else
                {
                    Logic.ShowException(null, "Selecciona un elemento de la lista para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
                this.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                if (result.Contains("Documento eliminado"))
                    Logic.ShowException(ex, "El documento fue eliminado, pero no fue posible continuar. Por favor revisa tu conexión a la red interna.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                this.Enabled = true;
                this.Close();
            }
        }
        #endregion

        #region methods
        public ListadoEMPendientes()
        {
            InitializeComponent();
            //FORMATO Año/Mes/Dia
            //FORMATO Año/Mes/Dia
            FI.Format = DateTimePickerFormat.Custom;
            FI.CustomFormat = "yyyy-MM-dd";
            FF.Format = DateTimePickerFormat.Custom;
            FF.CustomFormat = "yyyy-MM-dd";
            FI.Value = DateTime.Now;
            FF.Value = DateTime.Now;
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            List<Dictionary<string, string>> OC = Logic.ExecGetRequest("/Compras/GetDocumentosHHOC", Logic.AD.RequestParameters, false, true);
            EstadoInicializacion = DocumentosPendientes(OC);
        }
        private string DocumentosPendientes(List<Dictionary<string, string>> OClist)
        {
            string result = string.Empty;

            try
            {
                string rownumber = "0";

                // Limpiar el ListView antes de agregar nuevos elementos
                LvDocumentosPendientes.Items.Clear();

                //Validar si hay registros antes de intentar enlistarlos
                if (OClist[0]["Status"] == "NO" || OClist[0]["Status"] == "ERROR")
                {
                    result = OClist[0]["Message"];
                    return result;
                }
                else
                {

                    // Recorrer los ítems y agregarlos al ListView
                    foreach (var item in OClist)
                    {
                       
                        if (Headers == false)
                        {
                            // Agregar columnas al ListView
                            LvDocumentosPendientes.Columns.Add("ID", 0, HorizontalAlignment.Left);
                            LvDocumentosPendientes.Columns.Add("Orden Compra", -2, HorizontalAlignment.Left);//DocNum
                            LvDocumentosPendientes.Columns.Add("Sap Document", 0, HorizontalAlignment.Left);//DocEntry
                            LvDocumentosPendientes.Columns.Add("Folio", -2, HorizontalAlignment.Left);
                            LvDocumentosPendientes.Columns.Add("Estatus", -2, HorizontalAlignment.Left);
                            LvDocumentosPendientes.Columns.Add("Fecha", -2, HorizontalAlignment.Left);
                            LvDocumentosPendientes.Columns.Add("Autorizado", -2, HorizontalAlignment.Left);
                            Headers = true;
                        }

                        // Crear un nuevo ítem de ListView con la primera columna
                        ListViewItem listViewItem = new ListViewItem(item["ID"]);//0
                        listViewItem.SubItems.Add(item["SapDocument"]);        // 1
                        listViewItem.SubItems.Add(item["OrdenCompra"]);        // 2
                        listViewItem.SubItems.Add(item["Folio"]); //3
                        listViewItem.SubItems.Add(item["Estatus"]); //4
                        listViewItem.SubItems.Add(item["Fecha"]);// 5
                        listViewItem.SubItems.Add(item["Autorizacion"]);//6


                        listViewItem.BackColor = rownumber == "0" ? Color.FromArgb(242, 242, 242) : Color.White;
                        listViewItem.ForeColor = Color.Black;
                        rownumber = rownumber == "0" ? "1" : "0";
                        LvDocumentosPendientes.Items.Add(listViewItem);
                    }
                }

                if (OClist.Count == 0)
                    Detalle.Enabled = false;
                else
                    Detalle.Enabled = true;

                FormHelper.AjustarColumnas(LvDocumentosPendientes);
                result = "OK";
                return result;
            }
            catch (Exception ex)
            {
                StringBuilder Error = new StringBuilder();
                Error.Append(ex.Message != null ? ex.Message.ToString() : string.Empty);
                Error.Append(ex.InnerException != null ? ex.InnerException.ToString() : string.Empty);
                result = "No fue posible consultar el listado de documentos: " + Error.ToString();
                return result;
            }
        }
        private void ShowOCDetail()
        {
            try
            {
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                if (LvDocumentosPendientes.SelectedIndices.Count > 0)
                {
                    int index = LvDocumentosPendientes.SelectedIndices[0]; // Obtiene el índice de la fila seleccionada
                    ListViewItem item = LvDocumentosPendientes.Items[index]; // Obtiene el ítem (fila)
                    string Folio = item.SubItems[3].Text;

                    try
                    {
                        FormHelper.AbrirFormularioConValidacion(
                        this,
                        () => new DetailsEMPendientes(Folio),
                        (form) =>
                        {
                            var ir = form as DetailsEMPendientes;
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
                        Logic.ShowException(ex, "No fue posible obtener el detalle del documento.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }
                }
                else
                {
                    Logic.ShowException(null, "Debes seleccionar un documento para continuar. ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible consultar el listado de documentos pendientes: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        private string CreateEMByOC(string OrdenCompra, string Folio, string FechaEntrada)
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
                writer.WriteStartElement("MultipleEntradaMercanciaByOC");//MultipleEntradaMercanciaByOC (INICIO)
                writer.WriteElementString("OrdenCompra", OrdenCompra);
                writer.WriteElementString("Folio", Folio);
                writer.WriteElementString("Fecha", FechaEntrada);
                writer.WriteEndElement(); //MultipleEntradaMercanciaByOC(FIN)
                writer.WriteEndDocument();
            }

            string Xml = sw.ToString();
            string resultEM = string.Empty;
            string result = string.Empty;
            List<Dictionary<string, string>> items = Logic.ExecPostRequest("/Compras/CreateEMByOC", Xml, false, string.Empty);
            if (items.Count > 0)
            {
                resultEM = items[0]["Message"].ToString();
                if (resultEM.Contains("Error"))
                    Logic.ShowException(null, resultEM, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
                else
                    Logic.ShowException(null, resultEM, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            }
            Cursor.Current = Cursors.WaitCursor;
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            List<Dictionary<string, string>> OC = Logic.ExecGetRequest("/Compras/GetDocumentosHHOC", Logic.AD.RequestParameters, false, true);
            result = DocumentosPendientes(OC);
            return result;
        }
        private string EliminarDocumento()
        {
            string result = string.Empty;

            try
            {

                if (LvDocumentosPendientes.SelectedIndices.Count > 0) // Verifica que haya una fila seleccionada
                {
                    Cursor.Current = Cursors.WaitCursor;
                    int index = LvDocumentosPendientes.SelectedIndices[0]; // Obtiene el índice de la fila seleccionada
                    ListViewItem item = LvDocumentosPendientes.Items[index]; // Obtiene el ítem (fila)

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
                        writer.WriteStartElement("DocumentosHHOC");
                        writer.WriteElementString("Folio", item.SubItems[3].Text);
                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                    }

                    string Xml = sw.ToString();

                    List<Dictionary<string, string>> borrado = Logic.ExecPostRequest("/Compras/EliminarDocumentosHHOC", Xml, false, string.Empty);
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
                            Logic.ShowException(null, Message, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                        }
                    }
                }
                else
                {
                    Logic.ShowException(null, "Selecciona un elemento de la lista para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
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