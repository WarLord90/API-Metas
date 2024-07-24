using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Metas.Models
{
    public class Estatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEstatus { get; set; }

        public string NombreEstatus { get; set; }
        public int Porcentaje { get; set; }
    }
}