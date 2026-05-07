using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FFISA.Model;
using System.Net;
using System.IO;
using FFISA.Main;
using System.Xml;

namespace FFISA.Inventarios.RecuentoInventarios
{
    public partial class DocumentosSap : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal(); //Logica para hacer peticiones HTTP, MOSTRAR EXCEPCIONES, ETC
        public string EstadoInicializacion { get; private set; }
        Dictionary<string, string> CurrentData = new Dictionary<string, string>();
        public bool Headers = false;

        #region events
        private void Continuar_Click(object sender, EventArgs e)
        {
            string Resultado = string.Empty;
            try
            {
                FormHelper.ClickEvent();
                //Generar registro automatico con folio consecutivo para entregas
                Resultado = InsertaEncabezadoRecuentos();

                if (Resultado == "OK" || Resultado == "SI")
                {
                    GetDetailsRecuento(); //Avanzar a la siguiente pantalla
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                if (Resultado == "OK" || Resultado == "SI")
                    Logic.ShowException(ex, "Se guardo el folio de documento, pero no fue posible mostrar el formulario de recuento, intenta de nuevo más tarde: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                else
                Logic.ShowException(ex, "No fue posible obtener los detalles del recuento.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        private void ptbBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();

                if (TxtRecuentoInventario.Text == string.Empty)
                    Logic.ShowException(null, "Debes colocar el número de recuento.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                else
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Logic.AD.RequestParameters = new Dictionary<string, string>();
                    //Orden Venta
                    Logic.AD.RequestParameters.Add("Recuento", TxtRecuentoInventario.Text);
                    string result = RecuentosSAP(Logic.AD.RequestParameters);
                    if (result != "OK")
                        Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, (result.Contains("Error") ? MessageBoxIcon.Exclamation : MessageBoxIcon.Asterisk), MessageBoxDefaultButton.Button2);
                }

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible consultar el listado de recuentos de SAP: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuRecuentoInventarios MenuRecuentoInventarios = new MenuRecuentoInventarios();
            FormHelper.AbrirFormulario(this, MenuRecuentoInventarios, false);
        }
        private void LvRecuentosSap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    GetDetailsRecuento(); //continuar en el menu siguiente
                }
                catch (Exception ex)
                {
                    Logic.ShowException(ex, "No fue posible obtener los detalles de la OV", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    Cursor.Current = Cursors.Default;
                }
            }
        }
        private void TxtRecuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si se presiona Enter, ejecutar la lógica y salir
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Evita que se agregue un salto de línea si el TextBox permite múltiples líneas

                try
                {
                    FormHelper.ClickEvent();

                    if (TxtRecuentoInventario.Text == null || TxtRecuentoInventario.Text.Trim() == string.Empty)
                    {
                        Logic.ShowException(null, "Debes colocar el número de recuento.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }
                    else
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        Logic.AD.RequestParameters = new Dictionary<string, string>
                {
                    { "Recuento", TxtRecuentoInventario.Text }
                };

                        string result = RecuentosSAP(Logic.AD.RequestParameters);
                        if (result != "OK")
                            Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }

                    Cursor.Current = Cursors.Default;
                }
                catch (Exception ex)
                {
                    Logic.ShowException(ex, "No fue posible consultar el listado de recuentos en SAP: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    Cursor.Current = Cursors.Default;
                }

                return; // Ya procesaste Enter, no sigas evaluando más
            }

            // Para las demás teclas, permitir solo dígitos y teclas de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Bloquear cualquier otra tecla
            }
        }
        #endregion

