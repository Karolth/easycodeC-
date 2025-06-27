using TuProyecto.Models;

public class UsuarioRolService
{
    private readonly AppDbContext _context;

    public UsuarioRolService(AppDbContext context)
    {
        _context = context;
    }

    public int RegistrarUsuario(string documento, string nombre, string email, string celular)
    {
        var usuario = new Usuario
        {
            Documento = documento,
            Nombre = nombre,
            Email = email,
            Celular = celular
        };
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
        return usuario.IdUsuario;
    }

    public bool AsignarRol(int idUsuario, int idRol)
    {
        var usuarioRol = new UsuarioRol
        {
            IdUsuario = idUsuario,
            IdRol = idRol
        };
        _context.UsuarioRoles.Add(usuarioRol);
        return _context.SaveChanges() > 0;
    }
}