using Generador_ABM.Pages;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static MudBlazor.CategoryTypes;

namespace Generador_ABM.Data
{
    public class ManejoComponentePopup
    {
        public static StringBuilder ImprimirDialogContentForm(StringBuilder sbuilderPop,List<Clase> Listado,string  NombreClase)
        {
            StringBuilder SBuilder = sbuilderPop;

            sbuilderPop.AppendLine("\t <DialogContent>");
            sbuilderPop.AppendLine("\t <MudForm @bind-IsValid=\"@valid\">");
            int indice = 0;
            foreach (Clase clase in Listado.Skip(1))
            {
                switch (clase.TipoDato)
                {   
                    case EnumTipoDato.DataType.String:
                        SBuilder.AppendLine("\t\t <MudTextField T = \"string\" Class=\"pt-2\"");
						SBuilder.AppendLine($"\t\t\t\t@bind-Value=\"{NombreClase.ToLower()}.{clase.NombreAtributo}\" ");

						break;
                    case EnumTipoDato.DataType.Int:
                        SBuilder.AppendLine("\t\t <MudTextField T = \"int?\" Class=\"pt-2\"");
						SBuilder.AppendLine($"\t\t\t\t@bind-Value=\"{NombreClase.ToLower()}.{clase.NombreAtributo}\" ");
						break;
					case EnumTipoDato.DataType.Decimal:
						SBuilder.AppendLine("\t\t <MudTextField T = \"decimal?\" Class=\"pt-2\"");
						SBuilder.AppendLine($"\t\t\t\t@bind-Value=\"{NombreClase.ToLower()}.{clase.NombreAtributo}\" ");
						break;
					case EnumTipoDato.DataType.Double:
                        SBuilder.AppendLine("\t\t <MudTextField T = \"double?\" Class=\"pt-2\"");
						SBuilder.AppendLine($"\t\t\t\t@bind-Value=\"{NombreClase.ToLower()}.{clase.NombreAtributo}\" ");
						break;
                    case EnumTipoDato.DataType.Bool:
                       
                        SBuilder.AppendLine("\t\t <MudCheckBox  Color = \"Color.Surface\" Class=\"pt-2\"");
						SBuilder.AppendLine($"\t\t\t\t @bind-Checked=\"{NombreClase.ToLower()}.{clase.NombreAtributo}\" ");

						break;
                    case EnumTipoDato.DataType.DateTime:
                        SBuilder.AppendLine("\t\t <MudTextField ");
                        SBuilder.AppendLine("\t\t\t\t T=\"DateTime?\"");
                        SBuilder.AppendLine("\t\t\t\t  InputType=\"InputType.Date\" Format=\"dd/MM/yyyy\" Class=\"pt-2\"");
						SBuilder.AppendLine($"\t\t\t\t @bind-Date=\"{NombreClase.ToLower()}.{clase.NombreAtributo}\" ");
						break;
                    //case EnumTipoDato.DataType.Char:
                    //    SBuilder.AppendLine("< MudTextField T = \"char\"");
                    //    break;
                    //case EnumTipoDato.DataType.Decimal:
                    //    break;
                    //case EnumTipoDato.DataType.Float:
                    //    break;
                    //case EnumTipoDato.DataType.Long:
                    //    break;
                    //case EnumTipoDato.DataType.Short:
                    //    break;
                    //case EnumTipoDato.DataType.Byte:
                    //    break;
                    //case EnumTipoDato.DataType.SByte:
                    //    break;
                    //case EnumTipoDato.DataType.UInt:
                    //    break;
                    case EnumTipoDato.DataType.ULong:
                        SBuilder.AppendLine("<\t\t\t MudTextField T = \"long?\" Class=\"pt-2\"");
						SBuilder.AppendLine($"\t\t\t\t@bind-Value=\"{NombreClase.ToLower()}.{clase.NombreAtributo}\" ");
						break;
                    //case EnumTipoDato.DataType.UShort:
                    //    break;
                    //default:
                        break;
                }
                //Variable indice para hacer imprimir Autofous en el primer atributo
                if (indice == 0)
                {
                    //SBuilder.AppendLine("\t\t<MudTextField T = \"string\"");
                    SBuilder.AppendLine("\t\t\tAutoFocus= \"true\"");                
                }

                if (clase.TipoDato==EnumTipoDato.DataType.Bool)
                {
					SBuilder.AppendLine("\t\t\t\tReadOnly=\"@SoloLectura\"/>");
                    SBuilder.AppendLine("");
                }
                else
                {
                SBuilder.AppendLine($"\t\t\t\tLabel=\"{clase.NombreAtributo}\"");
                SBuilder.AppendLine("\t\t\t\tVariant=\"Variant.Outlined\"");
                SBuilder.AppendLine("\t\t\t\tReadOnly=\"@SoloLectura\"");
                SBuilder.AppendLine("\t\t\t\tMargin=\"Margin.Dense\"");
                SBuilder.AppendLine("\t\t\t\tRequired=\"true\"");
                SBuilder.AppendLine($"\t\t\t\tRequiredError=\"Ingrese su texto de error\"");
                SBuilder.AppendLine("\t\t\t\tImmediate=\"true\" />");
                SBuilder.AppendLine("");
                }
                indice++;
            }
               
            SBuilder.AppendLine("\t </MudForm>");
            SBuilder.AppendLine("\t </DialogContent>");
            return SBuilder;
        }
        
