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
using FFISA.Inventarios.EntradasDirectas;
using FFISA.Inventarios.SalidasDirectas;
using FFISA.Inventarios.TransferenciaStock;
using FFISA.Inventarios.RecuentoInventarios;

namespace FFISA.Inventarios
{
    public partial class MenuInventarios : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal();

        #region events
        private void EntradaDirecta_Click(object sender, EventArgs e)
        {
            if (FormHelper.isproductiveEntradaDirecta)
            {
                FormHelper.ClickEvent();
                MenuEntradasDirectas MenuEntradasDirectas = new MenuEntradasDirectas();
                FormHelper.AbrirFormulario(this, MenuEntradasDirectas, false);
            }
            else 
            {
                Logic.ShowException(null, "El módulo actualmente no se encuentra disponible en productivo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        private void SalidaDirecta_Click(object sender, EventArgs e)
        {
            if (FormHelper.isproductiveSalidaDirecta)
            {
                FormHelper.ClickEvent();
                MenuSalidasDirectas MenuSalidasDirectas = new MenuSalidasDirectas();
                FormHelper.AbrirFormulario(this, MenuSalidasDirectas, false);
            }
            else
            {
                Logic.ShowException(null, "El módulo actualmente no se encuentra disponible en productivo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuPrincipal MenuPrincipal = new MenuPrincipal();
            MenuPrincipal.LblUsuario.Text = "BIENVENIDO " + FormHelper.UsuarioMin.ToLower();
            MenuPrincipal.LblPerfilUsuario.Text = FormHelper.Perfil.ToLower();
            MenuPrincipal.LblMainTitle.Text = "FISFIBER " + FormHelper.Sociedad;
            FormHelper.AbrirFormulario(this, MenuPrincipal, false);
        }
        private void PlantillaImpresion_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            ConfiguracionPlantillas();
        }
        private void TraspasoMercancia_Click(object sender, EventArgs e)
        {
            if (FormHelper.isproductiveTraspasoMercancia)
            {
                FormHelper.ClickEvent();
                MenuTransferencias MenuTransferencias = new MenuTransferencias();
                FormHelper.AbrirFormulario(this, MenuTransferencias, false);
            }
            else
            {
                Logic.ShowException(null, "El módulo actualmente no se encuentra disponible en productivo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        private void RecuentoInventarios_Click(object sender, EventArgs e)
        {
            if (FormHelper.isproductiveRecuentoInventarios)
            {
                FormHelper.ClickEvent();
                MenuRecuentoInventarios MenuRecuentoInventarios = new MenuRecuentoInventarios();
                FormHelper.AbrirFormulario(this, MenuRecuentoInventarios, false);
            }
            else
            {
                Logic.ShowException(null, "El módulo actualmente no se encuentra disponible en productivo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        #endregion

        #region methods
        public MenuInventarios()
        {
            InitializeComponent();
        }
        private void ConfiguracionPlantillas()
        {
            try
            {
                FormHelper.AbrirFormularioConValidacion(
                this,
                () => new PlantillasEtiqueta(),
                (form) =>
                {
                    var ir = form as PlantillasEtiqueta;
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
                Logic.ShowException(ex, "No fue posible obtener el listado de plantillas.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        #endregion
    }
}