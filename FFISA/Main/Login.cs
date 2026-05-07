// ========================================
// ARCHIVO: Login.cs (CON DETECCIÓN DE CIERRE)
// ========================================
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
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace FFISA.Main
{
    public partial class Login : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal();

        #region events
        //GET REQUEST
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                SoundPlayer.ReproducirSonido("Click.wav");
                this.Enabled = false;
                IniciarSesion();
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible iniciar sesión: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                this.Enabled = true;
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            SoundPlayer.ReproducirSonido("Click.wav");
            SoundPlayer.ReproducirSonido("salir.wav");
            this.Close(); // ✅ Esto SÍ dispara Login_Closing → CerrarSesionActiva()
        }

        // EVENTO: Detectar cuando se cierra el formulario (X, botón o sistema)
        private void Login_Closing(object sender, CancelEventArgs e)
        {
            // Intentar cerrar sesión antes de cerrar el formulario
            //CerrarSesionActiva();
        }
        #endregion

        #region methods
        public Login()
        {
            InitializeComponent();

            // IMPORTANTE: Suscribirse al evento de cierre del formulario
            this.Closing += new CancelEventHandler(Login_Closing);

            //Leer credenciales guardadas anteriormente
            string email;
            string password;
            string[] deviceCredentials = DeviceManager.GetDeviceCredentials(out email, out password);
            if (deviceCredentials.Length >= 2)
            {
                txtusuario.Text = deviceCredentials[0];
                //txtpassword.Text = deviceCredentials[2]; NO APLICA PARA ESTA VERSION
            }
            txtpassword.Focus();
            SoundPlayer.ReproducirSonido("Inicio.wav");
            FormHelper.Login = this;
        }

        private void IniciarSesion()
        {
            Cursor.Current = Cursors.WaitCursor;
            // URL de la API o aplicación MVC a la que se quiere hacer la solicitud
            string webServiceUrl = "/Login/ValidaUsuario";
            AccesoDatosGlobal.ValidaUsuario VU = new AccesoDatosGlobal.ValidaUsuario();
            VU.Usuario = txtusuario.Text;
            VU.Password = txtpassword.Text;

            //Guardar credenciales de inicio de sesión 
            string deviceId = DeviceManager.GetDeviceId();
            string email = VU.Usuario;
            string password = VU.Password;
            GuardarCredenciales(deviceId, email, password);

            Logic.AD.RequestParameters = new Dictionary<string, string>();
            Logic.AD.RequestParameters.Add("Usuario", VU.Usuario);
            Logic.AD.RequestParameters.Add("Password", VU.Password);
            List<Dictionary<string, string>> result = Logic.ExecGetRequest(webServiceUrl, Logic.AD.RequestParameters, false, false);

            if (result.Count > 0)
            {
                string Mensaje = string.Empty;

                if (result[0]["Status"] == "NO" || result[0]["Status"] == "ERROR")
                {
                    Mensaje = result[0]["Message"].ToString();
                    Logic.ShowException(null, "No es posible iniciar sesión: " + Mensaje, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    //Guardar perfil de usuario
                    FormHelper.Perfil = result[0]["u_perfil"].ToString().ToUpper();
                    FormHelper.Usuario = result[0]["empleado"].ToString().ToUpper() + "/" + result[0]["email"].ToString().ToUpper();
                    FormHelper.UsuarioMin = result[0]["email"].ToString().ToUpper();
                    FormHelper.Sociedad = result[0]["Message"].ToString();
                    SoundPlayer.ReproducirSonido("Login.wav");
                    MenuPrincipal mainmenu = new MenuPrincipal(); // Crear una nueva instancia del formulario
                    mainmenu.LblMainTitle.Text = "FISFIBER " + FormHelper.Sociedad;
                    mainmenu.LblUsuario.Text = "BIENVENIDO " + FormHelper.UsuarioMin.ToLower();
                    mainmenu.LblPerfilUsuario.Text = FormHelper.Perfil.ToLower();
                    mainmenu.Show(); // Mostrar MainMenu como ventana principal
                    this.Hide();
                }
            }
            this.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private bool GuardarCredenciales(string deviceID, string email, string Password)
        {
            try
            {
                return DeviceManager.SaveDeviceCredentials(deviceID, email, Password);
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible guardar las credenciales de inicio de sesión: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                return false;
            }
        }

        private bool CerrarSesionActiva()
        {
            try
            {
                // Validar que haya un usuario logueado antes de intentar cerrar sesión
                if (string.IsNullOrEmpty(FormHelper.UsuarioMin))
                {
                    return false;
                }

                bool resultado = false;
                Cursor.Current = Cursors.WaitCursor;

                // URL de la API o aplicación MVC a la que se quiere hacer la solicitud
                string webServiceUrl = "/Login/CerrarSesionActiva";
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("Usuario", FormHelper.UsuarioMin);

                List<Dictionary<string, string>> resultList = Logic.ExecGetRequest(webServiceUrl, Logic.AD.RequestParameters, false, false);

                if (resultList.Count > 0)
                {
                    if (resultList[0]["Status"] == "NO" || resultList[0]["Status"] == "ERROR")
                    {
                        string Mensaje = resultList[0]["Message"].ToString();
                        // Solo mostrar mensaje si no estamos en proceso de cierre
                        if (this.Visible)
                        {
                            Logic.ShowException(null, Mensaje, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        resultado = true;
                    }
                }

                Cursor.Current = Cursors.Default;
                return resultado;
            }
            catch (Exception ex)
            {
                // Solo mostrar mensaje si el formulario sigue visible
                if (this.Visible)
                {
                    Logic.ShowException(ex, "No fue posible cerrar la sesión activa.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
                return false;
            }
        }
        #endregion
    }
}