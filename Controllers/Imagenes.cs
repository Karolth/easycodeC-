using Microsoft.AspNetCore.Mvc;
using TuProyecto.Services;

[ApiController]
[Route("[controller]")]
public class ImagenController : ControllerBase
{
    private readonly ImagenService _service;

    public ImagenController(ImagenService service)
    {
        _service = service;
    }

    [HttpPost("subir")]
    public IActionResult SubirImagen([FromForm] IFormFile imagen)
    {
        if (imagen == null || imagen.Length == 0)
            return BadRequest("No se recibió ninguna imagen.");

        using (var ms = new MemoryStream())
        {
            imagen.CopyTo(ms);
            var contenido = ms.ToArray();
            var nombreArchivo = Path.GetFileNameWithoutExtension(imagen.FileName);

            if (_service.GuardarImagen(nombreArchivo, contenido))
                return Ok($"Imagen '{imagen.FileName}' subida con éxito.");
            else
                return BadRequest($"Error al subir la imagen '{imagen.FileName}'.");
        }
    }
}