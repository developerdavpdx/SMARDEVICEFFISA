using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace FFISA.Model
{
    public class DeviceManager
    {
        [DllImport("coredll.dll", SetLastError = true)]
        private static extern int SystemParametersInfo(int uiAction, int uiParam, StringBuilder pvParam, int fWinIni);

        private const int SPI_GETPLATFORMTYPE = 257;
        private const int SPI_GETOEMINFO = 258;

        public static string osVersion  {get{return Environment.OSVersion.Version.ToString();}}
        public static string platform {get{return Environment.OSVersion.Platform.ToString();}}

        private static LogicaGlobal Logic = new LogicaGlobal();
        private static string DeviceIdFilePath = @"\Application\Program Files\FFISASETUP\DONT_REMOVE\device_info.txt";
        private static string EncryptionKey = "PDX#2025"; // puedes cambiarla, pero debe ser la misma para cifrar y descifrar

        public static string GetDeviceId()
        {
            try
            {
                if (File.Exists(DeviceIdFilePath))
                {
                    string[] lines = ReadLines(DeviceIdFilePath);
                    if (lines.Length > 0)
                    {
                        return lines[0]; // ID del dispositivo sin cifrar
                    }
                }

                string newId = Guid.NewGuid().ToString();
                SaveDeviceCredentials(newId, "", "");
                return newId;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static bool SaveDeviceCredentials(string deviceId, string email, string password)
        {
            try
            {
                string folderPath = @"\Application\Program Files\FFISASETUP\DONT_REMOVE";

                // Crear carpeta si no existe
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string[] lines = new string[2];
                lines[0] = deviceId; // se guarda sin cifrar
                string joined = email + "|" + password;
                lines[1] = EncryptDecrypt(joined, EncryptionKey); // cifrado XOR simple

                using (StreamWriter writer = new StreamWriter(DeviceIdFilePath, false))
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        writer.WriteLine(lines[i]);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible iniciar sesión: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                return false;
            }
        }

        public static string[] GetDeviceCredentials(out string email, out string password)
        {
            email = "";
            password = "";

            try
            {
                if (File.Exists(DeviceIdFilePath))
                {
                    string[] lines = ReadLines(DeviceIdFilePath);

                    if (lines.Length >= 2)
                    {
                        string decrypted = EncryptDecrypt(lines[1], EncryptionKey);
                        lines = decrypted.Split('|');
                    }

                    return lines;
                }
                else
                {
                    string newId = Guid.NewGuid().ToString();
                    SaveDeviceCredentials(newId, "", "");
                    return new string[] { "", "" };
                }
            }
            catch (Exception ex)
            {
                Logic.ShowException(ex, "No fue posible obtener las credenciales guardadas: ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                return new string[2];
            }
        }

        private static string[] ReadLines(string path)
        {
            System.Collections.ArrayList list = new System.Collections.ArrayList();

            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }

            string[] result = new string[list.Count];
            list.CopyTo(result);
            return result;
        }

        private static string EncryptDecrypt(string input, string key)
        {
            char[] output = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                output[i] = (char)(input[i] ^ key[i % key.Length]);
            }

            return new string(output);
        }

        //DEVICE INFO

        //Tipo de plataforma por ejemplo: "PocketPC"
        public static string GetPlatformType()
        {
            StringBuilder sb = new StringBuilder(256);
            SystemParametersInfo(SPI_GETPLATFORMTYPE, sb.Capacity, sb, 0);
            return sb.ToString();
        }
        //Información del fabricante
        public static string GetOemInfo()
        {
            StringBuilder sb = new StringBuilder(256);
            SystemParametersInfo(SPI_GETOEMINFO, sb.Capacity, sb, 0);
            return sb.ToString();
        }

    }

}
