using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyCode.Models
{
    public class TipoVehiculo
    {
        [Key]
        public int IdTipoVehiculo { get; set; }

        [StringLength(45)]
        public string Tipo { get; set; }

        public ICollection<Vehiculo> Vehiculos { get; set; }
    }
}