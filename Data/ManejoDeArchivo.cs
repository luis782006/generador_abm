using Generador_ABM.Data;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Generador_ABM;

public class ManejoDeArchivo
{

    public static string? nombreArchivo = "";
    public static void CrearArchivoDeClase(List<Clase> lstAtributos,string NameSpace, string NombreDeClase, string path,string NombreInterface, Boolean SqlBase, Boolean SqlInsert, Boolean SqlUpdate, Boolean SqlDelete, Boolean SqlToString)
    {

        //Console.WriteLine(SqlBase.ToString(), SqlInsert.ToString(), SqlUpdate.ToString(), SqlDelete.ToString(), SqlToString.ToString());

        nombreArchivo = $"{path}\\{NombreDeClase}.cs";
        string IsNulo = "";
        string ValorXDefect = null;
        //Se crea objeto Stream para escribir archivo
        using (StreamWriter writer = new StreamWriter(nombreArchivo))
        {
            try
            {
                writer.WriteLine("using System;");
                writer.WriteLine();
                writer.WriteLine($"namespace {NameSpace}");
                writer.WriteLine("{");

                //Nombre de Interfaz
                if (NombreInterface == null || NombreInterface =="")
                {
                    writer.WriteLine($"\t public class {NombreDeClase}");
                }
                else
                {
                    writer.WriteLine($"\t public class {NombreDeClase}: {NombreInterface}");
                }
                //// Interfaz
                ///


                writer.WriteLine("\t {");
                foreach (var item in lstAtributos)
                {
                    // control de tipos de datos.
                    if (!item.AceptaNull)
                    {
                        IsNulo = "?";
                    }
                    else
                    {
                        if (item.ValorPorDefecto != null)// if (item.ValorPorDefecto != ValorXDefect)
                        {
                            ValorXDefect = $"='{item.ValorPorDefecto}';";
                        }
                    }

                    writer.WriteLine($"\t\t public {item.TipoDato}{IsNulo} {item.NombreAtributo} {{ get; set; }}{ValorXDefect}");
                    ValorXDefect = "";
                    IsNulo = "";
                }
                // Control de booleanos SQL Base,Insert,Update,Delete,ToString
                //Extraccion de datos
                 List<string> DatosAux = new List<string>();
                 DatosAux= DatosAExtraer(lstAtributos); //resultados DatosAux[0]=atributos, DatosAux[1]=@atributos, DatosAux[2]=atributos=@atrubutos


                //Base
                if (SqlBase)
                {   
                    writer.WriteLine();
                    writer.WriteLine($"\t\t public const string QueryBase='SELECT {DatosAux[0]} FROM  {NombreDeClase} ;'");
                }
                //Insert
                if (SqlInsert)
                {
                    string OutputInserted = lstAtributos[0].NombreAtributo; // Primer elemento del listado original para el OutPut Inserted

                    writer.WriteLine();
                    writer.WriteLine($"\t\t public const string InsertQuery='INSERT INTO  {NombreDeClase} ({DatosAux[0]}) OUTPUT INSERTED.{OutputInserted} VALUES ({DatosAux[1]});'");
                }
                // Update
                if (SqlUpdate)
                {
                    string IdAtributo = lstAtributos[0].NombreAtributo; // Primer elemento del listado original para el where
                    writer.WriteLine();
                    writer.WriteLine($"\t\t public const string UpdateQuery='UPDATE {NombreDeClase} SET {DatosAux[2]} WHERE {IdAtributo}=@{IdAtributo};'");
                }
                
                //Delete
                if (SqlDelete)
                {
                    string IdAtributo = lstAtributos[0].NombreAtributo; // Primer elemento del listado original para el where
                    writer.WriteLine();
                    writer.WriteLine( $"\t\t public const string DeleteQuery='DELETE FROM {NombreDeClase} WHERE {IdAtributo}=@{IdAtributo};'");
                }

                //ToString
                if (SqlToString)
                {
                    writer.WriteLine() ;
                    //writer.WriteLine($"return {{}}");
                    writer.WriteLine("\t\t public override string ToString()");
                    writer.WriteLine("\t\t {");
                    writer.WriteLine("\t\t\t \treturn $\"{{\"");
                    foreach (var item in lstAtributos) 
                    {
                       if (item.TipoDato.ToString() == "DateTime")
                        {
                            writer.WriteLine($"\t\t\t\t\t+ $\"\\\"{item.NombreAtributo}\\\" : {{ {item.NombreAtributo} : u }},\"");
                        }
                        else
                        {
                            writer.WriteLine($"\t\t\t\t\t+ $\"\\\"{item.NombreAtributo}\\\" : {{ {item.NombreAtributo} }},\"");
                        }
                    }
                   
                }
                writer.WriteLine("\t\t }");
               
                // Metodos fijos por clase en caso de uso Select
                // Equals
                writer.WriteLine();
                writer.WriteLine($"\t\t public override bool Equals(object o)");
                writer.WriteLine("\t\t { ");
                writer.WriteLine($"\t\t\t  var other = o as {NombreDeClase} ; ");
                writer.WriteLine($"\t\t\t return other?.{lstAtributos[0].NombreAtributo}=={lstAtributos[0].NombreAtributo}");
                writer.WriteLine("\t\t}");

                //GetHasCode
                writer.WriteLine();
                writer.WriteLine($"\t\t public override int GetHashCode() => {lstAtributos[0].NombreAtributo}.GetHashCode();");



                writer.WriteLine("\t}"); // cierre de clase
                writer.WriteLine("}");// cierre de NameSpace
            }
            finally
            {
                writer.Close();
                writer.Dispose();

            }
        }
        //Abrir el explorador de la carpetas de windows
        //string argumentos = nombreArchivo;
        //Process.Start("explorer.exe", argumentos);           
    }
    //Metodo para Extraer los atributos y concatenarlos para consultas SQl
    public static List<string> DatosAExtraer(List<Clase> lstAtributos)
    {
        var atributosAux= new List<string>();
        string atrib = "";
        string atributosParam = "";
        string atribParamUpdate = "";

        foreach (var item in lstAtributos)
        {
            atrib += $"{item.NombreAtributo},";
            atributosParam += $"@{item.NombreAtributo},";
            atribParamUpdate += $"{item.NombreAtributo}=@{item.NombreAtributo},";
        }
        //Elimino las comas.
        atrib = atrib.Substring(0, atrib.Length - 1);
        atributosParam = atributosParam.Substring(0, atributosParam.Length - 1);
        atribParamUpdate = atribParamUpdate.Substring(0, atribParamUpdate.Length - 1);

        atributosAux.Add(atrib);
        atributosAux.Add(atributosParam);
        atributosAux.Add(atribParamUpdate);

        //Modificacion de datos para SQLUpdate.

        
        return atributosAux;
    }


    public static string RutaDeArchivo()
    {
        return nombreArchivo;
    }

    public static void ActualizarArchivo(string[] linea)
    {
        string path = RutaDeArchivo();
        foreach (var item in linea)
        {
            string appendText = item + Environment.NewLine;
            File.AppendAllText(path, appendText);
            //File.CreateText(path);
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (var i in linea)
                {
                    writer.WriteLine(i);
                }
                
                writer.Close();
            }
           
        }
    }
}
