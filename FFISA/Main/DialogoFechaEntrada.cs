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
using FFISA.Main;
using System.Threading;

namespace FFISA.Main
{
    public partial class DialogoFechaEntrada : BaseForm
    {
        //Para generar entrada de mercancia
        public string FechaEntrada { get; private set; }
        public string IsPreeliminar { get; private set; }
        public string EstadoInicializacion { get; private set; }
        LogicaGlobal Logic = new LogicaGlobal();


        #region events
        private void Continuar_Click(object sender, EventArgs e)
        {
            try
            {
                SoundPlayer.ReproducirSonido("Click.wav");
                Cursor.Current = Cursors.WaitCursor;

                // Guardar fecha entrada
                this.FechaEntrada = TxtFechaEntrada.Text;
                this.IsPreeliminar = (Preeliminar.Checked == true ? "SI" : "NO");
                Cursor.Current = Cursors.Default;

                this.Close(); // Cierra el formulario sin deshabilitarlo antes
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible establecer la fecha de entrada.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
            }
        }

        private void Regresar_Click(object sender, EventArgs e)
        {
            SoundPlayer.ReproducirSonido("Click.wav");
            this.FechaEntrada = string.Empty;
            this.Close();
        }
        #endregion

        #region methods
        public DialogoFechaEntrada(string Folio)
        {
            InitializeComponent();
            LblTitleModule.Text = "FECHA ENTRADA DOCUMENTO: " + Folio;
            //FORMATO Año/Mes/Dia
            TxtFechaEntrada.Format = DateTimePickerFormat.Custom;
            TxtFechaEntrada.CustomFormat = "yyyy-MM-dd";
            TxtFechaEntrada.Value = DateTime.Now;
            PnlFechaContainer.Focus();
            this.EstadoInicializacion = "OK";
        }
        #endregion
    }
}