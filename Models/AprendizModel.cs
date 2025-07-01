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

        public ICollection<FichaModel> FichaAprendices { get; set; }
        public ICollection<VehiculoModel> Vehiculos { get; set; }
        public ICollection<MaterialModel> Materiales { get; set; }
        public ICollection<MovimientoModel> Movimientos { get; set; }
        public ICollection<ImagenAprendiz> ImagenesAprendiz { get; set; }
    }
}