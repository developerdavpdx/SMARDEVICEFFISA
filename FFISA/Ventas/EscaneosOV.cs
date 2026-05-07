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

namespace FFISA.Ventas
{
    public partial class EscaneosOV : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal();
        public string EstadoInicializacion { get; private set; }
        private string FolioSelected { get; set; }
        public bool FromDatosMaestros { get; set; } //Viene desde los datos maestros
        public bool Headers = false;


        #region events
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            EntregasPendientes();
        }
        private void PtbEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                bool shouldclose = false;
                FormHelper.ClickEvent();
                if (LvDetailsOC.Items.Count == 1)
                {
                    if (LvDetailsOC.SelectedIndices.Count > 0) // Verifica que haya una fila seleccionada
                    {
                        string result = Logic.ShowException(null, "Si eliminas este registro el documento tambien será eliminado, ¿Deseas Continuar?", 250, "AVISO", "Aviso.wav", true);

                        //Si esta de acuerdo
                        if (result == "Yes")
                        {
                            result = EliminarEscaneo(true);
                            if (result == "OK")
                            {
                                result = ListaDetalles(this.FolioSelected);
                                if (result != "OK")
                                {
                                    Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                                    //Validar si tiene conexion a la red para continuar
                                    if (Logic.AD.PuedeAlcanzarIP())
                                        shouldclose = true;
                                    else
                                    {
                                        Logic.ShowException(null, "Por favor comprueba tu conexión a la red interna para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                        this.PtbEliminar.Enabled = false;
                                        shouldclose = false;
                                    }
                                }
                            }
                            else
                                Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        }
                    }
                    else
                    {
                        Logic.ShowException(null, "Selecciona un elemento de la lista para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }
                }
                else
                {
                    if (LvDetailsOC.SelectedIndices.Count > 0) // Verifica que haya una fila seleccionada
                    {
                        string result = Logic.ShowException(null, "¿Deseas eliminar este registro?", 250, "AVISO", "Aviso.wav", true);

                        //Si esta de acuerdo
                        if (result == "Yes")
                        {

                            result = EliminarEscaneo(false);
                            if (result == "OK")
                            {
                                result = ListaDetalles(this.FolioSelected);

                                if (result != "OK")
                                {
                                    Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                                    //Validar si tiene conexion a la red para continuar
                                    if (Logic.AD.PuedeAlcanzarIP())
                                        shouldclose = false;
                                    else
                                    {
                                        Logic.ShowException(null, "Por favor comprueba tu conexión a la red interna para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                        this.PtbEliminar.Enabled = false;
                                        shouldclose = false;
                                    }
                                }
                            }
                            else
                                Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        }
                    }
                    else
                    {
                        Logic.ShowException(null, "Selecciona un elemento de la lista para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }
                }
                if (shouldclose)
                {
                    EntregasPendientes();
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible continuar con el proceso, revisa tu conexión a internet: " + ex.ToString(), "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

            }
        }
        #endregion

        #region methods
        public EscaneosOV(string Folio)
        {
            InitializeComponent();
            EstadoInicializacion = ListaDetalles(Folio);
            this.FolioSelected = Folio;
            lblOC.Text = "OV: " + Folio;
        }
        private string ListaDetalles(string Folio)
        {
            string result = string.Empty;

            try
            {
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("Folio", Folio);
                Logic.AD.RequestParameters.Add("Usuario", FormHelper.Usuario);
                List<Dictionary<string, string>> Details = Logic.ExecGetRequest("/VentasMovil/GetLinesDocumentosVentasHHEMOV", Logic.AD.RequestParameters, true, false);

                string rownumber = "0";

                // Limpiar el ListView antes de agregar nuevos elementos
                LvDetailsOC.Items.Clear();

                //Validar si hay registros antes de intentar enlistarlos
                if (Details[0]["Status"] == "NO" || Details[0]["Status"] == "ERROR")
                {
                    result = Details[0]["Message"];
                    return result;
                }
                else
                {
                    // Recorrer los ítems y agregarlos al ListView
                    foreach (var item in Details)
                    {

                        if (Headers == false)
                        {
                            // Agregar columnas al ListView
                            LvDetailsOC.Columns.Add("ID", 0, HorizontalAlignment.Left);
                            LvDetailsOC.Columns.Add("Artículo", -2, HorizontalAlignment.Left);
                            LvDetailsOC.Columns.Add("Lote", -2, HorizontalAlignment.Left);
                            LvDetailsOC.Columns.Add("Almacén", -2, HorizontalAlignment.Left);
                            LvDetailsOC.Columns.Add("UMS", -2, HorizontalAlignment.Left);
                            LvDetailsOC.Columns.Add("UMV", -2, HorizontalAlignment.Left);
                            LvDetailsOC.Columns.Add("UMI", -2, HorizontalAlignment.Left);
                            LvDetailsOC.Columns.Add(("Cantidad " + item["UMS"]), -2, HorizontalAlignment.Left);
                            LvDetailsOC.Columns.Add("Referencia", -2, HorizontalAlignment.Left);
                            LvDetailsOC.Columns.Add("CantidadReferencia", -2, HorizontalAlignment.Left);
                            LvDetailsOC.Columns.Add("Fecha", -2, HorizontalAlignment.Left);
                            Headers = true;
                        }

                        // Crear un nuevo ítem de ListView con la primera columna
                        ListViewItem listViewItem = new ListViewItem(item["ID"]);//0
                        listViewItem.SubItems.Add(item["Articulo"]);        // 1
                        listViewItem.SubItems.Add(item["Lote"]);//2
                        listViewItem.SubItems.Add(item["Almacen"]); //3
                        listViewItem.SubItems.Add(item["UMS"]); //4
                        listViewItem.SubItems.Add(item["UMV"]); //5
                        listViewItem.SubItems.Add(item["UMI"]);//6
                        listViewItem.SubItems.Add(item["CantidadConversion"]);//7
                        listViewItem.SubItems.Add(item["Referencia"]);//8
                        listViewItem.SubItems.Add(item["CantidadReferencia"]);//9
                        listViewItem.SubItems.Add(item["Fecha"]);//10


                        listViewItem.BackColor = rownumber == "0" ? Color.FromArgb(242, 242, 242) : Color.White;
                        listViewItem.ForeColor = Color.Black;
                        rownumber = rownumber == "0" ? "1" : "0";
                        LvDetailsOC.Items.Add(listViewItem);
                    }

                    LblNoRollos.Text = "Total Rollos: " + Details.Count;
                }
                FormHelper.AjustarColumnas(LvDetailsOC);
                result = "OK";
                return result;
            }
            catch (Exception ex)
            {
                StringBuilder Error = new StringBuilder();
                Error.Append(ex.Message != null ? ex.Message.ToString() : string.Empty);
                Error.Append(Environment.NewLine);
                Error.Append(Environment.NewLine);
                Error.Append(ex.InnerException != null ? ex.InnerException.ToString() : string.Empty);
                result = "No fue posible consultar el detalle del documento: " + Error.ToString();
                return result;
            }
        }
        private string EliminarEscaneo(bool CloseForm)
        {
            string result = string.Empty;

            try
            {

                Cursor.Current = Cursors.WaitCursor;
                int index = LvDetailsOC.SelectedIndices[0]; // Obtiene el índice de la fila seleccionada
                ListViewItem item = LvDetailsOC.Items[index]; // Obtiene el ítem (fila)

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
                    writer.WriteStartElement("EscaneosVentasHHEMOV");
                    writer.WriteElementString("ID", item.SubItems[0].Text);
                    writer.WriteElementString("Folio", this.FolioSelected);
                    writer.WriteElementString("Usuario", FormHelper.Usuario);
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }

                string Xml = sw.ToString();

                List<Dictionary<string, string>> borrado = Logic.ExecPostRequest("/VentasMovil/EliminaEscaneosVentasHHEMOV", Xml, false, string.Empty);
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

                return result;
            }
            catch (Exception ex)
            {
                StringBuilder Error = new StringBuilder();
                Error.Append(ex.Message != null ? ex.Message.ToString() : string.Empty);
                Error.Append(Environment.NewLine);
                Error.Append(Environment.NewLine);
                Error.Append(ex.InnerException != null ? ex.InnerException.ToString() : string.Empty);
                result = "No fue posible eliminar el escaneo del documento: " + Error.ToString();
                return result;
            }
        }
        private void EntregasPendientes()
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
                        if (FromDatosMaestros)
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
                        }
                        else
                        {
                            ir.Regresar.Click += new EventHandler(ir.Regresar_Click); //Asignar evento click
                        }

                        return true;
                    }
                    else
                    {
                        if (ir.EstadoInicializacion.Contains("No se encontraron"))
                        {
                            if (FromDatosMaestros)
                            {
                                ir.Modificar.Visible = false; //Desactivar Modificar
                                ir.Eliminar.Visible = false; //Desactivar eliminar
                                ir.Detalle.Visible = false;
                                ir.Guardar.Visible = false; //Mostrar boton con leyenda guardar
                                ir.FromDatosMaestros = true; //Indicar que venemos de datos maestros para mostrar diferentes botones
                                ir.GenerarEntrega.Visible = false;
                                //Cambiar la imagen del botin regresar
                                string rutaImagen = Path.Combine(Logic.directorioBase, "Imagenes\\Home.jpg");
                                if (File.Exists(rutaImagen))
                                {
                                    ir.Regresar.Image = new Bitmap(rutaImagen);
                                    ir.Regresar.Click += new EventHandler(ir.MenuEntregas_Click); //Asignar evento click
                                    ir.Regresar.Visible = true;
                                    ir.Regresar.Enabled = true;
                                    ir.Regresar.Location = new Point(8, 1);
                                    ir.Footer.Controls.Add(ir.Regresar);
                                }
                            }
                            return true;
                        }
                        else
                        {
                            Logic.ShowException(null, ir.EstadoInicializacion, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                            return false;
                        }
                    }
                }, false);
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible mostrar la vista de entregas pendientes.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        #endregion
    }
}