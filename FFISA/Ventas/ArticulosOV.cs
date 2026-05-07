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
    public partial class ArticulosOV : BaseForm
    {
        //Para generar entrada de mercancia
        LogicaGlobal Logic = new LogicaGlobal();
        private Dictionary<string, string> CurrentData { get; set; }
        public string EstadoInicializacion { get; private set; }
        public string FromNuevoDocumento { get; set; } //Viene desde los datos maestros
        public bool Headers = false;


        #region events
        private void Continuar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                GetDetailsArticle();
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible consultar los detalles del artículo: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        private void MenuEntregas_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuEntregas MenuEntregas = new MenuEntregas();
            FormHelper.AbrirFormulario(this, MenuEntregas, false);
        }
        private void LvArticulos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    FormHelper.ClickEvent();
                    GetDetailsArticle();
                }
                catch (Exception ex)
                {
                    Logic.ShowException(ex, "No fue posible consultar los detalles del artículo: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                }
            }
        }
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            EntregasPendientes();
        }
        #endregion

        #region methods
        public ArticulosOV(Dictionary<string, string> CurrentData)
        {
            InitializeComponent();
            this.CurrentData = CurrentData;
            lblOV.Text = "OV: " + this.CurrentData["OrdenVenta"] + "";
            EstadoInicializacion = ListadoArticulos();
        }//METODO INICIAL
        private string ListadoArticulos()
        {
            string result = string.Empty;

            try
            {
                //Avanzar al escaneo de artículos
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("OV", CurrentData["DocEntry"]);
                List<Dictionary<string, string>> OVCheck = Logic.ExecGetRequest("/VentasMovil/GetArticulosPorOVHH", Logic.AD.RequestParameters, true, true);
                lblTotalOV.Text = OVCheck.Count.ToString();
                string rownumber = "0";

                // Limpiar el ListView antes de agregar nuevos elementos
                LvArticulos.Items.Clear();

                //Validar si hay registros antes de intentar enlistarlos
                if (OVCheck[0]["Status"] == "NO" || OVCheck[0]["Status"] == "ERROR")
                {
                    result = OVCheck[0]["Message"];
                    return result;
                }
                else
                {
                    // Recorrer los ítems y agregarlos al ListView
                    foreach (var item in OVCheck)
                    {
      
                        if (Headers == false)
                        {
                            // Agregar columnas al ListView
                            string TituloCantidad = string.Empty;
                            switch (item["DescripcionUnidadSolicitada"])
                            {
                                case "Metros":
                                    TituloCantidad = "Cantidad en metros";
                                    break;
                                case "Kg":
                                    TituloCantidad = "Cantidad en kilos";
                                    break;
                                case "Pzas":
                                    TituloCantidad = "Cantidad";
                                    break;

                                case "Yardas":
                                    TituloCantidad = "Cantidad en yardas";
                                    break;
                            }
                            LvArticulos.Columns.Add("CodigoArticulo", 0, HorizontalAlignment.Left); //0
                            LvArticulos.Columns.Add("OrdenVenta", -2, HorizontalAlignment.Left); //1
                            LvArticulos.Columns.Add("Artículo", -2, HorizontalAlignment.Left); //2
                            LvArticulos.Columns.Add(TituloCantidad, -2, HorizontalAlignment.Left);//3
                            LvArticulos.Columns.Add("UMS", -2, HorizontalAlignment.Left);//4
                            LvArticulos.Columns.Add("UMV", -2, HorizontalAlignment.Left);//5
                            LvArticulos.Columns.Add("UMI", -2, HorizontalAlignment.Left);//6
                            Headers = true;
                        }

                        // Crear un nuevo ítem de ListView con la primera columna
                        ListViewItem listViewItem = new ListViewItem(item["CodigoArticulo"]); //0
                        listViewItem.SubItems.Add(item["OrdenVenta"]);//1
                        listViewItem.SubItems.Add(item["CodigoArticulo"]);//2
                        listViewItem.SubItems.Add(item["CantidadSolicitada"]); //3
                        listViewItem.SubItems.Add(item["DescripcionUnidadSolicitada"]); //4
                        listViewItem.SubItems.Add(item["UnidadMedidaVenta"]); //5
                        listViewItem.SubItems.Add(item["UnidadMedidaInventario"]); //6

                        listViewItem.BackColor = rownumber == "0" ? Color.FromArgb(242, 242, 242) : Color.White;
                        listViewItem.ForeColor = Color.Black;
                        rownumber = rownumber == "0" ? "1" : "0";
                        LvArticulos.Items.Add(listViewItem);
                    }

                    result = "OK";
                    return result;
                }
            }
            catch (Exception ex)
            {
                StringBuilder Error = new StringBuilder();
                Error.Append(ex.Message != null ? ex.Message.ToString() : string.Empty);
                Error.Append(ex.InnerException != null ? ex.InnerException.ToString() : string.Empty);
                string CustomError = (Error.ToString().Contains("SocketException") || Error.ToString().Contains("timed-out")
                                    ? "No es posible establecer conexión con el servidor, intenta de nuevo más tarde."
                                    : Error.ToString());
                result = "No fue posible consultar el listado de articulos para la orden: " + CurrentData["OrdenVenta"] + " " + CustomError;
                return result;
            }
        }
        private void GetDetailsArticle()
        {
            if (LvArticulos.SelectedIndices.Count > 0) // Verifica que haya una fila seleccionada
            {
                int index = LvArticulos.SelectedIndices[0]; // Obtiene el índice de la fila seleccionada
                ListViewItem item = LvArticulos.Items[index]; // Obtiene el ítem (fila)
                if (CurrentData.ContainsKey("CodigoArticulo"))
                    CurrentData["CodigoArticulo"] = item.SubItems[0].Text;
                else
                    CurrentData.Add("CodigoArticulo", item.SubItems[0].Text);

                try
                {
                    FormHelper.AbrirFormularioConValidacion(
                    this,
                    () => new DatosMaestrosArticulo(CurrentData),
                    (form) =>
                    {
                        var ir = form as DatosMaestrosArticulo;
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
                    Logic.ShowException(ex, "No fue posible mostrar los datos maestros del artículo seleccionado.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
            }
            else
            {
                Logic.ShowException(null, "Debes seleccionar un artículo para continuar. ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
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
                        ir.Regresar.Click += new EventHandler(ir.Regresar_Click); //Asignar evento click
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
                Logic.ShowException(ex, "No fue posible mostrar la vista de entregas pendientes.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        #endregion
    }
}