using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyCode.Models
{
    public class RolModel
    {
        [Key]
        public int IdRol { get; set; }

        [StringLength(250)]
        public string RolNombre { get; set; }

        public ICollection<UsuarioRolModel> UsuarioRoles { get; set; }
    }
}