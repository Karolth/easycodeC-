using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyCode.Models
{
    public class Usuario
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

        public ICollection<Vehiculo> Vehiculos { get; set; }
        public ICollection<Material> Materiales { get; set; }
        public ICollection<Movimiento> Movimientos { get; set; }
        public ICollection<UsuarioRol> UsuarioRoles { get; set; }
    }
}