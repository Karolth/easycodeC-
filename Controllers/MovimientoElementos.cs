using EasyCode.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Easycode.Controllers
{
    public class MovimientoElementosRequest
    {
        public List<int> materiales { get; set; }
        public List<int> vehiculos { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class MovimientoElementosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovimientoElementosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Registrar([FromBody] MovimientoElementosRequest data)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var ultimoMovimiento = _context.Movimientos
                    .OrderByDescending(m => m.IdMovimiento)
                    .FirstOrDefault();

                if (ultimoMovimiento == null)
                    return BadRequest(new { success = false, message = "No hay movimientos previos." });

                var idMovimiento = ultimoMovimiento.IdMovimiento;
                var movimiento = ultimoMovimiento.MovimientoTipo;

                if (data.materiales != null)
                {
                    foreach (var idMaterial in data.materiales)
                    {
                        var existe = _context.MovimientoMateriales
                            .Any(mm => mm.Estado == movimiento && mm.IdMaterial == idMaterial);

                        if (!existe)
                        {
                            var nuevo = new MovimientoMaterial
                            {
                                Estado = movimiento,
                                IdMovimiento = idMovimiento,
                                IdMaterial = idMaterial
                            };
                            _context.MovimientoMateriales.Add(nuevo);
                        }
                    }
                }

                if (data.vehiculos != null)
                {
                    foreach (var idVehiculo in data.vehiculos)
                    {
                        var existe = _context.MovimientoVehiculos
                            .Any(mv => mv.Estado == movimiento && mv.IdVehiculo == idVehiculo);

                        if (!existe)
                        {
                            var nuevo = new MovimientoVehiculo
                            {
                                Estado = movimiento,
                                IdMovimiento = idMovimiento,
                                IdVehiculo = idVehiculo
                            };
                            _context.MovimientoVehiculos.Add(nuevo);
                        }
                    }
                }

                _context.SaveChanges();
                transaction.Commit();
                return Ok(new { success = true, message = "Movimientos registrados correctamente." });
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return BadRequest(new { success = false, message = "Error al registrar los movimientos: " + ex.Message });
            }
        }
    }
}