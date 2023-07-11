using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;
using static MudBlazor.CategoryTypes;

namespace Generador_ABM.Data
{
    public class ManejoComponenteLista
    {
        public static StringBuilder ImprimirInicio(StringBuilder objSBuilder, string NameSpace,string link)
        {
            StringBuilder SBuilder = objSBuilder;

            SBuilder.AppendLine($"@page \"/{link}\"");
            SBuilder.AppendLine("using System;");
            SBuilder.AppendLine();
            SBuilder.AppendLine("@using System.Threading;");
            SBuilder.AppendLine("@inject NavigationManager nav");
            SBuilder.AppendLine("@inject IDialogService DialogService");
            SBuilder.AppendLine("@implements IDisposable");
            SBuilder.AppendLine($"namespace {NameSpace}");

            return SBuilder;
        }
        public static StringBuilder ImprimolBarContent(StringBuilder sBuilder,string NombreClase)
        {
            StringBuilder SBuilder = sBuilder;

            SBuilder.AppendLine("{");
            SBuilder.AppendLine($"<MudTable Items={NombreClase} Dense=\"true\" Hover=\"true\" Bordered=\"false\" Striped=\"true\" @ref=\"table\" FixedHeader=\"true\" Filter=\"new Func<{NombreClase},bool>(FilterFunc1)\" ");
            SBuilder.AppendLine("\t <ToolBarContent>");
            SBuilder.AppendLine("\t\t <MudTextField T=\"string\" @bind-Value=\"searchString1\" style=\"width: 300px;\" Placeholder=\"Buscar...\" Adornment=\"Adornment.Start\" AdornmentIcon=\"@Icons.Material.Filled.Search\" IconSize=\"Size.Medium\" Class=\"mt-0\"></MudTextField>");
            SBuilder.AppendLine($"\t\t\t<MudTooltip Text=\"Agregar {NombreClase}\"><MudFab Size=\"Size.Small\" Variant=\"Variant.Filled\" Icon=\"@Icons.Material.Filled.Add\" Color=\"Color.Success\" OnClick=\"@(e) => NuevoVExpediente(maxWidth))\" /></MudTooltip>");
            SBuilder.AppendLine("\t</ToolBarContent>");

            return SBuilder;
        }


        public static StringBuilder  ImprimirCabecera(List<Clase> Listado, StringBuilder objSBuilder)
        {
            StringBuilder SBuilder = objSBuilder;

            SBuilder.AppendLine("\t\t\t<MudTh></MudTh>");

            foreach (Clase item in Listado)
            {
                SBuilder.AppendLine("\t\t\t" + $"<MudTh>{item.NombreAtributo}</MudTh>");

            }
            return SBuilder;
        }
        public static StringBuilder Acciones(List<Clase> Listado, StringBuilder objSBuilder, bool ver,bool editar,bool eliminar,string NombreClase)
        {
            StringBuilder SBuilder = objSBuilder;

            if (ver)
            {
                SBuilder.AppendLine($"\t\t\t<MudTooltip Text = \"Ver {NombreClase}\">");
                SBuilder.AppendLine($"\t\t\t\t<MudIconButton Size=\"Size.Small\" Icon=\"@Icons.Material.Filled.History\" Color=\"Color.Info\" OnClick=\"@((e) => Ver{NombreClase}(maxWidth,@context.{Listado[0].NombreAtributo}))\" />");
                SBuilder.AppendLine("\t\t\t</MudTooltip>");
            }
            if (editar)
            {
                SBuilder.AppendLine($"\t\t\t<MudTooltip Text = \"Editar {NombreClase}\">");
                SBuilder.AppendLine($"\t\t\t\t<MudIconButton Size=\"Size.Small\" Icon=\"@Icons.Material.Filled.History\" Color=\"Color.Info\" OnClick=\"@((e) => Editar{NombreClase}(maxWidth,@context.{Listado[0].NombreAtributo}))\" />");
                SBuilder.AppendLine("\t\t\t</MudTooltip>");
            }
            if (eliminar)
            {
                SBuilder.AppendLine($"\t\t\t<MudTooltip Text = \"Eliminar {NombreClase}\">");
                SBuilder.AppendLine($"\t\t\t<MudIconButton Disabled=\"@SoloLeer(@context.TipoEstado)\"  Size=\"Size.Small\" Icon=\"@Icons.Material.Filled.DriveFileMove\" Color=\"Color.Error\" OnClick=\"@((e) => Eliminar{NombreClase}(maxWidth,@context.{Listado[0].NombreAtributo}");
                SBuilder.AppendLine("\t\t\t</MudTooltip>");
            }

            return SBuilder;
        }

