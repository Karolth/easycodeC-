using EasyCode.Models;

public class VehiculoService
{
    private readonly AppDbContext _context;

    public VehiculoService(AppDbContext context)
    {
        _context = context;
    }

    public bool RegistrarVehiculo(VehiculoModel vehiculo)
    {
        _context.Vehiculos.Add(vehiculo);
        return _context.SaveChanges() > 0;
    }

    public List<TipoVehiculoModel> ObtenerTiposVehiculo()
    {
        return _context.TipoVehiculos.ToList();
    }
}