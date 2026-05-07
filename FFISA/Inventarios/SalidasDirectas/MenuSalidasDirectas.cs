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

namespace FFISA.Inventarios.SalidasDirectas
{
    public partial class MenuSalidasDirectas : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal();
        Dictionary<string, string> CurrentData = new Dictionary<string, string>();
        public string EstadoInicializacion { get; set; }

        #region events
        private void PbNuevoDocumento_Click(object sender, EventArgs e)
        {
            string Resultado = string.Empty;

            try
            {
                FormHelper.ClickEvent();
                //Generar registro automatico con folio consecutivo para entregas
                Resultado = InsertaEncabezadoSalidasDirectas();

                if (Resultado == "OK" || Resultado == "SI")
                {
                    ListadoArticulos(); //Avanzar a la siguiente pantalla
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                if (Resultado == "OK" || Resultado == "SI")
                    Logic.ShowException(ex, "Se guardo el folio de documento, pero no fue posible mostrar la vista de artículos, intenta de nuevo más tarde: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                else
                Logic.ShowException(ex, "No fue posible mostrar la vista de artículos, intenta de nuevo más tarde: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

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
                () => new SalidasPendientes(),
                (form) =>
                {
                    var ir = form as SalidasPendientes;
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
        public MenuSalidasDirectas()
        {
            InitializeComponent();
        }
        private void ListadoArticulos()
        {
            try
            {
                FormHelper.AbrirFormularioConValidacion(
                this,
                () => new Articulos(CurrentData),
                (form) =>
                {
                    var ir = form as Articulos;
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
                Logic.ShowException(ex, "No fue posible mostrar el listado de artículos.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        private string InsertaEncabezadoSalidasDirectas()
        {
            string resultado = string.Empty;
            AccesoDatosInventarios.EncabezadoSalidasHHEMD EED = new AccesoDatosInventarios.EncabezadoSalidasHHEMD();
            EED.Usuario = FormHelper.Usuario;

            StringWriter sw = new StringWriter();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = false, // Evita saltos de línea
                OmitXmlDeclaration = false, // Incluye la declaración <?xml version="1.0" encoding="utf-8"?>
                Encoding = System.Text.Encoding.UTF8
            };

            using (XmlWriter writer = XmlWriter.Create(sw, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("EncabezadoSalidasHHEMD");
                writer.WriteElementString("Usuario", EED.Usuario);
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            string Xml = sw.ToString();

            //Listado dinamico
            List<Dictionary<string, string>> items = Logic.ExecPostRequest("/InventariosMovil/InsertaEncabezadoSalidasHHEMD", Xml, false, string.Empty);

            if (items[0].ContainsKey("Folio"))
            {
                //Guardar el folio obtenido de la cabecera de la entrega de mercancia
                CurrentData = new Dictionary<string, string>();
                string Folio = items[0]["Folio"].ToString();
                string Estatus = items[0]["Status"].ToString();
                if (CurrentData.ContainsKey("FolioSD"))
                    CurrentData.Remove("FolioSD");
                CurrentData.Add("FolioSD", Folio);
                resultado = Estatus;
            }
            else
            {
                resultado = "ERROR";
                string Mensaje = items[0]["Message"].ToString();
                Logic.ShowException(null, Mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            return resultado;
        }
        #endregion
    }
}