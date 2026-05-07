// ========================================
// ARCHIVO: Program.cs (SIN CAMBIOS)
// ========================================
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using FFISA.Main;

namespace FFISA
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            // En CF 3.5 no están disponibles ApplicationExit ni ProcessExit
            // Solo usamos el evento Closing del formulario
            Application.Run(new Login());
        }
    }
}