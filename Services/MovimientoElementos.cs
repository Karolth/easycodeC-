using TuProyecto.Models;
using Microsoft.EntityFrameworkCore;

public class MovimientoElementoService
{
    private readonly AppDbContext _context;

    public MovimientoElementoService(AppDbContext context)
    {
        _context = context;
    }

    public Movimiento ObtenerUltimoMovimiento()
    {
        return _context.Movimientos
            .OrderByDescending(m => m.IdMovimiento)
            .FirstOrDefault();
    }

    public int CrearMovimiento(string estado)
    {
        var movimiento = new Movimiento
        {
            MovimientoTipo = estado,
            FechaHora = DateTime.Now
        };
        _context.Movimientos.Add(movimiento);
        _context.SaveChanges();
        return movimiento.IdMovimiento;
    }

    public void InsertarMaterial(string movimiento, int idMovimiento, int idMaterial)
    {
        var existe = _context.MovimientoMateriales
            .Any(mm => mm.Estado == movimiento && mm.IdMaterial == idMaterial);

        if (!existe)
        {
            var nuevo = new MovimientoMaterial
            {
                Estado = movimiento,
                IdMovimiento = idMovimiento,
                IdMaterial = idMaterial
            };
            _context.MovimientoMateriales.Add(nuevo);
            _context.SaveChanges();
        }
    }

    public void InsertarVehiculo(string estado, int idMovimiento, int idVehiculo)
    {
        var existe = _context.MovimientoVehiculos
            .Any(mv => mv.Estado == estado && mv.IdVehiculo == idVehiculo);

        if (!existe)
        {
            var nuevo = new MovimientoVehiculo
            {
                Estado = estado,
                IdMovimiento = idMovimiento,
                IdVehiculo = idVehiculo
            };
            _context.MovimientoVehiculos.Add(nuevo);
            _context.SaveChanges();
        }
    }
}