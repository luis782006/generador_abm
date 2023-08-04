using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;
using static MudBlazor.CategoryTypes;

namespace Generador_ABM.Data
{
    public class ManejoComponenteLista
    {
	
		public static StringBuilder ImprimirInicio(StringBuilder objSBuilder, string NameSpace,int identificador,bool IsDialog,string link)
        {
            StringBuilder SBuilder = objSBuilder;
            if (!IsDialog)
            {
				SBuilder.AppendLine($"@page \"/{link}\"");       

            }
            SBuilder.AppendLine("@using System;");
            SBuilder.AppendLine();
            SBuilder.AppendLine("@using System.Threading;");
            SBuilder.AppendLine("@inject IDataAccess db;");
            SBuilder.AppendLine("@using Serilog;");
            SBuilder.AppendLine($"@using {NameSpace}");
            SBuilder.AppendLine("@inject NavigationManager nav;");
            SBuilder.AppendLine("@inject IDialogService DialogService;");
            SBuilder.AppendLine("@using Microsoft.Extensions.Logging;");
            SBuilder.AppendLine("@inject ISnackbar Snackbar;");           
            SBuilder.AppendLine("");
            if (identificador != 1)
            {
                SBuilder.AppendLine($"namespace {NameSpace}");
                SBuilder.AppendLine("{");

            }

            return SBuilder;
        }
        public static StringBuilder ImprimolBarContent(StringBuilder sBuilder,string NombreClase)
        {
            StringBuilder SBuilder = sBuilder;
            SBuilder.AppendLine($"<MudText Typo=\"Typo.h4\">Listado {NombreClase}</MudText>");
            SBuilder.AppendLine("<MudPaper Class=\"d-flex justify-content-center p-4\" Width=\"100%\">");
            SBuilder.AppendLine("\t <MudPaper style=\"width:80rem;\" >");
            SBuilder.AppendLine($"\t @if (Cargando)");
            SBuilder.AppendLine("\t{");
            SBuilder.AppendLine("\t\t<MudGrid Style=\"text-align:center;\">");
            SBuilder.AppendLine("\t\t\t<MudItem sm=\"12\">");
            SBuilder.AppendLine("\t\t\t\t<MudText Typo=\"Typo.h6\">Cargando registros....</MudText>");
            SBuilder.AppendLine("\t\t\t\t<MudProgressCircular Color=\"Color.Primary\" Style=\"height:70px;width:70px;\" Indeterminate=\"true\" />");
            SBuilder.AppendLine("\t\t\t</MudItem>");
            SBuilder.AppendLine("\t\t</MudGrid>");
            SBuilder.AppendLine("\t}");
            SBuilder.AppendLine("\telse");
            SBuilder.AppendLine("\t{"); // apertura del else. El cierre esta en el Metodo ImprimirPaginador quien hace el cierre
            SBuilder.AppendLine($"\t<MudTable Items=\"@lst{NombreClase}\" Breakpoint=\"Breakpoint.Sm\" style=\"width:80%;padding:1rem;\" Dense=\"true\" Hover=\"true\" Bordered=\"false\" Striped=\"true\" @ref=\"table\" FixedHeader=\"true\" Filter=\"new Func<{NombreClase},bool>(FilterFunc1)\">");
            SBuilder.AppendLine("\t <ToolBarContent>");
            SBuilder.AppendLine("\t\t <MudTextField T=\"string\" @bind-Value=\"searchString1\" style=\"width: 300px;\" Placeholder=\"Buscar...\" Adornment=\"Adornment.Start\" AdornmentIcon=\"@Icons.Material.Filled.Search\" IconSize=\"Size.Medium\" Class=\"mt-0\"></MudTextField>");
            SBuilder.AppendLine($"\t\t\t<MudTooltip Text=\"Agregar {NombreClase}\"><MudFab Size=\"Size.Small\" Variant=\"Variant.Filled\" StartIcon=\"@Icons.Material.Filled.Add\" Color=\"Color.Success\" OnClick=\"@((e) => Accion{NombreClase}(maxWidth,\"Insertar\",0))\" /></MudTooltip>");
            SBuilder.AppendLine("\t</ToolBarContent>");

            return SBuilder;
        }


