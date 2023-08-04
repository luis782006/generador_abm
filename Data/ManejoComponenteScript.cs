using Generador_ABM.Pages;
using MudBlazor;
using Serilog;
using System.Net.NetworkInformation;
using System.Text;
using Serilog;
using Microsoft.Extensions.Logging;


namespace Generador_ABM.Data
{
    public class ManejoComponenteScript
    {
        public static StringBuilder GuardarScript(string NombreClase,List<Clase> listado,string rutaArchivoScript,StringBuilder sbuilder )
        {
            rutaArchivoScript = $"{rutaArchivoScript}\\{NombreClase}.sql";
                    StringBuilder SBuilder = new StringBuilder();

                    SBuilder.AppendLine($"CREATE TABLE {NombreClase}(");
                    SBuilder.AppendLine($"{listado[0].NombreAtributo} {listado[0].TipoDato}(18,0) PRIMARY KEY IDENTITY (1,1) NOT NULL,");


                    foreach (var item in listado.Skip(1))
                    {
                        switch (item.TipoDato)
                        {
                            case EnumTipoDato.DataType.String:
                                //Manejo de Strings
                                if (item.CantidadChar < 10)
                                {
                                    if (item.AceptaNull)
                                    {
                                        SBuilder.AppendLine($"{item.NombreAtributo} nchar({item.CantidadChar}) NOT NULL,");
                                    }
                                    else
                                    {
                                        SBuilder.AppendLine($"{item.NombreAtributo} nchar({item.CantidadChar}),");
                                    }
                                }
                                else
                                {
                                    if (item.AceptaNull)
                                    {
                                        SBuilder.AppendLine($"{item.NombreAtributo} nvarchar({item.CantidadChar}) NOT NULL,");
                                    }
                                    else
                                    {
                                        SBuilder.AppendLine($"{item.NombreAtributo} nvarchar({item.CantidadChar}),");
                                    }
                                }
                                break;

                            case EnumTipoDato.DataType.Int:
                            case EnumTipoDato.DataType.UShort:
                            case EnumTipoDato.DataType.UInt:
                                if (item.AceptaNull)
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} int NOT NULL,");
                                }
                                else
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} int,");
                                }
                                break;

                            case EnumTipoDato.DataType.Double:
                                if (item.AceptaNull)
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} float NOT NULL,");
                                }
                                else
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} float,");
                                }
                                break;

                            case EnumTipoDato.DataType.Bool:
                                if (item.AceptaNull)
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} bit NOT NULL,");
                                }
                                else
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} bit,");
                                }
                                break;
                            case EnumTipoDato.DataType.Char:
                                // Manejo para el tipo de dato Char
                                // ...
                                break;

                            case EnumTipoDato.DataType.DateTime:
                                if (item.AceptaNull)
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} datetime NOT NULL,");
                                }
                                else
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} datetime,");
                                }
                                break;

                            case EnumTipoDato.DataType.Decimal:
                                if (item.AceptaNull)
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} decimal(18,0) NOT NULL,");
                                }
                                else
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} decimal(18,0),");
                                }
                                break;

                            case EnumTipoDato.DataType.Float:
                                if (item.AceptaNull)
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} real NOT NULL,");
                                }
                                else
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} real,");
                                }
                                break;

                            case EnumTipoDato.DataType.Long:
                                if (item.AceptaNull)
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} numeric(18,0) NOT NULL,");
                                }
                                else
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} numeric(18,0),");
                                }
                                break;

                            case EnumTipoDato.DataType.Short:
                                if (item.AceptaNull)
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} smallint NOT NULL,");
                                }
                                else
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} smallint,");
                                }
                                break;

                            case EnumTipoDato.DataType.Byte:
                                if (item.AceptaNull)
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} tinyint NOT NULL,");
                                }
                                else
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} tinyint,");
                                }
                                break;

                            case EnumTipoDato.DataType.SByte:
                                if (item.AceptaNull)
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} smallint NOT NULL,");
                                }
                                else
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} smallint,");
                                }
                                break;

                            case EnumTipoDato.DataType.ULong:
                                if (item.AceptaNull)
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} numeric(20,0) NOT NULL,");
                                }
                                else
                                {
                                    SBuilder.AppendLine($"{item.NombreAtributo} numeric(0,0),");
                                }
                                break;


                            default:
                                // Tipo de dato desconocido o no manejado
                                Console.WriteLine($"Tipo de dato no reconocido: {item.TipoDato}");
                                break;
                        }
                    }
                    SBuilder = SBuilder.Remove(SBuilder.Length - 2, 1);
                    SBuilder.AppendLine(");");

            return SBuilder;
        }
           
    }
}
