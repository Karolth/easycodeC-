using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyCode.Models
{
    public class UsuarioModel
    {
        [Key]
        public int IdUsuario { get; set; }

        [StringLength(250)]
        public string Documento { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Celular { get; set; }

        public ICollection<VehiculoModel> Vehiculos { get; set; }
        public ICollection<MaterialModel> Materiales { get; set; }
        public ICollection<MovimientoModel> Movimientos { get; set; }
        public ICollection<UsuarioRolModel> UsuarioRoles { get; set; }
    }
}