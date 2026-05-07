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
    public partial class CuentasContables : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal(); //Logica para hacer peticiones HTTP, MOSTRAR EXCEPCIONES, ETC
        public string EstadoInicializacion { get; private set; }
        public Dictionary<string,string> CuentaSeleccionada { get; set; }
        private Timer debounceTimer;
        //Paginacion
        private List<Dictionary<string, string>> CuentasTotales = new List<Dictionary<string, string>>();
        private int PaginaActual = 0;
        private int ArticulosPorPagina = 50;
        public bool Headers = false;

        #region events
        private void Continuar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                if (LvCuentas.SelectedIndices.Count > 0) // Verifica que haya una fila seleccionada
                {
                    int index = LvCuentas.SelectedIndices[0]; // Obtiene el índice de la fila seleccionada
                    ListViewItem item = LvCuentas.Items[index]; // Obtiene el ítem (fila)
                    CuentaSeleccionada = new Dictionary<string, string>();
                    CuentaSeleccionada.Add("CodigoCuentaContable",item.SubItems[0].Text);
                    CuentaSeleccionada.Add("CuentaContable",item.SubItems[1].Text);
                    this.Close();
                }
                else
                {
                    Logic.ShowException(null, "Selecciona un elemento de la lista para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    this.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible continuar, intenta de nuevo más tarde: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
                this.Enabled = true;
                this.Close();
            }
        }
        private void Regresar_Click(object sender, EventArgs e)
        {
            SoundPlayer.ReproducirSonido("Click.wav");
            CuentaSeleccionada = new Dictionary<string, string>();
            this.Close();
        }
        private void TxtCuenta_KeyUp(object sender, KeyEventArgs e)
        {
            if (TxtCuenta.Text.Length > 2)
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
            BuscarCuentas();     // Realiza la búsqueda
        }
        private void Siguiente_Click(object sender, EventArgs e)
        {
            PaginaActual++;
            BuscarCuentas();
        }
        private void Anterior_Click(object sender, EventArgs e)
        {
            if (PaginaActual > 1)
            {
                PaginaActual--;
                BuscarCuentas();
            }
        }
        #endregion

        #region methods
        public CuentasContables()
        {
            InitializeComponent();
            debounceTimer = new Timer();
            debounceTimer.Interval = 800; // 800 MILESIMAS
            debounceTimer.Enabled = false;
            debounceTimer.Tick += new EventHandler(DebounceTimer_Tick);
            EstadoInicializacion = "OK";
        }
        private void BuscarCuentas()
        {
            try
            {
                if (TxtCuenta.Text.Length > 2)
                {

                    Cursor.Current = Cursors.WaitCursor;
                    Logic.AD.RequestParameters = new Dictionary<string, string>();
                    Logic.AD.RequestParameters.Add("Busqueda", TxtCuenta.Text);
                    Logic.AD.RequestParameters.Add("PaginaActual", PaginaActual.ToString());
                    Logic.AD.RequestParameters.Add("ArticulosPorPagina", ArticulosPorPagina.ToString());
                    string result = ListadoCuentas(Logic.AD.RequestParameters);
                    if (result != "OK")
                        Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    Cursor.Current = Cursors.Default;
                    this.Enabled = true;
                    TxtCuenta.Focus();
                }
                else {
                    this.Enabled = true;
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible consultar el listado de cuentas contables: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                this.Enabled = true;
                Cursor.Current = Cursors.Default;
            }

        }
        private string ListadoCuentas(Dictionary<string, string> parameters)
        {
            string result = string.Empty;

            try
            {
                List<Dictionary<string, string>> OVList = Logic.ExecGetRequest("/InventariosMovil/GetCuentasContablesHHEMD", parameters, false, false);

                // Validar si hay registros antes de intentar enlistarlos
                if (OVList[0]["Status"] == "NO" || OVList[0]["Status"] == "ERROR")
                {
                    result = OVList[0]["Message"];
                    lblTotalOV.Text = "0";
                    LvCuentas.Items.Clear();
                    //lblPagina.Text = "";
                    Anterior.Enabled = false;
                    Siguiente.Enabled = false;
                    return result;
                }

                CuentasTotales = OVList; // Solo la página actual
                lblTotalOV.Text = OVList[0]["TotalRegistros"]; // Mostramos cuántos artículos hay en esta página

                MostrarPagina(CuentasTotales); // Mostrar artículos

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
                this.Enabled = true;
                result = "OK";
                return result;
            }
            catch (Exception ex)
            {
                StringBuilder Error = new StringBuilder();
                Error.Append(ex.Message ?? string.Empty);
                Error.Append(Environment.NewLine);
                Error.Append(Environment.NewLine);
                Error.Append(ex.InnerException != null ? ex.InnerException.ToString() : string.Empty);
                result = "No fue posible consultar el listado de cuentas contables: " + Error.ToString();
                return result;
            }
        }
        private void MostrarPagina(List<Dictionary<string, string>> listaPagina)
        {
            LvCuentas.Items.Clear();
            string rownumber = "0";

            if (Headers == false)
            {
                LvCuentas.Columns.Add("Código", 0, HorizontalAlignment.Left);
                LvCuentas.Columns.Add("Número Cuenta", -2, HorizontalAlignment.Left);
                LvCuentas.Columns.Add("Cuenta", -2, HorizontalAlignment.Left);
                Headers = true;
            }

            foreach (var item in listaPagina)
            {
                ListViewItem listViewItem = new ListViewItem(item["CodigoInterno"]);//0
                listViewItem.SubItems.Add(item["CodigoContable"]);//1
                listViewItem.SubItems.Add(item["NombreCuenta"]);//2
                listViewItem.BackColor = rownumber == "0" ? Color.FromArgb(242, 242, 242) : Color.White;
                listViewItem.ForeColor = Color.Black;
                rownumber = rownumber == "0" ? "1" : "0";

                LvCuentas.Items.Add(listViewItem);
            }

            FormHelper.AjustarColumnas(LvCuentas);
        }
        private void ReiniciaBuscador() 
        {

            this.Enabled = true;
            PaginaActual = 0;
            Siguiente.BackColor = Color.LightGray;
            Siguiente.Enabled = false;
            Anterior.BackColor = Color.LightGray;
            Anterior.Enabled = false;
            LvCuentas.Items.Clear();
            LblTotalPaginas.Text = "Página 0 de 0";
            lblTotalOV.Text = "0";
            Cursor.Current = Cursors.Default;
            TxtCuenta.Focus();
        }
        #endregion
    }
}