using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.WindowsCE.Forms;

namespace FFISA.Model
{
    public static class FormHelper
    {
        public static string Perfil { get; set; }
        public static string Usuario { get; set; }
        public static string UsuarioMin { get; set; }
        public static string Sociedad { get; set; }
        public static int EscaneosVentas { get; set; }
        public static Form Login { get; set; }
        public static Dictionary<string, object> ModuleData = new Dictionary<string, object>();
        public static bool isproductiveCompras = true;
        public static bool isproductiveVentas = true;
        public static bool isproductiveEntradaDirecta = true;
        public static bool isproductiveSalidaDirecta = true;
        public static bool isproductiveTraspasoMercancia = true;
        public static bool isproductiveRecuentoInventarios = true;

        // Variable estática para el teclado en pantalla
        private static InputPanel _teclado;

        // Propiedad para acceder al teclado (se crea solo una vez)
        private static InputPanel Teclado
        {
            get
            {
                if (_teclado == null)
                {
                    _teclado = new InputPanel();
                }
                return _teclado;
            }
        }

        /// <summary>
        /// Muestra u oculta el teclado en pantalla
        /// </summary>
        /// <param name="mostrar">True para mostrar, False para ocultar</param>
        public static void MostrarTeclado(bool mostrar)
        {
            try
            {
                Teclado.Enabled = mostrar;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al mostrar/ocultar teclado: " + ex.Message);
            }
        }

        /// <summary>
        /// Alterna el estado del teclado (si está visible lo oculta, si está oculto lo muestra)
        /// </summary>
        public static void AlternarTeclado()
        {
            try
            {
                Teclado.Enabled = !Teclado.Enabled;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al alternar teclado: " + ex.Message);
            }
        }

        /// <summary>
        /// Configura eventos GotFocus/LostFocus en un TextBox para mostrar/ocultar el teclado automáticamente
        /// </summary>
        /// <param name="textBox">TextBox al que se le agregarán los eventos</param>
        /// <param name="ocultarAlPerderFoco">Si es true, oculta el teclado cuando pierde el foco</param>
        public static void ConfigurarTecladoAutomatico(TextBox textBox, bool ocultarAlPerderFoco)
        {
            if (textBox == null) return;

            textBox.GotFocus += new EventHandler(TextBox_GotFocus_MostrarTeclado);

            if (ocultarAlPerderFoco)
            {
                textBox.LostFocus += new EventHandler(TextBox_LostFocus_OcultarTeclado);
            }
        }

        /// <summary>
        /// Configura eventos GotFocus en un TextBox para mostrar el teclado automáticamente (sin ocultar al perder foco)
        /// </summary>
        /// <param name="textBox">TextBox al que se le agregarán los eventos</param>
        public static void ConfigurarTecladoAutomatico(TextBox textBox)
        {
            ConfigurarTecladoAutomatico(textBox, false);
        }

        // Manejadores de eventos separados
        private static void TextBox_GotFocus_MostrarTeclado(object sender, EventArgs e)
        {
            MostrarTeclado(true);
        }

        private static void TextBox_LostFocus_OcultarTeclado(object sender, EventArgs e)
        {
            MostrarTeclado(false);
        }

        public static T GetModuleValue<T>(string key)
        {
            if (ModuleData != null && ModuleData.ContainsKey(key) && ModuleData[key] is T)
            {
                return (T)ModuleData[key];
            }
            return default(T);
        }

        public static void ClickEvent()
        {
            Cursor.Current = Cursors.WaitCursor;
            SoundPlayer.ReproducirSonido("Click.wav");
        }

        public static void AbrirFormulario(Form formularioActual, Form nuevoFormulario, bool ShouldRefresh)
        {
            try
            {
                nuevoFormulario.Show();

                if (ShouldRefresh)
                {
                    nuevoFormulario.BringToFront();
                    nuevoFormulario.Refresh();
                    Application.DoEvents();
                }

                formularioActual.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar de formulario: " + ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public static void AbrirFormularioConValidacion(Form formularioActual, Func<Form> crearNuevoFormulario, Func<Form, bool> validarFormulario, bool ShouldRefresh)
        {
            try
            {
                Form nuevoFormulario = crearNuevoFormulario();

                if (validarFormulario(nuevoFormulario))
                {
                    nuevoFormulario.Show();
                    if (ShouldRefresh)
                    {
                        nuevoFormulario.BringToFront();
                        nuevoFormulario.Refresh();
                        Application.DoEvents();
                    }
                    formularioActual.Close();
                }
                else
                {
                    nuevoFormulario.Close();
                    formularioActual.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir formulario: " + ex.Message);
                formularioActual.Enabled = true;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public static void AjustarColumnas(ListView lv)
        {
            foreach (ColumnHeader col in lv.Columns)
            {
                if (col.Width != 0)
                {
                    col.Width = -2;
                }
            }
        }
    }
}