using Microsoft.AspNetCore.Mvc;
using System;

namespace Easycode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsultarEstado : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConsultarEstado(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Consultar([FromQuery] int? idUsuario, [FromQuery] int? idAprendiz)
        {
            if (!idUsuario.HasValue && !idAprendiz.HasValue)
                return BadRequest(new { success = false, message = "Debe proporcionar un IdUsuario o un IdAprendiz." });

            string estado = null;

            if (idUsuario.HasValue)
            {
                var movimiento = _context.Movimientos
                    .Where(m => m.IdUsuario == idUsuario.Value)
                    .OrderByDescending(m => m.FechaHora)
                    .FirstOrDefault();
                estado = movimiento?.MovimientoTipo;
            }

            if (idAprendiz.HasValue)
            {
                var movimiento = _context.Movimientos
                    .Where(m => m.IdAprendiz == idAprendiz.Value)
                    .OrderByDescending(m => m.FechaHora)
                    .FirstOrDefault();
                estado = movimiento?.MovimientoTipo;
            }

            if (estado == null)
                return Ok(new { success = false, message = "No se encontró estado para la persona seleccionada." });
            else
                return Ok(new { success = true, estado });
        }
    }
}