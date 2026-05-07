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
using System.Xml;

namespace FFISA.Inventarios.SalidasDirectas
{
    public partial class Articulos : BaseForm
    {
        LogicaGlobal Logic = new LogicaGlobal(); //Logica para hacer peticiones HTTP, MOSTRAR EXCEPCIONES, ETC
        Dictionary<string, string> CurrentData = new Dictionary<string, string>();
        public string EstadoInicializacion { get; private set; }
        private Timer debounceTimer;
        //Paginacion
        private List<Dictionary<string, string>> ArticulosTotales = new List<Dictionary<string, string>>();
        private int PaginaActual = 0;
        private int ArticulosPorPagina = 50;
        public bool Headers = false;

        #region events
        private void Continuar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                GetDetailsArticulo(); //continuar en el menu siguiente
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible consultar los detalles del artículo: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        private void Regresar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                Cursor.Current = Cursors.WaitCursor;
                //Validar si no se guardo nada para eliminar el documento
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("Folio", CurrentData["FolioSD"]);
                Logic.AD.RequestParameters.Add("Usuario", FormHelper.Usuario);
                List<Dictionary<string, string>> Details = Logic.ExecGetRequest("/InventariosMovil/GetLinesDocumentosSalidasHHEMD", Logic.AD.RequestParameters, true, false);

