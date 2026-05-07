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
    class AccesoDatosInventarios
    {
        public class PlantillasEtiquetaEMD
        {
            public string Id { get; set; }
            public string Plantilla { get; set; }
            public string Usuario { get; set; }
        }

        public class EncabezadoEntradasHHEMD
        {
            public string Usuario { get; set; }
        }

        public class EncabezadoSalidasHHEMD
        {
            public string Usuario { get; set; }
        }

        public class EncabezadoTransferenciasHHTS
        {
            public string Usuario { get; set; }
        }

        public class EncabezadoRecuentosHHRI
        {
            public string Usuario { get; set; }
            public string DocEntry { get; set; }
            public string SapDocument { get; set; }
        }

        public class LineasEntradasHHEMD
        {
            // Campos privados
            private string _folio = "";
            private string _articulo = "";
            private string _etiqueta = "";
            private string _almacen = "";
            private string _lote = "";
            private string _cantidad = "";
            private string _cantidadMetros = "";
            private string _comentarios = "";
            private string _cuentaContable = "";
            private string _codigocuentaContable = "";

            // Propiedad: Folio
            public string Folio
            {
                get { return _folio; }
                set { _folio = value ?? ""; }
            }

            // Propiedad: Articulo
            public string Articulo
            {
                get { return _articulo; }
                set { _articulo = value ?? ""; }
            }

            // Propiedad: Etiqueta
            public string Etiqueta
            {
                get { return _etiqueta; }
                set { _etiqueta = value ?? ""; }
            }

            // Propiedad: Almacen
            public string Almacen
            {
                get { return _almacen; }
                set { _almacen = value ?? ""; }
            }

            // Propiedad: Lote
            public string Lote
            {
                get { return _lote; }
                set { _lote = string.IsNullOrEmpty(value) ? null : value; }
            }

            // Propiedad: Cantidad
            public string Cantidad
            {
                get { return _cantidad; }
                set { _cantidad = value ?? ""; }
            }

            // Propiedad: CantidadMetros
            public string CantidadMetros
            {
                get { return _cantidadMetros; }
                set { _cantidadMetros = value ?? ""; }
            }

            // Propiedad: Comentarios
            public string Comentarios
            {
                get { return _comentarios; }
                set { _comentarios = value ?? ""; }
            }

            // Propiedad: CuentaContable
            public string CuentaContable
            {
                get { return _cuentaContable; }
                set { _cuentaContable = value ?? ""; }
            }
            // Propiedad: CuentaContable
            public string CodigoCuentaContable
            {
                get { return _codigocuentaContable; }
                set { _codigocuentaContable = value ?? ""; }
            }
        }

        public class LineasSalidasHHEMD
        {
            // Campos privados
            private string _folio = "";
            private string _articulo = "";
            private string _almacen = "";
            private string _lote = "";
            private string _cantidad = "";
            private string _cantidadMetros = "";
            private string _comentarios = "";
            private string _cuentaContable = "";
            private string _codigocuentaContable = "";

            // Propiedad: Folio
            public string Folio
            {
                get { return _folio; }
                set { _folio = value ?? ""; }
            }

            // Propiedad: Articulo
            public string Articulo
            {
                get { return _articulo; }
                set { _articulo = value ?? ""; }
            }

            // Propiedad: Almacen
            public string Almacen
            {
                get { return _almacen; }
                set { _almacen = value ?? ""; }
            }

            // Propiedad: Lote
            public string Lote
            {
                get { return _lote; }
                set { _lote = value ?? ""; }
            }

            // Propiedad: Cantidad
            public string Cantidad
            {
                get { return _cantidad; }
                set { _cantidad = value ?? ""; }
            }

            // Propiedad: CantidadMetros
            public string CantidadMetros
            {
                get { return _cantidadMetros; }
                set { _cantidadMetros = value ?? ""; }
            }

            // Propiedad: Comentarios
            public string Comentarios
            {
                get { return _comentarios; }
                set { _comentarios = value ?? ""; }
            }

            // Propiedad: CuentaContable
            public string CuentaContable
            {
                get { return _cuentaContable; }
                set { _cuentaContable = value ?? ""; }
            }
            // Propiedad: CuentaContable
            public string CodigoCuentaContable
            {
                get { return _codigocuentaContable; }
                set { _codigocuentaContable = value ?? ""; }
            }
        }

        public class LineasTransferenciasHHTS
        {
            // Campos privados
            private string _folio = "";
            private string _loteOrigen = "";
            private string _cantidad = "";
            private string _almacenOrigen = "";
            private string _almacenDestino = "";
            private string _articulo = "";
            private string _comentarios = "";
            private DateTime _fecha = DateTime.MinValue;

            // Constructor vacío
            public LineasTransferenciasHHTS()
            {
            }

            // Constructor con parámetros
            public LineasTransferenciasHHTS(
                string folio,
                string loteOrigen,
                string cantidad,
                string almacenOrigen,
                string almacenDestino,
                string articulo,
                string comentarios
            )
            {
                _folio = folio ?? "";
                _loteOrigen = loteOrigen ?? "";
                _cantidad = cantidad ?? "";
                _almacenOrigen = almacenOrigen ?? "";                
                _almacenDestino = almacenDestino ?? "";
                _articulo = articulo ?? "";
                _comentarios = comentarios ?? "";
            }

            // Propiedad: Folio
            public string FolioTS
            {
                get { return _folio; }
                set { _folio = value ?? ""; }
            }

            // Propiedad: LoteOrigen
            public string LoteOrigen
            {
                get { return _loteOrigen; }
                set { _loteOrigen = value ?? ""; }
            }

            // Propiedad: Cantidad
            public string Cantidad
            {
                get { return _cantidad; }
                set { _cantidad = value ?? ""; }
            }

            // Propiedad: AlmacenOrigen
            public string AlmacenOrigen
            {
                get { return _almacenOrigen; }
                set { _almacenOrigen = value ?? ""; }
            }

            // Propiedad: AlmacenDestino
            public string AlmacenDestino
            {
                get { return _almacenDestino; }
                set { _almacenDestino = value ?? ""; }
            }

            // Propiedad: Articulo
            public string Articulo
            {
                get { return _articulo; }
                set { _articulo = value ?? ""; }
            }

            // Propiedad: Comentarios
            public string Comentarios
            {
                get { return _comentarios; }
                set { _comentarios = value ?? ""; }
            }
        }

        public class LineasRecuentosHHRI
        {
            // Campos privados
            private string _folio = "";
            private string _lote = "";
            private string _cantidad = "";
            private string _articulo = "";
            private string _almacen = "";
            private string _existeenrecuento = "";
            private string _usuario = "";

            // Constructor vacío
            public LineasRecuentosHHRI()
            {
            }

            // Constructor con parámetros
            public LineasRecuentosHHRI(
                string folio,
                string lote,
                string cantidad,
                string articulo,
                string almacen,
                string existeenrecuento,
                string usuario
            )
            {
                _folio = folio ?? "";
                _lote = lote ?? "";
                _cantidad = cantidad ?? "";
                _articulo = articulo ?? "";
                _almacen = almacen ?? "";
                _existeenrecuento = existeenrecuento ?? "";
                _usuario = usuario ?? "";
            }

            // Propiedad: Folio
            public string FolioRI
            {
                get { return _folio; }
                set { _folio = value ?? ""; }
            }

            // Propiedad: LoteOrigen
            public string Lote
            {
                get { return _lote; }
                set { _lote = value ?? ""; }
            }

            // Propiedad: Cantidad
            public string Cantidad
            {
                get { return _cantidad; }
                set { _cantidad = value ?? ""; }
            }
            // Propiedad: Articulo
            public string Articulo
            {
                get { return _articulo; }
                set { _articulo = value ?? ""; }
            }

            // Propiedad: AlmacenOrigen
            public string Almacen
            {
                get { return _almacen; }
                set { _almacen = value ?? ""; }
            }
            // Propiedad: AlmacenOrigen
            public string ExisteEnRecuento
            {
                get { return _existeenrecuento; }
                set { _existeenrecuento = value ?? ""; }
            }
            // Propiedad: AlmacenOrigen
            public string Usuario
            {
                get { return _usuario; }
                set { _usuario = value ?? ""; }
            }
        }
    }
}
