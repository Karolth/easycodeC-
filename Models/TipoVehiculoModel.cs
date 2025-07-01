using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyCode.Models
{
    public class TipoVehiculoModel
    {
        [Key]
        public int IdTipoVehiculo { get; set; }

        [StringLength(45)]
        public string Tipo { get; set; }

        public ICollection<VehiculoModel> Vehiculos { get; set; }
    }
}