        public static StringBuilder  ImprimirCabecera(List<Clase> Listado, StringBuilder objSBuilder, string NombreClase)
        {
            StringBuilder SBuilder = objSBuilder;

            SBuilder.AppendLine("\t\t\t<MudTh>Acciones</MudTh>");

            foreach (Clase item in Listado)
            {
                if (item.TipoDato.ToString() == "String" || item.TipoDato.ToString()=="DateTime")
                {
                    SBuilder.AppendLine("\t\t\t" + $"<MudTh><MudTableSortLabel SortBy=\"new Func<{NombreClase}, object?>(x=>x.{item.NombreAtributo})\">{item.NombreAtributo}</MudTableSortLabel></MudTh>");
                }
                else
                {
					SBuilder.AppendLine("\t\t\t" + $"<MudTh>{item.NombreAtributo}</MudTh>");
				}

            }
            return SBuilder;
        }

        public static StringBuilder Acciones(List<Clase> Listado, StringBuilder objSBuilder, bool ver,bool editar,bool eliminar,string NombreClase)
        {
            StringBuilder SBuilder = objSBuilder;

            if (ver)
            {
                SBuilder.AppendLine($"\t\t\t<MudTooltip Text = \"Ver {NombreClase}\">");
                SBuilder.AppendLine($"\t\t\t\t<MudIconButton Size=\"Size.Small\" Icon=\"@Icons.Material.Filled.Search\" Color=\"Color.Info\" OnClick=\"@((e) => Accion{NombreClase}(maxWidth,\"Ver\",@context.{Listado[0].NombreAtributo}))\" />");
                SBuilder.AppendLine("\t\t\t</MudTooltip>");
            }
            if (editar)
            {
                SBuilder.AppendLine($"\t\t\t<MudTooltip Text = \"Editar {NombreClase}\">");
                SBuilder.AppendLine($"\t\t\t\t<MudIconButton Size=\"Size.Small\" Icon=\"@Icons.Material.Filled.Edit\" Color=\"Color.Success\" OnClick=\"@((e) => Accion{NombreClase}(maxWidth,\"Editar\",@context.{Listado[0].NombreAtributo}))\" />");
                SBuilder.AppendLine("\t\t\t</MudTooltip>");
            }
            if (eliminar)
            {
                SBuilder.AppendLine($"\t\t\t<MudTooltip Text = \"Eliminar {NombreClase}\">");
                SBuilder.AppendLine($"\t\t\t<MudIconButton Size=\"Size.Small\" Icon=\"@Icons.Material.Filled.Delete\" Color=\"Color.Error\" OnClick=\"@((e) => Accion{NombreClase}(maxWidth,\"Eliminar\",@context.{Listado[0].NombreAtributo}))\" />");
                SBuilder.AppendLine("\t\t\t</MudTooltip>");
            }

            return SBuilder;
        }

        public static StringBuilder ImprimirColumnas(List<Clase> Listado, StringBuilder objBuilder)
        {
            StringBuilder SBuilder = objBuilder;
            // Controlo los tipos de datos que llegan
            foreach (Clase item in Listado)
            {               
                switch (item.TipoDato)
                {
                    case EnumTipoDato.DataType.String:  
                    case EnumTipoDato.DataType.Int: // modificar para usar Mud numerico
                    case EnumTipoDato.DataType.Long:
                    case EnumTipoDato.DataType.Double:
                    case EnumTipoDato.DataType.Decimal:
                    case EnumTipoDato.DataType.Float: 
                    case EnumTipoDato.DataType.Short: 
                    case EnumTipoDato.DataType.Byte: 
                    case EnumTipoDato.DataType.SByte:
					case EnumTipoDato.DataType.UInt:
                    case EnumTipoDato.DataType.UShort:
					case EnumTipoDato.DataType.ULong:
						SBuilder.AppendLine("\t\t\t\t" + $"<MudTd style=\"height: auto; width: auto;\" DataLabel=\"{item.NombreAtributo}\">@context.{item.NombreAtributo}</MudTd>");
                        break;
                    case EnumTipoDato.DataType.Bool:
						SBuilder.AppendLine("\t\t\t\t" + $"<MudCheckBox style=\"height: auto; width: auto;\" @bind-Checked=\"@context.{item.NombreAtributo}\"></MudCheckBox>");
                        break;
                    case EnumTipoDato.DataType.DateTime:
						SBuilder.AppendLine("\t\t\t\t" + $"<MudTd style=\"height: auto; width: auto;\" DataLabel=\"{item.NombreAtributo}\">@context.{item.NombreAtributo}?.ToString(\"dd/MM/yyyy\")</MudTd>");
                        break;
					default:
                        break;
                }
			}

            return SBuilder;
        }

