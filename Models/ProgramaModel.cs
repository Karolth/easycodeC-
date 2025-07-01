using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCode.Models
{
    public class ProgramaModel
    {
        [Key]
        public int IdPrograma { get; set; }

        [StringLength(250)]
        public string Nombre { get; set; }

        [StringLength(50)]
        public string Version { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Fecha { get; set; }

        public int IdTipoPrograma { get; set; }

        [ForeignKey("IdTipoPrograma")]
        public TipoProgramaModel TipoPrograma { get; set; }

        public ICollection<FichaModel> Fichas { get; set; }
    }
}