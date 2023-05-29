//Importo la Clase con el enum para traer el tipo de dato
using System;
using static Generador_ABM.Data.EnumTipoDato;

namespace Generador_ABM.Data
{
    public  class Clase
    {
        public string? NombreAtributo { get; set; }
        //Dataype Enumerador
        public DataType TipoDato { get; set; }
        public bool AceptaNull { get; set; }
        public string? ValorPorDefecto { get; set; }
        public bool IsExpanded { get; set; } = false;
        public Clase() { }

        public List<Clase> listaAtributos=new List<Clase>();
        public void AgregarAtributo(string nombreAtributo,DataType tipoDatoSeleccionado,bool aceptaNull, string valorPorDefecto)
        {
            listaAtributos.Add(new Clase
            {
                NombreAtributo = nombreAtributo,
                TipoDato = tipoDatoSeleccionado,
                AceptaNull = aceptaNull,
                ValorPorDefecto = valorPorDefecto
            });
        }
    }
}
