using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace FFISA.Main
{
    public partial class CustomMessageBox : BaseForm
    {
        // 🔹 Declaramos las variables aquí para que sean reconocidas en toda la clase
        // Obtener la ruta del ejecutable en Compact Framework
        private static string directorioBase { get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase); } }
        public Panel PnlCapturaEMModal; // Asegúrate de exponer el Panel

        public CustomMessageBox(string message,string TypeMessage)
        {
            InitializeComponent(); // Llamar al método generado automáticamente
            TxtMensaje.Text = message; // Asignar el mensaje dinámicamente
            TxtMensaje.ScrollToCaret(); // Desplaza el texto hacia el final cada vez que se cambia el contenido
            string rutaImagen = string.Empty;//Cambiar la imagen del picturebox
            switch (TypeMessage)
            {
                case "ERROR":
                    rutaImagen = Path.Combine(directorioBase, "Imagenes\\warning.jpg");
                    break;

                case "OK":
                    rutaImagen = Path.Combine(directorioBase, "Imagenes\\ok.jpg");
                    break;

                case "WARNING":
                    rutaImagen = Path.Combine(directorioBase, "Imagenes\\warning.jpg");
                    break;
            }
            if (File.Exists(rutaImagen))
            {
                PtbMessageIcon.Image = new Bitmap(rutaImagen);
            }

            PnlCapturaEMModal = this.PnlMainContainer; // Asigna el panel a la propiedad pública
        }

        public static void ShowMessage(string message, string TypeMessage)
        {
            CustomMessageBox msgBox = new CustomMessageBox(message,TypeMessage);
            //Cambiar la imagen del picturebox
            string rutaImagen = string.Empty;
            switch (TypeMessage)
            {
                case "ERROR":
                    rutaImagen = Path.Combine(directorioBase, "Imagenes\\close.jpg");
                    break;

                case "OK":
                    rutaImagen = Path.Combine(directorioBase, "Imagenes\\ok.jpg");
                    break;

                case "WARNING":
                    rutaImagen = Path.Combine(directorioBase, "Imagenes\\warning.jpg");
                    break;
            }
            if (File.Exists(rutaImagen))
            {
                msgBox.PtbMessageIcon.Image = new Bitmap(rutaImagen);
            }

            msgBox.ShowDialog();
        }

        private void Regresar_Click(object sender, EventArgs e)
        {

        }

        private void Impresion_Click(object sender, EventArgs e)
        {

        }

    }
}
