using Microsoft.AspNetCore.Mvc;
using System;

namespace Easycode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrarOtro : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegistrarOtro(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Registrar([FromForm] string nombre, [FromForm] string observaciones, [FromForm] int idTipoMaterial, [FromForm] int idUsuario, [FromForm] int idAprendiz)
        {
            if (string.IsNullOrEmpty(nombre) || idTipoMaterial == 0)
                return BadRequest("El nombre y el tipo de material son obligatorios.");

            var material = new Material
            {
                Nombre = nombre,
                Observaciones = observaciones,
                IdTipoMaterial = idTipoMaterial,
                IdUsuario = idUsuario,
                IdAprendiz = idAprendiz
            };

            _context.Materiales.Add(material);
            _context.SaveChanges();

            return Ok("Material registrado exitosamente.");
        }
    }
}