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
using System.Reflection;
using System.Text.RegularExpressions;


namespace FFISA.Compras
{
    public partial class CheckListOC : BaseForm
    {

        LogicaGlobal Logic = new LogicaGlobal();
        Dictionary<string, string> CurrentData = new Dictionary<string, string>();
        private string GoToNextStep { get; set; }
        private string EstatusSolicitud { get; set; }
        public string EstadoInicializacion { get; private set; }

        #region events
        //Al dar click en cerrar
        private void Regresar_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            //Conservar solo lo necesario
            var NeedData = new[] { "Series"};

            CurrentData = CurrentData
                .Where(kvp => NeedData.Contains(kvp.Key))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            ListadoOC ListadoOC = new ListadoOC(CurrentData);
            FormHelper.AbrirFormulario(this, ListadoOC,false);
        }
        //Al dar click en continuar
        private void Continuar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();

                //VALIDACION DE CHECKLIST
                if (EstatusSolicitud == "Revisado SI") //Si fue autorizado que lo deje avanzar
                    GoToNextStep = "SI";

                ValidaOCChecklist(GoToNextStep);
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No es posible continuar no hay conexión con el servidor, intenta de nuevo más tarde.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        //Al dar click para solicitar autorizacion
        private void Autorizacion_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                this.Enabled = false;
                int howmanychecks = 0;
                int howmanytext = 0;
                int howmanyparametersok = 0;

                Logic.AD.RequestParameters = new Dictionary<string, string>();

