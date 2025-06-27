using Microsoft.EntityFrameworkCore;
using TuProyecto.Models;

public class AdministradorService
{
    private readonly AppDbContext _context;

    public AdministradorService(AppDbContext context)
    {
        _context = context;
    }

    public Aprendiz BuscarAprendiz(string documento)
    {
        return (from a in _context.Aprendices
                join fa in _context.FichaAprendiz on a.IdAprendiz equals fa.IdAprendiz
                join f in _context.Fichas on fa.IdFicha equals f.IdFicha
                join p in _context.Programas on f.IdPrograma equals p.IdPrograma
                join tp in _context.TipoProgramas on p.IdTipoPrograma equals tp.IdTipoPrograma
                where a.Documento == documento
                select new Aprendiz
                {
                    IdAprendiz = a.IdAprendiz,
                    Nombre = a.Nombre,
                    RH = a.RH,
                    Documento = a.Documento,
                    TipoPrograma = tp.TipoPrograma,
                    Programa = p.Nombre
                }).FirstOrDefault();
    }

    public Usuario BuscarUsuario(string documento)
    {
        return (from u in _context.Usuarios
                join ur in _context.UsuarioRol on u.IdUsuario equals ur.IdUsuario
                join r in _context.Roles on ur.IdRol equals r.IdRol
                where u.Documento == documento
                select new Usuario
                {
                    IdUsuario = u.IdUsuario,
                    Nombre = u.Nombre,
                    Email = u.Email,
                    Rol = r.Rol
                }).FirstOrDefault();
    }

    public Movimiento ObtenerUltimoMovimiento(int? idUsuario, int? idAprendiz)
    {
        return _context.Movimientos
            .Where(m => m.IdUsuario == idUsuario || m.IdAprendiz == idAprendiz)
            .OrderByDescending(m => m.FechaHora)
            .FirstOrDefault();
    }

    public void InsertarMovimiento(string movimiento, int? idUsuario, int? idAprendiz)
    {
        var nuevoMovimiento = new Movimiento
        {
            FechaHora = DateTime.Now,
            MovimientoTipo = movimiento,
            IdUsuario = idUsuario,
            IdAprendiz = idAprendiz
        };
        _context.Movimientos.Add(nuevoMovimiento);
        _context.SaveChanges();
    }
}