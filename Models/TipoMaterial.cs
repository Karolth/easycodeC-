using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyCode.Models
{
    public class TipoMaterial
    {
        [Key]
        public int IdTipoMaterial { get; set; }

        [StringLength(45)]
        public string Tipo { get; set; }

        public ICollection<Material> Materiales { get; set; }
    }
}