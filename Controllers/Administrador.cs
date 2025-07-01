using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Easycode.Data;
using System.IO;
using Easycode.Models.ViewModels; // Agrega esta directiva
using Easycode.Data.Entities; // Asegúrate de que tus entidades estén en este namespace

namespace Easycode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Administrador : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public Administrador(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult BuscarPorDocumento([FromQuery] string documento)
        {
            if (string.IsNullOrEmpty(documento))
                return BadRequest(new { error = "Documento requerido." });

            string tipoPersona = "";
            int? idUsuario = null;
            int? idAprendiz = null;
            string rutaImagen = "";

            // Proyección a AprendizViewModel
            var aprendiz = (from a in _context.Aprendices
                            join fa in _context.FichaAprendiz on a.IdAprendiz equals fa.IdAprendiz
                            join f in _context.Fichas on fa.IdFicha equals f.IdFicha
                            join p in _context.Programas on f.IdPrograma equals p.IdPrograma
                            join tp in _context.TipoProgramas on p.IdTipoPrograma equals tp.IdTipoPrograma
                            where a.Documento == documento
                            select new AprendizViewModel // Proyecta directamente al ViewModel
                            {
                                IdAprendiz = a.IdAprendiz,
                                Nombre = a.Nombre,
                                RH = a.RH,
                                Documento = a.Documento,
                                TipoPrograma = tp.Tipo, // Asume que la propiedad es 'Tipo' en TipoPrograma
                                Programa = p.Nombre
                            }).FirstOrDefault();

            UsuarioViewModel usuario = null; // Inicializa como tipo UsuarioViewModel

            if (aprendiz != null)
            {
                tipoPersona = "aprendiz";
                idAprendiz = aprendiz.IdAprendiz;
                rutaImagen = Path.Combine(_env.WebRootPath, "Imagenes", $"{documento}.jpg");
                if (!System.IO.File.Exists(rutaImagen))
                    rutaImagen = Path.Combine(_env.WebRootPath, "Imagenes", "default-user.png");
            }
            else
            {
                // Proyección a UsuarioViewModel
                usuario = (from u in _context.Usuarios
                           join ur in _context.UsuarioRoles on u.IdUsuario equals ur.IdUsuario
                           join r in _context.Roles on ur.IdRol equals r.IdRol
                           where u.Documento == documento
                           select new UsuarioViewModel // Proyecta directamente al ViewModel
                           {
                               IdUsuario = u.IdUsuario,
                               Nombre = u.Nombre,
                               Email = u.Email,
                               Rol = r.Rol
                           }).FirstOrDefault();

                if (usuario != null)
                {
                    tipoPersona = "usuario";
                    idUsuario = usuario.IdUsuario; // Acceso directo a la propiedad tipada
                    rutaImagen = Path.Combine(_env.WebRootPath, "Imagenes", "default-user.png");
                }
                else
                {
                    return NotFound(new { error = "No se encontró el documento en la base de datos." });
                }
            }

            var ultimoMovimiento = _context.Movimientos
                .Where(m => m.IdUsuario == idUsuario || m.IdAprendiz == idAprendiz)
                .OrderByDescending(m => m.FechaHora)
                .FirstOrDefault();

            // Tu código ya usa MovimientoTipo, asegúrate de que tu modelo de entidad Movimiento lo tenga.
            var nuevoMovimiento = (ultimoMovimiento != null && ultimoMovimiento.MovimientoTipo == "Entrada") ? "Salida" : "Entrada";

            _context.Movimientos.Add(new Movimiento
            {
                FechaHora = DateTime.Now,
                MovimientoTipo = nuevoMovimiento,
                IdUsuario = idUsuario,
                IdAprendiz = idAprendiz
            });
            _context.SaveChanges();

            return Ok(new
            {
                tipo = tipoPersona,
                datos = (object)aprendiz ?? (object)usuario, // Castear a object es necesario aquí para el retorno anónimo
                imagen = rutaImagen,
                movimiento = nuevoMovimiento
            });
        }
    }
}