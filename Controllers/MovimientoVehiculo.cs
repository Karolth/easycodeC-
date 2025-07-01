using Microsoft.AspNetCore.Mvc;
using System;

namespace Easycode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimientoVehiculo : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovimientoVehiculo(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Registrar([FromForm] int idVehiculo, [FromForm] string estado, [FromForm] int idMovimiento)
        {
            if (idVehiculo == 0 || string.IsNullOrEmpty(estado) || idMovimiento == 0)
                return BadRequest("Todos los campos son obligatorios.");

            var movimientoVehiculo = new MovimientoVehiculo
            {
                Estado = estado,
                IdMovimiento = idMovimiento,
                IdVehiculo = idVehiculo
            };
            _context.MovimientoVehiculos.Add(movimientoVehiculo);
            _context.SaveChanges();

            return Ok("Movimiento registrado exitosamente.");
        }
    }
}