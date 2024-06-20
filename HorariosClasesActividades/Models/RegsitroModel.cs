using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorariosClasesActividades.Models
{
    public enum Categorias { Actividad, Clase}
    public enum Dias { Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Domingo }

    [Table("ClasesActividades")]
    public class RegsitroModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull, MaxLength(50)]
        public string Nombre { get; set; } = "";
        [NotNull]
        public Dias Dia { get; set; }
        [NotNull]
        public int Hora { get; set; }
        public string Docente { get; set; } = "";
        [MaxLength(80)]
        public string Descripcion { get; set; } = "";
        [NotNull]
        public Categorias Categoria { get; set; }

    }
}