        public static StringBuilder ImprimirDialogActions(StringBuilder sbuilder)
        {
            StringBuilder SBuilder = sbuilder;

            SBuilder.AppendLine("\t <DialogActions>");
            //Boton de cancelar Unico 
            SBuilder.AppendLine("\t\t @if (Modo==\"Ver\")");
            SBuilder.AppendLine("\t\t {");
            SBuilder.AppendLine("\t\t\t <MudButton OnClick=\"Cancelar\" ");
            SBuilder.AppendLine("\t\t\t\t Color=\"Color.Warning\"");
            SBuilder.AppendLine("\t\t\t\t Variant=\"Variant.Outlined\"");
            SBuilder.AppendLine("\t\t\t\t StartIcon=\"@Icons.Filled.Cancel\">");
            SBuilder.AppendLine($"\t\t Cerrar");
            SBuilder.AppendLine("\t\t\t </MudButton>");
            SBuilder.AppendLine("\t\t }");
			SBuilder.AppendLine("\t\t else");
			SBuilder.AppendLine("\t\t {");
			SBuilder.AppendLine("\t\t\t <MudButton OnClick=\"Cancelar\" ");
			SBuilder.AppendLine("\t\t\t\t Color=\"Color.Warning\"");
			SBuilder.AppendLine("\t\t\t\t Variant=\"Variant.Outlined\"");
			SBuilder.AppendLine("\t\t\t\t StartIcon=\"@Icons.Filled.Cancel\">");
			SBuilder.AppendLine($"\t\t Cancelar");
			SBuilder.AppendLine("\t\t\t </MudButton>");
			SBuilder.AppendLine("\t\t }");

			SBuilder.AppendLine("");

			//Boton según valor de parametro de ModoEdicion. Tener en cuenta si llega Id<>0 boton text sera Actualizar o Eliminar. Si ModoEdicion Ver entonces ocultar botón 
			SBuilder.AppendLine("\t\t @if (!IsModoVer)");
            SBuilder.AppendLine("\t\t\t {");
			SBuilder.AppendLine("\t\t\t <MudButton  @onclick=\"@((e)=>AccionModo())\"");
            SBuilder.AppendLine("\t\t\t\t Color=\"Color.Success\"");
            SBuilder.AppendLine("\t\t\t\t Variant=\"Variant.Filled\"");
            SBuilder.AppendLine("\t\t\t\t StartIcon=\"@Icons.Material.Filled.Save\">");
            SBuilder.AppendLine($"\t\t\t @TituloBotonAccion");
            SBuilder.AppendLine("\t\t\t </MudButton>");
			SBuilder.AppendLine("\t\t\t }");
			SBuilder.AppendLine("</DialogActions>");

            return SBuilder;
        }