		public static StringBuilder ImprimirPaginador(StringBuilder objBuilder, string infoFormat)
		{
			StringBuilder SBuilder = objBuilder;

			SBuilder.AppendLine($"\t\t\t<MudTablePager RowsPerPageString=\"Filas por Pagina.\" InfoFormat=\"@($\"Filas {{infoFormat}}\")\" HorizontalAlignment=\"HorizontalAlignment.Center\" PageSizeOptions=\"new int[] {{ 25, 50, 100 }}\" />");
			SBuilder.AppendLine("\t\t</PagerContent>");
			SBuilder.AppendLine("\t</MudTable>");
            SBuilder.AppendLine("}");// Este esl cierre del if para mostrar ProgressCircular o la table.
            SBuilder.AppendLine("</MudPaper>");
			SBuilder.AppendLine("</MudPaper>");

			return SBuilder;
		}

		public static StringBuilder MetodoFiltro(string NombreClase, List<Clase> Listado, StringBuilder objBuilder)
        {
			StringBuilder sBuilder = objBuilder;

			sBuilder.AppendLine($"\tprivate bool FilterFunc1({NombreClase} element) => FilterFunc(element, searchString1);");
			sBuilder.AppendLine();
			sBuilder.AppendLine($"\tprivate bool FilterFunc({NombreClase} element, string searchString)");
			sBuilder.AppendLine("{");
			sBuilder.AppendLine("\t\tif (string.IsNullOrWhiteSpace(searchString))");
			sBuilder.AppendLine("\t\t\treturn true;");
			sBuilder.AppendLine();

			foreach (Clase item in Listado)
            {              
                if (item.TipoDato==EnumTipoDato.DataType.String || item.TipoDato ==EnumTipoDato.DataType.DateTime)
                {
                    sBuilder.AppendLine($"\t\t if (element.{item.NombreAtributo}.ToString().Trim().Contains(searchString, StringComparison.OrdinalIgnoreCase))");
                    sBuilder.AppendLine($"\t\t\t return true;");

                }
            }
            sBuilder.AppendLine("\t\t\t return false;");
            sBuilder.AppendLine("\t\t }");
            return sBuilder;
        }         

        public static StringBuilder ImprimirVariable(StringBuilder sbuilder, string NombreClase)
        {
            StringBuilder SBuilder= sbuilder;

            SBuilder.AppendLine("@code {");
            SBuilder.AppendLine("\t //Manejo para TABLE Lista MudBlazor");
            SBuilder.AppendLine($"\t\t private MudTable<{NombreClase}> table;");
            SBuilder.AppendLine("\t\t private string searchString = null;");
            SBuilder.AppendLine("\t\t private string searchString1 = null;");
            SBuilder.AppendLine("\t\t private string infoFormat = \"{first_item}-{last_item} de {all_items}\";");
            SBuilder.AppendLine("\t\t // OPciones de Popup Dialog");
            SBuilder.AppendLine("\t\t DialogOptions maxWidth = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true };");
            SBuilder.AppendLine("\t\t private int totalItems;");
            SBuilder.AppendLine("private bool Cargando = true;");
            SBuilder.AppendLine("private bool ExitoDeCarga = true");

            SBuilder.AppendLine("");
            SBuilder.AppendLine($"\t\t List<{NombreClase}> lst{NombreClase} =new List<{NombreClase}>();");

            return SBuilder;
        }



