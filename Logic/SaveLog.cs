using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi_LandingPreferencias.Logic
{
    public class SaveLog
    {
        public bool debug = false;

        public void Log(string message)
        {
            if (debug)
            {
                string[] carpetaTemporal = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.txt");

                foreach (string ruta in carpetaTemporal)
                {
                    FileInfo archivo = new FileInfo(ruta);
                    if (archivo.Name.Contains("log_"))
                    {
                        string nombreArchivo = archivo.Name.Split('_')[1].Split('.')[0];
                        DateTime fechaCreacion = DateTime.ParseExact(nombreArchivo, "yyyyMMdd", null);
                        int diasDiferencia = ((TimeSpan)(DateTime.Now - fechaCreacion)).Days;
                        if (diasDiferencia > 2)
                        {
                            archivo.Delete();
                        }
                    }
                }

                string nameFile = "log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nameFile);

                if (File.Exists(filePath))
                {
                    StringBuilder newFile = new StringBuilder();
                    string temp = "";
                    string[] file = File.ReadAllLines(filePath);

                    foreach (string line in file)
                    {
                        if (line.Contains("string"))
                        {
                            temp = line.Replace("string", "String");
                            newFile.Append(temp + "\r\n");
                            continue;
                        }

                        newFile.Append(line + "\r\n");
                    }

                    newFile.Append(message + "\r\n");

                    File.WriteAllText(filePath, newFile.ToString());
                }
                else
                {
                    using (StreamWriter streamWriter = new StreamWriter(filePath))
                    {
                        streamWriter.WriteLine(message);
                        streamWriter.Close();
                    }
                }
            }

        }
    }
}