        public static StringBuilder ImprimirDialogVariable(StringBuilder sbuilder, string NombreClase,List<Clase> lstAtributos)
        {
            //obtengo el tipo de dato del primer atributo de listado(Tipo de dato del ID)

            StringBuilder SBuilder = sbuilder;
            SBuilder.AppendLine("\t //Todo Dialog-----");
            SBuilder.AppendLine("\t [CascadingParameter] MudDialogInstance MudDialog { get; set; }");
            SBuilder.AppendLine("\t [Parameter] public string Modo { get; set; }");
            //obtengo el tipo de dato del primer atributo de listado(Tipo de dato del ID)
             SBuilder.AppendLine($"\t [Parameter] public {lstAtributos[0]?.TipoDato} ID {{get; set;}} // corriga el tipo"); 
            SBuilder.AppendLine("");
            SBuilder.AppendLine("\t // INSTANCIA DE OBJETO");
            SBuilder.AppendLine($"\t\t {NombreClase} {NombreClase.ToLower()}=new {NombreClase}();");
            SBuilder.AppendLine("");
            SBuilder.AppendLine("\t private bool SoloLectura = false;");
            SBuilder.AppendLine("\t bool valid=false;");
            SBuilder.AppendLine("\t string TituloBotonAccion=\"\";");
            SBuilder.AppendLine("\t private string TituloBotonCancelar=\"\";");
            SBuilder.AppendLine("\t private bool IsModoVer=false;");

            return SBuilder;
        }

