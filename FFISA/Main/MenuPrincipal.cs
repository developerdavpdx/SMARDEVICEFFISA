using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FFISA.Main;
using FFISA.Model;
using System.IO;
using System.Net.Sockets;
using FFISA.Ventas;
using FFISA.Compras;
using FFISA.Inventarios.EntradasDirectas;
using FFISA.Inventarios;

namespace FFISA.Main
{
    public partial class MenuPrincipal : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal();

        #region events
        private void Compras_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            if (FormHelper.Perfil == "COMPRAS" || FormHelper.Perfil == "ADMIN")
            {
                if (FormHelper.isproductiveCompras)
                {
                    MenuCompras compras = new MenuCompras();
                    FormHelper.AbrirFormulario(this, compras, false);
                }
                else
                {
                    Logic.ShowException(null, "El módulo actualmente no se encuentra disponible en productivo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
            }
            else
            {
                Logic.ShowException(null, "No tienes permiso para ingresar a este módulo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            //CerrarSesionActiva();
            FormHelper.AbrirFormulario(this, FormHelper.Login, false);
        }
        private void Impresion_Click(object sender, EventArgs e)
        {
            if (FormHelper.Perfil == "ADMIN")
            {
                FormHelper.ClickEvent();
                ConfiguracionImpresion();
            }
            else
            {
                Logic.ShowException(null, "No tienes permiso para ingresar a este módulo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        } //Configuracion de impresoras
        private void Ventas_Click(object sender, EventArgs e)
        {
            if (FormHelper.Perfil == "VENTAS" || FormHelper.Perfil == "ADMIN")
            {
                FormHelper.ClickEvent();
                MenuVentas ventas = new MenuVentas();
                FormHelper.AbrirFormulario(this, ventas, false);
            }
            else
            {
                Logic.ShowException(null, "No tienes permiso para ingresar a este módulo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }

        } //Modulo Ventas
        private void Inventarios_Click(object sender, EventArgs e)
        {
            if (FormHelper.Perfil == "ALMACÉN" || FormHelper.Perfil == "ADMIN")
            {
                FormHelper.ClickEvent();
                MenuInventarios Inventarios = new MenuInventarios();
                FormHelper.AbrirFormulario(this, Inventarios, false);
            }
            else
            {
                Logic.ShowException(null, "No tienes permiso para ingresar a este módulo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        } //Modulo Inventarios
        #endregion

        #region methods
        public MenuPrincipal()
        {
            InitializeComponent();
        }
        private void ConfiguracionImpresion()
        {

            try
            {
                FormHelper.AbrirFormularioConValidacion(
                this,
                () => new ImpresorasRed(),
                (form) =>
                {
                    var ir = form as ImpresorasRed;
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
                Logic.ShowException(ex, "No fue posible obtener el listado de impresoras.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        private bool CerrarSesionActiva()
        {
            bool resultLogout = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                // URL de la API o aplicación MVC a la que se quiere hacer la solicitud
                string webServiceUrl = "/Login/CerrarSesionActiva";
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("Usuario", FormHelper.UsuarioMin);
                List<Dictionary<string, string>> result = Logic.ExecGetRequest(webServiceUrl, Logic.AD.RequestParameters, false, false);

                if (result.Count > 0)
                {
                    string Mensaje = string.Empty;

                    if (result[0]["Status"] == "NO" || result[0]["Status"] == "ERROR")
                    {
                        Mensaje = result[0]["Message"].ToString();
                        Logic.ShowException(null, Mensaje, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        resultLogout = true;
                    }
                }

                Cursor.Current = Cursors.Default;
                return resultLogout;
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible cerrar la sesión activa.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                return resultLogout;
            }
        }
        #endregion
    }
}