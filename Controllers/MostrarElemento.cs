using Microsoft.AspNetCore.Mvc;
using TuProyecto.Services;

[ApiController]
[Route("[controller]")]
public class ElementoController : ControllerBase
{
    private readonly ElementoService _service;

    public ElementoController(ElementoService service)
    {
        _service = service;
    }

    [HttpGet("mostrar")]
    public IActionResult MostrarElemento([FromQuery] int idUsuario, [FromQuery] string tipoUsuario, [FromQuery] string tipoConsulta = "materiales")
    {
        if (idUsuario == 0)
            return BadRequest(new { error = "ID de usuario no proporcionado" });

        try
        {
            if (tipoConsulta == "materiales")
            {
                var materiales = _service.ObtenerMaterialesPorUsuario(idUsuario, tipoUsuario);
                return Ok(materiales);
            }
            else if (tipoConsulta == "vehiculo")
            {
                var vehiculos = _service.ObtenerVehiculosPorUsuario(idUsuario, tipoUsuario);
                return Ok(new { vehiculo = vehiculos });
            }
            else
            {
                return BadRequest(new { error = "Tipo de consulta no válido" });
            }
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }
}