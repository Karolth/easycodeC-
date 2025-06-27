using Microsoft.EntityFrameworkCore;
using TuProyecto.Models;

public class HistorialService
{
    private readonly AppDbContext _context;

    public HistorialService(AppDbContext context)
    {
        _context = context;
    }

    public List<HistorialItem> ObtenerHistorial()
    {
        var historialAprendiz = (from m in _context.Movimientos
                                 join a in _context.Aprendices on m.IdAprendiz equals a.IdAprendiz
                                 join mm in _context.MovimientoMateriales on m.IdMovimiento equals mm.IdMovimiento into mmJoin
                                 from mm in mmJoin.DefaultIfEmpty()
                                 join mat in _context.Materiales on mm.IdMaterial equals mat.IdMaterial into matJoin
                                 from mat in matJoin.DefaultIfEmpty()
                                 join mv in _context.MovimientoVehiculos on m.IdMovimiento equals mv.IdMovimiento into mvJoin
                                 from mv in mvJoin.DefaultIfEmpty()
                                 join v in _context.Vehiculos on mv.IdVehiculo equals v.IdVehiculo into vJoin
                                 from v in vJoin.DefaultIfEmpty()
                                 join tv in _context.TipoVehiculos on v.IdTipoVehiculo equals tv.IdTipoVehiculo into tvJoin
                                 from tv in tvJoin.DefaultIfEmpty()
                                 select new HistorialItem
                                 {
                                     Fecha = m.FechaHora.Date,
                                     Nombre = a.Nombre,
                                     TipoActor = "Aprendiz",
                                     Documento = a.Documento,
                                     FechaHora = m.FechaHora,
                                     Movimiento = m.MovimientoTipo,
                                     NombreMaterial = mat != null ? mat.Nombre : "-",
                                     Referencia = mat != null ? mat.Referencia : "-",
                                     Placa = v != null ? v.Placa : "-",
                                     TipoVehiculo = tv != null ? tv.Tipo : "-"
                                 });

        var historialUsuario = (from m in _context.Movimientos
                                join u in _context.Usuarios on m.IdUsuario equals u.IdUsuario
                                join mm in _context.MovimientoMateriales on m.IdMovimiento equals mm.IdMovimiento into mmJoin
                                from mm in mmJoin.DefaultIfEmpty()
                                join mat in _context.Materiales on mm.IdMaterial equals mat.IdMaterial into matJoin
                                from mat in matJoin.DefaultIfEmpty()
                                join mv in _context.MovimientoVehiculos on m.IdMovimiento equals mv.IdMovimiento into mvJoin
                                from mv in mvJoin.DefaultIfEmpty()
                                join v in _context.Vehiculos on mv.IdVehiculo equals v.IdVehiculo into vJoin
                                from v in vJoin.DefaultIfEmpty()
                                join tv in _context.TipoVehiculos on v.IdTipoVehiculo equals tv.IdTipoVehiculo into tvJoin
                                from tv in tvJoin.DefaultIfEmpty()
                                select new HistorialItem
                                {
                                    Fecha = m.FechaHora.Date,
                                    Nombre = u.Nombre,
                                    TipoActor = "Usuario",
                                    Documento = u.Documento,
                                    FechaHora = m.FechaHora,
                                    Movimiento = m.MovimientoTipo,
                                    NombreMaterial = mat != null ? mat.Nombre : "-",
                                    Referencia = mat != null ? mat.Referencia : "-",
                                    Placa = v != null ? v.Placa : "-",
                                    TipoVehiculo = tv != null ? tv.Tipo : "-"
                                });

        var historial = historialAprendiz
            .Union(historialUsuario)
            .OrderByDescending(h => h.Fecha)
            .ThenByDescending(h => h.FechaHora)
            .ToList();

        // Reemplazar valores null o vacíos por "-"
        foreach (var item in historial)
        {
            item.NombreMaterial = string.IsNullOrEmpty(item.NombreMaterial) ? "-" : item.NombreMaterial;
            item.Referencia = string.IsNullOrEmpty(item.Referencia) ? "-" : item.Referencia;
            item.Placa = string.IsNullOrEmpty(item.Placa) ? "-" : item.Placa;
            item.TipoVehiculo = string.IsNullOrEmpty(item.TipoVehiculo) ? "-" : item.TipoVehiculo;
        }

        return historial;
    }

    // Si tienes la función buscarSugerencias, agrégala aquí
    // public List<HistorialItem> BuscarSugerencias(string term) { ... }
}