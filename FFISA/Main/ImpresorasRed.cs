using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FFISA.Model;
using System.IO;
using System.Xml;

namespace FFISA.Main
{
    public partial class ImpresorasRed : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal();
        public List<string> ImpresorasSeleccionadas { get; private set; }
        public string EstadoInicializacion { get; private set; }
        private bool FirstTime = false;

        #region events
        // Método manejador de eventos para Compact Framework 3.5
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectedRadio = sender as RadioButton;

            if (selectedRadio != null && selectedRadio.Checked)
            {
                foreach (Control ctrl in PnlImpresoras.Controls)
                {
                    if (ctrl is RadioButton && ctrl != selectedRadio)
                    {
                        ((RadioButton)ctrl).Checked = false;
                    }
                }
            }
        }
        //Seleccionar
        private void RadioButton_Click(object sender, EventArgs e)
        {
            if (FirstTime != false)
                SoundPlayer.ReproducirSonido("Click.wav");
            else
                FirstTime = true;
        }
        //Guardar
        private void Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                SoundPlayer.ReproducirSonido("Click.wav");
                // Limpiar lista antes de agregar
                Cursor.Current = Cursors.WaitCursor;
                this.Enabled = false;
                ImpresorasSeleccionadas = new List<string>();
                ImpresorasSeleccionadas.Clear();

                // Recorrer los checkboxes dentro de un Panel o GroupBox (ajusta el control según tu formulario)
                foreach (Control ctrl in PnlImpresoras.Controls)
                {
                    if (ctrl is RadioButton)
                    {
                        RadioButton chk = (RadioButton)ctrl;

                        if (chk.Checked)
                        {
                            ImpresorasSeleccionadas.Add(chk.Name); //Agregar las impresoras seleccioandas
                        }
                    }
                }
                if (ImpresorasSeleccionadas.Count == 0)
                {
                    Logic.ShowException(null, "Selecciona al menos una impresora.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    this.Enabled = true;
                }
                else
                {
                    //Guardar Configuracion
                    string impresoras = string.Join(",", ImpresorasSeleccionadas.ToArray());
                    InsertaConfiguracionImpresion(impresoras);
                    this.Enabled = true;
                    MenuPrincipal MenuPrincipal = new MenuPrincipal();
                    MenuPrincipal.LblUsuario.Text = "BIENVENIDO " + FormHelper.UsuarioMin.ToLower();
                    MenuPrincipal.LblPerfilUsuario.Text = FormHelper.Perfil.ToLower();
                    MenuPrincipal.LblMainTitle.Text = "FISFIBER " + FormHelper.Sociedad;
                    FormHelper.AbrirFormulario(this, MenuPrincipal, false);
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible guardar la configuración de impresión: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                this.Enabled = true;
            }
        }
        //Cancelar
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuPrincipal MenuPrincipal = new MenuPrincipal();
            MenuPrincipal.LblUsuario.Text = "BIENVENIDO " + FormHelper.UsuarioMin.ToLower();
            MenuPrincipal.LblPerfilUsuario.Text = FormHelper.Perfil.ToLower();
            MenuPrincipal.LblMainTitle.Text = "FISFIBER " + FormHelper.Sociedad;
            FormHelper.AbrirFormulario(this, MenuPrincipal, false);
        }
        #endregion

        #region methods
        public ImpresorasRed()
        {
            InitializeComponent();
            EstadoInicializacion = ListaImpresoras();
        } //Componente
        private string ListaImpresoras()
        {
            string result = string.Empty;

            try
            {
                // Variables para controlar la disposición de los RadioButton
                int topPosition = 10; // Controla la posición vertical

                // Limpiar controles previos
                PnlImpresoras.Controls.Clear();

                // Obtener series de numeración
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("Usuario", FormHelper.Usuario);
                List<Dictionary<string, string>> impresoras = Logic.ExecGetRequest("/ImpresionEtiquetas/GetImpresorasRed", Logic.AD.RequestParameters, false, false);
                //Validar si hay registros antes de intentar enlistarlos
                if (impresoras[0]["Status"] == "NO" || impresoras[0]["Status"] == "ERROR")
                {
                    result = impresoras[0]["Message"];
                    return result;
                }
                else
                {
                    foreach (var impresora in impresoras)
                    {
                        // Crear el RadioButton
                        RadioButton radioButton = new RadioButton();
                        radioButton.Width = PnlImpresoras.Width - 30; // Ajusta el ancho para evitar cortes

                        // Máximo número de caracteres visibles
                        int maxLength = 15;
                        string nombreImpresora = impresora["Impresora"];
                        radioButton.Text = nombreImpresora.Length > maxLength ? nombreImpresora.Substring(0, maxLength) + "..." : nombreImpresora;

                        radioButton.Name = impresora["Code"];
                        radioButton.Location = new Point(10, topPosition); // Ubicamos el RadioButton en la posición deseada

                        // Seleccionar automáticamente si el valor es "SI"
                        radioButton.Checked = (impresora["Selected"] == "SI");

                        // Agregar evento CheckedChanged (usando método en lugar de lambda)
                        radioButton.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);

                        // Agregar evento Click (usando método en lugar de lambda)
                        radioButton.Click += new EventHandler(RadioButton_Click);

                        // Incrementar la posición para el siguiente RadioButton
                        topPosition += radioButton.Height + 5;

                        // Agregar el RadioButton al Panel
                        PnlImpresoras.Controls.Add(radioButton);
                    }
                    result = "OK";
                    return result;
                }
            }
            catch (Exception ex)
            {
                StringBuilder Error = new StringBuilder();
                Error.Append(ex.Message != null ? ex.Message.ToString() : string.Empty);
                Error.Append(Environment.NewLine);
                Error.Append(Environment.NewLine);
                Error.Append(ex.InnerException != null ? ex.InnerException.ToString() : string.Empty);
                result = "No fue posible obtener el listado de impresoras en red: " + Error.ToString();
                return result;
            }
        }
        private void InsertaConfiguracionImpresion(string Selected)
        {
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            //Datos de OC checklist
            AccesoDatosGlobal.ConfiguracionImpresoras CI = new AccesoDatosGlobal.ConfiguracionImpresoras();
            CI.Selected = Selected;

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
                writer.WriteStartElement("ConfiguracionImpresoras");
                writer.WriteElementString("Selected", CI.Selected);
                writer.WriteElementString("Usuario", FormHelper.Usuario);
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            string Xml = sw.ToString();

            //Listado dinamico
            List<Dictionary<string, string>> items = Logic.ExecPostRequest("/ImpresionEtiquetas/InsertaConfiguracionImpresion", Xml, false, string.Empty);
            if (items.Count > 0)
            {
                string Mensaje = string.Empty;

                if (items[0]["Status"] == "NO" || items[0]["Status"] == "ERROR")
                {
                    Mensaje = items[0]["Message"].ToString();
                    Logic.ShowException(null, Mensaje, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    Mensaje = items[0]["Message"].ToString();
                    Logic.ShowException(null, Mensaje, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                }
            }

            Cursor.Current = Cursors.Default;

        }
        #endregion

    }
}