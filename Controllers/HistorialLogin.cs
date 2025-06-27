using Microsoft.AspNetCore.Mvc;
using TuProyecto.Services;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _service;

    public UsuarioController(UsuarioService service)
    {
        _service = service;
    }

    [HttpGet("HistorialLogin")]
    public IActionResult HistorialLogin()
    {
        try
        {
            var historial = _service.ObtenerHistorialLogin();
            if (historial != null && historial.Any())
                return Ok(new { success = true, data = historial });
            else
                return Ok(new { success = false, message = "No se encontraron registros de inicio de sesión." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = "Error al obtener el historial: " + ex.Message });
        }
    }
}