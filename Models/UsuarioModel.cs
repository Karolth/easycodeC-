//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema; // A�adir para [Table] si el nombre de la tabla no coincide

//namespace Easycode.Data.Entities // O el namespace que uses para tus entidades
//{
//    // [Table("usuario")] // Usar si el nombre de la tabla en la DB es 'usuario' (min�sculas)
//    public class Usuario
//    {
//        [Key]
//        public int IdUsuario { get; set; }

//        [StringLength(250)]
//        public string Documento { get; set; }

//        [StringLength(250)]
//        public string Nombre { get; set; }

//        [StringLength(250)]
//        public string Email { get; set; }

//        [StringLength(250)]
//        public string Celular { get; set; }

//        // Propiedades de navegaci�n para relaciones
//        public ICollection<Vehiculo> Vehiculos { get; set; }
//        public ICollection<Material> Materiales { get; set; }
//        public ICollection<Movimiento> Movimientos { get; set; }
//        public ICollection<UsuarioRol> UsuarioRoles { get; set; }
//    }
//}