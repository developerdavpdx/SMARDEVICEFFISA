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

namespace FFISA.Inventarios.EntradasDirectas
{
    public partial class EscaneosEntradas : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal();
        public string EstadoInicializacion { get; private set; }
        private string FolioSelected { get; set; }
        public bool Headers = false;

        #region events
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            EntradasPendientes EntradasPendientes = new EntradasPendientes();
            FormHelper.AbrirFormulario(this, EntradasPendientes, false);
        }
        private void PtbEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                bool shouldclose = false;
                FormHelper.ClickEvent();
                if (LvDetailsEntrada.Items.Count == 1)
                {
                    if (LvDetailsEntrada.SelectedIndices.Count > 0) // Verifica que haya una fila seleccionada
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
                    if (LvDetailsEntrada.SelectedIndices.Count > 0) // Verifica que haya una fila seleccionada
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
                    EntradasPendientes EntradasPendientes = new EntradasPendientes();
                    FormHelper.AbrirFormulario(this, EntradasPendientes, false);
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible continuar con el proceso, revisa tu conexión a internet: " + ex.ToString(), "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        #endregion

        #region methods
        public EscaneosEntradas(string Folio)
        {
            InitializeComponent();
            EstadoInicializacion = ListaDetalles(Folio);
            this.FolioSelected = Folio;
            lblOC.Text = "Folio: " + Folio;
        }
        private string ListaDetalles(string Folio)
        {
            string result = string.Empty;

            try
            {
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("Folio", Folio);
                Logic.AD.RequestParameters.Add("Usuario", FormHelper.Usuario);
                List<Dictionary<string, string>> Details = Logic.ExecGetRequest("/InventariosMovil/GetLinesDocumentosEntradasHHEMD", Logic.AD.RequestParameters, true, false);

                string rownumber = "0";

                // Limpiar el ListView antes de agregar nuevos elementos
                LvDetailsEntrada.Items.Clear();

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
                            LvDetailsEntrada.Columns.Add("ID", 0, HorizontalAlignment.Left);
                            LvDetailsEntrada.Columns.Add("Artículo", -2, HorizontalAlignment.Left);
                            LvDetailsEntrada.Columns.Add("Existe Etiqueta", -2, HorizontalAlignment.Left);
                            LvDetailsEntrada.Columns.Add("Almacén", -2, HorizontalAlignment.Left);
                            LvDetailsEntrada.Columns.Add("Lote", -2, HorizontalAlignment.Left);
                            LvDetailsEntrada.Columns.Add("Cantidad", -2, HorizontalAlignment.Left);
                            LvDetailsEntrada.Columns.Add("Cantidad Metros", -2, HorizontalAlignment.Left);
                            LvDetailsEntrada.Columns.Add("Comentarios", -2, HorizontalAlignment.Left);
                            LvDetailsEntrada.Columns.Add("Cuenta Contable", -2, HorizontalAlignment.Left);
                            LvDetailsEntrada.Columns.Add("Fecha", -2, HorizontalAlignment.Left);
                            Headers = true;
                        }

                        // Crear un nuevo ítem de ListView con la primera columna
                        ListViewItem listViewItem = new ListViewItem(item["ID"]);
                        listViewItem.SubItems.Add(item["Articulo"]);        // Segunda columna
                        listViewItem.SubItems.Add(item["Etiqueta"]);// Segunda columna
                        listViewItem.SubItems.Add(item["NombreAlmacen"]); // septima columna
                        listViewItem.SubItems.Add(item["Lote"]); // septima columna
                        listViewItem.SubItems.Add(item["Cantidad"]);// Segunda columna
                        listViewItem.SubItems.Add(item["CantidadMetros"]);// Segunda columna
                        listViewItem.SubItems.Add(item["Comentarios"]);// Segunda columna
                        listViewItem.SubItems.Add(item["CuentaContable"]);// Segunda columna
                        listViewItem.SubItems.Add(item["Fecha"]);// Segunda columna


                        // Alternar colores de filas
                        if (rownumber == "0")
                        {
                            listViewItem.BackColor = Color.FromArgb(242, 242, 242);
                            listViewItem.ForeColor = Color.Black;
                            rownumber = "1";
                        }
                        else
                        {
                            listViewItem.BackColor = Color.FromArgb(255, 255, 255);
                            listViewItem.ForeColor = Color.Black;
                            rownumber = "0";
                        }

                        // Agregar el ítem al ListView
                        LvDetailsEntrada.Items.Add(listViewItem);
                    }
                }

                FormHelper.AjustarColumnas(LvDetailsEntrada);
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
                int index = LvDetailsEntrada.SelectedIndices[0]; // Obtiene el índice de la fila seleccionada
                ListViewItem item = LvDetailsEntrada.Items[index]; // Obtiene el ítem (fila)

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
                    writer.WriteStartElement("EscaneosEntradasHHEMD");
                    writer.WriteElementString("ID", item.SubItems[0].Text);
                    writer.WriteElementString("Folio", this.FolioSelected);
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }

                string Xml = sw.ToString();

                List<Dictionary<string, string>> borrado = Logic.ExecPostRequest("/InventariosMovil/EliminaEscaneosEntradasHHEMD", Xml, false, string.Empty);
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
        #endregion
    }
}