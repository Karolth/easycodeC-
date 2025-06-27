using Microsoft.AspNetCore.Mvc;
using TuProyecto.Services;

[ApiController]
[Route("[controller]")]
public class HistorialController : ControllerBase
{
    private readonly HistorialService _service;

    public HistorialController(HistorialService service)
    {
        _service = service;
    }

    [HttpGet("getHistorial")]
    public IActionResult GetHistorial()
    {
        var historial = _service.ObtenerHistorial();
        return Ok(historial);
    }

    // Si tienes la función buscarSugerencias, agrégala aquí
    // [HttpGet("buscarSugerencias")]
    // public IActionResult BuscarSugerencias([FromQuery] string term)
    // {
    //     var sugerencias = _service.BuscarSugerencias(term);
    //     return Ok(sugerencias);
    // }
}