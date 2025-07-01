using EasyCode.Models;

public class OtroMaterialService
{
    private readonly AppDbContext _context;

    public OtroMaterialService(AppDbContext context)
    {
        _context = context;
    }

    public bool RegistrarOtroMaterial(string nombre, string observaciones, int idTipoMaterial, int? idUsuario, int? idAprendiz)
    {
        var material = new MaterialModel
        {
            Nombre = nombre,
            Observaciones = observaciones,
            IdTipoMaterial = idTipoMaterial,
            IdUsuario = idUsuario,
            IdAprendiz = idAprendiz
        };
        _context.Materiales.Add(material);
        return _context.SaveChanges() > 0;
    }
}