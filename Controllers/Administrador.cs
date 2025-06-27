using Microsoft.AspNetCore.Mvc;
using TuProyecto.Models;
using TuProyecto.Services;

[ApiController]
[Route("[controller]")]
public class AdministradorController : ControllerBase
{
    private readonly AdministradorService _service;
    private readonly IWebHostEnvironment _env;

    public AdministradorController(AdministradorService service, IWebHostEnvironment env)
    {
        _service = service;
        _env = env;
    }

    [HttpGet("BuscarPorDocumento")]
    public IActionResult BuscarPorDocumento(string documento)
    {
        string tipoPersona = "";
        int? idUsuario = null;
        int? idAprendiz = null;
        string rutaImagen = "";

        var aprendiz = _service.BuscarAprendiz(documento);
        Usuario usuario = null;

        if (aprendiz != null)
        {
            tipoPersona = "aprendiz";
            idAprendiz = aprendiz.IdAprendiz;
            rutaImagen = Path.Combine("/Imagenes", $"{documento}.jpg");
            var imagenFisica = Path.Combine(_env.WebRootPath, "Imagenes", $"{documento}.jpg");
            if (!System.IO.File.Exists(imagenFisica))
                rutaImagen = "/Imagenes/default-user.png";
        }
        else
        {
            usuario = _service.BuscarUsuario(documento);
            if (usuario != null)
            {
                tipoPersona = "usuario";
                idUsuario = usuario.IdUsuario;
                rutaImagen = "/Imagenes/default-user.png";
            }
            else
            {
                return Ok(new { error = "No se encontró el documento en la base de datos." });
            }
        }

        var ultimoMovimiento = _service.ObtenerUltimoMovimiento(idUsuario, idAprendiz);
        var nuevoMovimiento = (ultimoMovimiento != null && ultimoMovimiento.MovimientoTipo == "Entrada") ? "Salida" : "Entrada";
        _service.InsertarMovimiento(nuevoMovimiento, idUsuario, idAprendiz);

        return Ok(new
        {
            tipo = tipoPersona,
            datos = aprendiz ?? usuario,
            imagen = rutaImagen,
            movimiento = nuevoMovimiento
        });
    }
}