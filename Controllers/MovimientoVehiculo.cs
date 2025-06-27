using Microsoft.AspNetCore.Mvc;
using TuProyecto.Services;

[ApiController]
[Route("[controller]")]
public class MovimientoVehiculoController : ControllerBase
{
    private readonly MovimientoVehiculoService _service;

    public MovimientoVehiculoController(MovimientoVehiculoService service)
    {
        _service = service;
    }

    [HttpPost("registrar")]
    public IActionResult Registrar([FromForm] int idVehiculo, [FromForm] string estado, [FromForm] int idMovimiento)
    {
        if (idVehiculo == 0 || string.IsNullOrEmpty(estado) || idMovimiento == 0)
            return BadRequest("Todos los campos son obligatorios.");

        try
        {
            if (_service.RegistrarMovimientoVehiculo(estado, idMovimiento, idVehiculo))
                return Ok("Movimiento registrado exitosamente.");
            else
                return BadRequest("Error al registrar el movimiento.");
        }
        catch (Exception ex)
        {
            return BadRequest("Error: " + ex.Message);
        }
    }
}