        public static StringBuilder ImprimirMetodoOnInitialized(StringBuilder sbuilder,string NombreClase,List<Clase> Listado)
        {
            StringBuilder SBuilder = sbuilder;

            SBuilder.AppendLine("");
            SBuilder.AppendLine("//Cargar Formulario.Ejecucion de codigo por Modo recibido");
            SBuilder.AppendLine(" protected override async Task OnInitializedAsync()");
            SBuilder.AppendLine("\t {");
			SBuilder.AppendLine($"\t\t var {NombreClase.ToLower()}Query=await db.ObtenerListadoAsync<{NombreClase}, dynamic>({NombreClase}.QueryBase + ");
			SBuilder.AppendLine($"\t\t\t\t\t\t\t\t\t \" Where {Listado[0].NombreAtributo} = @{Listado[0].NombreAtributo}\", new {{ {Listado[0].NombreAtributo} = ID}} );");
			SBuilder.AppendLine("\t\t switch (Modo)");
            SBuilder.AppendLine("\t\t { ");
            // Codigo para el modo Ver
            SBuilder.AppendLine("\t\t //ModoEdicion Ver");
            SBuilder.AppendLine("\t\t\t case \"Ver\":");            
            SBuilder.AppendLine("\t\t\t\tIsModoVer = false;");
            SBuilder.AppendLine("\t\t\t\t SoloLectura=true;");
            SBuilder.AppendLine("");           
            SBuilder.AppendLine($"\t\t\t\t {NombreClase.ToLower()}={NombreClase.ToLower()}Query.FirstOrDefault();");
            SBuilder.AppendLine("\t\t\t\t // Agregue codigo para usar el objeto");
            SBuilder.AppendLine("\t\t\t\t break;");
            SBuilder.AppendLine("");
            string[] ModoAImripir = new string[] {"Insertar","Editar","Eliminar"};
            for (int i = 0; i < 3; i++)
            {
                SBuilder.AppendLine($"\t\t // ModoEdicion {ModoAImripir[i]}");
                SBuilder.AppendLine($"\t\t\t case \"{ModoAImripir[i]}\":");             
               
                //Verificar que ModoImprimir del arreglo. Si distinto a Eliminar Boton=Guardar, sino Eliminar
                if (ModoAImripir[i] != "Eliminar") 
                {
					SBuilder.AppendLine($"\t\t\t\t {NombreClase.ToLower()}={NombreClase.ToLower()}Query.FirstOrDefault();");
					SBuilder.AppendLine("\t\t\t\tTituloBotonAccion = \"Guardar\";");
				    SBuilder.AppendLine("\t\t\t\tIsModoVer = true;");
                }
                else
                {
					SBuilder.AppendLine($"\t\t\t\t {NombreClase.ToLower()}={NombreClase.ToLower()}Query.FirstOrDefault();");
					SBuilder.AppendLine("\t\t\t\tTituloBotonAccion = \"Eliminar\";");
					SBuilder.AppendLine("\t\t\t\tIsModoVer = true;");
				}

				SBuilder.AppendLine("\t\t\t\t // Agregue el codigo si es necesario");
                SBuilder.AppendLine();
                SBuilder.AppendLine("\t\t\t\t break;");
            }

            // Codigo default si es necesario
            SBuilder.AppendLine("\t\t\tdefault:");
            SBuilder.AppendLine("\t\t\t\t //Agregue código para el caso por defecto");
            SBuilder.AppendLine("\t\t\t\t break;");
            SBuilder.AppendLine("\t\t }");
            SBuilder.AppendLine("\t}");

            return SBuilder;
        }
        public static StringBuilder MetodoAccionModo(StringBuilder sbuilder, string NombreClase)
        {
            StringBuilder SBuilder = sbuilder;

            SBuilder.AppendLine("");
            SBuilder.AppendLine("\t\t// Método para accionar por Modo");
            SBuilder.AppendLine("\t\tpublic async void AccionModo()");
            SBuilder.AppendLine("\t\t{");
            SBuilder.AppendLine("\t\t\tswitch (Modo)");
            SBuilder.AppendLine("\t\t\t{");
            // Método Insertar
            SBuilder.AppendLine("\t\t\t\tcase \"Insertar\":");
            SBuilder.AppendLine("\t\t\t\t\ttry");
            SBuilder.AppendLine("\t\t\t\t\t{");
            // TODO: Tengo que controlar que si hay un campo booleano al insertar tengo que verificar que si no se marco el campo tome el valor de false

			
			SBuilder.AppendLine($"\t\t\t\t\t\tawait db.EjecutarQueryAsync<{NombreClase}>({NombreClase}.InsertQuery, {NombreClase.ToLower()});");
            SBuilder.AppendLine("\t\t\t\t\t\tthis.MudDialog.Close(DialogResult.Ok(true));");
            SBuilder.AppendLine("\t\t\t\t\t\tSnackbar.Add(\"Se guardó correctamente\", Severity.Success);");
            SBuilder.AppendLine("\t\t\t\t\t}");
            SBuilder.AppendLine("\t\t\t\t\tcatch (Exception e)");
            SBuilder.AppendLine("\t\t\t\t\t{");
            SBuilder.AppendLine("\t\t\t\t\t\tSnackbar.Add(\"Error al ejecutar la inserción\", Severity.Error);");
            SBuilder.AppendLine("\t\t\t\t\t\tLog.Error(e.Message);");
            SBuilder.AppendLine("\t\t\t\t\t}");
            SBuilder.AppendLine("\t\t\t\t\tbreak;");
            // Método Editar
            SBuilder.AppendLine("\t\t\t\tcase \"Editar\":");
            SBuilder.AppendLine("\t\t\t\t\ttry");
            SBuilder.AppendLine("\t\t\t\t\t{");
            SBuilder.AppendLine($"\t\t\t\t\t\tawait db.EjecutarQueryAsync<{NombreClase}>({NombreClase}.UpdateQuery, {NombreClase.ToLower()});");
            SBuilder.AppendLine("\t\t\t\t\t\tthis.MudDialog.Close(DialogResult.Ok(true));");
            SBuilder.AppendLine("\t\t\t\t\t\tSnackbar.Add(\"Se editó correctamente\", Severity.Success);");
            SBuilder.AppendLine("\t\t\t\t\t}");
            SBuilder.AppendLine("\t\t\t\t\tcatch (Exception e)");
            SBuilder.AppendLine("\t\t\t\t\t{");
            SBuilder.AppendLine("\t\t\t\t\t\tSnackbar.Add(\"Error al ejecutar la actualización\", Severity.Error);");
            SBuilder.AppendLine("\t\t\t\t\t\tLog.Error(e.Message);");
            SBuilder.AppendLine("\t\t\t\t\t}");
            SBuilder.AppendLine("\t\t\t\t\tbreak;");
            // Método Eliminar
            SBuilder.AppendLine("\t\t\t\tcase \"Eliminar\":");
            SBuilder.AppendLine("\t\t\t\t\ttry");
            SBuilder.AppendLine("\t\t\t\t\t{");
            SBuilder.AppendLine($"\t\t\t\t\t\tawait db.EjecutarQueryAsync<{NombreClase}>({NombreClase}.DeleteQuery, {NombreClase.ToLower()});");
            SBuilder.AppendLine("\t\t\t\t\t\tthis.MudDialog.Close(DialogResult.Ok(true));");
            SBuilder.AppendLine("\t\t\t\t\t\tSnackbar.Add(\"Se eliminó correctamente\", Severity.Success);");
            SBuilder.AppendLine("\t\t\t\t\t}");
            SBuilder.AppendLine("\t\t\t\t\tcatch (Exception e)");
            SBuilder.AppendLine("\t\t\t\t\t{");
            SBuilder.AppendLine("\t\t\t\t\t\tSnackbar.Add(\"Error al ejecutar la eliminación\", Severity.Error);");
            SBuilder.AppendLine("\t\t\t\t\t\tLog.Error(e.Message);");
            SBuilder.AppendLine("\t\t\t\t\t}");
            SBuilder.AppendLine("\t\t\t\t\tbreak;");
            // Caso por defecto
            SBuilder.AppendLine("\t\t\t\tdefault:");
            SBuilder.AppendLine("\t\t\t\t\t// Agregue código para el caso por defecto");
            SBuilder.AppendLine("\t\t\t\t\tbreak;");
            SBuilder.AppendLine("\t\t\t}");

            SBuilder.AppendLine("\t\t}");

            return sbuilder;
        }

