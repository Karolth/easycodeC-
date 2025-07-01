using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCode.Models
{
    public class FichaModel
    {
        [Key]
        public int IdFicha { get; set; }

        public int NumFicha { get; set; }

        [Column(TypeName = "Date")]
        public DateTime FechaInicio { get; set; }

        [Column(TypeName = "Date")]
        public DateTime FechaFinal { get; set; }

        [StringLength(250)]
        public string Jornada { get; set; }

        public int IdPrograma { get; set; }

        [ForeignKey("IdPrograma")]
        public ProgramaModel Programa { get; set; }

        public ICollection<FichaModel> FichaAprendices { get; set; }
    }
}