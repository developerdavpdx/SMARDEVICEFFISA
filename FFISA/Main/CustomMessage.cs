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
    public partial class CustomMessage : BaseForm
    {
        //Para generar entrada de mercancia
        public string EstadoInicializacion { get; private set; }
        LogicaGlobal Logic = new LogicaGlobal();


        #region events
        private void Continuar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                SoundPlayer.ReproducirSonido("Click.wav");
                this.Close(); // Cierra el formulario sin deshabilitarlo antes
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible mostrar el mensaje personalizado.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
            }
        }
        #endregion

        #region methods
        public CustomMessage()
        {
            InitializeComponent();
            LblTitleModule.Text = "AVISO";
            TxtDescriptionModule.ReadOnly = true;
            TxtDescriptionModule.BackColor = Color.White; // Fuerza fondo blanco
            TxtDescriptionModule.ScrollToCaret(); // Desplaza el texto hacia el final cada vez que se cambia el contenido
            this.EstadoInicializacion = "OK";
        }
        #endregion
    }
}