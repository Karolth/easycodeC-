using Microsoft.AspNetCore.Mvc;
using TuProyecto.Services;

[ApiController]
[Route("[controller]")]
public class UsuarioRolController : ControllerBase
{
    private readonly UsuarioRolService _service;

    public UsuarioRolController(UsuarioRolService service)
    {
        _service = service;
    }

    [HttpPost("registrar")]
    public IActionResult Registrar([FromForm] string documento, [FromForm] string nombre, [FromForm] string email,
                                   [FromForm] string celular, [FromForm] int rol)
    {
        if (string.IsNullOrEmpty(documento) || string.IsNullOrEmpty(nombre) ||
            string.IsNullOrEmpty(email) || string.IsNullOrEmpty(celular) || rol == 0)
        {
            return BadRequest(new { error = "Todos los campos son obligatorios." });
        }

        using var transaction = _service._context.Database.BeginTransaction();
        try
        {
            var idUsuario = _service.RegistrarUsuario(documento, nombre, email, celular);
            if (idUsuario > 0)
            {
                if (_service.AsignarRol(idUsuario, rol))
                {
                    transaction.Commit();
                    return Ok(new { success = "Usuario registrado y rol asignado correctamente." });
                }
                else
                {
                    transaction.Rollback();
                    return BadRequest(new { error = "Error al asignar el rol." });
                }
            }
            else
            {
                transaction.Rollback();
                return BadRequest(new { error = "Error al registrar el usuario." });
            }
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return BadRequest(new { error = "Excepción capturada: " + ex.Message });
        }
    }
}