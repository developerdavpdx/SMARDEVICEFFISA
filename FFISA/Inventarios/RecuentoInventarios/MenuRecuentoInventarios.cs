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
using System.Xml;

namespace FFISA.Inventarios.RecuentoInventarios
{
    public partial class MenuRecuentoInventarios : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal();
        Dictionary<string, string> CurrentData = new Dictionary<string, string>();
        public string EstadoInicializacion { get; set; }

        #region events
        //NUEVO DOCUMENTO
        private void PbNuevoDocumento_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                ListadoDocumentosSap();
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible mostrar la vista de recuentos en SAP, intenta de nuevo más tarde: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuInventarios MenuInventarios = new MenuInventarios();
            FormHelper.AbrirFormulario(this, MenuInventarios, false);
        }
        private void PbEMPendientes_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();

                FormHelper.AbrirFormularioConValidacion(
                this,
                () => new RecuentosPendientes(),
                (form) =>
                {
                    var ir = form as RecuentosPendientes;
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
                Logic.ShowException(ex, "No fue posible mostrar el listado de documentos pendientes .", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        #endregion

        #region methods
        public MenuRecuentoInventarios()
        {
            InitializeComponent();
        }

        private void ListadoDocumentosSap()
        {
            try
            {
                FormHelper.AbrirFormularioConValidacion(
                this,
                () => new DocumentosSap(CurrentData),
                (form) =>
                {
                    var ir = form as DocumentosSap;
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
                Logic.ShowException(ex, "No fue posible mostrar el listado de recuentos en SAP.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        #endregion
    }
}