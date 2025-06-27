using Microsoft.AspNetCore.Mvc;
using TuProyecto.Services;

[ApiController]
[Route("[controller]")]
public class ProgramasController : ControllerBase
{
    private readonly FichaService _service;

    public ProgramasController(FichaService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var programas = _service.ObtenerProgramas();
        if (programas.Any())
        {
            return Ok(new { success = true, data = programas });
        }
        return Ok(new { success = false, message = "No se encontraron programas de formación." });
    }
}