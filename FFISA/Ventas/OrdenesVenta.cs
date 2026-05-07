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

namespace FFISA.Ventas
{
    public partial class OrdenesVenta : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal(); //Logica para hacer peticiones HTTP, MOSTRAR EXCEPCIONES, ETC
        public string EstadoInicializacion { get; private set; }
        public bool Headers = false;

        #region events
        private void Continuar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                GetDetailsOV(); //continuar en el menu siguiente
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible obtener los detalles de la OV", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        private void ptbBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();

                if (TxtOrdenVenta.Text == string.Empty)
                    Logic.ShowException(null, "Debes colocar el número de orden de venta.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                else
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Logic.AD.RequestParameters = new Dictionary<string, string>();
                    //Orden Venta
                    Logic.AD.RequestParameters.Add("OV", TxtOrdenVenta.Text);
                    string result = DataTableOV(Logic.AD.RequestParameters);
                    if (result != "OK")
                        Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, (result.Contains("Error") ? MessageBoxIcon.Exclamation : MessageBoxIcon.Asterisk), MessageBoxDefaultButton.Button2);
                }

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible consultar el listado de OV: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuEntregas MenuEntregas = new MenuEntregas();
            FormHelper.AbrirFormulario(this, MenuEntregas, false);
        }
        private void LvOrdenesVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    GetDetailsOV(); //continuar en el menu siguiente
                }
                catch (Exception ex)
                {
                    Logic.ShowException(ex, "No fue posible obtener los detalles de la OV", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    Cursor.Current = Cursors.Default;
                }
            }
        }
        private void MenuEntregas_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuEntregas MenuEntregas = new MenuEntregas();
            FormHelper.AbrirFormulario(this, MenuEntregas, false);
        }
        private void TxtOrdenVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si se presiona Enter, ejecutar la lógica y salir
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Evita que se agregue un salto de línea si el TextBox permite múltiples líneas

                try
                {
                    FormHelper.ClickEvent();

                    if (TxtOrdenVenta.Text == null || TxtOrdenVenta.Text.Trim() == string.Empty)
                    {
                        Logic.ShowException(null, "Debes colocar el número de orden de venta.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }
                    else
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        Logic.AD.RequestParameters = new Dictionary<string, string>
                {
                    { "OV", TxtOrdenVenta.Text }
                };

                        string result = DataTableOV(Logic.AD.RequestParameters);
                        if (result != "OK")
                            Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }

                    Cursor.Current = Cursors.Default;
                }
                catch (Exception ex)
                {
                    Logic.ShowException(ex, "No fue posible consultar el listado de OV: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
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
        public OrdenesVenta()
        {
            InitializeComponent();
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            EstadoInicializacion = DataTableOV(Logic.AD.RequestParameters);
            TxtOrdenVenta.Focus();
        }
        private string DataTableOV(Dictionary<string, string> parameters)
        {

            string result = string.Empty;

            try
            {
                List<Dictionary<string, string>> OVList = Logic.ExecGetRequest("/VentasMovil/GetOrdenesVenta", parameters, false, false);
                string rownumber = "0";

                // Limpiar el ListView antes de agregar nuevos elementos
                LvOrdenesVenta.Items.Clear();

                //Validar si hay registros antes de intentar enlistarlos
                if (OVList[0]["Status"] == "NO" || OVList[0]["Status"] == "ERROR")
                {
                    result = OVList[0]["Message"];
                    lblTotalOV.Text = "0";
                    return result;
                }
                else
                {
                    lblTotalOV.Text = OVList.Count.ToString();
                    // Recorrer los ítems y agregarlos al ListView
                    foreach (var item in OVList)
                    {
                        if (Headers == false)
                        {
                            // Agregar columnas al ListView
                            LvOrdenesVenta.Columns.Add("DocEntry", 0, HorizontalAlignment.Left); //0
                            LvOrdenesVenta.Columns.Add("Orden Venta", -2, HorizontalAlignment.Left); //1
                            LvOrdenesVenta.Columns.Add("Fecha", -2, HorizontalAlignment.Left); //3
                            LvOrdenesVenta.Columns.Add("Cliente", -2, HorizontalAlignment.Left); //4
                            LvOrdenesVenta.Columns.Add("CodigoCliente", -2, HorizontalAlignment.Left); //5
                            Headers = true;
                        }

                        // Crear un nuevo ítem de ListView con la primera columna
                        ListViewItem listViewItem = new ListViewItem(item["DocEntry"]); //DocEntry (0)
                        listViewItem.SubItems.Add(item["OrdenVenta"]);//Orden Venta (1)
                        listViewItem.SubItems.Add(item["FechaCreacionOrden"]); // PrecioTotalOrden (2)
                        listViewItem.SubItems.Add(item["Cliente"]);// Cliente (3)
                        listViewItem.SubItems.Add(item["CodigoCliente"]);// CodigoCliente (4)


                        listViewItem.BackColor = rownumber == "0" ? Color.FromArgb(242, 242, 242) : Color.White;
                        listViewItem.ForeColor = Color.Black;
                        rownumber = rownumber == "0" ? "1" : "0";
                        LvOrdenesVenta.Items.Add(listViewItem);
                    }

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
                result = "No fue posible consultar el listado de OV: " + Error.ToString();
                return result;
            }
        }
        private void GetDetailsOV() 
        {

            if (LvOrdenesVenta.SelectedIndices.Count > 0) // Verifica que haya una fila seleccionada
            {
                Cursor.Current = Cursors.WaitCursor;
                int index = LvOrdenesVenta.SelectedIndices[0]; // Obtiene el índice de la fila seleccionada
                ListViewItem item = LvOrdenesVenta.Items[index]; // Obtiene el ítem (fila)
                Dictionary<string, string> DatosOV = new Dictionary<string, string>();
                DatosOV.Add("DocEntry", item.SubItems[0].Text); //Id orden de venta
                DatosOV.Add("OrdenVenta", item.SubItems[1].Text); //Pedido OV
                DatosOV.Add("Cliente", item.SubItems[3].Text); //Cliente
                DatosOV.Add("CodigoCliente", item.SubItems[4].Text); //Codigo Cliente

                //Antes de avanzar realizar validaciones
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("DocEntry", DatosOV["DocEntry"]);
                Logic.AD.RequestParameters.Add("OrdenVenta", DatosOV["OrdenVenta"]);
                List<Dictionary<string, string>> ValidarOrdenVenta = Logic.ExecGetRequest("/VentasMovil/ValidarOrdenVentaHHEMOV", Logic.AD.RequestParameters, false, false);
                CustomMessage CM = new CustomMessage();
                string Mensaje = string.Empty;
                string ResultadoValidacionesOV = string.Empty;
                string Consideraciones = string.Empty;
                if (ValidarOrdenVenta[0]["Status"] == "NO" || ValidarOrdenVenta[0]["Status"] == "ERROR")
                {
                    ResultadoValidacionesOV = "ERROR";
                    Mensaje = ValidarOrdenVenta[0]["Message"].ToString();
                    Logic.ShowException(null, Mensaje, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    ResultadoValidacionesOV = "OK";
                    Consideraciones = ValidarOrdenVenta[0]["Consideraciones"].ToString();
                    if (Consideraciones != "OK")
                    {
                        CM.LblTitleModule.Text = "AVISO OV: " + DatosOV["OrdenVenta"];

                        if (CM.EstadoInicializacion == "OK")
                        {
                            CM.TxtDescriptionModule.Text = Consideraciones;

                            Cursor.Current = Cursors.Default;
                            // Restaura el cursor al estado normal antes de abrir el formulario de diálogo.
                            // Es buena práctica para que el usuario no vea el ícono de "espera" mientras interactúa con la ventana modal.
                            SoundPlayer.ReproducirSonido("Aviso.wav");

                            CM.ShowDialog(); //mostrar el dialogo
                            // Muestra el formulario de selección de fecha (DFE) como un cuadro de diálogo modal.
                            // La ejecución se detiene aquí hasta que el usuario cierre esa ventana con "Continuar" o "Cancelar".

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
                        }
                        else
                        {
                            Logic.ShowException(null, CM.EstadoInicializacion, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        }
                    }
                }

                DatosMaestrosOV(DatosOV);
            }
            else
            {
                Logic.ShowException(null, "Selecciona un elemento de la lista para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        private void DatosMaestrosOV(Dictionary<string, string> DatosOV)
        {
            try
            {
                FormHelper.AbrirFormularioConValidacion(
                this,
                () => new DatosMaestrosOV(DatosOV),
                (form) =>
                {
                    var ir = form as DatosMaestrosOV;
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
                Logic.ShowException(ex, "No fue posible mostrar el formulario de datos generales de la orden de venta.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        #endregion
    }
}