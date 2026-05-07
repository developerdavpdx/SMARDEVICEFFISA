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

namespace FFISA.Compras
{
    public partial class ListadoOC : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal();
        Dictionary<string, string> CurrentData = new Dictionary<string, string>();
        public string EstadoInicializacion { get; private set; }
        public bool Headers = false;

        #region events
        private void Continuar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            GetOCCheckList(); //Siguiente pantalla
        }
        private void ptbBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                this.Enabled = false;

                if (TxtOrdenCompra.Text == string.Empty)
                    Logic.ShowException(null, "Debes colocar el número de orden de compra.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                else
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Logic.AD.RequestParameters.Clear();
                    //Orden Compra
                    Logic.AD.RequestParameters.Add("OC", TxtOrdenCompra.Text);
                    //Series
                    Logic.AD.RequestParameters.Add("Series", CurrentData["Series"]);
                    string result = DataTableOC(Logic.AD.RequestParameters);
                    if (result != "OK")
                        Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }

                Cursor.Current = Cursors.Default;
                this.Enabled = true;
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible consultar el listado de OC: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                this.Enabled = true;
            }
        }
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            SeriesNumeracionOC SeriesNumeracionOC = new SeriesNumeracionOC();
            FormHelper.AbrirFormulario(this, SeriesNumeracionOC,false);
        }
        private void LvOrdenesCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    this.Enabled = false;
                    GetOCCheckList(); //continuar en el menu siguiente
                }
                catch (Exception ex)
                {
                    Logic.ShowException(ex, "No fue posible consultar el listado de OC: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    this.Enabled = true;
                }
            }
        }
        private void MenuCompras_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuCompras MenuCompras = new MenuCompras();
            FormHelper.AbrirFormulario(this, MenuCompras,false);
        }
        private void TxtOrdenCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Para las demás teclas, permitir solo dígitos y teclas de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Bloquear cualquier otra tecla
            }
        }
        #endregion

        #region methods
        public ListadoOC(Dictionary<string, string> CurrentData)
        {
            InitializeComponent();
            this.CurrentData = CurrentData;
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            Logic.AD.RequestParameters.Add("Series", CurrentData["Series"]);
            EstadoInicializacion = DataTableOC(Logic.AD.RequestParameters);
        }
        private string DataTableOC(Dictionary<string, string> parameters)
        {

            string result = string.Empty;

            try
            {
                List<Dictionary<string, string>> OCList = Logic.ExecGetRequest("/Compras/GetOrdenesCompra", parameters, false, false);
                string rownumber = "0";

                // Limpiar el ListView antes de agregar nuevos elementos
                LvOrdenesCompra.Items.Clear();

                //Validar si hay registros antes de intentar enlistarlos
                if (OCList[0]["Status"] == "NO" || OCList[0]["Status"] == "ERROR")
                {
                    result = "Error: " + OCList[0]["Message"];
                    lblTotalOC.Text = "0";
                    return result;
                }
                else
                {
                    lblTotalOC.Text = OCList.Count.ToString();
                    // Recorrer los ítems y agregarlos al ListView
                    foreach (var item in OCList)
                    {
                        if (Headers == false)
                        {
                            // Agregar columnas al ListView
                            LvOrdenesCompra.Columns.Add("DocEntry", 0, HorizontalAlignment.Left);
                            LvOrdenesCompra.Columns.Add("Pedido", -2, HorizontalAlignment.Left);
                            LvOrdenesCompra.Columns.Add("Artículo", -2, HorizontalAlignment.Left);
                            LvOrdenesCompra.Columns.Add("Codigo Artículo", 0, HorizontalAlignment.Left);
                            LvOrdenesCompra.Columns.Add("Cantidad Total", -2, HorizontalAlignment.Left);
                            LvOrdenesCompra.Columns.Add("Precio Total", -2, HorizontalAlignment.Left);
                            LvOrdenesCompra.Columns.Add("Almacen", 0, HorizontalAlignment.Left);
                            LvOrdenesCompra.Columns.Add("PrecioUnitario", 0, HorizontalAlignment.Left);
                            LvOrdenesCompra.Columns.Add("Proveedor", -2, HorizontalAlignment.Left);
                            Headers = true;
                        }

                        // Crear un nuevo ítem de ListView con la primera columna
                        ListViewItem listViewItem = new ListViewItem(item["DocEntry"]);
                        listViewItem.SubItems.Add(item["Pedido"]);        // Segunda columna
                        listViewItem.SubItems.Add(item["Articulo"]); // septima columna
                        listViewItem.SubItems.Add(item["CodigoArticulo"]); // septima columna
                        listViewItem.SubItems.Add(item["CantidadTotalOrden"]);// Segunda columna
                        listViewItem.SubItems.Add(item["PrecioTotalOrden"]); // novena columna
                        listViewItem.SubItems.Add(item["Almacen"]);// Segunda columna
                        listViewItem.SubItems.Add(item["PrecioUnitario"]);// Segunda columna
                        listViewItem.SubItems.Add(item["Proveedor"]);       // Tercera columna


                        listViewItem.BackColor = rownumber == "0" ? Color.FromArgb(242, 242, 242) : Color.White;
                        listViewItem.ForeColor = Color.Black;
                        rownumber = rownumber == "0" ? "1" : "0";
                        LvOrdenesCompra.Items.Add(listViewItem);
                    }

                    FormHelper.AjustarColumnas(LvOrdenesCompra);
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
                result = "No fue posible consultar el listado de OC: " + Error.ToString();
                return result;
            }
        }
        private string OCComplete(string U_OrdenFisica, string U_Pedimento, string U_PackingList, string U_CertificadoCalidad)
        {
            StringBuilder result = new StringBuilder();
            if (U_CertificadoCalidad == "SI" && U_Pedimento == "SI" && U_PackingList == "SI" && U_CertificadoCalidad == "SI")
                result.Append("OK");
            if (U_OrdenFisica == "NO")
            {
                result.Append("No cuenta con orden física.");
            }
            if (U_Pedimento == "NO")
            {
                result.AppendLine();
                result.Append("No cuenta con pedimento.");
            }
            if (U_PackingList == "NO")
            {
                result.AppendLine();
                result.Append("No cuenta con packing list.");
            }
            if (U_CertificadoCalidad == "NO")
            {
                result.AppendLine();
                result.Append("No cuenta con certificado de calidad.");
            }

            return result.ToString();

        }
        private void GetOCCheckList() // AVANZAR A PACKING LIST
        {

            if (LvOrdenesCompra.SelectedIndices.Count > 0) // Verifica que haya una fila seleccionada
            {
                Cursor.Current = Cursors.WaitCursor;
                int index = LvOrdenesCompra.SelectedIndices[0]; // Obtiene el índice de la fila seleccionada
                ListViewItem item = LvOrdenesCompra.Items[index]; // Obtiene el ítem (fila)
                CurrentData.Add("DocEntry", item.SubItems[0].Text); //Id orden de compra
                CurrentData.Add("DocNum", item.SubItems[1].Text); //Pedido OC

                try
                {
                    FormHelper.AbrirFormularioConValidacion(
                    this,
                    () => new CheckListOC(CurrentData),
                    (form) =>
                    {
                        var ir = form as CheckListOC;
                        if (ir.EstadoInicializacion == "OK")
                        {
                            //Mensaje de aviso de moneda
                            Logic.ShowException(null, "No olvides revisar que la moneda sea la correcta.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                            return true;
                        }
                        else
                        {
                            Logic.ShowException(null, ir.EstadoInicializacion, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                            return false;
                        }
                    },false);
                }
                catch (Exception ex)
                {
                    Logic.ShowException(ex, "No fue posible obtener el checklist de la orden de compra.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
            }
            else
            {
                Logic.ShowException(null, "Selecciona un elemento de la lista para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        #endregion
    }
}