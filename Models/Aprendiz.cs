using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyCode.Models
{
    public class Aprendiz
    {
        [Key]
        public int IdAprendiz { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }

        [StringLength(20)]
        public string Documento { get; set; }

        [StringLength(10)]
        public string RH { get; set; }

        public ICollection<FichaAprendiz> FichaAprendices { get; set; }
        public ICollection<Vehiculo> Vehiculos { get; set; }
        public ICollection<Material> Materiales { get; set; }
        public ICollection<Movimiento> Movimientos { get; set; }
        public ICollection<ImagenAprendiz> ImagenesAprendiz { get; set; }
    }
}