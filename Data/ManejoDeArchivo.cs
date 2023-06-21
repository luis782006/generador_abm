using Generador_ABM.Data;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Generador_ABM;

    public class ManejoDeArchivo
    {

        public static string? nombreArchivo = "";
        public static void CrearArchivoDeClase(List<Clase> lstAtributos, string NombreDeClase, string path)
        {
            
            nombreArchivo = $"{path}\\{NombreDeClase}.cs";
            string IsNulo = "";
            string ValorXDefect = "";
            using (StreamWriter writer = new StreamWriter(nombreArchivo))
            {
              
            writer.WriteLine($"public class {NombreDeClase}");
                writer.WriteLine("{");
                foreach (var item in lstAtributos)
                {
                // control de tipos de datos. 
                    if (!item.AceptaNull)
                    {
                       IsNulo = "?";
                }
                    else
                    {
                        if (item.ValorPorDefecto != ValorXDefect)
                        {
                        ValorXDefect = $"={item.ValorPorDefecto};";
                        }

                        
                    }
                writer.WriteLine($" public {item.TipoDato}{IsNulo} {item.NombreAtributo} {{ get; set; }}{ValorXDefect}");
                ValorXDefect = "";
                IsNulo = "";
                }
                writer.WriteLine("}");

                writer.Close();
                
            }
            //Abrir el explorador de la carpetas de windows
            //string argumentos = nombreArchivo;
            //Process.Start("explorer.exe", argumentos);           
        }

        public static string RutaDeArchivo()
        {
            return nombreArchivo;               
        }

    public static void ActualizarArchivo(string[] test)
    {
        string path = RutaDeArchivo();
        foreach (var item in test)
        {
        string appendText = item + Environment.NewLine;
        File.AppendAllText(path, appendText);

        }
    }
}
