using EasyCode.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Easycode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagenesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ImagenesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult SubirImagenes([FromForm] List<IFormFile> imagenes)
        {
            if (imagenes == null || imagenes.Count == 0)
                return BadRequest("No se recibieron imágenes.");

            var tiposValidos = new[] { "image/jpeg", "image/png", "image/gif" };
            var tamañoMaximo = 2 * 1024 * 1024; // 2MB

            foreach (var imagen in imagenes)
            {
                if (!tiposValidos.Contains(imagen.ContentType))
                    return BadRequest($"El archivo {imagen.FileName} no es una imagen válida.");

                if (imagen.Length > tamañoMaximo)
                    return BadRequest($"El archivo {imagen.FileName} es demasiado grande (Máximo 2MB).");

                using var ms = new MemoryStream();
                imagen.CopyTo(ms);
                var contenido = ms.ToArray();

                var nombreSinExtension = Path.GetFileNameWithoutExtension(imagen.FileName);

                var imagenEntidad = new ImagenAprendiz
                {
                    NombreArchivo = nombreSinExtension,
                    RutaArchivo = contenido // Ajusta según tu modelo
                };
                _context.ImagenesAprendiz.Add(imagenEntidad);
            }

            _context.SaveChanges();
            return Ok("Imágenes subidas correctamente.");
        }
    }
}