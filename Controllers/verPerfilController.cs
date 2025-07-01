using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Easycode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VerPerfilController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VerPerfilController(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult GetPerfil()
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetInt32("user_id");
            if (!userId.HasValue)
                return Unauthorized(new { success = false, message = "Usuario no autenticado" });

            var user = _context.Usuarios
                .Where(u => u.IdUsuario == userId.Value)
                .Select(u => new
                {
                    u.Nombre,
                    u.Documento,
                    u.Email,
                    u.Celular
                })
                .FirstOrDefault();

            if (user == null)
                return NotFound(new { success = false, message = "Usuario no encontrado" });

            return Ok(new { success = true, Nombre = user.Nombre, Documento = user.Documento, Email = user.Email, Celular = user.Celular });
        }

        [HttpPost("modificar")]
        public IActionResult Modificar([FromBody] dynamic input)
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetInt32("user_id");
            if (!userId.HasValue)
                return Unauthorized(new { success = false, message = "Usuario no autenticado" });

            string email = input.email;
            string celular = input.celular;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(celular))
                return BadRequest(new { success = false, message = "Datos incompletos" });

            var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == userId.Value);
            if (usuario == null)
                return NotFound(new { success = false, message = "Usuario no encontrado" });

            usuario.Email = email;
            usuario.Celular = celular;
            _context.SaveChanges();

            return Ok(new { success = true, message = "Perfil actualizado correctamente" });
        }
    }
}