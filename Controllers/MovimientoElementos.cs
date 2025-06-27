using Microsoft.AspNetCore.Mvc;
using TuProyecto.Services;

public class MovimientoElementosRequest
{
    public List<int> materiales { get; set; }
    public List<int> vehiculos { get; set; }
}

[ApiController]
[Route("[controller]")]
public class MovimientoElementosController : ControllerBase
{
    private readonly MovimientoElementoService _service;

    public MovimientoElementosController(MovimientoElementoService service)
    {
        _service = service;
    }

    [HttpPost("registrar")]
    public IActionResult Registrar([FromBody] MovimientoElementosRequest data)
    {
        using var transaction = _service._context.Database.BeginTransaction();
        try
        {
            var ultimoMovimiento = _service.ObtenerUltimoMovimiento();
            if (ultimoMovimiento == null)
                return BadRequest(new { success = false, message = "No hay movimientos previos." });

            var idMovimiento = ultimoMovimiento.IdMovimiento;
            var movimiento = ultimoMovimiento.MovimientoTipo;

            if (data.materiales != null)
            {
                foreach (var idMaterial in data.materiales)
                {
                    _service.InsertarMaterial(movimiento, idMovimiento, idMaterial);
                }
            }

            if (data.vehiculos != null)
            {
                foreach (var idVehiculo in data.vehiculos)
                {
                    _service.InsertarVehiculo(movimiento, idMovimiento, idVehiculo);
                }
            }

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