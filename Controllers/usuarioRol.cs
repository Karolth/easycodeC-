using Microsoft.AspNetCore.Mvc;
using System;

namespace Easycode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioRolController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioRolController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Registrar([FromForm] string documento, [FromForm] string nombre, [FromForm] string email,
                                       [FromForm] string celular, [FromForm] int rol)
        {
            if (string.IsNullOrEmpty(documento) || string.IsNullOrEmpty(nombre) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(celular) || rol == 0)
            {
                return BadRequest(new { error = "Todos los campos son obligatorios." });
            }

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var usuario = new Usuario
                {
                    Documento = documento,
                    Nombre = nombre,
                    Email = email,
                    Celular = celular
                };
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                var usuarioRol = new UsuarioRolEntidad
                {
                    IdUsuario = usuario.IdUsuario,
                    IdRol = rol
                };
                _context.UsuarioRoles.Add(usuarioRol);
                _context.SaveChanges();

                transaction.Commit();
                return Ok(new { success = "Usuario registrado y rol asignado correctamente." });
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return BadRequest(new { error = "Excepción capturada: " + ex.Message });
            }
        }
    }
}