using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Easycode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosISController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuariosISController(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] dynamic input)
        {
            string documento = input.Documento;
            string password = input.password;

            if (string.IsNullOrEmpty(documento) || string.IsNullOrEmpty(password))
                return BadRequest(new { success = false, message = "Todos los campos son obligatorios" });

            var user = _context.Usuarios.FirstOrDefault(u => u.Documento == documento);
            if (user == null)
                return Ok(new { success = false, message = "El documento no está registrado" });

            if (user.Documento != password)
                return Ok(new { success = false, message = "La contraseña es incorrecta" });

            var role = (from ur in _context.UsuarioRoles
                        join r in _context.Roles on ur.IdRol equals r.IdRol
                        where ur.IdUsuario == user.IdUsuario
                        select r.Rol).FirstOrDefault();

            if (string.IsNullOrEmpty(role))
                return Ok(new { success = false, message = "Error al obtener el rol del usuario." });

            if (role.ToLower() != "administrador")
                return Ok(new { success = false, message = "Acceso denegado. Solo los administradores pueden ingresar." });

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

            var perfil = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == userId.Value);
            if (perfil == null)
                return Ok(new { success = false, message = "Perfil no encontrado" });

            return Ok(new { success = true, data = perfil });
        }
    }
}