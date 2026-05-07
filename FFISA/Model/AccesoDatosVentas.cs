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
    class AccesoDatosVentas
    {
        public class EncabezadoVentasHHEMOV
        {
            public string OrdenVenta { get; set; }
            public string SapDocument { get; set; }
            public string Cliente { get; set; }
            public string Usuario { get; set; }
            public string Comentarios { get; set; }
            public string Estatus { get; set; }
        }

        public class LineasVentasHHEMOV
        {
            // Campos privados para respaldo de propiedades
            private string _folio = "";
            private string _articulo = "";
            private string _linea = "";
            private string _almacen = "";
            private string _umv = "";
            private string _umi = "";
            private string _ums = "";
            private string _lote = "";
            private string _cantidad = "";
            private string _cantidadconversion = "";
            private string _referencia = "";
            private string _cantidadReferencia = "";
            private string _usuario = "";

            // Propiedad: Folio
            public string Folio
            {
                get { return _folio; }
                set { _folio = value ?? ""; } // Asegura que no sea null
            }

            // Propiedad: Articulo
            public string Articulo
            {
                get { return _articulo; }
                set { _articulo = value ?? ""; }
            }

            // Propiedad: Articulo
            public string Linea
            {
                get { return _linea; }
                set { _linea = value ?? ""; }
            }

            // Propiedad: Almacen
            public string Almacen
            {
                get { return _almacen; }
                set { _almacen = value ?? ""; }
            }

            // Propiedad: UMV
            public string UMV
            {
                get { return _umv; }
                set { _umv = value ?? ""; }
            }

            // Propiedad: UMI
            public string UMI
            {
                get { return _umi; }
                set { _umi = value ?? ""; }
            }

            // Propiedad: UMS
            public string UMS
            {
                get { return _ums; }
                set { _ums = value ?? ""; }
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

            // Propiedad: Cantidad Conversion
            public string CantidadConversion
            {
                get { return _cantidadconversion; }
                set { _cantidadconversion = value ?? ""; }
            }

            // Propiedad: Referencia
            public string Referencia
            {
                get { return _referencia; }
                set { _referencia = value ?? ""; }
            }

            // Propiedad: CantidadReferencia
            public string CantidadReferencia
            {
                get { return _cantidadReferencia; }
                set { _cantidadReferencia = value ?? ""; }
            }

            // Propiedad: Usuario
            public string Usuario
            {
                get { return _usuario; }
                set { _usuario = value ?? ""; }
            }
        }
    }
}
