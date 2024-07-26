using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Metas.Models
{
    public class Tareas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTarea { get; set; }
        public string NombreTarea { get; set; }
        public int IdEstatus { get; set; }
        public int IdMeta { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
