using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FFISA.Model;

namespace FFISA.Compras
{
    public partial class SeriesNumeracionOC : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal();
        Dictionary<string, string> CurrentData = new Dictionary<string, string>();
        public string EstadoInicializacion { get; private set; }

        #region events
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuCompras MenuCompras = new MenuCompras();
            FormHelper.AbrirFormulario(this, MenuCompras,false);
        }
        private void Continuar_Click(object sender, EventArgs e)
        {
            try
            {
                SoundPlayer.ReproducirSonido("Click.wav");
                Cursor.Current = Cursors.WaitCursor;
                this.Enabled = false;
                string series = string.Empty;

                foreach (Control ctrl in PnlSeriesNumeracion.Controls)
                {
                    if (ctrl is CheckBox)
                    {
                        CheckBox chk = (CheckBox)ctrl;

                        if (chk.Checked)
                        {
                            series += chk.Name + ",";
                        }
                    }
                }
                if (series == string.Empty)
                {
                    Logic.ShowException(null, "Debes seleccionar al menos una serie de numeración.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    series = series.TrimEnd(',');
                    CurrentData.Add("Series", series); //Guardar las series seleccionadas
                    ListadoOC(CurrentData); //Siguiente pantalla
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible seleccionar las series de numeración: ", 250, "ERROR", "Error.wav", this, false);
            }
        }
        private void SeleccionarTodos_Click(object sender, EventArgs e)
        {
            SoundPlayer.ReproducirSonido("Click.wav");
            if (SeleccionarTodos.Checked)
            {
                foreach (Control ctrl in PnlSeriesNumeracion.Controls)
                {
                    if (ctrl is CheckBox)
                    {
                        CheckBox chk = (CheckBox)ctrl;

                        chk.Checked = true;
                    }
                }
            }
            else
            {
                if (!SeleccionarTodos.Checked)
                {
                    foreach (Control ctrl in PnlSeriesNumeracion.Controls)
                    {
                        if (ctrl is CheckBox)
                        {
                            CheckBox chk = (CheckBox)ctrl;

                            chk.Checked = false;
                        }
                    }
                }
            }
        }
        private void Checkbox_Click(object sender, EventArgs e)
        {
            SoundPlayer.ReproducirSonido("Click.wav");
        }
        #endregion

        #region methods
        public SeriesNumeracionOC()
        {
            InitializeComponent();
            EstadoInicializacion = ListaSeries();
        }
        private string ListaSeries()
        {
            string result = string.Empty;

            try
            {
                // Variables para controlar la disposición de los CheckBox
                int topPosition = 10;   // Controla la posición vertical
                int leftPosition = 10;  // Controla la posición horizontal
                int columnIndex = 0;    // Controla cuántos CheckBox van por fila

                PnlSeriesNumeracion.Controls.Clear();
                PnlSeriesNumeracion.Enabled = true;
                // Obtener series de numeración
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("ObjectCode", "22");
                List<Dictionary<string, string>> series = Logic.ExecGetRequest("/Compras/SeriesNumeracion", Logic.AD.RequestParameters, false, false);

                //Validar si hay registros antes de intentar enlistarlos
                if (series[0]["Status"] == "NO" || series[0]["Status"] == "ERROR")
                {
                    result = series[0]["Message"];
                    return result;
                }
                else
                {
                    foreach (var serie in series)
                    {
                        // Crear el CheckBox
                        CheckBox checkBox = new CheckBox();

                        // Copiar propiedades de "SeleccionarTodos"
                        checkBox.Anchor = SeleccionarTodos.Anchor;
                        checkBox.AutoCheck = SeleccionarTodos.AutoCheck;
                        checkBox.BackColor = SeleccionarTodos.BackColor;
                        checkBox.Checked = SeleccionarTodos.Checked;
                        checkBox.CheckState = SeleccionarTodos.CheckState;
                        checkBox.ContextMenu = SeleccionarTodos.ContextMenu;
                        checkBox.Enabled = true;
                        checkBox.Font = SeleccionarTodos.Font;
                        checkBox.ForeColor = SeleccionarTodos.ForeColor;


                        // Agregar evento Click (usando método en lugar de lambda)
                        checkBox.Click += new EventHandler(Checkbox_Click);

                        // Asignar texto y nombre específico
                        checkBox.Text = serie["SeriesName"];
                        checkBox.Name = serie["Series"];

                        // Posición y tamaño
                        //checkBox.Size = SeleccionarTodos.Size;
                        checkBox.Location = new Point(leftPosition, topPosition);

                        // Agregar el CheckBox al Panel
                        PnlSeriesNumeracion.Controls.Add(checkBox);

                        // Alternar posición para la siguiente columna
                        if (columnIndex == 0) // Primera columna (izquierda)
                        {
                            leftPosition = 120; // Mover a la segunda columna (derecha)
                            columnIndex = 1;
                        }
                        else // Segunda columna (derecha)
                        {
                            leftPosition = 10;  // Reiniciar a la primera columna
                            topPosition += checkBox.Height + 5; // Avanzar a la siguiente fila
                            columnIndex = 0;
                        }
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
                result = "Error: No fue posible obtener el listado de series de numeración: " + Error.ToString();
                return result;
            }
        }
        private void ListadoOC(Dictionary<string, string> CurrentData)
        {
            try
            {
                FormHelper.AbrirFormularioConValidacion(
                this,
                () => new ListadoOC(CurrentData),
                (form) =>
                {
                    var ir = form as ListadoOC;
                    if (ir.EstadoInicializacion == "OK")
                    {
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
                Logic.ShowException(ex, "No fue posible obtener el listado de ordenes de compra.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        #endregion
    }
}