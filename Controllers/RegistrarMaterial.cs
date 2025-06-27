using Microsoft.AspNetCore.Mvc;
using TuProyecto.Models;
using TuProyecto.Services;

[ApiController]
[Route("[controller]")]
public class MaterialController : ControllerBase
{
    private readonly MaterialService _service;

    public MaterialController(MaterialService service)
    {
        _service = service;
    }

    [HttpPost("registrar")]
    public IActionResult Registrar([FromForm] Material material)
    {
        if (string.IsNullOrEmpty(material.Nombre) || string.IsNullOrEmpty(material.Referencia) ||
            string.IsNullOrEmpty(material.Marca) || material.IdTipoMaterial == 0 ||
            (material.IdUsuario == null && material.IdAprendiz == null))
        {
            return BadRequest("Todos los campos son obligatorios.");
        }

        if (_service.RegistrarMaterial(material))
            return Ok("Material registrado exitosamente.");
        else
            return BadRequest("Error al registrar el material.");
    }
}