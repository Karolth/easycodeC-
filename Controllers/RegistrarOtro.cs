using Microsoft.AspNetCore.Mvc;
using TuProyecto.Services;

[ApiController]
[Route("[controller]")]
public class OtroMaterialController : ControllerBase
{
    private readonly OtroMaterialService _service;

    public OtroMaterialController(OtroMaterialService service)
    {
        _service = service;
    }

    [HttpPost("registrar")]
    public IActionResult Registrar([FromForm] string nombre, [FromForm] string observaciones,
                                   [FromForm] int idTipoMaterial, [FromForm] int? idUsuario, [FromForm] int? idAprendiz)
    {
        if (string.IsNullOrEmpty(nombre) || idTipoMaterial == 0)
            return BadRequest("El nombre y el tipo de material son obligatorios.");

        if (_service.RegistrarOtroMaterial(nombre, observaciones, idTipoMaterial, idUsuario, idAprendiz))
            return Ok("Material registrado exitosamente.");
        else
            return BadRequest("Error al registrar el material.");
    }
}