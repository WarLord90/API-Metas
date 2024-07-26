using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Metas.Models
{
    public class Metas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMeta { get; set; }
        public string NombreMeta { get; set; }
        public int IdEstatus { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
