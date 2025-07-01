using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCode.Models
{
    public class VehiculoModel
    {
        [Key]
        public int IdVehiculo { get; set; }

        [StringLength(250)]
        public string Placa { get; set; }

        public int IdTipoVehiculo { get; set; }
        public int IdUsuario { get; set; }
        public int? IdAprendiz { get; set; }

        [ForeignKey("IdTipoVehiculo")]
        public TipoVehiculoModel TipoVehiculo { get; set; }

        [ForeignKey("IdUsuario")]
        public UsuarioModel Usuario { get; set; }

        [ForeignKey("IdAprendiz")]
        public Aprendiz Aprendiz { get; set; }

        public ICollection<MovimientoVehiculoModel> MovimientoVehiculos { get; set; }
    }
}