using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using FFISA.Main;
using System.Net.Sockets;
using System.Net;

namespace FFISA.Model
{
    class AccesoDatosGlobal
    {
        public string INTERNALSERVER { get { return "172.16.2.42"; } } //SERVIDOR FFISA
        public int INTERNALPORT { get { return 81; } } //PUERTO API PRUEBAS
        public string RESTAPIFFISA { get { return "http://172.16.2.42:83"; } } //REST API PRODUCTIVO
        public string RESTAPIPDX { get { return "http://172.16.2.42:81"; } } // REST API PRUEBAS
        public string RESTAPIPDXLH { get { return "http://192.168.0.22:52585"; } } //REST API LOCALHOST
        public string RESTAPI { get { return RESTAPIPDXLH; } } //URL BASE API
        public Dictionary<string, string> RequestParameters { get; set; }
        public class ValidaUsuario
        {
            public string Usuario { get; set; }
            public string Password { get; set; }
        }
        public class OcCheckList
        {
            public string DocEntry { get; set; }
            public string U_CertificadoCalidad { get; set; }
            public string U_OrdenFisica { get; set; }
            public string U_PackingList { get; set; }
            public string U_NumPed { get; set; }
        }
        public class EntradaMercanciaByOC
        {
            public string OrdenCompra { get; set; }
            public string SapDocument { get; set; }
            public string Articulo { get; set; }
            public string Linea { get; set; }
            public string Lote { get; set; }
            public string Cantidad { get; set; }
            public string Folio { get; set; }
            public string Mensaje { get; set; }
        }
        public class SeriesNumeracion
        {
            public string ObjectCode { get; set; }
        }
        public class OrdenCompra
        {
            public string DocEntry { get; set; }
        }
        public class Response
        {
            public string Status { get; set; }
            public string Message { get; set; }
            public string Data { get; set; } // Este campo contiene un JSON, lo deserializaremos por separado
        }
        public class UserData
        {
            public int existe { get; set; }
            public string email { get; set; }
            public string empleado { get; set; }
            public string u_perfil { get; set; }
        }
        public class ConfiguracionImpresoras
        {
            public string Selected { get; set; }
        }
        public Dictionary<string, object> ParseXml(string xmlData)
        {
            try
            {
                Dictionary<string, object> response = new Dictionary<string, object>();

                // Crear el objeto XmlDocument y cargar el XML
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlData);

                // Crear el XmlNamespaceManager para manejar los namespaces
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                nsmgr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");

                // Obtener el nodo <JsonResponse> (sin prefix, ya que no tiene un namespace)
                XmlNode root = doc.SelectSingleNode("JsonResponse", nsmgr);

                if(root == null)
                // Obtener el nodo <JsonResponse> (sin prefix, ya que no tiene un namespace)
                 root = doc.SelectSingleNode("JsonResponse2", nsmgr);


                // Obtener los valores de <Status> y <Message> (sin namespace necesario)
                string status = root.SelectSingleNode("Status").InnerText;
                string message = root.SelectSingleNode("Message").InnerText;
                string TotalRegistros = root.SelectSingleNode("TotalRegistros") != null ? root.SelectSingleNode("TotalRegistros").InnerText : "N/A";
                string TotalPaginas = root.SelectSingleNode("TotalPaginas") != null ? root.SelectSingleNode("TotalPaginas").InnerText : "N/A";

                // Crear una lista para los items
                List<Dictionary<string, string>> itemsList = new List<Dictionary<string, string>>();

                // Obtener los nodos de <Item> dentro de <Data> (especificando el namespace)
                XmlNode DataList = root.SelectSingleNode("Data"); ;


             
                // Crear el objeto XmlDocument
                XmlDocument docItem = new XmlDocument();
                XmlDocument docList = new XmlDocument();

                // Cargar el XML en el XmlDocument
                docItem.LoadXml(DataList.InnerText);

                // Obtener el nodo de <List> dentro de <Data> (sin especificar el namespace)
                XmlNode itemList = docItem.SelectSingleNode("List");

                foreach (XmlNode itemlistdata in itemList)
                {
                    // Crear un diccionario para cada item
                    Dictionary<string, string> itemData = new Dictionary<string, string>();
                    // Obtener los valores dentro de cada <Item> (sin namespace necesario)
                    foreach (XmlNode child in itemlistdata.ChildNodes)
                    {
                        itemData[child.Name] = child.InnerText;
                    }
                    itemsList.Add(itemData);
                }

                // Agregar los valores al diccionario de respuesta
                response.Add("status", status);
                response.Add("Message", message);
                response.Add("TotalRegistros", TotalRegistros);
                response.Add("TotalPaginas", TotalPaginas);
                response.Add("items", itemsList); // Ahora "items" es una lista de diccionarios
                return response;
            }
            catch (Exception ex)
            {
                StringBuilder Error = new StringBuilder();
                Error.Append(ex.Message != null ? ex.Message.ToString() : string.Empty);
                Error.Append(ex.InnerException != null ? ex.InnerException.ToString().Substring(0, 230) + "...." : string.Empty);
                SoundPlayer.ReproducirSonido("Error.wav");
                MessageBox.Show("Error al procesar el XML" + Error.ToString(), "ERROR");
                return null;
            }
        }

        public bool PuedeAlcanzarIP()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RESTAPI);
                request.Method = "HEAD";  // Solo verifica si el servidor responde sin descargar contenido
                request.Timeout = 5000;   // Timeout en milisegundos (5 segundos)

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return (response.StatusCode == HttpStatusCode.OK);
                }
            }
            catch
            {
                return false; // No hay conexión o el servidor no responde
            }
        }
    }
}
