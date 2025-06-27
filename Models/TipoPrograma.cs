using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyCode.Models
{
    public class TipoPrograma
    {
        [Key]
        public int IdTipoPrograma { get; set; }

        [StringLength(45)]
        public string Tipo { get; set; }

        public ICollection<Programa> Programas { get; set; }
    }
}