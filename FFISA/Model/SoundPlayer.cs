using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using FFISA.Main;
using System.Windows.Forms;


namespace FFISA.Model
{
    class SoundPlayer
    {
        [DllImport("coredll.dll", SetLastError = true)]
        private static extern bool PlaySound(string pszSound, IntPtr hmod, uint fdwSound);
       

        private const uint SND_SYNC = 0x0000;  // Reproducir y esperar a que termine
        private const uint SND_ASYNC = 0x0001; // Reproducir sin esperar
        private const uint SND_FILENAME = 0x00020000; // Especificamos un archivo

        public static void ReproducirSonido(string archivo)
        {
            string directorioBase = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string rutaSonido = System.IO.Path.Combine(directorioBase, "Sonidos\\" + archivo);

            if (!System.IO.File.Exists(rutaSonido))
            {
                MessageBox.Show("No se encontró el sonido: " + rutaSonido,"ERROR");
                return;
            }

            PlaySound(rutaSonido, IntPtr.Zero, SND_ASYNC | SND_FILENAME);
        }
    }
}
