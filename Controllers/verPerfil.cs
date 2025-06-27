using Microsoft.AspNetCore.Mvc;
using TuProyecto.Services;
using Microsoft.AspNetCore.Http;

public class ModificarPerfilRequest
{
    public string Email { get; set; }
    public string Celular { get; set; }
}

[ApiController]
[Route("[controller]")]
public class PerfilController : ControllerBase
{
    private readonly PerfilService _service;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PerfilController(PerfilService service, IHttpContextAccessor httpContextAccessor)
    {
        _service = service;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet("getPerfil")]
    public IActionResult GetPerfil()
    {
        var userId = _httpContextAccessor.HttpContext.Session.GetInt32("user_id");
        if (!userId.HasValue)
            return Unauthorized(new { success = false, message = "Usuario no autenticado" });

        var user = _service.ObtenerPerfilPorId(userId.Value);
        if (user == null)
            return NotFound(new { success = false, message = "Usuario no encontrado" });

        return Ok(new
        {
            success = true,
            Nombre = user.Nombre,
            Documento = user.Documento,
            Email = user.Email,
            Celular = user.Celular
        });
    }

    [HttpPost("modificar")]
    public IActionResult Modificar([FromBody] ModificarPerfilRequest input)
    {
        var userId = _httpContextAccessor.HttpContext.Session.GetInt32("user_id");
        if (!userId.HasValue)
            return Unauthorized(new { success = false, message = "Usuario no autenticado" });

        if (string.IsNullOrEmpty(input.Email) || string.IsNullOrEmpty(input.Celular))
            return BadRequest(new { success = false, message = "Datos incompletos" });

        var resultado = _service.ActualizarPerfil(userId.Value, input.Email, input.Celular);

        if (resultado)
            return Ok(new { success = true, message = "Perfil actualizado correctamente" });
        else
            return BadRequest(new { success = false, message = "No se pudo actualizar el perfil" });
    }
}