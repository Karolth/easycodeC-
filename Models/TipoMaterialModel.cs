using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyCode.Models
{
    public class TipoMaterialModel
    {
        [Key]
        public int IdTipoMaterial { get; set; }

        [StringLength(45)]
        public string Tipo { get; set; }

        public ICollection<MaterialModel> Materiales { get; set; }
    }
}