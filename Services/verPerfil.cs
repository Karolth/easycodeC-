using TuProyecto.Models;

public class PerfilService
{
    private readonly AppDbContext _context;

    public PerfilService(AppDbContext context)
    {
        _context = context;
    }

    public Usuario ObtenerPerfilPorId(int id)
    {
        return _context.Usuarios
            .Where(u => u.IdUsuario == id)
            .Select(u => new Usuario
            {
                IdUsuario = u.IdUsuario,
                Nombre = u.Nombre,
                Documento = u.Documento,
                Email = u.Email,
                Celular = u.Celular
            })
            .FirstOrDefault();
    }

    public bool ActualizarPerfil(int id, string email, string celular)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
        if (usuario == null) return false;

        usuario.Email = email;
        usuario.Celular = celular;
        return _context.SaveChanges() > 0;
    }
}