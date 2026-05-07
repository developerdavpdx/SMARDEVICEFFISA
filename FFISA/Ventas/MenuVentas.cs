using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FFISA.Model;
using FFISA.Main;

namespace FFISA.Ventas
{
    public partial class MenuVentas : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal();

        #region events
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuPrincipal MenuPrincipal = new MenuPrincipal();
            MenuPrincipal.LblUsuario.Text = "BIENVENIDO " + FormHelper.UsuarioMin.ToLower();
            MenuPrincipal.LblPerfilUsuario.Text = FormHelper.Perfil.ToLower();
            MenuPrincipal.LblMainTitle.Text = "FISFIBER " + FormHelper.Sociedad;
            FormHelper.AbrirFormulario(this, MenuPrincipal, false);
        }

        private void EntregaMercancia_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();

            if (FormHelper.Perfil != "")
            {
                MenuEntregas MenuEntregas = new MenuEntregas();
                FormHelper.AbrirFormulario(this, MenuEntregas, false);
            }
            else
            {
                Logic.ShowException(null, "No tienes permiso para ingresar a este módulo. ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        #endregion

        #region methods
        public MenuVentas()
        {
            InitializeComponent();
        }
        #endregion
    }
}