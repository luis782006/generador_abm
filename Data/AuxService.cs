
using MudBlazor.Interfaces;

namespace Generador_ABM.Data
{
    public class AuxService
    {
        
        public List<Clase> ListaAtributos { get; set; }

        public List<Clase> Listado;

        public string NameSpace, NombreClase;

        public string Ruta;

        public void ObtenerAtributos(List<Clase> ListaAtributos,string PNameSpace,string PNombreClase, string rutaAcrhivo)
        {
             Listado = ListaAtributos;  
             NameSpace=PNameSpace;
             NombreClase=PNombreClase;
             Ruta = rutaAcrhivo;
            //foreach (var item in Listado)
            //{
            //    Console.WriteLine(item.NombreAtributo);
            //}
        }
    }
}
