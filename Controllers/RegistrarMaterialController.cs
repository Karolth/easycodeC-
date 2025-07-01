using Microsoft.AspNetCore.Mvc;
using System;

namespace Easycode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrarMaterialController : ControllerBase
    {
        private readonly AppDbContext _context;

<<<<<<< HEAD:Controllers/RegistrarMaterialController.cs
    [HttpPost("registrar")]
    public IActionResult Registrar([FromForm] MaterialModel material)
    {
        if (string.IsNullOrEmpty(material.Nombre) || string.IsNullOrEmpty(material.Referencia) ||
            string.IsNullOrEmpty(material.Marca) || material.IdTipoMaterial == 0 ||
            (material.IdUsuario == null && material.IdAprendiz == null))
=======
        public RegistrarMaterialController(AppDbContext context)
>>>>>>> 9870ee2aabb8417c4885ff7ebad518bfd9254f03:Controllers/RegistrarMaterial.cs
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