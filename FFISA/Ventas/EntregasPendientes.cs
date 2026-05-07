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

namespace FFISA.Ventas
{
    public partial class EntregasPendientes : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal();
        public string EstadoInicializacion { get; private set; }
        public bool RequiresPreeliminar { get; set; }
        public bool FromDatosMaestros { get; set; } //Viene desde los datos maestros
        private int CantidadRollos { get; set; }
        public bool Headers = false;

        #region events
        public void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuEntregas MenuEntregas = new MenuEntregas();
            FormHelper.AbrirFormulario(this, MenuEntregas, false);
        }
        private void Generar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                // ✅ PROTECCIÓN: Deshabilitar botón inmediatamente
                GenerarEntrega.Enabled = false;

                // ✅ Validar que haya selección
                if (LvDocumentosPendientes.SelectedIndices.Count == 0)
                {
                    Logic.ShowException(null, "Selecciona un documento para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    GenerarEntrega.Enabled = true;
                    return;
                }
                // ✅ Obtener folio seleccionado
                int index = LvDocumentosPendientes.SelectedIndices[0];
                ListViewItem item = LvDocumentosPendientes.Items[index];
                string folioActual = item.SubItems[2].Text; // FolioEM
                string OV = item.SubItems[5].Text; // OV

                string question = string.Empty;
                string HowManyRollos = this.ObtenerNumeroRollosv2(folioActual);
                if (HowManyRollos == "NO")
                {
                    Logic.ShowException(null, "No fue posible obtener el número de rollos de la orden de venta debido a que no hay conexión con el servidor, por favor intenta de nuevo para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    GenerarEntrega.Enabled = true;
                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    question = Logic.ShowException(null, "El total de rollos escaneados para la OV " + OV + " es de " + HowManyRollos + ", ¿deseas continuar? en caso contrario puedes seguir agregando escaneos.", 250, "AVISO", "Aviso.wav", true);

                    if (question == "Yes")
                    {
                        GenerarEntregaMercanciaOV();
                        Cursor.Current = Cursors.Default;
                        GenerarEntrega.Enabled = true;
                    }
                }
                GenerarEntrega.Enabled = true;
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No es posible generar la entrega de mercancía para el documento: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
                GenerarEntrega.Enabled = true;
            }
        }
        private void LvOrdenesCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    ShowOVDetail();
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
                //Usuario
                Logic.AD.RequestParameters.Add("Usuario", FormHelper.Usuario);
                //Obtener listado de entradas pendientes
                List<Dictionary<string, string>> OV = Logic.ExecGetRequest("/VentasMovil/GetDocumentosVentasHHEMOV", Logic.AD.RequestParameters, false, true);
                string result = DocumentosPendientes(OV);
                if (result.Contains("No se encontraron") || result.Contains("No se encontró información"))
                    Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);

                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible consultar el listado de entregas: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;

            }
        }
        private void Detalle_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                ShowOVDetail();
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
                            List<Dictionary<string, string>> OV = Logic.ExecGetRequest("/VentasMovil/GetDocumentosVentasHHEMOV", Logic.AD.RequestParameters, false, true);
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
            }
        }
        private void Modificar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                ModificarDocumento();
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No es posible abrir la edición del documento ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        public void MenuEntregas_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuEntregas MenuEntregas = new MenuEntregas();
            FormHelper.AbrirFormulario(this, MenuEntregas, false);
        }
        #endregion

        #region methods
        public EntregasPendientes()
        {
            InitializeComponent();
            //FORMATO Año/Mes/Dia
            FI.Format = DateTimePickerFormat.Custom;
            FI.CustomFormat = "yyyy-MM-dd";
            FF.Format = DateTimePickerFormat.Custom;
            FF.CustomFormat = "yyyy-MM-dd";
            FI.Value = DateTime.Now;
            FF.Value = DateTime.Now;
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            Logic.AD.RequestParameters.Add("Usuario", FormHelper.Usuario);
            List<Dictionary<string, string>> OV = Logic.ExecGetRequest("/VentasMovil/GetDocumentosVentasHHEMOV", Logic.AD.RequestParameters, false, true);
            EstadoInicializacion = DocumentosPendientes(OV);
        }
        private string DocumentosPendientes(List<Dictionary<string, string>> OVList)
        {
            string result = string.Empty;

            try
            {
                string rownumber = "0";

                // Limpiar el ListView antes de agregar nuevos elementos
                LvDocumentosPendientes.Items.Clear();

                //Validar si hay registros antes de intentar enlistarlos
                if (OVList[0]["Status"] == "NO" || OVList[0]["Status"] == "ERROR")
                {
                    result = OVList[0]["Message"];
                    return result;
                }
                else
                {
                    // Recorrer los ítems y agregarlos al ListView
                    foreach (var item in OVList)
                    {

                        if (Headers == false)
                        {
                            // Agregar columnas al ListView
                            LvDocumentosPendientes.Columns.Add("ID", 0, HorizontalAlignment.Left);//0
                            LvDocumentosPendientes.Columns.Add("DocEntry", 0, HorizontalAlignment.Left);//1
                            LvDocumentosPendientes.Columns.Add("Folio", -2, HorizontalAlignment.Left); //2
                            LvDocumentosPendientes.Columns.Add("Usuario", -2, HorizontalAlignment.Left);//3
                            LvDocumentosPendientes.Columns.Add("Cliente", -2, HorizontalAlignment.Left);//4
                            LvDocumentosPendientes.Columns.Add("Orden Venta", -2, HorizontalAlignment.Left);//5
                            LvDocumentosPendientes.Columns.Add("Comentarios", -2, HorizontalAlignment.Left);//6
                            LvDocumentosPendientes.Columns.Add("Estatus", -2, HorizontalAlignment.Left);//7
                            LvDocumentosPendientes.Columns.Add("Fecha", -2, HorizontalAlignment.Left);//8
                            LvDocumentosPendientes.Columns.Add("Autorizado", -2, HorizontalAlignment.Left);//9
                            LvDocumentosPendientes.Columns.Add("TotalArticulos", 0, HorizontalAlignment.Left);//10
                            Headers = true;
                        }

                        // Crear un nuevo ítem de ListView con la primera columna
                        ListViewItem listViewItem = new ListViewItem(item["ID"]);//ID interno
                        listViewItem.SubItems.Add(item["OrdenVenta"]);// DocEntry
                        listViewItem.SubItems.Add(item["Folio"]);// Cliente
                        listViewItem.SubItems.Add(item["Usuario"]);// Usuario
                        listViewItem.SubItems.Add(item["Cliente"]);// Cliente
                        listViewItem.SubItems.Add(item["SapDocument"]);//DocNum
                        listViewItem.SubItems.Add(item["Comentarios"]);//Comentarios
                        listViewItem.SubItems.Add(item["Estatus"]);//Estatus
                        listViewItem.SubItems.Add(item["Fecha"]);// Fecha
                        listViewItem.SubItems.Add(item["Autorizacion"]);//Autorización
                        listViewItem.SubItems.Add(item["TotalArticulos"]);//TotalArtículos

                        listViewItem.BackColor = rownumber == "0" ? Color.FromArgb(242, 242, 242) : Color.White;
                        listViewItem.ForeColor = Color.Black;
                        rownumber = rownumber == "0" ? "1" : "0";
                        LvDocumentosPendientes.Items.Add(listViewItem);
                    }
                }

                if (OVList.Count == 0)
                    Detalle.Enabled = false;
                else
                    Detalle.Enabled = true;

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
        private void ShowOVDetail()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                if (LvDocumentosPendientes.SelectedIndices.Count > 0)
                {
                    int index = LvDocumentosPendientes.SelectedIndices[0]; // Obtiene el índice de la fila seleccionada
                    ListViewItem item = LvDocumentosPendientes.Items[index]; // Obtiene el ítem (fila)
                    string Folio = item.SubItems[2].Text;
                    int TotalArticulos = int.Parse(item.SubItems[10].Text);

                    if (TotalArticulos == 0)
                    {
                        Logic.ShowException(null, "No hay ningun escaneo asociado al folio, puedes generar escaneos dando click en el icono de editar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }
                    else
                    {

                        try
                        {
                            FormHelper.AbrirFormularioConValidacion(
                            this,
                            () => new EscaneosOV(Folio),
                            (form) =>
                            {
                                var ir = form as EscaneosOV;
                                if (ir.EstadoInicializacion == "OK")
                                {
                                    ir.FromDatosMaestros = this.FromDatosMaestros; //Para volver al estado original
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
                            Logic.ShowException(ex, "No fue posible consultar los detalles del documento seleccionado.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        }
                    }
                }
                else
                {
                    Logic.ShowException(null, "Debes seleccionar un documento para continuar. ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible consultar los detalles del documento seleccionado.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        private void GenerarEntregaMercanciaOV()
        {
            //Datos de envio
            string OrdenVenta = string.Empty;
            string FolioEM = string.Empty;
            string Usuario = string.Empty;
            string Comentarios = string.Empty;

            int TotalArticulos = 0;
            if (LvDocumentosPendientes.SelectedIndices.Count > 0)
            {
                int index = LvDocumentosPendientes.SelectedIndices[0]; // Obtiene el índice de la fila seleccionada
                ListViewItem item = LvDocumentosPendientes.Items[index]; // Obtiene el ítem (fila)
                FolioEM = item.SubItems[2].Text;
                Usuario = item.SubItems[3].Text;
                OrdenVenta = item.SubItems[1].Text;
                Comentarios = item.SubItems[6].Text;
                TotalArticulos = int.Parse(item.SubItems[10].Text);

                //Obtener numero de rollos
                CantidadRollos = ObtenerNumeroRollos(FolioEM);
                if (CantidadRollos == 0)
                    CantidadRollos = TotalArticulos;

                if (TotalArticulos == 0)
                {
                    Logic.ShowException(null, "No hay ningun escaneo asociado al folio, puedes generar escaneos dando click en el icono de editar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    //Seleccionar fecha de entrega
                    DialogoFechaEntrada DFE = new DialogoFechaEntrada(FolioEM);
                    if (DFE.EstadoInicializacion == "OK")
                    {
                        DFE.LblTitleModule.Text = "FECHA ENTREGA MERCANCÍA";
                        DFE.lblDescriptionModule.Text = "SELECCIONE LA FECHA DE ENTREGA";
                        RequiresPreeliminar = true;
                        if (RequiresPreeliminar) //Mostrar opcion de preeliminar solo si se requiere
                        {
                            DFE.LblPreeliminar.Visible = true;
                            DFE.Preeliminar.Visible = true;
                        }
                        Cursor.Current = Cursors.Default;

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
                                    writer.WriteStartElement("EntregasMercanciaVentasHHEMOV");

                                    writer.WriteElementString("FolioEM", FolioEM);
                                    writer.WriteElementString("OrdenVenta", OrdenVenta);
                                    writer.WriteElementString("FechaEntrega", DFE.FechaEntrada);
                                    writer.WriteElementString("IsBorrador", DFE.IsPreeliminar);
                                    writer.WriteElementString("Usuario", Usuario);
                                    writer.WriteElementString("Comentarios", Comentarios);
                                    writer.WriteElementString("Rollos", CantidadRollos.ToString());

                                    writer.WriteEndElement();
                                    writer.WriteEndDocument();
                                }

                                string Xml = sw.ToString();

                                List<Dictionary<string, string>> Entrega = Logic.ExecPostRequest("/VentasMovil/GenerarEntregaMercanciaOV", Xml, false, string.Empty);

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
                                    List<Dictionary<string, string>> OV = Logic.ExecGetRequest("/VentasMovil/GetDocumentosVentasHHEMOV", Logic.AD.RequestParameters, false, true);
                                    string result = DocumentosPendientes(OV);
                                    if (result.Contains("No se encontraron") || result.Contains("No se encontró información"))
                                    {
                                        Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                                        //Solo si viene de datos maestros
                                        if (FromDatosMaestros == true)
                                        {
                                            //Desactivar elementos
                                            Guardar.Enabled = false;
                                            Guardar.Visible = false;
                                            GenerarEntrega.Enabled = false;
                                            GenerarEntrega.Visible = false;
                                            Eliminar.Enabled = false;
                                            Eliminar.Visible = false;
                                            Detalle.Enabled = false;
                                            Detalle.Visible = false;
                                            ptbBuscar.Enabled = false;
                                            ptbBuscar.Visible = false;

                                            //Cambiar la imagen del botin regresar
                                            string rutaImagen = Path.Combine(Logic.directorioBase, "Imagenes\\Home.jpg");
                                            if (File.Exists(rutaImagen))
                                            {
                                                Regresar.Image = new Bitmap(rutaImagen);
                                                Regresar.Location = new Point(208, 5);  //Agregar nuevo boton con leyenda (Guardar)
                                                Footer.Controls.Add(Regresar); //Agregar al panel
                                                Regresar.Click += new EventHandler(MenuEntregas_Click); //Asignar evento click
                                                Regresar.Visible = true;
                                                Regresar.Enabled = true;
                                            }
                                        }
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
                            Logic.ShowException(null, "debes confirmar una fecha para realizar la entrega de mercancía en la ventana de selección utilizando el icono de color verde.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        }
                    }
                    else
                    {
                        Logic.ShowException(null, DFE.EstadoInicializacion, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }
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
                        writer.WriteStartElement("DocumentosVentasEMOV");
                        writer.WriteElementString("Folio", item.SubItems[2].Text);
                        writer.WriteElementString("Usuario", FormHelper.Usuario);
                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                    }

                    string Xml = sw.ToString();

                    List<Dictionary<string, string>> borrado = Logic.ExecPostRequest("/VentasMovil/EliminarDocumentosVentasEMOV", Xml, false, string.Empty);
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
        private void ModificarDocumento()
        {
            if (LvDocumentosPendientes.SelectedIndices.Count > 0) // Verifica que haya una fila seleccionada
            {
                int index = LvDocumentosPendientes.SelectedIndices[0]; // Obtiene el índice de la fila seleccionada
                ListViewItem item = LvDocumentosPendientes.Items[index]; // Obtiene el ítem (fila)
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("DocEntry", item.SubItems[1].Text);
                Logic.AD.RequestParameters.Add("FolioEM", item.SubItems[2].Text);
                Logic.AD.RequestParameters.Add("OrdenVenta", item.SubItems[5].Text);
                Logic.AD.RequestParameters.Add("FromNuevoDocumento", "NO");


                try
                {
                    FormHelper.AbrirFormularioConValidacion(
                    this,
                    () => new ArticulosOV(Logic.AD.RequestParameters),
                    (form) =>
                    {
                        var ir = form as ArticulosOV;
                        if (ir.EstadoInicializacion == "OK")
                        {
                            ir.MenuEntregas.Visible = false; //Ocultar menu para regresar a menu ventas
                            ir.lblOV.Text = "Folio: " + Logic.AD.RequestParameters["FolioEM"] + "";
                            ir.FromNuevoDocumento = "NO";
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
            else
            {
                Logic.ShowException(null, "Selecciona un elemento de la lista para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        private int ObtenerNumeroRollos(string Folio)
        {
            int CantRollos = 0;

            try
            {
                //Obtener el numero de rollos
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("Folio", Folio);
                List<Dictionary<string, string>> Rollos = Logic.ExecGetRequest("/VentasMovil/GetRollosHHEMOV", Logic.AD.RequestParameters, false, false);

                if (Rollos[0]["Status"] == "NO" || Rollos[0]["Status"] == "ERROR")
                {
                    string result = Rollos[0]["Message"];
                    CantRollos = 0;
                }
                else
                {
                    //Asignar numero de rollos escaneados
                    CantRollos = int.Parse(Rollos[0]["NumeroRollos"]);
                }

                return CantRollos;
            }
            catch (Exception)
            {
                Logic.ShowException(null, "No es posible obtener el numero de rollos en este momento", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                CantRollos = 0;
                return CantRollos;
            }
        }
        private string ObtenerNumeroRollosv2(string Folio)
        {
            //Obtener el numero de rollos
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            Logic.AD.RequestParameters.Add("Folio", Folio);
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
                return NumeroRollos;
            }
        }
        #endregion
    }
}