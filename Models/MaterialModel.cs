//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace EasyCode.Models
//{
//    public class MaterialModel
//    {
//        [Key]
//        public int IdMaterial { get; set; }

//        [StringLength(250)]
//        public string Nombre { get; set; }

//        [StringLength(250)]
//        public string Referencia { get; set; }

//        [StringLength(250)]
//        public string Marca { get; set; }

//        [StringLength(100)]
//        public string Observaciones { get; set; }

//        public int IdTipoMaterial { get; set; }
//        public int IdUsuario { get; set; }
//        public int? IdAprendiz { get; set; }

//        [ForeignKey("IdTipoMaterial")]
//        public TipoMaterialModel TipoMaterial { get; set; }

//        [ForeignKey("IdUsuario")]
//        public UsuarioModel Usuario { get; set; }

//        [ForeignKey("IdAprendiz")]
//        public Aprendiz Aprendiz { get; set; }

//        public ICollection<MovimientoMaterialModel> MovimientoMateriales { get; set; }
//    }
//}