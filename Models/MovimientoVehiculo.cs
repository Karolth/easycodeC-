using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCode.Models
{
    public class MovimientoVehiculo
    {
        [Key]
        public int IdMovimientoVehiculo { get; set; }

        [StringLength(250)]
        public string Estado { get; set; }

        public int IdMovimiento { get; set; }
        public int IdVehiculo { get; set; }

        [ForeignKey("IdMovimiento")]
        public Movimiento Movimiento { get; set; }

        [ForeignKey("IdVehiculo")]
        public Vehiculo Vehiculo { get; set; }
    }
}