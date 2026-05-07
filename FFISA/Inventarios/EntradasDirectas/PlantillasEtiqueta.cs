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

namespace FFISA.Inventarios.EntradasDirectas
{
    public partial class PlantillasEtiqueta : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal();
        public string PlantillaSeleccionada { get; private set; }
        public string EstadoInicializacion { get; private set; }
        private bool FirstTime = false;

        #region events
        // Método manejador de eventos para Compact Framework 3.5
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectedRadio = sender as RadioButton;

            if (selectedRadio != null && selectedRadio.Checked)
            {
                foreach (Control ctrl in PnlPlantillas.Controls)
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
                FormHelper.ClickEvent();
                PlantillaSeleccionada = string.Empty;


                // Recorrer los checkboxes dentro de un Panel o GroupBox (ajusta el control según tu formulario)
                foreach (Control ctrl in PnlPlantillas.Controls)
                {
                    if (ctrl is RadioButton)
                    {
                        RadioButton chk = (RadioButton)ctrl;

                        if (chk.Checked)
                        {
                            PlantillaSeleccionada = chk.Name; //Agregar las impresoras seleccioandas
                        }
                    }
                }
                if (PlantillaSeleccionada == string.Empty)
                {
                    Logic.ShowException(null, "Selecciona la plantilla a utilizar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    //Guardar Configuracion
                    InsertaConfiguracionPlantillas();
                    MenuInventarios MenuInventarios = new MenuInventarios();
                    FormHelper.AbrirFormulario(this, MenuInventarios, false);
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible guardar la configuración de impresión: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        //Cancelar
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuInventarios MenuInventarios = new MenuInventarios();
            FormHelper.AbrirFormulario(this, MenuInventarios, false);
        }
        #endregion

        #region methods
        public PlantillasEtiqueta()
        {
            InitializeComponent();
            EstadoInicializacion = ListaPlantillas();
        } //Componente
        private string ListaPlantillas()
        {
            string result = string.Empty;
            string PlantillaSeleccionada = string.Empty;

            try
            {
                // Variables para controlar la disposición de los RadioButton
                int topPosition = 0; // Controla la posición vertical

                // Limpiar controles previos
                PnlPlantillas.Controls.Clear();

                // Obtener series de numeración
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("Usuario", FormHelper.Usuario);
                List<Dictionary<string, string>> plantillas = Logic.ExecGetRequest("/InventariosMovil/GetPlantillas", Logic.AD.RequestParameters, false, false);
                //Validar si hay registros antes de intentar enlistarlos
                if (plantillas[0]["Status"] == "NO" || plantillas[0]["Status"] == "ERROR")
                {
                    result = plantillas[0]["Message"];
                    return result;
                }
                else
                {
                    foreach (var plantilla in plantillas)
                    {
                        // Crear el RadioButton
                        RadioButton radioButton = new RadioButton();
                        radioButton.Width = PnlPlantillas.Width - 30; // Ajusta el ancho para evitar cortes

                        // Máximo número de caracteres visibles
                        int maxLength = 20;
                        string nombrePlantilla = plantilla["plantilla"];
                        radioButton.Text = nombrePlantilla.Length > maxLength ? nombrePlantilla.Substring(0, maxLength) + "..." : nombrePlantilla;

                        radioButton.Name = plantilla["plantilla"];
                        radioButton.Location = new Point(10, topPosition); // Ubicamos el RadioButton en la posición deseada

                        // Seleccionar automáticamente si el valor es "SI"
                        radioButton.Checked = (plantilla["Selected"] == "SI" ? true : false);

                        // Agregar evento CheckedChanged (usando método en lugar de lambda)
                        radioButton.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);

                        // Agregar evento Click (usando método en lugar de lambda)
                        radioButton.Click += new EventHandler(RadioButton_Click);

                        // Incrementar la posición para el siguiente RadioButton
                        topPosition += radioButton.Height + 5;

                        // Agregar el RadioButton al Panel
                        PnlPlantillas.Controls.Add(radioButton);
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
                result = "No fue posible obtener el listado de plantillas: " + (Error.ToString().Contains("SocketException") ? "No es posible establecer conexión con el servidor, intenta de nuevo más tarde." : Error.ToString());
                return result;
            }
        }
        private void InsertaConfiguracionPlantillas()
        {
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            //Datos de OC checklist
            AccesoDatosInventarios.PlantillasEtiquetaEMD PED = new AccesoDatosInventarios.PlantillasEtiquetaEMD();
            PED.Plantilla = PlantillaSeleccionada;
            PED.Usuario = FormHelper.Usuario;

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
                writer.WriteStartElement("PlantillasEtiquetaEMD");
                writer.WriteElementString("Plantilla", PED.Plantilla);
                writer.WriteElementString("Usuario", PED.Usuario);
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            string Xml = sw.ToString();

            //Listado dinamico
            List<Dictionary<string, string>> plantilla = Logic.ExecPostRequest("/InventariosMovil/UpdatePlantillaEtiquetaHHEMD", Xml, false, string.Empty);

            string Mensaje = string.Empty;

            if (plantilla[0]["Status"] == "NO" || plantilla[0]["Status"] == "ERROR")
            {
                Mensaje = plantilla[0]["Message"].ToString();
                Logic.ShowException(null, Mensaje, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
            else
            {
                Mensaje = plantilla[0]["Message"].ToString();
                Logic.ShowException(null, Mensaje, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            }
        }
        #endregion
    }
}