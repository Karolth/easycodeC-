using TuProyecto.Models;
using Microsoft.EntityFrameworkCore;

public class UsuarioISService
{
    private readonly AppDbContext _context;

    public UsuarioISService(AppDbContext context)
    {
        _context = context;
    }

    public Usuario ObtenerUsuarioPorDocumento(string documento)
    {
        return _context.Usuarios.FirstOrDefault(u => u.Documento == documento);
    }

    public string ObtenerRolPorUsuario(int idUsuario)
    {
        var rol = (from ur in _context.UsuarioRoles
                   join r in _context.Roles on ur.IdRol equals r.IdRol
                   where ur.IdUsuario == idUsuario
                   select r.Rol).FirstOrDefault();
        return rol;
    }

    public Usuario ObtenerPerfilPorId(int idUsuario)
    {
        return _context.Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
    }

    public List<object> ObtenerHistorialLogin()
    {
        var historial = (from m in _context.Movimientos
                         join u in _context.Usuarios on m.IdUsuario equals u.IdUsuario
                         join ur in _context.UsuarioRoles on u.IdUsuario equals ur.IdUsuario
                         join r in _context.Roles on ur.IdRol equals r.IdRol
                         where m.MovimientoTipo == "Entrada" && r.Rol == "Administrador"
                         orderby m.FechaHora descending
                         select new
                         {
                             u.Documento,
                             u.Nombre,
                             m.FechaHora,
                             Rol = r.Rol
                         }).ToList<object>();
        return historial;
    }
}