                //Validar si hay registros antes de intentar enlistarlos
                if (Details[0]["Status"] == "NO" || Details[0]["Status"] == "ERROR")
                {
                    if (Details[0]["Status"] == "ERROR")
                    {
                        Logic.ShowException(null, "No fue posible continuar verifica tu conexión a los servidores e intenta de nuevo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }
                    else
                    {
                        string question = string.Empty;
                        question = Logic.ShowException(null, "El documento generado con folio: " + CurrentData["FolioSD"] + " no cuenta con ningún escaneo, si regresas al menú inicial el documento sera eliminado, ¿Deseas Continuar?", 250, "AVISO", "Aviso.wav", true);

                        if (question == "Yes")
                        {
                            string resultdel = EliminarDocumento(CurrentData["FolioSD"]);
                            if (resultdel == "OK")
                            {
                                MenuSalidasDirectas MenuSalidasDirectas = new MenuSalidasDirectas();
                                FormHelper.AbrirFormulario(this, MenuSalidasDirectas, false);
                            }
                            else
                            {
                                Logic.ShowException(null, "No fue posible continuar verifica tu conexión a los servidores e intenta de nuevo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                Cursor.Current = Cursors.Default;
                            }
                        }
                    }
                }
                else
                {
                    MenuSalidasDirectas MenuSalidasDirectas = new MenuSalidasDirectas();
                    FormHelper.AbrirFormulario(this, MenuSalidasDirectas, false);
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible continuar verifica tu conexión a los servidores: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }

        }
        private void MenuEntradas_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                Cursor.Current = Cursors.WaitCursor;
                //Validar si no se guardo nada para eliminar el documento
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("Folio", CurrentData["FolioSD"]);
                Logic.AD.RequestParameters.Add("Usuario", FormHelper.Usuario);
                List<Dictionary<string, string>> Details = Logic.ExecGetRequest("/InventariosMovil/GetLinesDocumentosSalidasHHEMD", Logic.AD.RequestParameters, true, false);

                //Validar si hay registros antes de intentar enlistarlos
                if (Details[0]["Status"] == "NO" || Details[0]["Status"] == "ERROR")
                {
                    if (Details[0]["Status"] == "ERROR")
                    {
                        Logic.ShowException(null, "No fue posible continuar verifica tu conexión a los servidores e intenta de nuevo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }
                    else
                    {
                        string question = string.Empty;
                        question = Logic.ShowException(null, "El documento generado con folio: " + CurrentData["FolioSD"] + " no cuenta con ningún escaneo, si regresas al menú inicial el documento sera eliminado, ¿Deseas Continuar?", 250, "AVISO", "Aviso.wav", true);

                        if (question == "Yes")
                        {
                            string resultdel = EliminarDocumento(CurrentData["FolioSD"]);
                            if (resultdel == "OK")
                            {
                                MenuSalidasDirectas MenuSalidasDirectas = new MenuSalidasDirectas();
                                FormHelper.AbrirFormulario(this, MenuSalidasDirectas, false);
                            }
                            else
                            {
                                Logic.ShowException(null, "No fue posible continuar verifica tu conexión a los servidores e intenta de nuevo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                Cursor.Current = Cursors.Default;
                            }
                        }
                    }
                }
                else
                {
                    MenuSalidasDirectas MenuSalidasDirectas = new MenuSalidasDirectas();
                    FormHelper.AbrirFormulario(this, MenuSalidasDirectas, false);
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible continuar verifica tu conexión a los servidores: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        private void DebounceTimer_Tick(object sender, EventArgs e) //Disparar la busqueda si el usuario deja de escribir
        {
            debounceTimer.Enabled = false;  // Detén el timer para que no se repita
            this.Enabled = false; //Desactivar el formulario
            PaginaActual = 1; //Pagina actual
            ArticulosPorPagina = 50; //Articulos por pagina
            BuscarArticulos();     // Realiza la búsqueda
        }
        private void Siguiente_Click(object sender, EventArgs e)
        {
            PaginaActual++;
            BuscarArticulos();
        }
        private void Anterior_Click(object sender, EventArgs e)
        {
            if (PaginaActual > 1)
            {
                PaginaActual--;
                BuscarArticulos();
            }
        }
        private void TxtArticulo_TextChanged(object sender, EventArgs e)
        {
            if (TxtArticulo.Text.Length > 1)
            {
                debounceTimer.Enabled = false;  // Reinicia el temporizador
                debounceTimer.Enabled = true;
            }
            else
            {
                ReiniciaBuscador();
            }
        }
        private void PtbTeclado_Click(object sender, EventArgs e)
        {
            FormHelper.ClickEvent(); // Para el sonido y cursor
            FormHelper.AlternarTeclado(); // Alterna entre mostrar/ocultar
            Cursor.Current = Cursors.Default;
        }
        #endregion

        #region methods
        public Articulos(Dictionary<string, string> CurrentData)
        {
            InitializeComponent();
            debounceTimer = new Timer();
            debounceTimer.Interval = 800; // 800 MILESIMAS
            debounceTimer.Enabled = false;
            debounceTimer.Tick += new EventHandler(DebounceTimer_Tick);
            this.CurrentData = CurrentData;
            this.TxtArticulo.Focus();
            EstadoInicializacion = "OK";
        }
        private void BuscarArticulos()
        {
            try
            {
                if (TxtArticulo.Text.Length > 1)
                {

                    Cursor.Current = Cursors.WaitCursor;
                    Logic.AD.RequestParameters = new Dictionary<string, string>();
                    Logic.AD.RequestParameters.Add("Busqueda", TxtArticulo.Text);
                    Logic.AD.RequestParameters.Add("PaginaActual", PaginaActual.ToString());
                    Logic.AD.RequestParameters.Add("ArticulosPorPagina", ArticulosPorPagina.ToString());
                    string result = ListadoArticulos(Logic.AD.RequestParameters);
                    if (result != "OK")
                        Logic.ShowException(null, result, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    Cursor.Current = Cursors.Default;
                    this.Enabled = true;
                    TxtArticulo.Focus();
                }
                else
                {
                    this.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    TxtArticulo.Focus();
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible consultar el listado de artículos: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                this.Enabled = true;
                Cursor.Current = Cursors.Default;
            }

        }
        private string ListadoArticulos(Dictionary<string, string> parameters)
        {
            string result = string.Empty;

            try
            {
                List<Dictionary<string, string>> OVList = Logic.ExecGetRequest("/InventariosMovil/GetArticulosHHEMD", parameters, false, false);

                // Validar si hay registros antes de intentar enlistarlos
                if (OVList[0]["Status"] == "NO" || OVList[0]["Status"] == "ERROR")
                {
                    result = OVList[0]["Message"];
                    lblTotalOV.Text = "0";
                    LvArticulos.Items.Clear();
                    TxtArticulo.Text = string.Empty;
                    Anterior.Enabled = false;
                    Siguiente.Enabled = false;
                    return result;
                }

                ArticulosTotales = OVList; // Solo la página actual
                lblTotalOV.Text = OVList[0]["TotalRegistros"]; // Mostramos cuántos artículos hay en esta página

                MostrarPagina(ArticulosTotales); // Mostrar artículos

                // Mostrar texto de página actual
                LblTotalPaginas.Text = "Página " + PaginaActual + " de " + OVList[0]["TotalPaginas"];

                // Lógica para botones (Anterior)
                if (PaginaActual > 1)
                {
                    Anterior.BackColor = Color.FromArgb(0, 128, 255);
                    Anterior.Enabled = true;
                }
                else
                {

                    Anterior.BackColor = Color.LightGray;
                    Anterior.Enabled = false;
                }
                // Lógica para botones (Siguiente)
                if (OVList.Count == ArticulosPorPagina)
                {
                    Siguiente.BackColor = Color.FromArgb(0, 128, 255);
                    Siguiente.Enabled = true;
                }
                else
                {
                    Siguiente.BackColor = Color.LightGray;
                    Siguiente.Enabled = false;
                }

                OVList.Clear();
                ArticulosTotales.Clear();
                this.Enabled = true;
                result = "OK";
                return result;
            }
            catch (Exception ex)
            {
                string CustomError = string.Empty;
                StringBuilder Error = new StringBuilder();
                Error.Append(ex.Message ?? string.Empty);
                Error.Append(Environment.NewLine);
                Error.Append(Environment.NewLine);
                Error.Append(ex.InnerException != null ? ex.InnerException.ToString() : string.Empty);
                CustomError = (Error.ToString().Contains("SocketException") || Error.ToString().Contains("timed-out")
            ? "No es posible establecer conexión con el servidor, intenta de nuevo más tarde."
            : Error.ToString());

            CustomError = (Error.ToString().Contains("OutOfMemoryException") ? "La memoria del dispositivo no es suficiente para realizar el proceso en este momento, intenta de nuevo más tarde.": CustomError);
                result = "No fue posible consultar el listado de artículos: " + CustomError;
                return result;
            }
        }
        private void MostrarPagina(List<Dictionary<string, string>> listaPagina)
        {
            LvArticulos.Items.Clear();
            string rownumber = "0";

            if (Headers == false)
            {
                LvArticulos.Columns.Add("Cuenta Contable", 0, HorizontalAlignment.Left);
                LvArticulos.Columns.Add("CodCC", 0, HorizontalAlignment.Left); //Codigo Cuenta Contable
                LvArticulos.Columns.Add("Artículo", -2, HorizontalAlignment.Left);
                LvArticulos.Columns.Add("Grupo de artículos", -2, HorizontalAlignment.Left);
                Headers = true;
            }

            foreach (var item in listaPagina)
            {
                ListViewItem listViewItem = new ListViewItem(item["CuentaContable"]);
                 listViewItem.SubItems.Add(item["CodigoCuentaContable"]);
                listViewItem.SubItems.Add(item["Codigo"]);
                listViewItem.SubItems.Add(item["GrupoDeArticulos"]);
               

                listViewItem.BackColor = rownumber == "0" ? Color.FromArgb(242, 242, 242) : Color.White;
                listViewItem.ForeColor = Color.Black;
                rownumber = rownumber == "0" ? "1" : "0";
                LvArticulos.Items.Add(listViewItem);
            }

            FormHelper.AjustarColumnas(LvArticulos);
        }
        private void GetDetailsArticulo() // AVANZAR A INFORMACION ARTICULO
        {
            if (LvArticulos.SelectedIndices.Count > 0) // Verifica que haya una fila seleccionada
            {
                int index = LvArticulos.SelectedIndices[0]; // Obtiene el índice de la fila seleccionada
                ListViewItem item = LvArticulos.Items[index]; // Obtiene el ítem (fila)
                CurrentData.Add("CuentaContable", item.SubItems[0].Text); //CuentaContable
                CurrentData.Add("CodigoCuentaContable", item.SubItems[1].Text); //CodigoCuentaContable
                CurrentData.Add("CodigoArticulo", item.SubItems[2].Text); //Codigo
                CurrentData.Add("GrupoDeArticulos", item.SubItems[3].Text); //GrupoDeArticulos
               

                try
                {
                    FormHelper.AbrirFormularioConValidacion(
                    this,
                    () => new DatosMaestrosArticulo(CurrentData),
                    (form) =>
                    {
                        var ir = form as DatosMaestrosArticulo;
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
                    Logic.ShowException(ex, "No fue posible mostrar el formulario de captura para los datos maestros de artículo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
            }
            else
            {
                Logic.ShowException(null, "Selecciona un elemento de la lista para continuar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                this.Enabled = true;
            }
        }
        private void ReiniciaBuscador()
        {

            this.Enabled = true;
            PaginaActual = 0;
            Siguiente.BackColor = Color.LightGray;
            Siguiente.Enabled = false;
            Anterior.BackColor = Color.LightGray;
            Anterior.Enabled = false;
            LvArticulos.Items.Clear();
            LblTotalPaginas.Text = "Página 0 de 0";
            lblTotalOV.Text = "0";
            Cursor.Current = Cursors.Default;
            TxtArticulo.Focus();
        }
        private string EliminarDocumento(string Folio)
        {
            string result = string.Empty;

            try
            {
                    Cursor.Current = Cursors.WaitCursor;

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
                        writer.WriteStartElement("DocumentosSalidasHHEMD");
                        writer.WriteElementString("Folio", Folio);
                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                    }

                    string Xml = sw.ToString();

                    List<Dictionary<string, string>> borrado = Logic.ExecPostRequest("/InventariosMovil/EliminarDocumentosSalidasHHEMD", Xml, false, string.Empty);
                    if (borrado.Count > 0)
                    {
                        string Message = borrado[0]["Message"].ToString();
                        if (Message.Contains("No fue posible"))
                        {
                            result = "ERROR";
                        }

                        else
                        {
                            result = "OK";
                        }
                    }


                return result;
            }
            catch (Exception ex)
            {
                StringBuilder Error = new StringBuilder();
                Error.Append(ex.Message != null ? ex.Message.ToString() : string.Empty);
                Error.Append(Environment.NewLine);
                Error.Append(Environment.NewLine);
                Error.Append(ex.InnerException != null ? ex.InnerException.ToString() : string.Empty);
                result = "No fue posible eliminar el documento: " + Error.ToString();
                return result;
            }
        }
        #endregion
    }
}