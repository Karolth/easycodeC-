using TuProyecto.Models;

public class EstadoService
{
    private readonly AppDbContext _context;

    public EstadoService(AppDbContext context)
    {
        _context = context;
    }

    public string ObtenerEstadoPorUsuario(int idUsuario)
    {
        var movimiento = _context.Movimientos
            .Where(m => m.IdUsuario == idUsuario)
            .OrderByDescending(m => m.FechaHora)
            .FirstOrDefault();

        return movimiento?.MovimientoTipo;
    }

    public string ObtenerEstadoPorAprendiz(int idAprendiz)
    {
        var movimiento = _context.Movimientos
            .Where(m => m.IdAprendiz == idAprendiz)
            .OrderByDescending(m => m.FechaHora)
            .FirstOrDefault();

        return movimiento?.MovimientoTipo;
    }

    public string ObtenerEstadoMateriales(List<int> idsMateriales)
    {
        var movimientoMaterial = _context.MovimientoMateriales
            .Where(mm => idsMateriales.Contains(mm.IdMaterial))
            .OrderByDescending(mm => mm.FechaHora)
            .FirstOrDefault();

        return movimientoMaterial?.Estado;
    }
}