using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyCode.Models
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }

        [StringLength(250)]
        public string RolNombre { get; set; }

        public ICollection<UsuarioRol> UsuarioRoles { get; set; }
    }
}