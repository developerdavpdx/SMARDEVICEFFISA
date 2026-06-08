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
using FFISA.Main;
using System.Threading;
using System.Reflection;

namespace FFISA.Inventarios.TransferenciaStock
{
    public partial class DatosMaestrosTraspaso : BaseForm
    {
        //Para generar entrada de mercancia
        LogicaGlobal Logic = new LogicaGlobal();
        Dictionary<string, string> CurrentData = new Dictionary<string, string>();
        Dictionary<string, string> DatosLote = new Dictionary<string, string>();
        Dictionary<string, string> DatosEnvio = new Dictionary<string, string>();
        public string EstadoInicializacion { get; set; }
        private bool LoteOrigenOK = false;
        private bool AlmacenDestinoValidado = true;

        #region events
        private void Regresar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                //Validar si no se guardo nada para eliminar el documento
                Logic.AD.RequestParameters = new Dictionary<string, string>();
                Logic.AD.RequestParameters.Add("Folio", CurrentData["FolioTS"]);
                Logic.AD.RequestParameters.Add("Usuario", FormHelper.Usuario);
                List<Dictionary<string, string>> Details = Logic.ExecGetRequest("/InventariosMovil/GetLinesDocumentosTransferenciasHHTS", Logic.AD.RequestParameters, true, false);

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
                        question = Logic.ShowException(null, "El documento generado con folio: " + CurrentData["FolioTS"] + " no cuenta con ningún escaneo, si regresas al menú inicial el documento sera eliminado, ¿Deseas Continuar?", 250, "AVISO", "Aviso.wav", true);

