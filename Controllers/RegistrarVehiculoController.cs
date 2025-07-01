falta que generes los sigientes archivos sin el service y todo quede en el controlador

<<<<<<< HEAD:Controllers/RegistrarVehiculoController.cs
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
    public IActionResult Registrar([FromForm] VehiculoModel vehiculo)
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
=======
>>>>>>> 9870ee2aabb8417c4885ff7ebad518bfd9254f03:Controllers/RegistrarVehiculo.cs
