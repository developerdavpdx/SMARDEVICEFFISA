using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using FFISA.Main;
using System.Reflection;
using System.Drawing;
using System.Xml;

namespace FFISA.Model
{
    class LogicaGlobal
    {

        public AccesoDatosGlobal AD = new AccesoDatosGlobal();
        // Obtener la ruta del ejecutable en Compact Framework
        public string directorioBase { get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase); } }

        #region HttpWebRequest
        public void GetSeriesNumeracion()
        {

            try
            {
                // URL de la API o aplicación MVC a la que se quiere hacer la solicitud
                string webServiceUrl = AD.RESTAPI + "/Home/SeriesNumeracion";
                AccesoDatosGlobal.SeriesNumeracion SU = new AccesoDatosGlobal.SeriesNumeracion();
                SU.ObjectCode = "17";
                // Crear el XML de la solicitud
                string xmlData = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                 "<Credenciales>" +
                                 "<ObjectCode>" + SU.ObjectCode + "</ObjectCode>" +
                                 "</Credenciales>";

                // Crear la solicitud HTTP
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webServiceUrl);
                request.Method = "GET";
                request.ContentType = "application/xml";
                request.ContentLength = xmlData.Length;
                request.Timeout = 600000; // 2 minutos en milisegundos



                // Escribir el XML en el cuerpo de la solicitud
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(xmlData);
                    writer.Flush();
                }

                // Obtener la respuesta del servidor
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    //Obtener respuesta de API REST
                    string responseText = reader.ReadToEnd();
                    Dictionary<string, object> Response = AD.ParseXml(responseText);
                    if (Response["status"].ToString() == "NO" || Response["status"].ToString() == "ERROR")
                    {
                        ShowException(null, Response["Message"].ToString(), "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        //Listado dinamico
                        List<Dictionary<string, string>> items = (List<Dictionary<string, string>>)Response["items"];

                        // Recorrer los items
                        foreach (var item in items)
                        {
                            string existe = item["existe"];
                            string email = item["email"];
                            string empleado = item["empleado"];
                            string perfil = item["u_perfil"];
                        }

                    }
                }

            }
            catch (WebException ex)
            {
                ShowException(ex, "No es posible obtener las series de numeración.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }

        }
        public List<Dictionary<string, string>> ExecGetRequest(string url, Dictionary<string, string> parameters, bool showwarning, bool PlaySound)
        {
            List<Dictionary<string, string>> items = new List<Dictionary<string, string>>();
            string webServiceUrl = AD.RESTAPI + url;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webServiceUrl);
                request.Method = "GET";
                request.ContentType = "application/xml";
                request.Timeout = 600000;

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (var parameter in parameters)
                    {
                        request.Headers[parameter.Key] = parameter.Value;
                    }
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    StringBuilder sb = new StringBuilder();
                    char[] buffer = new char[1024];
                    int bytesRead;

                    while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        sb.Append(buffer, 0, bytesRead);
                    }

                    string responseText = sb.ToString();

                    Dictionary<string, object> Response = AD.ParseXml(responseText);
                    items = (List<Dictionary<string, string>>)Response["items"];

                    if (items.Count > 0)
                    {
                        items[0].Add("Status", Response["status"].ToString());
                        items[0].Add("Message", Response["Message"].ToString());

                        if (Response.ContainsKey("TotalRegistros"))
                            if (Response["TotalRegistros"].ToString() != "N/A")
                            items[0].Add("TotalRegistros", Response["TotalRegistros"].ToString());

                        if (Response.ContainsKey("TotalPaginas"))
                            if (Response["TotalPaginas"].ToString() != "N/A")
                            items[0].Add("TotalPaginas", Response["TotalPaginas"].ToString());
                    }
                    else
                    {
                        items = new List<Dictionary<string, string>>();
                        Dictionary<string, string> Messages = new Dictionary<string, string>();
                        Messages.Add("Status", Response["status"].ToString());
                        Messages.Add("Message", Response["Message"].ToString());
                        items.Add(Messages);
                    }
                }
            }
            catch (Exception ex)
            {
                items = new List<Dictionary<string, string>>();
                Dictionary<string, string> error = new Dictionary<string, string>();
                error.Add("Status", "ERROR");
                string CustomError = string.Empty;
                StringBuilder errorMsg = new StringBuilder();
                errorMsg.Append(ex.Message != null ? ex.Message : string.Empty);
                errorMsg.Append(ex.InnerException != null ? " | " + ex.InnerException.ToString() : string.Empty);

                CustomError = (errorMsg.ToString().Contains("SocketException") || errorMsg.ToString().Contains("timed-out")
                               ? "No es posible establecer conexión con el servidor, intenta de nuevo más tarde."
                               : errorMsg.ToString());

                CustomError = (errorMsg.ToString().Contains("OutOfMemoryException") ? "La memoria del dispositivo no es suficiente para realizar el proceso en este momento, intenta de nuevo más tarde."
                : CustomError);

                error.Add("Message", "No fue posible consultar el listado de datos: " + CustomError);
                items.Add(error);
            }

            return items;
        }
        public List<Dictionary<string, string>> ExecPostRequest(string url, string XML, bool PlaySound, string CustomMessage)
        {

            List<Dictionary<string, string>> items = new List<Dictionary<string, string>>();
            string webServiceUrl = AD.RESTAPI + url;

            // Convertir XML a bytes en UTF-8
            byte[] xmlBytes = Encoding.UTF8.GetBytes(XML);

            // Crear la solicitud HTTP
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webServiceUrl);
            request.Timeout = 600000;
            request.Method = "POST";
            request.ContentType = "application/xml";
            request.ContentLength = xmlBytes.Length; // Usamos la longitud correcta en bytes

            // Escribir el XML en el cuerpo de la solicitud
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(xmlBytes, 0, xmlBytes.Length);
                requestStream.Flush();
            }

            // Obtener la respuesta del servidor
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string responseText = reader.ReadToEnd();

                Dictionary<string, object> Response = AD.ParseXml(responseText);
                if (Response["status"].ToString() == "NO" || Response["status"].ToString() == "ERROR")
                {
                    if (PlaySound)
                        SoundPlayer.ReproducirSonido("Error.wav");

                    Dictionary<string, string> message = new Dictionary<string, string>();
                    message.Add("Message", CustomMessage + Response["Message"].ToString());
                    message.Add("Status", Response["status"].ToString());
                    items = new List<Dictionary<string, string>>();
                    items.Add(message);

                }
                else
                {
                    // Listado dinámico
                    items = (List<Dictionary<string, string>>)Response["items"];
                    if (items.Count > 0)
                    {
                        if (!items[0].ContainsKey("Status"))
                            items[0].Add("Status", Response["status"].ToString());
                        if (!items[0].ContainsKey("Message"))
                            items[0].Add("Message", Response["Message"].ToString());
                    }
                    else
                    {
                        if (Response["status"].ToString().Contains("OK") || Response["status"].ToString().Contains("SI"))
                        {
                            items = new List<Dictionary<string, string>>();
                            Dictionary<string, string> Messages = new Dictionary<string, string>();
                            Messages.Add("Status", Response["status"].ToString());
                            Messages.Add("Message", Response["Message"].ToString());
                            items.Add(Messages);
                        }
                    }
                }
            }
            return items;

        }
        #endregion

        #region global
        //metodo 1 con message box
        public void ShowException(Exception ex, string CustomMessage, string TypeMessage, MessageBoxButtons Buttons, MessageBoxIcon Icon, MessageBoxDefaultButton DefaultButton)
        {
            StringBuilder Error = new StringBuilder();
            string CustomError = string.Empty;
            //Solo si hay excepcion
            if (ex != null)
            {
                Error.Append(ex.Message != null ? ex.Message.ToString() : string.Empty);
                Error.Append(Environment.NewLine);
                Error.Append(Environment.NewLine);
                Error.Append(ex.InnerException != null ? ex.InnerException.ToString() : string.Empty);
            }

            CustomError = (Error.ToString().Contains("SocketException") || Error.ToString().Contains("timed-out")
            ? "No es posible establecer conexión con el servidor, intenta de nuevo más tarde."
            : Error.ToString());

            CustomError = (Error.ToString().Contains("OutOfMemoryException") ? "La memoria del dispositivo no es suficiente para realizar el proceso en este momento, intenta de nuevo más tarde."
            : CustomError);

            Cursor.Current = Cursors.Default;
            if ((CustomMessage + " " + CustomError).Length > 280)
                MessageBox.Show((Environment.NewLine + (CustomMessage + " " + CustomError).Substring(0, 280) + "...."), TypeMessage, Buttons, Icon, DefaultButton);//V1
            else
                MessageBox.Show((Environment.NewLine + CustomMessage + " " + CustomError), TypeMessage, Buttons, Icon, DefaultButton);//V2
        }
        //metodo 2 con message box y dialogo
        public string ShowException(Exception ex, string CustomMessage, int LenghtMessage, string TypeMessage, string Sound, bool ShowDialog)
        {
            DialogResult result = new DialogResult();
            StringBuilder Error = new StringBuilder();
            //Solo si hay excepcion
            if (ex != null)
            {
                Error.Append(ex.Message != null ? ex.Message.ToString() : string.Empty);
                Error.Append(ex.InnerException != null ? ex.InnerException.ToString().Substring(0, LenghtMessage) + "...." : string.Empty);
            }
            SoundPlayer.ReproducirSonido(Sound);
            Cursor.Current = Cursors.Default;
            if (ShowDialog)
                result = MessageBox.Show((Environment.NewLine + CustomMessage + " " + Error.ToString()), TypeMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);//V1
            else
                MessageBox.Show((Environment.NewLine + CustomMessage + " " + Error.ToString()), TypeMessage);//V2

            return result.ToString();
        }
        //metodo 3 con formulario completo
        public void ShowException(Exception ex, string CustomMessage, int LenghtMessage, string TypeMessage, string Sound, Form formulario, bool CloseForm)
        {
            StringBuilder Error = new StringBuilder();
            // Solo si hay excepción
            if (ex != null)
            {
                Error.Append(ex.Message != null ? ex.Message.ToString() : string.Empty);
                Error.Append(ex.InnerException != null ? ex.InnerException.ToString().Substring(0, LenghtMessage) + "...." : string.Empty);
            }
            SoundPlayer.ReproducirSonido(Sound);
            Cursor.Current = Cursors.Default;

            // Crear una instancia de CustomMessageBox
            CustomMessageBox customMessageBox = new CustomMessageBox(CustomMessage + " " + Error.ToString(), TypeMessage);
            customMessageBox.BtnAceptar.Click += (s, e) =>
            {
                formulario.Enabled = true;

                //Recorrer los controles y habilitarlos o vaciarlos
                foreach (Control control in formulario.Controls)
                {
                    if (control.Name.Contains("ENB"))
                    {
                        control.Enabled = true;
                    }

                    if (control.Name.Contains("CLN"))
                    {
                        control.Text = string.Empty;
                    }
                }

                if (CloseForm)
                    formulario.Close();
                // Al hacer clic en el botón aceptar, ocultamos el panel y habilitamos el formulario
                customMessageBox.Close();
            };

            customMessageBox.Show();

        }
        //metodo 4 con opcion de dialogo
        public void ShowException(Exception ex, string CustomMessage, int LenghtMessage, string TypeMessage, string Sound, Form formulario, bool CloseForm, bool ShowDialog)
        {
            StringBuilder Error = new StringBuilder();
            // Solo si hay excepción
            if (ex != null)
            {
                Error.Append(ex.Message != null ? ex.Message.ToString() : string.Empty);
                Error.Append(ex.InnerException != null ? ex.InnerException.ToString().Substring(0, LenghtMessage) + "...." : string.Empty);
            }
            SoundPlayer.ReproducirSonido(Sound);
            Cursor.Current = Cursors.Default;

            // Crear una instancia de CustomMessageBox
            CustomMessageBox customMessageBox = new CustomMessageBox(CustomMessage + " " + Error.ToString(), TypeMessage);
            customMessageBox.BtnAceptar.Click += (s, e) =>
            {
                formulario.Enabled = true;

                //Recorrer los controles y habilitarlos o vaciarlos
                foreach (Control control in formulario.Controls)
                {
                    if (control.Name.Contains("ENB"))
                    {
                        control.Enabled = true;
                    }

                    if (control.Name.Contains("CLN"))
                    {
                        control.Text = string.Empty;
                    }
                }

                if (CloseForm)
                    formulario.Close();
                // Al hacer clic en el botón aceptar, ocultamos el panel y habilitamos el formulario
                if (ShowDialog)
                    customMessageBox.DialogResult = DialogResult.OK;

                customMessageBox.Close();
            };
            if (ShowDialog)
                customMessageBox.ShowDialog();
            else
                customMessageBox.Show();
        }
        //metodo 5 con opcion de dialogo y icono extra
        public void ShowException(Exception ex, string CustomMessage, int LenghtMessage, string TypeMessage, string Sound, Form formulario, bool CloseForm, bool ShowDialog, bool ShowAditionalIcon, string AditionalIcon)
        {
            StringBuilder Error = new StringBuilder();
            // Solo si hay excepción
            if (ex != null)
            {
                Error.Append(ex.Message != null ? ex.Message.ToString() : string.Empty);
                Error.Append(ex.InnerException != null ? ex.InnerException.ToString().Substring(0, LenghtMessage) + "...." : string.Empty);
            }
            SoundPlayer.ReproducirSonido(Sound);
            Cursor.Current = Cursors.Default;

            // Crear una instancia de CustomMessageBox
            CustomMessageBox customMessageBox = new CustomMessageBox(CustomMessage + " " + Error.ToString(), TypeMessage);
            customMessageBox.BtnAceptar.Click += (s, e) =>
            {
                formulario.Enabled = true;

                //Recorrer los controles y habilitarlos o vaciarlos
                foreach (Control control in formulario.Controls)
                {
                    if (control.Name.Contains("ENB"))
                    {
                        control.Enabled = true;
                    }

                    if (control.Name.Contains("CLN"))
                    {
                        control.Text = string.Empty;
                    }
                }

                if (CloseForm)
                    formulario.Close();
                // Al hacer clic en el botón aceptar, ocultamos el panel y habilitamos el formulario
                if (ShowDialog)
                    customMessageBox.DialogResult = DialogResult.OK;

                customMessageBox.Close();
            };
            if (ShowAditionalIcon)
            {
                //Cambiar la imagen del picturebox
                string rutaImagen = Path.Combine(directorioBase, "Imagenes\\" + AditionalIcon);
                if (File.Exists(rutaImagen))
                {
                    customMessageBox.PtbMessageIcon2.Image = new Bitmap(rutaImagen);
                }
                customMessageBox.PtbMessageIcon2.Visible = true;
            }

            if (ShowDialog)
                customMessageBox.ShowDialog();
            else
                customMessageBox.Show();
        }
        
        public void LimpiaFormulario(Panel PnlControls)
        {
            foreach (Control ctrl in PnlControls.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox txt = (TextBox)ctrl;
                    //Solo a los txt que contienen en su nombre VAL deben ser validados
                    if (txt.Name.Contains("CLN"))
                    {
                        txt.Text = string.Empty;
                    }
                }

                else if (ctrl is CheckBox)
                {
                    CheckBox chk = (CheckBox)ctrl;
                    //Solo a los txt que contienen en su nombre VAL deben ser validados
                    if (chk.Name.Contains("CLN"))
                    {
                        chk.Checked = false;
                    }
                }
            }
        }
        public string ConvertToXml(object obj, string rootElementName)
        {
            StringWriter sw = new StringWriter();

            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = false,
                OmitXmlDeclaration = false,
                Encoding = Encoding.UTF8
            };

            using (XmlWriter writer = XmlWriter.Create(sw, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(rootElementName);

                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    object value = prop.GetValue(obj, null) ?? "";
                    writer.WriteElementString(prop.Name, value.ToString());
                }

                writer.WriteEndElement(); // Cierra root
                writer.WriteEndDocument();
            }

            return sw.ToString();
        }
        #endregion
    }
}
