using Microsoft.AspNetCore.Mvc;
using System;

namespace Easycode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrarMaterial : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegistrarMaterial(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Registrar([FromForm] string nombre, [FromForm] string referencia, [FromForm] string marca,
                                      [FromForm] string observaciones, [FromForm] int idTipoMaterial,
                                      [FromForm] int idUsuario, [FromForm] int idAprendiz)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(referencia) || string.IsNullOrEmpty(marca) ||
                idTipoMaterial == 0 || idUsuario == 0 || idAprendiz == 0)
            {
                return BadRequest("Todos los campos son obligatorios.");
            }

            var material = new Material
            {
                Nombre = nombre,
                Referencia = referencia,
                Marca = marca,
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