using Microsoft.AspNetCore.Mvc;
using System;

namespace Easycode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HistorialLoginController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HistorialLoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetHistorialLogin()
        {
            var historial = (from m in _context.Movimientos
                             join u in _context.Usuarios on m.IdUsuario equals u.IdUsuario
                             join ur in _context.UsuarioRoles on u.IdUsuario equals ur.IdUsuario
                             join r in _context.Roles on ur.IdRol equals r.IdRol
                             where m.MovimientoTipo == "Entrada" && r.Rol == "Administrador"
                             orderby m.FechaHora descending
                             select new
                             {
                                 u.Documento,
                                 u.Nombre,
                                 m.FechaHora,
                                 Rol = r.Rol
                             }).ToList();

            if (historial.Any())
                return Ok(new { success = true, data = historial });
            else
                return Ok(new { success = false, message = "No se encontraron registros de inicio de sesión." });
        }
    }
}