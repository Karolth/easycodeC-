using TuProyecto.Models;
using Microsoft.EntityFrameworkCore;

public class ElementoService
{
    private readonly AppDbContext _context;

    public ElementoService(AppDbContext context)
    {
        _context = context;
    }

    public List<object> ObtenerMaterialesPorUsuario(int idUsuario, string tipoUsuario)
    {
        IQueryable<Material> query;

        if (tipoUsuario == "aprendiz")
            query = _context.Materiales.Where(m => m.IdAprendiz == idUsuario);
        else
            query = _context.Materiales.Where(m => m.IdUsuario == idUsuario);

        var materiales = query
            .Include(m => m.TipoMaterial)
            .GroupJoin(
                _context.MovimientoMateriales,
                m => m.IdMaterial,
                mm => mm.IdMaterial,
                (m, mm) => new { m, mm }
            )
            .SelectMany(
                temp => temp.mm.DefaultIfEmpty(),
                (temp, mm) => new
                {
                    IdMaterial = temp.m.IdMaterial,
                    Nombre = temp.m.Nombre,
                    Referencia = temp.m.Referencia,
                    Marca = temp.m.Marca,
                    Tipo = temp.m.TipoMaterial.Tipo,
                    Estado = mm != null ? mm.Estado : null
                }
            )
            .OrderByDescending(x => x.IdMaterial)
            .ToList<object>();

        return materiales;
    }

    public List<object> ObtenerVehiculosPorUsuario(int idUsuario, string tipoUsuario)
    {
        IQueryable<Vehiculo> query;

        if (tipoUsuario == "aprendiz")
            query = _context.Vehiculos.Where(v => v.IdAprendiz == idUsuario);
        else
            query = _context.Vehiculos.Where(v => v.IdUsuario == idUsuario);

        var vehiculos = query
            .Include(v => v.TipoVehiculo)
            .GroupJoin(
                _context.MovimientoVehiculos,
                v => v.IdVehiculo,
                mv => mv.IdVehiculo,
                (v, mv) => new { v, mv }
            )
            .SelectMany(
                temp => temp.mv.DefaultIfEmpty(),
                (temp, mv) => new
                {
                    IdVehiculo = temp.v.IdVehiculo,
                    Placa = temp.v.Placa,
                    Tipo = temp.v.TipoVehiculo.Tipo,
                    Estado = mv != null ? mv.Estado : null
                }
            )
            .OrderByDescending(x => x.IdVehiculo)
            .ToList<object>();

        return vehiculos;
    }
}