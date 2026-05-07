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

namespace FFISA.Compras
{
    public partial class SeleccionArticuloOC : BaseForm
    {
        //Para generar entrada de mercancia
        LogicaGlobal Logic = new LogicaGlobal();
        Dictionary<string, string> CurrentData = new Dictionary<string, string>();
        private string FolioEM { get; set; } //Folio EM
        public string EstadoInicializacion { get; private set; }
        public bool Headers = false;


        #region events
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            CheckListOC CheckListOC = new CheckListOC(CurrentData);
            FormHelper.AbrirFormulario(this, CheckListOC,false);
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
                    this.Enabled = true;

                }
            }
        } //DETALLES DE ARTICULO
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
            }
        }
        private void MenuCompras_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuCompras MenuCompras = new MenuCompras();
            FormHelper.AbrirFormulario(this, MenuCompras, false);
        }
        #endregion

        #region methods
        public SeleccionArticuloOC(Dictionary<string, string> CurrentData)
        {
            InitializeComponent();
            this.CurrentData = CurrentData;
            lblOC.Text = "OC: " + this.CurrentData["DocNum"] + "";
            EstadoInicializacion = ListadoArticulos();
        }//METODO INICIAL
        private string ListadoArticulos()
        {
            string result = string.Empty;

            try
            {
                //Avanzar al escaneo de artículos
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("OrdenCompra", CurrentData["DocEntry"]);
                List<Dictionary<string, string>> OCCheck = Logic.ExecGetRequest("/Compras/GetLinesOrdenesCompra", Logic.AD.RequestParameters, true, true);
                lblTotalOC.Text = OCCheck.Count.ToString();
                string rownumber = "0";

                // Limpiar el ListView antes de agregar nuevos elementos
                LvArticulos.Items.Clear();

                 //Validar si hay registros antes de intentar enlistarlos
                if (OCCheck[0]["Status"] == "NO" || OCCheck[0]["Status"] == "ERROR")
                {
                    result = OCCheck[0]["Message"];
                    return result;
                }
                else
                {
                    // Recorrer los ítems y agregarlos al ListView
                    foreach (var item in OCCheck)
                    {
                        if (Headers == false)
                        {
                            // Agregar columnas al ListView
                            LvArticulos.Columns.Add("ID", 0, HorizontalAlignment.Left); //0
                            LvArticulos.Columns.Add("Orden Compra", 0, HorizontalAlignment.Left); //1
                            LvArticulos.Columns.Add("Artículo", -2, HorizontalAlignment.Left); //2
                            LvArticulos.Columns.Add("Descripcion", 0, HorizontalAlignment.Left); //3
                            LvArticulos.Columns.Add("Kilos", 0, HorizontalAlignment.Left);//4
                            LvArticulos.Columns.Add("Etiqueta", 0, HorizontalAlignment.Left);//5
                            LvArticulos.Columns.Add("Línea", 0, HorizontalAlignment.Left);//6
                            LvArticulos.Columns.Add("Cantidad", -2, HorizontalAlignment.Left);//7
                            LvArticulos.Columns.Add("Almacén", -2, HorizontalAlignment.Left);//8
                            Headers = true;
                        }

                        // Crear un nuevo ítem de ListView con la primera columna
                        ListViewItem listViewItem = new ListViewItem(item["DocEntry"]); //ID 0
                        listViewItem.SubItems.Add(item["DocNum"]);  // OrdenCompra 1
                        listViewItem.SubItems.Add(item["ItemCode"]);  // Articulo 2
                        listViewItem.SubItems.Add(item["Descripcion"]);  // Descripcion 3
                        listViewItem.SubItems.Add(item["Kilos"]);  // Kilos 4
                        listViewItem.SubItems.Add(item["Etiqueta"]);  // Etiqueta 5
                        listViewItem.SubItems.Add(item["LineNum"]);  // Linea 6
                        listViewItem.SubItems.Add(item["Quantity"]);  // Cantidad 7   
                        listViewItem.SubItems.Add(item["WhsCode"]);   // Almacen 8


                        listViewItem.BackColor = rownumber == "0" ? Color.FromArgb(242, 242, 242) : Color.White;
                        listViewItem.ForeColor = Color.Black;
                        rownumber = rownumber == "0" ? "1" : "0";
                        LvArticulos.Items.Add(listViewItem);
                    }

                    FormHelper.AjustarColumnas(LvArticulos);
                    result = "OK";
                    return result;
                }
            }
            catch (Exception ex)
            {
                StringBuilder Error = new StringBuilder();
                Error.Append(ex.Message != null ? ex.Message.ToString() : string.Empty);
                Error.Append(ex.InnerException != null ? ex.InnerException.ToString() : string.Empty);
                result = "No fue posible consultar el listado de articulos para la orden: " + CurrentData["DocNum"] + " " + Error.ToString();
                return result;
            }
        }
        private void GetDetailsArticle()
        {
            if (LvArticulos.SelectedIndices.Count > 0) // Verifica que haya una fila seleccionada
            {
                int index = LvArticulos.SelectedIndices[0]; // Obtiene el índice de la fila seleccionada
                ListViewItem item = LvArticulos.Items[index]; // Obtiene el ítem (fila)
                if (!CurrentData.ContainsKey("DocEntry"))
                CurrentData.Add("DocEntry", item.SubItems[0].Text); //Id
                if (!CurrentData.ContainsKey("DocNum"))
                CurrentData.Add("DocNum", item.SubItems[1].Text); //Pedido
                CurrentData.Add("Articulo", item.SubItems[2].Text); //Articulo
                CurrentData.Add("Descripcion", item.SubItems[3].Text); //Descripcion
                CurrentData.Add("Kilos", item.SubItems[4].Text); //Kilos
                CurrentData.Add("Etiqueta", item.SubItems[5].Text); //Etiqueta
                CurrentData.Add("Linea", item.SubItems[6].Text); //Linea
                CurrentData.Add("Cantidad", item.SubItems[7].Text); //Cantidad
                CurrentData.Add("Almacen", item.SubItems[8].Text); //Almacen
                if (!CurrentData.ContainsKey("FolioEM"))
                CurrentData.Add("FolioEM", FolioEM); //Folio de EM para generar registros


                DatosGeneralesEM DatosGeneralesEM = new DatosGeneralesEM(CurrentData);
                FormHelper.AbrirFormulario(this, DatosGeneralesEM,false);
            }
            else {
                Logic.ShowException(null, "Debes seleccionar un artículo para continuar. ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        #endregion
    }
}