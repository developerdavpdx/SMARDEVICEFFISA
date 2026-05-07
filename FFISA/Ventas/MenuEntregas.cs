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

namespace FFISA.Ventas
{
    public partial class MenuEntregas : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal();

        #region events
        private void PbNuevoDocumento_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            OrdenesVenta();
        }
        private void PbEMPendientes_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            EntregasPendientes();
        }
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuVentas MenuVentas = new MenuVentas();
            FormHelper.AbrirFormulario(this, MenuVentas, false);
        }
        #endregion

        #region methods
        public MenuEntregas()
        {
            InitializeComponent();
        }
        private void OrdenesVenta()
        {
            try
            {
                FormHelper.AbrirFormularioConValidacion(
                this,
                () => new OrdenesVenta(),
                (form) =>
                {
                    var ir = form as OrdenesVenta;
                    if (ir.EstadoInicializacion == "OK")
                    {
                        return true;
                    }
                    else
                    {
                        Logic.ShowException(null, ir.EstadoInicializacion, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
                        return false;
                    }
                }, false);
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible obtener el listado de ordenes de venta.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        private void EntregasPendientes()
        {
            try
            {
                FormHelper.AbrirFormularioConValidacion(
                this,
                () => new EntregasPendientes(),
                (form) =>
                {
                    var ir = form as EntregasPendientes;
                    if (ir.EstadoInicializacion == "OK")
                    {
                        ir.Regresar.Click += new EventHandler(ir.Regresar_Click); //Asignar evento click
                        ir.RequiresPreeliminar = true;
                        ir.FromDatosMaestros = false;
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
                Logic.ShowException(ex, "No fue posible mostrar la vista de entregas pendientes.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        #endregion
    }
}