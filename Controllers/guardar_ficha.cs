using EasyCode.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Easycode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuardarFichaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GuardarFichaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult GuardarFicha([FromForm] int programaFormacion, [FromForm] string jornada, [FromForm] string tipoPrograma,
                                          [FromForm] string fechaInicio, [FromForm] string fechaFin, [FromForm] string numeroFicha,
                                          [FromForm] IFormFile archivoExcel)
        {
            if (programaFormacion == 0 || string.IsNullOrEmpty(jornada) || string.IsNullOrEmpty(tipoPrograma)
                || string.IsNullOrEmpty(fechaInicio) || string.IsNullOrEmpty(fechaFin) || string.IsNullOrEmpty(numeroFicha) || archivoExcel == null)
            {
                return BadRequest(new { success = false, message = "Todos los campos del formulario y el archivo Excel son obligatorios." });
            }

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var ficha = new Ficha
                {
                    Numficha = numeroFicha,
                    FechaInicio = DateTime.Parse(fechaInicio),
                    FechaFinal = DateTime.Parse(fechaFin),
                    Jornada = jornada,
                    IdPrograma = programaFormacion
                };
                _context.Fichas.Add(ficha);
                _context.SaveChanges();

                var idFicha = ficha.IdFicha;

                // Aquí deberías procesar el archivo Excel y registrar los aprendices
                // Este ejemplo asume que el archivo es CSV con ; como separador
                using (var reader = new StreamReader(archivoExcel.OpenReadStream()))
                {
                    string line;
                    int i = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (i != 0)
                        {
                            var datos = line.Split(';');
                            var documento = datos[1]?.Trim();
                            if (!string.IsNullOrEmpty(documento))
                            {
                                var aprendiz = _context.Aprendices.FirstOrDefault(a => a.Documento == documento);
                                if (aprendiz != null)
                                {
                                    var existe = _context.FichaAprendiz.Any(fa => fa.IdFicha == idFicha && fa.IdAprendiz == aprendiz.IdAprendiz);
                                    if (!existe)
                                    {
                                        _context.FichaAprendiz.Add(new FichaAprendiz
                                        {
                                            IdFicha = idFicha,
                                            IdAprendiz = aprendiz.IdAprendiz
                                        });
                                    }
                                }
                            }
                        }
                        i++;
                    }
                }

                _context.SaveChanges();
                transaction.Commit();

                return Ok(new { success = true, message = "Ficha creada y aprendices registrados exitosamente." });
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return BadRequest(new { success = false, message = "Error al crear ficha: " + ex.Message });
            }
        }
    }
}