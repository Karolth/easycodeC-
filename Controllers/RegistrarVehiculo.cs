using Microsoft.AspNetCore.Mvc;
using TuProyecto.Models;
using TuProyecto.Services;

[ApiController]
[Route("[controller]")]
public class VehiculoController : ControllerBase
{
    private readonly VehiculoService _service;

    public VehiculoController(VehiculoService service)
    {
        _service = service;
    }

    [HttpPost("registrar")]
    public IActionResult Registrar([FromForm] Vehiculo vehiculo)
    {
        if (string.IsNullOrEmpty(vehiculo.Placa) || vehiculo.IdTipoVehiculo == 0 ||
            (vehiculo.IdUsuario == null && vehiculo.IdAprendiz == null))
        {
            return BadRequest("Todos los campos son obligatorios.");
        }

        if (_service.RegistrarVehiculo(vehiculo))
            return Ok("Vehículo registrado correctamente.");
        else
            return BadRequest("Error al registrar el vehículo.");
    }

    [HttpGet("tipos")]
    public IActionResult ObtenerTipos()
    {
        var tipos = _service.ObtenerTiposVehiculo();
        return Ok(new { success = true, TipoVehiculo = tipos });
    }
}