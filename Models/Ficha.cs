using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCode.Models
{
    public class Ficha
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
        public Programa Programa { get; set; }

        public ICollection<FichaAprendiz> FichaAprendices { get; set; }
    }
}