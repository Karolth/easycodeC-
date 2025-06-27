using TuProyecto.Models;

public class UsuarioService
{
    private readonly AppDbContext _context;

    public UsuarioService(AppDbContext context)
    {
        _context = context;
    }

    public List<HistorialLoginItem> ObtenerHistorialLogin()
    {
        // Ajusta los nombres de tablas y columnas según tu base de datos real
        var historial = (from h in _context.HistorialLogins
                         join u in _context.Usuarios on h.IdUsuario equals u.IdUsuario
                         select new HistorialLoginItem
                         {
                             IdUsuario = u.IdUsuario,
                             Nombre = u.Nombre,
                             Email = u.Email,
                             FechaHora = h.FechaHora,
                             Accion = h.Accion
                         }).ToList();

        return historial;
    }
}