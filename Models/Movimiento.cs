using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCode.Models
{
    public class Movimiento
    {
        [Key]
        public int IdMovimiento { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime FechaHora { get; set; }

        [StringLength(45)]
        public string MovimientoTipo { get; set; }

        public int IdUsuario { get; set; }
        public int? IdAprendiz { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }

        [ForeignKey("IdAprendiz")]
        public Aprendiz Aprendiz { get; set; }

        public ICollection<MovimientoMaterial> MovimientoMateriales { get; set; }
        public ICollection<MovimientoVehiculo> MovimientoVehiculos { get; set; }
    }
}