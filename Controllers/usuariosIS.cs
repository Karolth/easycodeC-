using Microsoft.AspNetCore.Mvc;
using TuProyecto.Services;

public class LoginRequest
{
    public string Action { get; set; }
    public string Documento { get; set; }
    public string Password { get; set; }
}

[ApiController]
[Route("[controller]")]
public class UsuariosISController : ControllerBase
{
    private readonly UsuarioISService _service;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsuariosISController(UsuarioISService service, IHttpContextAccessor httpContextAccessor)
    {
        _service = service;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest input)
    {
        if (input == null || string.IsNullOrEmpty(input.Documento) || string.IsNullOrEmpty(input.Password))
            return BadRequest(new { success = false, message = "Todos los campos son obligatorios" });

        // Verificar si el Documento existe
        var user = _service.ObtenerUsuarioPorDocumento(input.Documento);
        if (user == null)
            return Ok(new { success = false, message = "El documento no está registrado" });

        // Verificar si la contraseña es correcta (igual al documento, según tu lógica)
        if (user.Documento != input.Password)
            return Ok(new { success = false, message = "La contraseña es incorrecta" });

        // Obtener el rol del usuario
        var role = _service.ObtenerRolPorUsuario(user.IdUsuario);
        if (string.IsNullOrEmpty(role))
            return Ok(new { success = false, message = "Error al obtener el rol del usuario." });

        if (role.ToLower() != "administrador")
            return Ok(new { success = false, message = "Acceso denegado. Solo los administradores pueden ingresar." });

        // Guardar sesión (simulado, puedes usar JWT o Session)
        _httpContextAccessor.HttpContext.Session.SetInt32("user_id", user.IdUsuario);
        _httpContextAccessor.HttpContext.Session.SetString("Documento", user.Documento);

        return Ok(new { success = true, message = "Inicio de sesión exitoso" });
    }

    [HttpGet("perfil")]
    public IActionResult GetPerfil()
    {
        var userId = _httpContextAccessor.HttpContext.Session.GetInt32("user_id");
        if (!userId.HasValue)
            return Ok(new { success = false, message = "Acción no válida" });

        var perfil = _service.ObtenerPerfilPorId(userId.Value);
        if (perfil == null)
            return Ok(new { success = false, message = "Perfil no encontrado" });

        return Ok(new { success = true, data = perfil });
    }
}