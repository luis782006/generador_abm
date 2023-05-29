using System.ComponentModel.DataAnnotations;

namespace Generador_ABM.Data
{
    public class EnumTipoDato
    {
        //Propiedades
        public string Name { get; set; }
        public DataType Type { get; set; }

        //Constructor
        public EnumTipoDato(string name, DataType type)
        {
            Name = name;
            Type = type;
        }

        //Enumerador con tipos de datos
        public enum DataType
        {
            String,
            Int,
            Double,
            Bool,
            Char,
            DateTime,
            Decimal,
            Float,
            Long,
            Short,
            Byte,
            SByte,
            UInt,
            ULong,
            UShort,
            Otro
        }

    }
}