        public static StringBuilder ImprimirMetodoBotones(StringBuilder sbuilder)
        {
            StringBuilder SBuilder = sbuilder;

            SBuilder.AppendLine("");
            SBuilder.AppendLine("\t// Función para mostrar y renombrar los botones");
            SBuilder.AppendLine("\tpublic void Botones(string Modo,bool IsModoVer, string TituloBotonCancelar)");
            SBuilder.AppendLine("\t{");
            SBuilder.AppendLine("\t\tif (Modo == \"Ver\")");
            SBuilder.AppendLine("\t\t{");
            SBuilder.AppendLine("\t\t\tIsModoVer = true;");
            SBuilder.AppendLine("\t\t\tTituloBotonCancelar = \"Cerrar\";");
            SBuilder.AppendLine("\t\t}");
            SBuilder.AppendLine("\t\telse if (Modo == \"Insertar\" || Modo == \"Editar\")");
            SBuilder.AppendLine("\t\t{");
            SBuilder.AppendLine("\t\t\tIsModoVer = false;");
            SBuilder.AppendLine("\t\t\tTituloBotonAccion = \"Guardar\";");
            SBuilder.AppendLine("\t\t\tTituloBotonCancelar = \"Cancelar\";");
            SBuilder.AppendLine("\t\t}");
            SBuilder.AppendLine("\t\telse");
            SBuilder.AppendLine("\t\t{");
            SBuilder.AppendLine("\t\t\tIsModoVer = false;");
            SBuilder.AppendLine("\t\t\tTituloBotonAccion = \"Eliminar\";");
            SBuilder.AppendLine("\t\t\tTituloBotonCancelar = \"Cancelar\";");
            SBuilder.AppendLine("\t\t}");
            SBuilder.AppendLine("\t}");

            return SBuilder;
        }

       
        public static StringBuilder ImprimirMetodoCancelacion(StringBuilder sbuilder)
        {
            StringBuilder SBuilder = sbuilder;
            SBuilder.AppendLine("");
            SBuilder.AppendLine("  void Cancelar() => MudDialog.Cancel();");

            return sbuilder;
        }
    }
}
