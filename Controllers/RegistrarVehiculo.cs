using Microsoft.AspNetCore.Mvc;
using System;

namespace Easycode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrarVehiculoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegistrarVehiculoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Registrar([FromForm] string placa, [FromForm] int idTipoVehiculo, [FromForm] int idUsuario, [FromForm] int idAprendiz)
        {
            if (string.IsNullOrWhiteSpace(placa) || idTipoVehiculo == 0 || idUsuario == 0 || idAprendiz == 0)
                return BadRequest("Todos los campos son obligatorios.");

            var vehiculo = new Vehiculo
            {
                Placa = placa,
                IdTipoVehiculo = idTipoVehiculo,
                IdUsuario = idUsuario,
                IdAprendiz = idAprendiz
            };

            _context.Vehiculos.Add(vehiculo);
            var resultado = _context.SaveChanges();

            if (resultado > 0)
                return Ok("Vehículo registrado correctamente.");
            else
                return BadRequest("Error al registrar el vehículo.");
        }

        [HttpGet("cargarTipo")]
        public IActionResult CargarTipo()
        {
            var tiposVehiculo = _context.TipoVehiculos.ToList();
            return Ok(new { success = true, TipoVehiculo = tiposVehiculo });
        }
    }
}