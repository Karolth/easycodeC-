using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCode.Models
{
    public class ImagenAprendiz
    {
        [Key]
        public int IdImagen { get; set; }

        [StringLength(255)]
        public string NombreArchivo { get; set; }

        [Column(TypeName = "VARBINARY(MAX)")]
        public byte[] RutaArchivo { get; set; }

        public int IdAprendiz { get; set; }

        [ForeignKey("IdAprendiz")]
        public Aprendiz Aprendiz { get; set; }
    }
}