        public static StringBuilder ImprimirAccionDialog(StringBuilder sbuilder, string NombreClase,List<Clase> lstAtributos)
        {
            
            

			StringBuilder SBuilder = sbuilder;

            SBuilder.AppendLine($"\t private async void Accion{NombreClase}(DialogOptions options,string Modo,{lstAtributos[0].TipoDato.ToString().ToLower()} ID)");
            SBuilder.AppendLine("\t {");
            SBuilder.AppendLine("\t\t var parameters = new DialogParameters();");
            SBuilder.AppendLine("\t\t parameters.Add(\"ID\", ID);");
            SBuilder.AppendLine("\t\t parameters.Add(\"Modo\", Modo);");     //$"{Modo.ToUpper()}
			SBuilder.AppendLine($"\t\t\t var dialog = DialogService.Show<D_{NombreClase}>($\"{{Modo.ToUpper()}} {NombreClase.ToUpper()}\", parameters, options);");
            SBuilder.AppendLine("\t\t var result = await dialog.Result;");
            SBuilder.AppendLine("\t\t if (!result.Canceled)");
            SBuilder.AppendLine("\t\t {");      
            SBuilder.AppendLine("\t\t\ttry");
            SBuilder.AppendLine("\t\t\t{");
            SBuilder.AppendLine($"\t\t\t\t lst{NombreClase} = await db.ObtenerListadoAsync<{NombreClase}, dynamic>({NombreClase}.QueryBase, new {{ }});");
            SBuilder.AppendLine("\t\t\t\t StateHasChanged();");           
            SBuilder.AppendLine("\t\t\t}");
            SBuilder.AppendLine("\t\t\tcatch(Exception e)");
            SBuilder.AppendLine("\t\t\t{");
			SBuilder.AppendLine("\t\t\t\tSnackbar.Add(\"Error al cargar los datos\", Severity.Error);");
			SBuilder.AppendLine("\t\t\t\tLog.Error(e.Message);");            
            SBuilder.AppendLine("\t\t\t}");
			SBuilder.AppendLine("\t\t}");
            SBuilder.AppendLine("\t }");

            return SBuilder;
        }

		public static StringBuilder MetodoCargaLista(string NombreClase, StringBuilder objBuilder)
		{
			StringBuilder SBuilder = objBuilder;

			SBuilder.AppendLine("\t\t protected override async Task OnAfterRenderAsync(bool firstRender)");
			SBuilder.AppendLine("\t\t {");
			SBuilder.AppendLine("\t\t\t //Cargo lista de Objetos de Clases");
            SBuilder.AppendLine("\t\t\t if (firstRender)");
            SBuilder.AppendLine("\t\t\t{");
			SBuilder.AppendLine("\t\t\t\t await CargarDatos();");
            SBuilder.AppendLine("\t\t\t\t Cargando = false;");
			SBuilder.AppendLine("\t\t\t\t StateHasChanged();");
            SBuilder.AppendLine("\t\t\t\t if (!ExitoDeCarga)");
            SBuilder.AppendLine("\t\t\t\t{");
            SBuilder.AppendLine("\t\t\t\t\tCargando = true;");
            SBuilder.AppendLine("\t\t\t\t\tStateHasChanged();");
            SBuilder.AppendLine("\t\t\t\t}");
			SBuilder.AppendLine("\t\t\t}");
			SBuilder.AppendLine("\t\t\t //Agregue el codigo necesario");
			SBuilder.AppendLine("\t\t }");
			SBuilder.AppendLine("");

			return SBuilder;
		}
        public static StringBuilder MetodoCargarDatos(string NombreClase, StringBuilder objBuilder)
        {
			StringBuilder SBuilder = objBuilder;

            SBuilder.AppendLine("\tpublic async Task CargarDatos()");
			SBuilder.AppendLine("\t{");
			SBuilder.AppendLine("\t\ttry");
			SBuilder.AppendLine("\t\t{");
			SBuilder.AppendLine($"\t\t\t lst{NombreClase}= await db.ObtenerListadoAsync<{NombreClase}, dynamic>({NombreClase}.QueryBase, new {{ }});");
			SBuilder.AppendLine("\t\t\t StateHasChanged();");            
            SBuilder.AppendLine("\t\t}");
            SBuilder.AppendLine("\t\tcatch(Exception e)");
			SBuilder.AppendLine("\t\t{");
			SBuilder.AppendLine("\t\t\tSnackbar.Add(\"Error al cargar los datos\", Severity.Error);");
			SBuilder.AppendLine("\t\t\tExitoDeCarga = false;");
			SBuilder.AppendLine("\t\t\tLog.Error(e.Message);");
			SBuilder.AppendLine("\t\t}");
			SBuilder.AppendLine("\t}");
            return SBuilder;
		}
   
	}
}