        #region methods
        public DocumentosSap(Dictionary<string, string> CurrentData)
        {
            InitializeComponent();
            this.CurrentData = CurrentData;
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            EstadoInicializacion = RecuentosSAP(Logic.AD.RequestParameters);
            TxtRecuentoInventario.Focus();
        }
        private string RecuentosSAP(Dictionary<string, string> parameters)
        {

            string result = string.Empty;

            try
            {
                List<Dictionary<string, string>> Recuentos = Logic.ExecGetRequest("/InventariosMovil/GetRecuentosInventarioHHRI", parameters, false, false);
                string rownumber = "0";

                // Limpiar el ListView antes de agregar nuevos elementos
                LvOrdenesVenta.Items.Clear();

                //Validar si hay registros antes de intentar enlistarlos
                if (Recuentos[0]["Status"] == "NO" || Recuentos[0]["Status"] == "ERROR")
                {
                    result = Recuentos[0]["Message"];
                    lblTotalRecuentos.Text = "0";
                    return result;
                }
                else
                {
                    lblTotalRecuentos.Text = Recuentos.Count.ToString();
                    // Recorrer los ítems y agregarlos al ListView
                    foreach (var item in Recuentos)
                    {
                        if (Headers == false)
                        {
               
                            // Agregar columnas al ListView
                            LvOrdenesVenta.Columns.Add("DocEntry", 0, HorizontalAlignment.Left); //0
                            LvOrdenesVenta.Columns.Add("Recuento", -2, HorizontalAlignment.Left); //1
                            LvOrdenesVenta.Columns.Add("Fecha", -2, HorizontalAlignment.Left); //2
                            LvOrdenesVenta.Columns.Add("Comentarios", -2, HorizontalAlignment.Left); //3
                            Headers = true;
                        }

                        // Crear un nuevo ítem de ListView con la primera columna
                        ListViewItem listViewItem = new ListViewItem(item["DocEntry"]); //DocEntry (0)
                        listViewItem.SubItems.Add(item["Recuento"]);//Recuento (1)
                        listViewItem.SubItems.Add(item["FechaConteo"]); // FechaConteo (2)
                        listViewItem.SubItems.Add(item["Comentarios"]);// Comentarios (3)


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
                        LvOrdenesVenta.Items.Add(listViewItem);
                    }

                    FormHelper.AjustarColumnas(LvOrdenesVenta);
                    result = "OK";
                    return result;
                }
            }
            catch (Exception ex)
            {
                StringBuilder Error = new StringBuilder();
                Error.Append(ex.Message != null ? ex.Message.ToString() : string.Empty);
                Error.Append(Environment.NewLine);
                Error.Append(Environment.NewLine);
                Error.Append(ex.InnerException != null ? ex.InnerException.ToString() : string.Empty);
                result = "No fue posible consultar el listado de recuentos en SAP: " + Error.ToString();
                return result;
            }
        }
        private void GetDetailsRecuento() 
        {

            if (LvOrdenesVenta.SelectedIndices.Count > 0) // Verifica que haya una fila seleccionada
            {
                Cursor.Current = Cursors.WaitCursor;
                int index = LvOrdenesVenta.SelectedIndices[0]; // Obtiene el índice de la fila seleccionada
                ListViewItem item = LvOrdenesVenta.Items[index]; // Obtiene el ítem (fila)

                if (CurrentData.ContainsKey("DocEntry"))
                    CurrentData["DocEntry"] = item.SubItems[0].Text; //Id orden de venta
                else
                CurrentData.Add("DocEntry",item.SubItems[0].Text); //Id orden de venta

                if (CurrentData.ContainsKey("Recuento"))
                    CurrentData["Recuento"] = item.SubItems[1].Text; //Id orden de venta
                else
                    CurrentData.Add("Recuento", item.SubItems[1].Text); //Id orden de venta

                DatosMaestrosRecuento DatosMaestrosRecuento = new DatosMaestrosRecuento(CurrentData);
                FormHelper.AbrirFormulario(this, DatosMaestrosRecuento, false);
            }
            else
            {
                Logic.ShowException(null, "Selecciona un elemento de la lista para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        private string InsertaEncabezadoRecuentos()
        {
            if (LvOrdenesVenta.SelectedIndices.Count > 0) // Verifica que haya una fila seleccionada
            {
                int index = LvOrdenesVenta.SelectedIndices[0]; // Obtiene el índice de la fila seleccionada
                ListViewItem item = LvOrdenesVenta.Items[index]; // Obtiene el ítem (fila)

                if (CurrentData.ContainsKey("DocEntry"))
                    CurrentData["DocEntry"] = item.SubItems[0].Text; //Id orden de venta
                else
                    CurrentData.Add("DocEntry", item.SubItems[0].Text); //Id orden de venta

                if (CurrentData.ContainsKey("Recuento"))
                    CurrentData["Recuento"] = item.SubItems[1].Text; //Id orden de venta
                else
                    CurrentData.Add("Recuento", item.SubItems[1].Text); //Id orden de venta

                string resultado = string.Empty;
                AccesoDatosInventarios.EncabezadoRecuentosHHRI RI = new AccesoDatosInventarios.EncabezadoRecuentosHHRI();
                RI.Usuario = FormHelper.Usuario;
                RI.DocEntry = CurrentData["DocEntry"];
                RI.SapDocument = CurrentData["Recuento"];

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
                    writer.WriteStartElement("EncabezadoRecuentosHHRI");
                    writer.WriteElementString("Usuario", RI.Usuario);
                    writer.WriteElementString("DocEntry", RI.DocEntry);
                    writer.WriteElementString("SapDocument", RI.SapDocument);
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }

                string Xml = sw.ToString();

                //Listado dinamico
                List<Dictionary<string, string>> items = Logic.ExecPostRequest("/InventariosMovil/InsertaEncabezadoRecuentosHHRI", Xml, false, string.Empty);

                if (items[0].ContainsKey("Folio"))
                {
                    //Guardar el folio obtenido de la cabecera de la entrega de mercancia
                    CurrentData = new Dictionary<string, string>();
                    string Folio = items[0]["Folio"].ToString();
                    string Estatus = items[0]["Status"].ToString();
                    if (CurrentData.ContainsKey("FolioRI"))
                        CurrentData.Remove("FolioRI");
                    CurrentData.Add("FolioRI", Folio);
                    resultado = Estatus;
                }
                else
                {
                    resultado = "ERROR";
                    string Mensaje = items[0]["Message"].ToString();
                    Logic.ShowException(null, Mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                return resultado;
            }
            else
            {
                Logic.ShowException(null, "Selecciona un elemento de la lista para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                return "NO";
            }
        }
        #endregion
    }
}