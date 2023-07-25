using System;

namespace Generador_ABM.Data
{
	 public class Persona
	 {
		 public decimal ID_Persona { get; set; }
		 public string? Nombre { get; set; }
		 public string? Apellido { get; set; }
		 public int? Edad { get; set; }
		 public bool? Trabaja { get; set; }=false;
		 public DateTime? Cumpleanos { get; set; }

		 public const string QueryBase="SELECT ID_Persona,Nombre,Apellido,Edad,Trabaja,Cumpleanos FROM  Persona ";

		 public const string InsertQuery="INSERT INTO  Persona (Nombre,Apellido,Edad,Trabaja,Cumpleanos) OUTPUT INSERTED.ID_Persona VALUES (@Nombre,@Apellido,@Edad,@Trabaja,@Cumpleanos);";

		 public const string UpdateQuery="UPDATE Persona SET Nombre=@Nombre,Apellido=@Apellido,Edad=@Edad,Trabaja=@Trabaja,Cumpleanos=@Cumpleanos WHERE ID_Persona=@ID_Persona;";

		 public const string DeleteQuery="DELETE FROM Persona WHERE ID_Persona=@ID_Persona;";

		 public override string ToString()
		 {
			 	return $"{{"
					+ $"\"ID_Persona\" : { ID_Persona } , " 
					+ $"\"Nombre\" : { Nombre } , " 
					+ $"\"Apellido\" : { Apellido } , " 
					+ $"\"Edad\" : { Edad } , " 
					+ $"\"Trabaja\" : { Trabaja } , " 
					+ $"\"Cumpleanos\" : { Cumpleanos:u} , " 
					+ $"}}";
		 }

		 public override bool Equals(object o)
		 { 
			  var other = o as Persona ; 
			 return other?.ID_Persona==ID_Persona;
		}

		 public override int GetHashCode() => ID_Persona.GetHashCode();
	}
}

