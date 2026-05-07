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
using FFISA.Compras;
using FFISA.Main;

namespace FFISA.Inventarios.SalidasDirectas
{
    public partial class SalidasPendientes : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal();
        public string EstadoInicializacion { get; private set; }
        public bool RequiresPreeliminar { get; set; }
        public bool Headers = false;

        #region events
        public void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuSalidasDirectas MenuSalidasDirectas = new MenuSalidasDirectas();
            FormHelper.AbrirFormulario(this, MenuSalidasDirectas, false);
        } //Regresar a la ventana anterior
        private void Generar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                GenerarSalidaMercanciaDirecta();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No es posible generar la entrada de mercancía para el documento: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        private void LvEntradas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    ShowEntradaDetail();
                }
                catch (Exception ex)
                {
                    Logic.ShowException(ex, "No es posible mostrar el detalle del documento: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
                    Cursor.Current = Cursors.Default;
                }
            }
        }
        private void ptbBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                //FI
                Logic.AD.RequestParameters.Add("FI", FI.Text);
                //FF
                Logic.AD.RequestParameters.Add("FF", FF.Text);
                Logic.AD.RequestParameters.Add("Usuario", FormHelper.Usuario);
                List<Dictionary<string, string>> Entradas = Logic.ExecGetRequest("/InventariosMovil/GetDocumentosSalidasHHEMD", Logic.AD.RequestParameters, false, true);
                string result = DocumentosPendientes(Entradas);
                if (result.Contains("No se encontraron") || result.Contains("No se encontró información"))
                    Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);

                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible consultar el listado de entradas: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        private void Detalle_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                ShowEntradaDetail();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No es posible mostrar el detalle del documento: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        private void PtbEliminar_Click(object sender, EventArgs e)
        {
            string result = string.Empty;
            try
            {
                FormHelper.ClickEvent();
                if (LvDocumentosPendientes.SelectedIndices.Count > 0) // Verifica que haya una fila seleccionada
                {
                    result = Logic.ShowException(null, "El documento con todos sus registros será eliminado ¿Deseas Continuar?", 250, "AVISO", "Aviso.wav", true);
                    //Si esta de acuerdo
                    if (result == "Yes")
                    {

                        Cursor.Current = Cursors.WaitCursor;
                        // Restaura el cursor al estado normal antes de abrir el formulario de diálogo.
                        // Es buena práctica para que el usuario no vea el ícono de "espera" mientras interactúa con la ventana modal.

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

                        result = EliminarDocumento();
                        if (result != "OK")
                        {
                            Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        }
                        else
                        {
                            result = "Documento eliminado.";
                            Logic.AD.RequestParameters = new Dictionary<string, string>();
                            Logic.AD.RequestParameters.Add("Usuario", FormHelper.Usuario);
                            List<Dictionary<string, string>> OV = Logic.ExecGetRequest("/InventariosMovil/GetDocumentosSalidasHHEMD", Logic.AD.RequestParameters, false, true);
                            result = DocumentosPendientes(OV);
                            if (result != "OK")
                            {
                                if (result != "No se encontraron nuevos documentos.")
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
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                if (result.Contains("Documento eliminado"))
                    Logic.ShowException(ex, "El documento fue eliminado, pero no fue posible continuar. Por favor revisa tu conexión a la red interna.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        public void MenuSalidas_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuSalidasDirectas MenuSalidasDirectas = new MenuSalidasDirectas();
            FormHelper.AbrirFormulario(this, MenuSalidasDirectas, false);
        }
        #endregion

        #region methods
        public SalidasPendientes()
        {
            InitializeComponent();
            //FORMATO Año/Mes/Dia
            FI.Format = DateTimePickerFormat.Custom;
            FI.CustomFormat = "yyyy-MM-dd";
            FF.Format = DateTimePickerFormat.Custom;
            FF.CustomFormat = "yyyy-MM-dd";
            FI.Value = DateTime.Now;
            FF.Value = DateTime.Now;
            RequiresPreeliminar = false;
            EliminarDocumentosVacios();
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            Logic.AD.RequestParameters.Add("Usuario", FormHelper.Usuario);
            List<Dictionary<string, string>> Entradas = Logic.ExecGetRequest("/InventariosMovil/GetDocumentosSalidasHHEMD", Logic.AD.RequestParameters, false, true);
            EstadoInicializacion = DocumentosPendientes(Entradas);
        }
        private string DocumentosPendientes(List<Dictionary<string, string>> EntradasList)
        {
            string result = string.Empty;

            try
            {
                string rownumber = "0";

                // Limpiar el ListView antes de agregar nuevos elementos
                LvDocumentosPendientes.Items.Clear();

                //Validar si hay registros antes de intentar enlistarlos
                if (EntradasList[0]["Status"] == "NO" || EntradasList[0]["Status"] == "ERROR")
                {
                    result = EntradasList[0]["Message"];
                    return result;
                }
                else
                {
                    // Recorrer los ítems y agregarlos al ListView
                    foreach (var item in EntradasList)
                    {
                        if (Headers == false)
                        {
                            // Agregar columnas al ListView
                            LvDocumentosPendientes.Columns.Add("Folio", -2, HorizontalAlignment.Left); //0
                            LvDocumentosPendientes.Columns.Add("Usuario", -2, HorizontalAlignment.Left);//1
                            LvDocumentosPendientes.Columns.Add("Estatus", -2, HorizontalAlignment.Left);//2
                            LvDocumentosPendientes.Columns.Add("Fecha", -2, HorizontalAlignment.Left);//3
                            Headers = true;
                        }

                        // Crear un nuevo ítem de ListView con la primera columna
                        ListViewItem listViewItem = new ListViewItem(item["Folio"]);//0
                        listViewItem.SubItems.Add(item["Usuario"]);//1
                        listViewItem.SubItems.Add(item["Estatus"]);//2
                        listViewItem.SubItems.Add(item["Fecha"]);//3

                        listViewItem.BackColor = rownumber == "0" ? Color.FromArgb(242, 242, 242) : Color.White;
                        listViewItem.ForeColor = Color.Black;
                        rownumber = rownumber == "0" ? "1" : "0";
                        LvDocumentosPendientes.Items.Add(listViewItem);
                    }
                }

                if (EntradasList.Count == 0)
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
        private void ShowEntradaDetail()
        {
            try
            {
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                if (LvDocumentosPendientes.SelectedIndices.Count > 0)
                {
                    int index = LvDocumentosPendientes.SelectedIndices[0]; // Obtiene el índice de la fila seleccionada
                    ListViewItem item = LvDocumentosPendientes.Items[index]; // Obtiene el ítem (fila)
                    string Folio = item.SubItems[0].Text;


                    try
                    {
                        FormHelper.AbrirFormularioConValidacion(
                        this,
                        () => new EscaneosSalidas(Folio),
                        (form) =>
                        {
                            var ir = form as EscaneosSalidas;
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
                        Logic.ShowException(ex, "No fue posible consultar el listado de documentos pendientes.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
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
        private void GenerarSalidaMercanciaDirecta()
        {
            //Datos de envio
            string FolioEM = string.Empty;
            string Usuario = string.Empty;
            string Comentarios = string.Empty;

            if (LvDocumentosPendientes.SelectedIndices.Count > 0)
            {
                int index = LvDocumentosPendientes.SelectedIndices[0]; // Obtiene el índice de la fila seleccionada
                ListViewItem item = LvDocumentosPendientes.Items[index]; // Obtiene el ítem (fila)
                FolioEM = item.SubItems[0].Text;

                //Seleccionar fecha de entrada
                DialogoFechaEntrada DFE = new DialogoFechaEntrada(FolioEM);
                if (DFE.EstadoInicializacion == "OK")
                {
                    DFE.LblTitleModule.Text = "FECHA SALIDA MERCANCÍA";
                    DFE.lblDescriptionModule.Text = "SELECCIONE LA FECHA DE SALIDA";
                    DFE.lblDescriptionModule.Location = new Point(13, 40);
                    DFE.TxtFechaEntrada.Location = new Point(69, 64);

                    if (RequiresPreeliminar) //Mostrar opcion de preeliminar solo si se requiere
                    {
                        DFE.LblPreeliminar.Visible = true;
                        DFE.Preeliminar.Visible = true;
                    }

                    Cursor.Current = Cursors.Default;
                    // Restaura el cursor al estado normal antes de abrir el formulario de diálogo.
                    // Es buena práctica para que el usuario no vea el ícono de "espera" mientras interactúa con la ventana modal.

                    DFE.ShowDialog();
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


                    Cursor.Current = Cursors.WaitCursor;

                    //Continuar con el flujo , la opcion preeliminar es opcional
                    if (DFE.FechaEntrada != string.Empty)
                    {

                        string resultEM = string.Empty;

                        if (LvDocumentosPendientes.SelectedIndices.Count > 0)
                        {
                            //Antes de generar la entrada de mercancia, validar que los articulos no hayan excedido la cantidad en los escaneos
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
                                writer.WriteStartElement("SalidasMercanciaHHEMD");
                                writer.WriteElementString("FolioSM", FolioEM);
                                writer.WriteElementString("FechaSalida", DFE.FechaEntrada);
                                writer.WriteElementString("Usuario", FormHelper.Usuario);
                                writer.WriteEndElement();
                                writer.WriteEndDocument();
                            }

                            string Xml = sw.ToString();

                            List<Dictionary<string, string>> Entrega = Logic.ExecPostRequest("/InventariosMovil/GenerarSalidaMercancia", Xml, false, string.Empty);

                            string Message = string.Empty;
                            if (Entrega[0]["Status"] == "NO" || Entrega[0]["Status"] == "ERROR")
                            {
                                Message = Entrega[0]["Message"].ToString();
                                Logic.ShowException(null, Message, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                            }
                            else
                            {
                                Message = Entrega[0]["Message"].ToString();
                                Logic.ShowException(null, Message, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);

                                Cursor.Current = Cursors.WaitCursor;
                                Logic.AD.RequestParameters = new Dictionary<string, string>();
                                Logic.AD.RequestParameters.Add("Usuario", FormHelper.Usuario);
                                List<Dictionary<string, string>> Entradas = Logic.ExecGetRequest("/InventariosMovil/GetDocumentosSalidasHHEMD", Logic.AD.RequestParameters, false, true);
                                string result = DocumentosPendientes(Entradas);
                                if (result.Contains("No se encontraron") || result.Contains("No se encontró información"))
                                {
                                    Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                                }
                            }

                            if (resultEM.Contains("Error"))
                            {
                                Logic.ShowException(null, "Por favor valida tu conexión a internet: " + resultEM, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                            }
                            else
                            {
                                Cursor.Current = Cursors.Default;
                            }
                        }
                        else
                            Logic.ShowException(null, "Selecciona un documento para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }
                    else
                    {
                        Logic.ShowException(null, "debes confirmar una fecha para realizar la salida de mercancía en la ventana de selección utilizando el icono de color verde.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }
                }
                else
                {
                    Logic.ShowException(null, DFE.EstadoInicializacion, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
            }
            else
            {
                Logic.ShowException(null, "Selecciona un documento para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
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
                        writer.WriteStartElement("DocumentosSalidasHHEMD");
                        writer.WriteElementString("Folio", item.SubItems[0].Text);
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
        private string EliminarDocumentosVacios()
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
                    writer.WriteStartElement("DocumentosSalidasHHSMD");
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }

                string Xml = sw.ToString();

                List<Dictionary<string, string>> borrado = Logic.ExecPostRequest("/InventariosMovil/EliminarDocumentosVaciosSalidasHHSMD", Xml, false, string.Empty);
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
                result = "No fue posible eliminar los documentos temporales, regresa a la pantalla anterior y vuelve a ingresar para reintentar: " + Error.ToString();
                return result;
            }
        }
        #endregion
    }
}