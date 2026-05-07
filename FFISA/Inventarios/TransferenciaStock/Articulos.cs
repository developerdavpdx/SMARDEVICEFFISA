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

namespace FFISA.Inventarios.TransferenciaStock
{
    public partial class Articulos : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal(); //Logica para hacer peticiones HTTP, MOSTRAR EXCEPCIONES, ETC
        Dictionary<string, string> CurrentData = new Dictionary<string, string>();
        public string EstadoInicializacion { get; private set; }
        private Timer debounceTimer;
        //Paginacion
        private List<Dictionary<string, string>> ArticulosTotales = new List<Dictionary<string, string>>();
        private int PaginaActual = 0;
        private int ArticulosPorPagina = 50;
        public bool Headers = false;

        #region events
        private void Continuar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                GetDetailsArticulo(); //continuar en el menu siguiente
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible consultar los detalles del artículo: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuTransferencias MenuSalidasDirectas = new MenuTransferencias();
            FormHelper.AbrirFormulario(this, MenuSalidasDirectas, false);
        }
        private void MenuEntradas_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuTransferencias MenuSalidasDirectas = new MenuTransferencias();
            FormHelper.AbrirFormulario(this, MenuSalidasDirectas, false);
        }
        private void TxtArticulo_KeyUp(object sender, KeyEventArgs e)
        {
            if (TxtArticulo.Text.Length > 1)
            {
                debounceTimer.Enabled = false;  // Reinicia el temporizador
                debounceTimer.Enabled = true;
            }
            else
            {
                ReiniciaBuscador();
            }
        }
        private void DebounceTimer_Tick(object sender, EventArgs e) //Disparar la busqueda si el usuario deja de escribir
        {
            debounceTimer.Enabled = false;  // Detén el timer para que no se repita
            this.Enabled = false; //Desactivar el formulario
            PaginaActual = 1; //Pagina actual
            ArticulosPorPagina = 50; //Articulos por pagina
            BuscarArticulos();     // Realiza la búsqueda
        }
        private void Siguiente_Click(object sender, EventArgs e)
        {
            PaginaActual++;
            BuscarArticulos();
        }
        private void Anterior_Click(object sender, EventArgs e)
        {
            if (PaginaActual > 1)
            {
                PaginaActual--;
                BuscarArticulos();
            }
        }
        #endregion

        #region methods
        public Articulos(Dictionary<string, string> CurrentData)
        {
            InitializeComponent();
            debounceTimer = new Timer();
            debounceTimer.Interval = 800; // 800 MILESIMAS
            debounceTimer.Enabled = false;
            debounceTimer.Tick += new EventHandler(DebounceTimer_Tick);
            this.CurrentData = CurrentData;
            EstadoInicializacion = "OK";
        }
        private void BuscarArticulos()
        {
            try
            {
                if (TxtArticulo.Text.Length > 1)
                {

                    Cursor.Current = Cursors.WaitCursor;
                    Logic.AD.RequestParameters = new Dictionary<string, string>();
                    Logic.AD.RequestParameters.Add("Busqueda", TxtArticulo.Text);
                    Logic.AD.RequestParameters.Add("PaginaActual", PaginaActual.ToString());
                    Logic.AD.RequestParameters.Add("ArticulosPorPagina", ArticulosPorPagina.ToString());
                    string result = ListadoArticulos(Logic.AD.RequestParameters);
                    if (result != "OK")
                        Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    Cursor.Current = Cursors.Default;
                    this.Enabled = true;
                    TxtArticulo.Focus();
                }
                else
                {
                    this.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    TxtArticulo.Focus();
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible consultar el listado de artículos: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                this.Enabled = true;
                Cursor.Current = Cursors.Default;
            }

        }
        private string ListadoArticulos(Dictionary<string, string> parameters)
        {
            string result = string.Empty;

            try
            {
                List<Dictionary<string, string>> OVList = Logic.ExecGetRequest("/InventariosMovil/GetArticulosHHEMD", parameters, false, false);

                // Validar si hay registros antes de intentar enlistarlos
                if (OVList[0]["Status"] == "NO" || OVList[0]["Status"] == "ERROR")
                {
                    result = OVList[0]["Message"];
                    lblTotalOV.Text = "0";
                    LvArticulos.Items.Clear();
                    //lblPagina.Text = "";
                    Anterior.Enabled = false;
                    Siguiente.Enabled = false;
                    return result;
                }

                ArticulosTotales = OVList; // Solo la página actual
                lblTotalOV.Text = OVList[0]["TotalRegistros"]; // Mostramos cuántos artículos hay en esta página

                MostrarPagina(ArticulosTotales); // Mostrar artículos

                // Mostrar texto de página actual
                LblTotalPaginas.Text = "Página " + PaginaActual + " de " + OVList[0]["TotalPaginas"];

                // Lógica para botones (Anterior)
                if (PaginaActual > 1)
                {
                    Anterior.BackColor = Color.FromArgb(0, 128, 255);
                    Anterior.Enabled = true;
                }
                else
                {

                    Anterior.BackColor = Color.LightGray;
                    Anterior.Enabled = false;
                }
                // Lógica para botones (Siguiente)
                if (OVList.Count == ArticulosPorPagina)
                {
                    Siguiente.BackColor = Color.FromArgb(0, 128, 255);
                    Siguiente.Enabled = true;
                }
                else
                {
                    Siguiente.BackColor = Color.LightGray;
                    Siguiente.Enabled = false;
                }

                OVList.Clear();
                ArticulosTotales.Clear();
                this.Enabled = true;
                result = "OK";
                return result;
            }
            catch (Exception ex)
            {
                string CustomError = string.Empty;
                StringBuilder Error = new StringBuilder();
                Error.Append(ex.Message ?? string.Empty);
                Error.Append(Environment.NewLine);
                Error.Append(Environment.NewLine);
                Error.Append(ex.InnerException != null ? ex.InnerException.ToString() : string.Empty);
                CustomError = (Error.ToString().Contains("SocketException") || Error.ToString().Contains("timed-out")
            ? "No es posible establecer conexión con el servidor, intenta de nuevo más tarde."
            : Error.ToString());

            CustomError = (Error.ToString().Contains("OutOfMemoryException") ? "La memoria del dispositivo no es suficiente para realizar el proceso en este momento, intenta de nuevo más tarde.": CustomError);
                result = "No fue posible consultar el listado de artículos: " + CustomError;
                return result;
            }
        }
        private void MostrarPagina(List<Dictionary<string, string>> listaPagina)
        {
            LvArticulos.Items.Clear();
            string rownumber = "0";

            if (Headers == false)
            {
                LvArticulos.Columns.Add("Artículo", -2, HorizontalAlignment.Left);
                LvArticulos.Columns.Add("Desc. Artículo", 0, HorizontalAlignment.Left);
                LvArticulos.Columns.Add("Almacén", -2, HorizontalAlignment.Left);
                LvArticulos.Columns.Add("CodigoAlmacén", -2, HorizontalAlignment.Left);
                LvArticulos.Columns.Add("Stock Disponible", -2, HorizontalAlignment.Left);
                LvArticulos.Columns.Add("Grupo de artículos", -2, HorizontalAlignment.Left);
                LvArticulos.Columns.Add("Cuenta Contable", 0, HorizontalAlignment.Left);
                LvArticulos.Columns.Add("CodCC", 0, HorizontalAlignment.Left); //Codigo Cuenta Contable
                Headers = true;
            }

            foreach (var item in listaPagina)
            {
                ListViewItem listViewItem = new ListViewItem(item["Codigo"]);
                listViewItem.SubItems.Add(item["Descripcion"]);
                listViewItem.SubItems.Add(item["NombreAlmacen"]);
                listViewItem.SubItems.Add(item["CodigoAlmacen"]);
                listViewItem.SubItems.Add(item["StockDisponible"]);
                listViewItem.SubItems.Add(item["GrupoDeArticulos"]);
                listViewItem.SubItems.Add(item["CuentaContable"]);
                listViewItem.SubItems.Add(item["CodigoCuentaContable"]);

                listViewItem.BackColor = rownumber == "0" ? Color.FromArgb(242, 242, 242) : Color.White;
                listViewItem.ForeColor = Color.Black;
                rownumber = rownumber == "0" ? "1" : "0";

                LvArticulos.Items.Add(listViewItem);
            }

            FormHelper.AjustarColumnas(LvArticulos);
        }
        private void GetDetailsArticulo() // AVANZAR A INFORMACION ARTICULO
        {
            if (LvArticulos.SelectedIndices.Count > 0) // Verifica que haya una fila seleccionada
            {
                int index = LvArticulos.SelectedIndices[0]; // Obtiene el índice de la fila seleccionada
                ListViewItem item = LvArticulos.Items[index]; // Obtiene el ítem (fila)
                CurrentData.Add("CodigoArticulo", item.SubItems[0].Text); //Codigo
                CurrentData.Add("Articulo", item.SubItems[1].Text); //Articulo
                CurrentData.Add("Almacen", item.SubItems[2].Text); //Almacen
                CurrentData.Add("CodigoAlmacen", item.SubItems[3].Text); //Codigo Almacen
                CurrentData.Add("StockDisponible", item.SubItems[4].Text); //StockDisponible
                CurrentData.Add("GrupoDeArticulos", item.SubItems[5].Text); //GrupoDeArticulos
                CurrentData.Add("CuentaContable", item.SubItems[6].Text); //CuentaContable
                CurrentData.Add("CodigoCuentaContable", item.SubItems[7].Text); //CodigoCuentaContable

                try
                {
                    FormHelper.AbrirFormularioConValidacion(
                    this,
                    () => new DatosMaestrosTraspaso(CurrentData),
                    (form) =>
                    {
                        var ir = form as DatosMaestrosTraspaso;
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
                    Logic.ShowException(ex, "No fue posible mostrar el formulario de captura para los datos maestros de artículo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
            }
            else
            {
                Logic.ShowException(null, "Selecciona un elemento de la lista para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                this.Enabled = true;
            }
        }
        private void ReiniciaBuscador()
        {

            this.Enabled = true;
            PaginaActual = 0;
            Siguiente.BackColor = Color.LightGray;
            Siguiente.Enabled = false;
            Anterior.BackColor = Color.LightGray;
            Anterior.Enabled = false;
            LvArticulos.Items.Clear();
            LblTotalPaginas.Text = "Página 0 de 0";
            lblTotalOV.Text = "0";
            Cursor.Current = Cursors.Default;
            TxtArticulo.Focus();
        }
        #endregion
    }
}