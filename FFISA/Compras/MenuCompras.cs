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
using FFISA.Main;

namespace FFISA.Compras
{
    public partial class MenuCompras : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal();

        #region events
        //NUEVO DOCUMENTO
        private void PbNuevoDocumento_Click(object sender, EventArgs e)
        {
            SoundPlayer.ReproducirSonido("Click.wav");
            Cursor.Current = Cursors.WaitCursor;
            NuevoDocumento();
        }
        private void PbEMPendientes_Click(object sender, EventArgs e)
        {
            SoundPlayer.ReproducirSonido("Click.wav");
            Cursor.Current = Cursors.WaitCursor;
            DocumentosPendientes();
        }
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuPrincipal MenuPrincipal = new MenuPrincipal();
            MenuPrincipal.LblUsuario.Text = "BIENVENIDO " + FormHelper.UsuarioMin.ToLower();
            MenuPrincipal.LblPerfilUsuario.Text = FormHelper.Perfil.ToLower();
            MenuPrincipal.LblMainTitle.Text = "FISFIBER " + FormHelper.Sociedad;
            FormHelper.AbrirFormulario(this, MenuPrincipal,false);
        }
        #endregion

        #region methods
        public MenuCompras()
        {
            InitializeComponent();
        }
        private void NuevoDocumento()
        {
            try
            {
                FormHelper.AbrirFormularioConValidacion(
                this,
                () => new SeriesNumeracionOC(),
                (form) =>
                {
                    var ir = form as SeriesNumeracionOC;
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
        private void DocumentosPendientes()
        {
            try
            {
                FormHelper.AbrirFormularioConValidacion(
                this,
                () => new ListadoEMPendientes(),
                (form) =>
                {
                    var ir = form as ListadoEMPendientes;
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
                Logic.ShowException(ex, "No fue posible obtener el listado de documentos pendientes.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        #endregion
    }
}