                        if (question == "Yes")
                        {
                            string resultdel = EliminarDocumento(CurrentData["FolioTS"]);
                            if (resultdel == "OK")
                            {
                                MenuTransferencias MenuTransferencias = new MenuTransferencias();
                                FormHelper.AbrirFormulario(this, MenuTransferencias, false);
                            }
                            else {
                                Logic.ShowException(null, "No fue posible continuar verifica tu conexión a los servidores e intenta de nuevo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                Cursor.Current = Cursors.Default;
                            }

                        }
                    }
                }
                else
                {
                    MenuTransferencias MenuTransferencias = new MenuTransferencias();
                    FormHelper.AbrirFormulario(this, MenuTransferencias, false);
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible continuar verifica tu conexión a los servidores: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
        }
        private void TxtLoteOrigen_KeyPress(object sender, KeyPressEventArgs e)
        {
            LoteOrigenOK = false;

            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (TxtLoteOrigenVALCLN.Text == string.Empty)
                    {
                        Logic.ShowException(null, "Es necesario escanear el numero de lote.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    }
                    else
                    {
                        bool result = ValidarLoteTraspasosHHTM(TxtLoteOrigenVALCLN.Text);

                        if (result)
                        {
                            if (DatosLote["Codigo"] != "OK")
                            {
                                LoteOrigenOK = false;
                                Logic.ShowException(null, DatosLote["Mensaje"], "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                TxtCantidadVALCLN.Text = string.Empty;
                                TxtAlmacenOrigenVALCLN.Text = string.Empty;
                                TxtLoteOrigenVALCLN.Text = string.Empty;
                                TxtArticuloVALCLN.Text = string.Empty;
                            }
                            else
                            {
                                if (DatosEnvio.ContainsKey("LoteOrigen"))
                                    DatosEnvio["LoteOrigen"] = DatosLote["Lote"];
                                else
                                    DatosEnvio.Add("LoteOrigen", DatosLote["Lote"]);

                                if (DatosEnvio.ContainsKey("Cantidad"))
                                    DatosEnvio["Cantidad"] = DatosLote["Cantidad"];
                                else
                                    DatosEnvio.Add("Cantidad", DatosLote["Cantidad"]);

                                if (DatosEnvio.ContainsKey("AlmacenOrigen"))
                                    DatosEnvio["AlmacenOrigen"] = DatosLote["CodigoAlmacen"];
                                else
                                    DatosEnvio.Add("AlmacenOrigen", DatosLote["CodigoAlmacen"]);

                                if (DatosEnvio.ContainsKey("Articulo"))
                                    DatosEnvio["Articulo"] = DatosLote["Articulo"];
                                else
                                    DatosEnvio.Add("Articulo", DatosLote["Articulo"]);

                                TxtAlmacenOrigenVALCLN.Text = DatosLote["CodigoAlmacen"] + " / " + DatosLote["NombreAlmacen"];
                                TxtCantidadVALCLN.Text = DatosLote["Cantidad"];
                                TxtArticuloVALCLN.Text = DatosLote["Articulo"];
                                LoteOrigenOK = true;
                            }
                        }
                    }
                    Cursor.Current = Cursors.Default;
                }
                catch (Exception ex)
                {
                    Logic.ShowException(ex, "No fue posible realizar validaciones para el lote: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    Cursor.Current = Cursors.Default;
                    this.Enabled = true;
                }
            }
            else
            {
                TxtCantidadVALCLN.Text = string.Empty;
                TxtAlmacenOrigenVALCLN.Text = string.Empty;
                TxtArticuloVALCLN.Text = string.Empty;
            }
        }
        private void ValidarAlmacen_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                ValidaAlmacenHHEMD();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible validar el almacén, intenta de nuevo más tarde: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        private void Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ClickEvent();
                if (LoteOrigenOK && AlmacenDestinoValidado)
                {
                    bool validacion = validaCapturaArticulo(this.Body);
                    if (validacion)
                    {
                        //Agregar comentarios
                        if (DatosEnvio.ContainsKey("Comentarios"))
                            DatosEnvio["Comentarios"] = TxtComentariosCLN.Text;
                        else
                            DatosEnvio.Add("Comentarios", TxtComentariosCLN.Text);

                        InsertaLineasTraspasosHHEMD();
                        ObtenerTotalTraspasosHHRI();
                    }

                    else
                        Logic.ShowException(null, "Es necesario llenar toda la información.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                }
                else
                    Logic.ShowException(null, "Es necesario que valides el lote origen, adicional valida el almacén destino con el icono de color verde.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            }
            catch (Exception ex)
            {

                Logic.ShowException(ex, "No fue posible guardar el registro, intenta de nuevo más tarde: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                Cursor.Current = Cursors.Default;
            }
        }
        private void TxtAlmacenDestino_KeyPress(object sender, KeyPressEventArgs e)
        {
            AlmacenDestinoValidado = false;
        }
        #endregion

        #region methods
        public DatosMaestrosTraspaso(Dictionary<string, string> CurrentData)
        {
            InitializeComponent();
            this.CurrentData = CurrentData;
            TxtLoteOrigenVALCLN.Focus();
            LblFolio.Text = "Folio: " + this.CurrentData["FolioTS"];
            EstadoInicializacion = "OK";
        }
        private bool validaCapturaArticulo(Panel PnlControls)
        {
            foreach (Control ctrl in PnlControls.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox txt = (TextBox)ctrl;
                    //Solo a los txt que contienen en su nombre VAL deben ser validados
                    if (txt.Name.Contains("VAL"))
                    {
                        if (txt.Text == string.Empty)
                            return false;
                        else
                            continue;
                    }

                }
            }

            return true;
        }
        private void ValidaAlmacenHHEMD()
        {
            Dictionary<string, string> resultado = new Dictionary<string, string>();
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            Logic.AD.RequestParameters.Add("Almacen", TxtAlmacenDestinoVAL.Text);
            //Listado dinamico
            List<Dictionary<string, string>> result = Logic.ExecGetRequest("/InventariosMovil/ValidaAlmacenHHEMD", Logic.AD.RequestParameters, false, false);

            if (result[0]["Status"] == "NO" || result[0]["Status"] == "ERROR")
            {
                string Exception = result[0]["Message"].ToString();
                Logic.ShowException(null, Exception, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                AlmacenDestinoValidado = false;
                TxtAlmacenDestinoVAL.Text = string.Empty;
            }
            else
            {
                SoundPlayer.ReproducirSonido("Inicio.wav");
                resultado.Add("CodigoAlmacen", result[0]["CodigoAlmacen"]);
                resultado.Add("NombreAlmacen", result[0]["NombreAlmacen"]);
                AlmacenDestinoValidado = true;
                TxtAlmacenDestinoVAL.Text = resultado["CodigoAlmacen"] + "/" + resultado["NombreAlmacen"];
                //Actualizar datos de almacen seleccionado
                if (DatosEnvio.ContainsKey("AlmacenDestino"))
                    DatosEnvio["AlmacenDestino"] = resultado["CodigoAlmacen"];
                else
                    DatosEnvio.Add("AlmacenDestino", resultado["CodigoAlmacen"]);
            }
        }
        private bool ValidarLoteTraspasosHHTM(string Lote)
        {
            string Excepcion = string.Empty;
            DatosLote.Clear();
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            Logic.AD.RequestParameters.Add("Lote", Lote);

            //Listado dinamico
            List<Dictionary<string, string>> result = Logic.ExecGetRequest(
                "/InventariosMovil/ValidarLoteTraspasosHHTM",
                Logic.AD.RequestParameters, false, false);

            if (result[0]["Status"] == "NO" || result[0]["Status"] == "ERROR")
            {
                Excepcion = result[0]["Message"].ToString();
                Logic.ShowException(null, Excepcion, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                return false;
            }

            // Copiar todas las claves dinámicamente
            foreach (var kvp in result[0])
            {
                DatosLote[kvp.Key] = kvp.Value;
            }

            return true;
        }
        private void InsertaLineasTraspasosHHEMD()
        {
            string resultado = string.Empty;
            AccesoDatosInventarios.LineasTransferenciasHHTS ATI = new AccesoDatosInventarios.LineasTransferenciasHHTS(
                CurrentData["FolioTS"],       // folio
                DatosEnvio["LoteOrigen"],        // loteOrigen
                DatosEnvio["Cantidad"],          // cantidad
                DatosEnvio["AlmacenOrigen"],       // almacenOrigen
                DatosEnvio["AlmacenDestino"],       // almacenDestino
                DatosEnvio["Articulo"],      // articulo
                DatosEnvio["Comentarios"]     // comentarios
            );

            StringWriter sw = new StringWriter();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = false, // Evita saltos de línea
                OmitXmlDeclaration = false, // Incluye la declaración <?xml version="1.0" encoding="utf-8"?>
                Encoding = System.Text.Encoding.UTF8
            };

            string Xml = Logic.ConvertToXml(ATI, "LineasTransferenciasHHTS");

            //Listado dinamico
            List<Dictionary<string, string>> resultadoApi = Logic.ExecPostRequest("/InventariosMovil/InsertaLineasTraspasosHHEMD", Xml, false, string.Empty);
            if (resultadoApi[0].ContainsKey("Folio"))
            {
                resultado = resultadoApi[0]["Folio"];
                if (!resultado.Contains("Duplicado"))
                {
                    resultado = resultadoApi[0]["Message"] + " para el folio: " + resultadoApi[0]["Folio"];
                    Logic.LimpiaFormulario(this.Body);
                    TxtLoteOrigenVALCLN.Focus();
                    AlmacenDestinoValidado = true;
                }
                else
                {
                    resultado = "Ya has realizado un registro para el lote: " + DatosEnvio["LoteOrigen"];
                    TxtLoteOrigenVALCLN.Text = string.Empty;
                    TxtCantidadVALCLN.Text = string.Empty;
                    TxtAlmacenOrigenVALCLN.Text = string.Empty;
                    TxtArticuloVALCLN.Text = string.Empty;
                }

                Logic.ShowException(null, resultado, "AVISO", MessageBoxButtons.OK, (resultado.Contains("Ya has realizado") ? MessageBoxIcon.Exclamation : MessageBoxIcon.Asterisk), MessageBoxDefaultButton.Button1);
            }
            else
            {
                resultado = "ERROR";
                string Mensaje = resultadoApi[0]["Message"].ToString();
                Logic.ShowException(null, Mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
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
                    writer.WriteStartElement("DocumentosTransferenciasHHTS");
                    writer.WriteElementString("Folio", Folio);
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }

                string Xml = sw.ToString();

                List<Dictionary<string, string>> borrado = Logic.ExecPostRequest("/InventariosMovil/EliminarDocumentosTransferenciasHHTS", Xml, false, string.Empty);
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
        private void ObtenerTotalTraspasosHHRI()
        {
            //Obtener el numero de rollos
            Logic.AD.RequestParameters = new Dictionary<string, string>();
            Logic.AD.RequestParameters.Add("FolioTS", CurrentData["FolioTS"]);
            Logic.AD.RequestParameters.Add("Usuario", FormHelper.Usuario);
            List<Dictionary<string, string>> TotalTraspasos = Logic.ExecGetRequest("/InventariosMovil/GetTotalTransferenciasHHRI", Logic.AD.RequestParameters, false, false);

            if (TotalTraspasos[0]["Status"] == "NO" || TotalTraspasos[0]["Status"] == "ERROR")
            {
                string result = TotalTraspasos[0]["Message"];
                LblTotalTraspasos.Text = "No fue posible obtener el número de rollos del recuento.";
            }
            else
            {
                //Asignar numero de rollos escaneados
                string Total = TotalTraspasos[0]["TotalTraspaso"];
                LblTotalTraspasos.Text = "No. rollos: " + Total;
            }
        }
        #endregion
    }
}