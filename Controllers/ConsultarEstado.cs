using Microsoft.AspNetCore.Mvc;
using TuProyecto.Services;

[ApiController]
[Route("[controller]")]
public class EstadoController : ControllerBase
{
    private readonly EstadoService _service;

    public EstadoController(EstadoService service)
    {
        _service = service;
    }

    [HttpGet("Consultar")]
    public IActionResult Consultar([FromQuery] int? idUsuario, [FromQuery] int? idAprendiz)
    {
        if (!idUsuario.HasValue && !idAprendiz.HasValue)
        {
            return BadRequest(new { success = false, message = "Debe proporcionar un IdUsuario o un IdAprendiz." });
        }

        string estado = null;

        if (idUsuario.HasValue)
        {
            estado = _service.ObtenerEstadoPorUsuario(idUsuario.Value);
        }

        if (idAprendiz.HasValue)
        {
            estado = _service.ObtenerEstadoPorAprendiz(idAprendiz.Value);
        }

        if (estado == null)
        {
            return Ok(new { success = false, message = "No se encontró estado para la persona seleccionada." });
        }
        else
        {
            return Ok(new { success = true, estado });
        }
    }
}