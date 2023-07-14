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
            sbuilderPop.AppendLine("\t <MudForm @bind-IsValid=\"valid\">");
            int indice = 0;
            foreach (Clase clase in Listado)
            {
                switch (clase.TipoDato)
                {   
                    case EnumTipoDato.DataType.String:
                        SBuilder.AppendLine("\t\t < MudTextField T = \"string\"");
                        break;
                    case EnumTipoDato.DataType.Int:
                        SBuilder.AppendLine("\t\t < MudTextField T = \"int\"");
                        break;
                    case EnumTipoDato.DataType.Double:
                        SBuilder.AppendLine("\t\t < MudTextField T = \"double\"");
                        break;
                    case EnumTipoDato.DataType.Bool:
                        SBuilder.AppendLine("\t\t < MudTextField T = \"bool\"");
                        break;
                    case EnumTipoDato.DataType.DateTime:
                        SBuilder.AppendLine("<\t\t\t MudTextField ");
                        SBuilder.AppendLine("\t\t\t\t Format=\"dd/MM/yyyy \"");
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
                        SBuilder.AppendLine("<\t\t\t MudTextField T = \"long\"");
                        break;
                    //case EnumTipoDato.DataType.UShort:
                    //    break;
                    //default:
                        break;
                }
                //Variable indice para hacer imprimir Autofous en el primer atributo
                if (indice == 0)
                {
                    SBuilder.AppendLine("\t\t\t AutoFocus= true");                
                }
                SBuilder.AppendLine($"\t\t\t\t @bind-Value=\"{NombreClase.ToLower()}{clase.NombreAtributo}\" ");
                SBuilder.AppendLine($"\t\t\t\t Label=\"{clase.NombreAtributo}\"");
                SBuilder.AppendLine("\t\t\t\t Variant=\"Variant.Outlined\"");
                SBuilder.AppendLine("\t\t\t\t ReadOnly=\"@SoloLectura\"");
                SBuilder.AppendLine("\t\t\t\t Margin=\"Margin.Dense\"");
                SBuilder.AppendLine("\t\t\t\t Required=\"true\"");
                SBuilder.AppendLine($"\t\t\t\t RequiredError=\"Ingrese su texto de error\"");
                SBuilder.AppendLine("\t\t\t\t Immediate=\"true\" />");
                SBuilder.AppendLine("");
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
            SBuilder.AppendLine("\t\t @if (@IsModoVer)");
            SBuilder.AppendLine("\t\t {");
            SBuilder.AppendLine("\t\t\t <MudButton OnClick=\"Cancelar\" ");
            SBuilder.AppendLine("\t\t\t\t Color=\"Color.Warning\"");
            SBuilder.AppendLine("\t\t\t\t Variant=\"Variant.Outlined\"");
            SBuilder.AppendLine("\t\t\t\t StartIcon=\"@Icons.Filled.Cancel\">Cancelar");
            SBuilder.AppendLine("\t\t\t </MudButton>");
            SBuilder.AppendLine("\t\t }");
            SBuilder.AppendLine("");

            //Boton según valor de parametro de ModoEdicion. Tener en cuenta si llega Id<>0 boton text sera Actualizar o Eliminar. Si ModoEdicion Ver entonces ocultar botón 
            SBuilder.AppendLine("\t\t <MudButton  @onclick=\"@((e)=>guardarNotificacion())\"");
            SBuilder.AppendLine("\t\t\t Color=\"Color.Success\"");
            SBuilder.AppendLine("\t\t\t Variant=\"Variant.Filled\"");
            SBuilder.AppendLine("\t\t\t StartIcon=\"@Icons.Material.Filled.Save\">");
            SBuilder.AppendLine($"\t\t @TituloBotonCancelar");
            SBuilder.AppendLine("\t\t </MudButton>");
            SBuilder.AppendLine("</DialogActions>");

            return SBuilder;
        }

        public static StringBuilder ImprimirDialogVariable(StringBuilder sbuilder, string NombreClase)
        {
            StringBuilder SBuilder = sbuilder;
            SBuilder.AppendLine("\t //Todo Dialog-----");
            SBuilder.AppendLine("\t [CascadingParameter] MudDialogInstance MudDialog { get; set; }");
            SBuilder.AppendLine("\t [Parameter] public ModoEdicion Modo { get; set; }");
            SBuilder.AppendLine("\t [Parameter] public long ID {get; set;}");
            SBuilder.AppendLine("");
            SBuilder.AppendLine("\t // INSTANCIA DE OBJETO");
            SBuilder.AppendLine($"\t\t {NombreClase} {NombreClase.ToLower()}=new {NombreClase}();");
            SBuilder.AppendLine("");
            SBuilder.AppendLine("\t\t private bool SoloLectura = false;");
            SBuilder.AppendLine("\t\t bool valid=false");
            SBuilder.AppendLine("\t\t private string TituloBotonCancelar=\"\"");
            SBuilder.AppendLine("\t\t private bool IsModoVer=false;");

            return SBuilder;
        }

        public static StringBuilder ImprimirMetodoOnInitialized(StringBuilder sbuilder,string NombreClase,List<Clase> Listado)
        {
            StringBuilder SBuilder = sbuilder;

            SBuilder.AppendLine("");
            SBuilder.AppendLine("//Cargar Formulario. ModoEdicion Ver");
            SBuilder.AppendLine("\t protected override async Task OnInitializedAsync()");
            SBuilder.AppendLine("\t {");
            SBuilder.AppendLine("\t\t if (Modo == ModoEdicion.Ver)");
            SBuilder.AppendLine("\t\t {");
            SBuilder.AppendLine("\t\t IsModoVer=\"true\"");
            SBuilder.AppendLine("\t\t TituloBotonCancelar=\"Cerrar\"");
            SBuilder.AppendLine($"\t\t\t var {NombreClase.ToLower()}Query=await db.ObtenerListadoAsync<{NombreClase}, dynamic>({NombreClase}.QueryBase + ");
            SBuilder.AppendLine($"\t\t\t\t\t\t\t\t\t\t\t \" Where {Listado[0].NombreAtributo} = @{Listado[0].NombreAtributo}\", new {Listado[0].NombreAtributo} = ID );");
            SBuilder.AppendLine("");
            SBuilder.AppendLine($"\t\t\t {NombreClase.ToLower()}={NombreClase.ToLower()}Query.FirstOrDefault();");
            SBuilder.Append("\t\t // Agregue codigo para usar el objeto");
            SBuilder.AppendLine("\t\t\t }");
            SBuilder.AppendLine("\t\t }");
            SBuilder.AppendLine("\t }");


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
