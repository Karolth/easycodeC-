using TuProyecto.Models;

public class MovimientoVehiculoService
{
    private readonly AppDbContext _context;

    public MovimientoVehiculoService(AppDbContext context)
    {
        _context = context;
    }

    public bool RegistrarMovimientoVehiculo(string estado, int idMovimiento, int idVehiculo)
    {
        var movimientoVehiculo = new MovimientoVehiculo
        {
            Estado = estado,
            IdMovimiento = idMovimiento,
            IdVehiculo = idVehiculo
        };
        _context.MovimientoVehiculos.Add(movimientoVehiculo);
        return _context.SaveChanges() > 0;
    }
}