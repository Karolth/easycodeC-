using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCode.Models
{
    public class MovimientoMaterial
    {
        [Key]
        public int IdMovimientoMaterial { get; set; }

        [StringLength(250)]
        public string Estado { get; set; }

        public int IdMovimiento { get; set; }
        public int IdMaterial { get; set; }

        [ForeignKey("IdMovimiento")]
        public Movimiento Movimiento { get; set; }

        [ForeignKey("IdMaterial")]
        public Material Material { get; set; }
    }
}