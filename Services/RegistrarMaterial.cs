using TuProyecto.Models;

public class MaterialService
{
    private readonly AppDbContext _context;

    public MaterialService(AppDbContext context)
    {
        _context = context;
    }

    public bool RegistrarMaterial(Material material)
    {
        _context.Materiales.Add(material);
        return _context.SaveChanges() > 0;
    }
}