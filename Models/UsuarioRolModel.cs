using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCode.Models
{
    public class UsuarioRolModel
    {
        [Key]
        public int IdUsuarioRol { get; set; }

        public int IdUsuario { get; set; }
        public int IdRol { get; set; }

        [ForeignKey("IdUsuario")]
        public UsuarioModel Usuario { get; set; }

        [ForeignKey("IdRol")]
        public RolModel Rol { get; set; }
    }
}