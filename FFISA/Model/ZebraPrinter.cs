using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace FFISA.Model
{
    class ZebraPrinter
    {
        private string printerIp = "172.16.0.48"; // IP de la impresora
        private int printerPort = 9100; // Puerto por defecto para impresoras Zebra (RAW)

        public void PrintLabel()
        {
            // Crear el código ZPL para la etiqueta
            string zpl = "^XA" +  // Inicia la etiqueta
                         "^FO50,50" +  // Posición de texto (50, 50)
                         "^AD" +       // Fuente de texto por defecto
                         "^FDGenerando Etiqueta de Demo^FS" +  // Datos de la etiqueta
                         "^FO50,100" +  // Posición de texto para otra línea
                         "^B3N,N,100,Y,N" +  // Código de barras
                         "^FD>:123456^FS" +  // Datos del código de barras
                         "^FO50,200" +  // Posición del texto
                         "^FDEtiqueta ZPL en Zebra ZT411^FS" +  // Más texto
                         "^XZ";         // Fin de la etiqueta

            // Conectar con la impresora a través de un socket
            using (TcpClient client = new TcpClient(printerIp, printerPort))
            {
                // Obtener el flujo de la red
                NetworkStream networkStream = client.GetStream();
                StreamWriter writer = new StreamWriter(networkStream, Encoding.ASCII);

                // Enviar el código ZPL a la impresora
                writer.Write(zpl);
                writer.Flush();
            }

            Console.WriteLine("Etiqueta enviada a la impresora.");
        }
    }
}