        public static StringBuilder ImprimirColumnas(List<Clase> Listado, StringBuilder objBuilder)
        {
            StringBuilder SBuilder = objBuilder;
            
            foreach (Clase item in Listado)
            {
                SBuilder.AppendLine("\t\t\t\t" + $"<MudTd style=\"height: auto; width: auto;\" DataLabel=\"{item.NombreAtributo}\">@context.{item.NombreAtributo}</MudTd>");
                
            }

            return SBuilder;
        }

        public static StringBuilder MetodoCargaLista(string NombreClase,StringBuilder objBuilder)
        {
            StringBuilder SBuilder = objBuilder;

            SBuilder.AppendLine("\t\t protected override async Task OnInitializedAsync()");
            SBuilder.AppendLine("\t\t {");
            SBuilder.AppendLine("\t\t //Cargo lista de Objetos de Clases");
            SBuilder.AppendLine($"\t\t\t {NombreClase}= await db.ObtenerListadoAsync<{NombreClase}, dynamic>({NombreClase}.QueryBase, new {{ }});");            
            SBuilder.AppendLine("\t\t //Agregue el codigo necesario");
            SBuilder.AppendLine("\t\t }");
            SBuilder.AppendLine("");
        
            return SBuilder;    
        }

        public static StringBuilder MetodoFiltro(string NombreClase, List<Clase> Listado, StringBuilder objBuilder)
        {
            StringBuilder sBuilder =objBuilder;

            sBuilder.AppendLine($"\t\t private bool FilterFunc1({NombreClase} element) => FilterFunc(element, searchString1);");
            sBuilder.AppendLine("");
            sBuilder.AppendLine($"\t\t private bool FilterFunc({NombreClase} element, string searchString) ");
            sBuilder.Append("\t\t {");
            sBuilder.AppendLine("\t\t if (string.IsNullOrWhiteSpace(searchString))");
            sBuilder.AppendLine("\t\t\t return true;  ");
             foreach (Clase item in Listado)
            {
              
                if (item.TipoDato==EnumTipoDato.DataType.String || item.TipoDato ==EnumTipoDato.DataType.DateTime)
                {
                    sBuilder.AppendLine($"\t\t\t if (element.{item.NombreAtributo}.ToString().Trim().Contains(searchString, StringComparison.OrdinalIgnoreCase))");
                    sBuilder.AppendLine($"\t\t\t\t return true");
                    sBuilder.AppendLine("\t\t\t return true");

                }
            }
            sBuilder.AppendLine("\t\t }");
            return sBuilder;
        }
        public static StringBuilder MetodoBuscar(StringBuilder objBuilder)
        {
            StringBuilder sBuilder = objBuilder;
            sBuilder.AppendLine("\t\t private void OnSearch(string text)");
            sBuilder.AppendLine("\t\t {");
            sBuilder.AppendLine("\t\t\t searchString = text;");
            sBuilder.AppendLine("\t\t\t table.ReloadServerData();");

            sBuilder.AppendLine("\t\t }");

            return sBuilder;
        }

        public static StringBuilder ImprimirPaginador(StringBuilder objBuilder, string infoFormat) 
        {
            StringBuilder SBuilder = objBuilder;
            
            SBuilder.AppendLine($"\t\t\t<MudTablePager RowsPerPageString=\"Filas por Pagina.\" InfoFormat=\"@($\"Filas {infoFormat}\")\" HorizontalAlignment=\"HorizontalAlignment.Center\" PageSizeOptions=\"new int[] {{ 25, 50, 100 }}\" />");
            SBuilder.AppendLine("\t\t</PagerContent>");
            SBuilder.AppendLine("\t</MudTable>");
            SBuilder.AppendLine("</MudContainer>");

            return SBuilder;
        }

        public static StringBuilder ImprimirVariable(StringBuilder sbuilder, string NombreClase)
        {
            StringBuilder SBuilder= sbuilder;

            SBuilder.AppendLine("@code {");
            SBuilder.AppendLine("\t //Manejo para TABLE Lista MudBlazor");
            SBuilder.AppendLine("\t\t private MudTable<> table;");
            SBuilder.AppendLine("\t\t private string searchString = null;");
            SBuilder.AppendLine("\t\t private string searchString1 = null;");
            SBuilder.AppendLine("\t\t private string infoFormat = \"{first_item}-{last_item} de {all_items}\";");
            SBuilder.AppendLine("\t\t // OPciones de Popup Dialog");
            SBuilder.AppendLine("\t\t DialogOptions maxWidth = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true };");
            SBuilder.AppendLine("\t\t private int totalItems;");
            SBuilder.AppendLine("");
            SBuilder.AppendLine($"\t\t List<{NombreClase}> Lista{NombreClase} =new List<>()");

            return SBuilder;
        }
    }
}
