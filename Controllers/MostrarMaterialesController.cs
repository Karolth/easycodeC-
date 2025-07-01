using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Easycode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MostrarElementoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MostrarElementoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Mostrar([FromQuery] int idUsuario, [FromQuery] string tipoUsuario, [FromQuery] string tipoConsulta = "materiales")
        {
            if (idUsuario == 0)
                return BadRequest(new { error = "ID de usuario no proporcionado" });

            if (tipoConsulta == "materiales")
            {
                var materiales = _context.Materiales
                    .Include(m => m.TipoMaterial)
                    .Where(m => (tipoUsuario == "aprendiz" ? m.IdAprendiz == idUsuario : m.IdUsuario == idUsuario))
                    .Select(m => new
                    {
                        m.IdMaterial,
                        m.Nombre,
                        m.Referencia,
                        m.Marca,
                        Tipo = m.TipoMaterial.Tipo,
                        Estado = _context.MovimientoMateriales
                            .Where(mm => mm.IdMaterial == m.IdMaterial)
                            .OrderByDescending(mm => mm.IdMovimiento)
                            .Select(mm => mm.Estado)
                            .FirstOrDefault()
                    })
                    .OrderByDescending(m => m.IdMaterial)
                    .ToList();

                return Ok(materiales);
            }
            else if (tipoConsulta == "vehiculo")
            {
                var vehiculos = _context.Vehiculos
                    .Include(v => v.TipoVehiculo)
                    .Where(v => (tipoUsuario == "aprendiz" ? v.IdAprendiz == idUsuario : v.IdUsuario == idUsuario))
                    .Select(v => new
                    {
                        v.IdVehiculo,
                        v.Placa,
                        Tipo = v.TipoVehiculo.Tipo,
                        Estado = _context.MovimientoVehiculos
                            .Where(mv => mv.IdVehiculo == v.IdVehiculo)
                            .OrderByDescending(mv => mv.IdMovimiento)
                            .Select(mv => mv.Estado)
                            .FirstOrDefault()
                    })
                    .OrderByDescending(v => v.IdVehiculo)
                    .ToList();

                return Ok(new { vehiculo = vehiculos });
            }
            else
            {
                return BadRequest(new { error = "Tipo de consulta no válido" });
            }
        }
    }
}