                foreach (Control ctrl in pnlCheckListOC.Controls)
                {
                    if (ctrl is CheckBox)
                    {
                        CheckBox chk = (CheckBox)ctrl;

                        Logic.AD.RequestParameters.Add(chk.Name.Replace("U_", ""), (chk.Checked ? "SI" : "NO"));

                        howmanyparametersok = (chk.Checked == true ? (howmanyparametersok + 1) : howmanyparametersok);

                        howmanychecks++;
                    }

                    if (ctrl is TextBox)
                    {
                        TextBox txt = (TextBox)ctrl;

                        Logic.AD.RequestParameters.Add(txt.Name.Replace("U_", ""), txt.Text);

                        howmanyparametersok = (txt.Text != string.Empty ? (howmanyparametersok + 1) : howmanyparametersok);

                        howmanytext++;
                    }
                }
                if (howmanyparametersok == (howmanychecks + howmanytext))
                {
                    Logic.ShowException(null, "Si cuentas con toda la documentación no es necesario solicitar autorización.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    AutorizacionOC AutorizacionOC = new AutorizacionOC(CurrentData, Logic.AD.RequestParameters);
                    FormHelper.AbrirFormulario(this, AutorizacionOC,false);
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible solicitar autorización para la OC: " + CurrentData["DocNum"] + " ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
                this.Close();
            }
        }
        //Seleccionar todos los checkbox
        private void SeleccionarTodos_Click(object sender, EventArgs e)
        {
            SoundPlayer.ReproducirSonido("Click.wav");

            if (SeleccionarTodos.Checked)
            {

                foreach (Control ctrl in pnlCheckListOC.Controls)
                {
                    if (ctrl is CheckBox)
                    {
                        CheckBox chk = (CheckBox)ctrl;

                        chk.Checked = true;
                    }
                }
            }
            else
            {
                if (!SeleccionarTodos.Checked)
                {
                    foreach (Control ctrl in pnlCheckListOC.Controls)
                    {
                        if (ctrl is CheckBox)
                        {
                            CheckBox chk = (CheckBox)ctrl;

                            chk.Checked = false;
                        }
                    }
                }
            }
        }
        //Seleccionar
        private void Checkbox_Click(object sender, EventArgs e)
        {
            SoundPlayer.ReproducirSonido("Click.wav");
        }
        //Regresar al menu compras
        private void MenuCompras_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent();
            MenuCompras MenuCompras = new MenuCompras();
            FormHelper.AbrirFormulario(this, MenuCompras,false);
        }

        #endregion

        #region methods
        public CheckListOC(Dictionary<string, string> CurrentData)
        {
            try
            {
                InitializeComponent();
                this.CurrentData = CurrentData;
                EstadoInicializacion = OCChekList(pnlCheckListOC, CurrentData["DocEntry"]);
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible consultar el detalle de OC: " + CurrentData["DocNum"] + " ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2);
            }
        }
        private string OCChekList(Panel panel, string DocEntry)
        {
            string result = string.Empty;
            string rutaImagen = string.Empty;
            panel.Controls.Clear();//Limpiar el contenedor antes de agregar mas controles
            try
            {
                // Variable para controlar la posición vertical de los controles
                int topPosition = 10;
                string hasautorizacion = string.Empty;
                lblOC.Text = "OC: " + CurrentData["DocNum"] + "";
                int incomplete = 0;

                //Validar si solicitaron autorizacion
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("OrdenCompra", CurrentData["DocEntry"].ToString());
                Logic.AD.RequestParameters.Add("SapDocument", CurrentData["DocNum"].ToString());
                List<Dictionary<string, string>> auth = Logic.ExecGetRequest("/Compras/RevisarAutorizacionHHOC", Logic.AD.RequestParameters, false, false);

                if (auth[0]["Status"] == "NO" || auth[0]["Status"] == "ERROR")
                {
                    result = auth[0]["Message"].ToString();
                    return result;
                }
                else
                {
                    hasautorizacion = auth[0]["HasAutorizacion"].ToString().Trim();
                    //Guardar el estatus de la solicitud
                    this.EstatusSolicitud = hasautorizacion;


                    //realizar validacion de OC
                    Logic.AD.RequestParameters = new Dictionary<string, string>();
                    Logic.AD.RequestParameters.Add("DocEntry", DocEntry);
                    List<Dictionary<string, string>> OCCheck = Logic.ExecGetRequest("/Compras/GetDetailsOrdenesCompra", Logic.AD.RequestParameters, false, false);

                    foreach (var checklist in OCCheck[0])
                    {
                        if (checklist.Key != "Status" && checklist.Key != "Message")
                        {
                            incomplete += (checklist.Value == "NO" ? 1 : 0);

                            if (checklist.Key == "U_NumPed")
                            {
                                // Crear un Label para el TextBox
                                string OSversion = DeviceManager.osVersion + " " + DeviceManager.GetPlatformType() + " " + DeviceManager.GetOemInfo();
                                Label label = new Label();
                                label.Font = SeleccionarTodos.Font;
                                label.ForeColor = SeleccionarTodos.ForeColor;
                                label.Size = new Size((OSversion.Contains("5.2.23096") || (OSversion.Contains("5.2.23090")) ? 198 : (SeleccionarTodos.Width - 15)),
                                    (OSversion.Contains("5.2.23096") || (OSversion.Contains("5.2.23090")) ? 14 : (SeleccionarTodos.Height)));
                                label.Text = (checklist.Key.Replace("U_", "").Replace("NumPed", "Número de pedimento")); // Título del campo
                                label.Enabled = (checklist.Value != string.Empty ? false : true);
                                label.Location = new Point(10, topPosition);


                                // Ajustar habilitación según autorización
                                if (hasautorizacion == "En autorizacion" || hasautorizacion.Contains("Revisado") || hasautorizacion.Contains("SI"))
                                    label.Enabled = false;

                                // Agregar el Label al Panel dependiendo de la versión
                                panel.Controls.Add(label);

                               
                                int extraheight = (OSversion.Contains("5.2.23096") || (OSversion.Contains("5.2.23090"))  ? 3 : 35);
                                topPosition += label.Height + extraheight; // Espacio entre el label y el TextBox
                                
                                // Crear un TextBox
                                TextBox textBox = new TextBox();
                                textBox.Font = SeleccionarTodos.Font;
                                textBox.ForeColor = SeleccionarTodos.ForeColor;
                                textBox.Size = new Size((OSversion.Contains("5.2.23096") || (OSversion.Contains("5.2.23090")) ? 198 : (SeleccionarTodos.Width - 30)), 8);
                                textBox.Location = new Point(10, topPosition);
                                textBox.Name = checklist.Key;
                                textBox.Text = checklist.Value;
                                textBox.BorderStyle = BorderStyle.FixedSingle;
                                textBox.Enabled = (checklist.Value != string.Empty ? false : true);

                                // Ajustar habilitación según autorización
                                if (hasautorizacion == "En autorizacion" || hasautorizacion.Contains("Revisado") || hasautorizacion.Contains("SI"))
                                {
                                    textBox.Enabled = false;
                                    textBox.BackColor = Color.LightGray;
                                }

                                // Agregar el TextBox al Panel
                                panel.Controls.Add(textBox);
                            }
                            else
                            {
                                // Crear un CheckBox
                                CheckBox checkBox = new CheckBox();
                                checkBox.Anchor = SeleccionarTodos.Anchor;
                                checkBox.AutoCheck = SeleccionarTodos.AutoCheck;
                                checkBox.BackColor = SeleccionarTodos.BackColor;
                                checkBox.CheckState = SeleccionarTodos.CheckState;
                                checkBox.ContextMenu = SeleccionarTodos.ContextMenu;
                                checkBox.Font = SeleccionarTodos.Font;
                                checkBox.ForeColor = SeleccionarTodos.ForeColor;
                                checkBox.Size = SeleccionarTodos.Size;
                                string Name = checklist.Key.Replace("U_", "");
                                Name = Regex.Replace(Name, "(?<!^)([A-Z])", " $1");
                                checkBox.Text = Name; // Mantenemos el texto aquí solo en el CheckBox
                                checkBox.Checked = (checklist.Value == "SI");
                                checkBox.Enabled = (checklist.Value != "SI");
                                checkBox.Location = new Point(10, topPosition);
                                checkBox.Name = checklist.Key;

                                // Agregar evento Click (usando método en lugar de lambda)
                                checkBox.Click += new EventHandler(Checkbox_Click);

                                // Ajustar habilitación según autorización
                                if (hasautorizacion == "En autorizacion" || hasautorizacion.Contains("Revisado") || hasautorizacion.Contains("SI"))
                                    checkBox.Enabled = false;

                                // Agregar el CheckBox al Panel
                                panel.Controls.Add(checkBox);
                            }

                            // Incrementar la posición para el siguiente control
                            topPosition += SeleccionarTodos.Height + 5;
                        }
                    }


                    if (incomplete == 0)
                    {
                        lblestatusOC.Text = "La orden ya cuenta con todos los documentos.";
                        lblestatusOC.Visible = true;
                        ptbstatus.Visible = true;
                        Autorizacion.Enabled = false;
                        Autorizacion.ForeColor = Color.White;
                        Continuar.Enabled = true;
                        //Cambiar el color del picturebox
                        Autorizacion.BackColor = Color.Gray;
                        SeleccionarTodos.Enabled = false;
                        GoToNextStep = "SI"; //AVANZAR DIRECTAMENTE A EL PASO SIGUIENTE
                    }
                    else
                    {
                        //Habilitar o deshabilitar dependiendo si se encuentra en estado de autorizacion.
                        if (hasautorizacion == "En autorizacion")
                        {

                            Autorizacion.Enabled = false;
                            //Cambiar la imagen del picturebox
                            Autorizacion.BackColor = Color.Gray;
                            Autorizacion.ForeColor = Color.White;

                            Continuar.Enabled = false;
                            //Cambiar la imagen del picturebox
                            rutaImagen = Path.Combine(Logic.directorioBase, "Imagenes\\ContinuarD.jpg");
                            if (File.Exists(rutaImagen))
                            {
                                Continuar.Image = new Bitmap(rutaImagen);
                            }


                            SeleccionarTodos.Enabled = false;
                        }

                        //Habilitar o deshabilitar dependiendo si se encuentra en estado de revision.
                        else if (hasautorizacion.Contains("Revisado SI"))
                        {
                            Autorizacion.Enabled = false;
                            //Cambiar la imagen del picturebox
                            Autorizacion.BackColor = Color.Gray;
                            Autorizacion.ForeColor = Color.White;

                            Continuar.Enabled = true;
                            //Cambiar la imagen del picturebox
                            rutaImagen = Path.Combine(Logic.directorioBase, "Imagenes\\Continuar.jpg");
                            if (File.Exists(rutaImagen))
                            {
                                Continuar.Image = new Bitmap(rutaImagen);
                            }

                            SeleccionarTodos.Enabled = false;
                        }
                        else if (hasautorizacion == string.Empty)
                        {

                            Autorizacion.Enabled = true;
                            //Cambiar la imagen del picturebox
                            Autorizacion.BackColor = Color.FromArgb(0, 128, 255); // Un tono azul personalizado
                            Autorizacion.ForeColor = Color.White;
                            //Si no se ha solicitado ninguna autorizacion, tambien habilitar el boton
                            Continuar.Enabled = true;
                            //Cambiar la imagen del picturebox
                            rutaImagen = Path.Combine(Logic.directorioBase, "Imagenes\\Continuar.jpg");
                            if (File.Exists(rutaImagen))
                            {
                                Continuar.Image = new Bitmap(rutaImagen);
                            }

                            SeleccionarTodos.Enabled = true;
                        }

                        else
                        {
                            Autorizacion.Enabled = true;
                            Continuar.Enabled = true;
                            SeleccionarTodos.Enabled = true;
                        }


                        //Mostrar mensaje de estatus
                        if (hasautorizacion == "En autorizacion")
                        {
                            lblestatusOC.Text = "La orden se encuentra en estatus de autorización, para continuar la solicitud debe ser aprobada.";
                            lblestatusOC.ForeColor = Color.Red;
                            //Cambiar la imagen del picturebox
                            rutaImagen = Path.Combine(Logic.directorioBase, "Imagenes\\warning.jpg");
                            if (File.Exists(rutaImagen))
                            {
                                ptbstatus.Image = new Bitmap(rutaImagen);
                            }

                            lblestatusOC.Visible = true;
                            ptbstatus.Visible = true;
                            SoundPlayer.ReproducirSonido("Aviso.wav");
                        }

                        //Mostrar mensaje de estatus
                        else if (hasautorizacion == "Revisado SI")
                        {
                            lblestatusOC.Text = "La orden fue autorizada, puedes continuar con la solicitud.";
                            lblestatusOC.ForeColor = Color.LimeGreen;
                            //Cambiar la imagen del picturebox
                            rutaImagen = Path.Combine(Logic.directorioBase, "Imagenes\\ok2.jpg");
                            if (File.Exists(rutaImagen))
                            {
                                ptbstatus.Image = new Bitmap(rutaImagen);
                            }

                            lblestatusOC.Visible = true;
                            ptbstatus.Visible = true;
                        }

                        //Mostrar mensaje de estatus
                        else if (hasautorizacion == "Revisado NO")
                        {
                            lblestatusOC.Text = "La orden no fue autorizada, no puedes continuar con la solicitud.";
                            lblestatusOC.ForeColor = Color.Red;
                            //Cambiar la imagen del picturebox
                            rutaImagen = Path.Combine(Logic.directorioBase, "Imagenes\\close2.jpg");
                            if (File.Exists(rutaImagen))
                            {
                                ptbstatus.Image = new Bitmap(rutaImagen);
                            }

                            lblestatusOC.Visible = true;
                            ptbstatus.Visible = true;
                        }

                        GoToNextStep = "NO"; //AVANZAR DIRECTAMENTE A EL PASO SIGUIENTE
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
                result = "No fue posible consultar la documentación de la orden de compra: " + CurrentData["DocNum"] + " " + Error.ToString();
                return result;
            }
        }
        private void ValidaOCChecklist(string GoToNextStep)
        {
            Logic.AD.RequestParameters = new Dictionary<string, string>();

            switch (GoToNextStep)
            {
                case "SI":
                    GoArticleScan();
                    break;

                case "NO":
                    ActualizaDocumentosOC();
                    break;
            }
        }
        private void GoArticleScan()
        {
            try
            {
                FormHelper.AbrirFormularioConValidacion(
                this,
                () => new SeleccionArticuloOC(CurrentData),
                (form) =>
                {
                    var ir = form as SeleccionArticuloOC;
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
                Logic.ShowException(ex, "No fue posible obtener el listado de artículos de la orden de compra.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }

            //SeleccionArticuloOC COC = new SeleccionArticuloOC(DatosEMSelected);

            //if (COC.EstadoInicializacion == "OK")
            //{
            //    COC.Closed += (s, args) =>
            //    {
            //        BeginingForms.FormulariosAbiertosCompras.Remove(COC); // Remueve de la lista al cerrar

            //        if (BeginingForms.CerrandoMultiplesFormCompras == false)
            //        {
            //            Cursor.Current = Cursors.WaitCursor;
            //            this.Enabled = false;
            //            string result = OCChekList(pnlCheckListOC, DatosEMSelected["DocEntry"]);
            //            if (result.Contains("Error"))
            //                Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

            //            this.Enabled = true;
            //            Cursor.Current = Cursors.Default;
            //            this.Show();
            //        }
            //    };

            //    BeginingForms.FormulariosAbiertosCompras.Add(COC); // Agregar a la lista de abiertos

            //    COC.Show();
            //    this.Hide();
            //    this.Enabled = true;
            //}
            //else
            //{
            //    Logic.ShowException(null, COC.EstadoInicializacion, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            //}
            //Cursor.Current = Cursors.Default;
        }
        private void ActualizaDocumentosOC()
        {
            bool updated = false;//Resultado Final
            bool incomplete = false;

            foreach (Control ctrl in pnlCheckListOC.Controls)
            {
                if (ctrl is CheckBox)
                {
                    CheckBox chk = (CheckBox)ctrl;
                    Logic.AD.RequestParameters.Add(chk.Name, (chk.Checked == true ? "SI" : "NO"));
                    //Si alguno no esta completo
                    if (!chk.Checked)
                        incomplete = true;
                }

                if (ctrl is TextBox)
                {
                    TextBox txt = (TextBox)ctrl;
                    Logic.AD.RequestParameters.Add(txt.Name, txt.Text);
                    //Si alguno no esta completo
                    if (txt.Text == string.Empty)
                        incomplete = true;
                }

            }
            //Si no tiene todos los checks debe mandar solicitar autorizacion
            if (incomplete)
            {
                updated = false;
                Logic.ShowException(null, "Debes marcar todos los documentos ademas de colocar el número de pedimento, en caso de no contar con toda la documentación, debes solicitar autorización.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
            else
            {
                //Datos de OC checklist
                AccesoDatosGlobal.OcCheckList OC = new AccesoDatosGlobal.OcCheckList();
                OC.DocEntry = CurrentData["DocEntry"];
                OC.U_CertificadoCalidad = Logic.AD.RequestParameters["U_CertificadoCalidad"];
                OC.U_OrdenFisica = Logic.AD.RequestParameters["U_OrdenFisica"];
                OC.U_PackingList = Logic.AD.RequestParameters["U_PackingList"];
                OC.U_NumPed = Logic.AD.RequestParameters["U_NumPed"];

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
                    writer.WriteStartElement("OcCheckList");

                    writer.WriteElementString("DocEntry", OC.DocEntry.ToString());
                    writer.WriteElementString("CertificadoCalidad", OC.U_CertificadoCalidad);
                    writer.WriteElementString("OrdenFisica", OC.U_OrdenFisica);
                    writer.WriteElementString("PackingList", OC.U_PackingList);
                    writer.WriteElementString("Pedimento", OC.U_NumPed);

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }

                string Xml = sw.ToString();
                List<Dictionary<string, string>> result = Logic.ExecPostRequest("/Compras/UpdateDetailsOrdenesCompra", Xml, false, string.Empty);
                //Si no ocurrio alguna excepcion
                if (result != null)
                {

                    string Message = string.Empty;

                    if (result.Count > 0)
                    {

                        if (result[0]["Status"] == "NO" || result[0]["Status"] == "ERROR")
                        {
                            Message = result[0]["Message"].ToString();
                            Logic.ShowException(null, Message, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                            updated = false;
                        }
                        else
                        {
                            updated = true;
                        }
                    }
                }
                else
                {
                    updated = false;
                }

                if (updated) //Avanzar al escaneo de artículos
                    GoArticleScan();
            }

        }
        #endregion
    }
}