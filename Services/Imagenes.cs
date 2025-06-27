using TuProyecto.Models;

public class ImagenService
{
    private readonly AppDbContext _context;

    public ImagenService(AppDbContext context)
    {
        _context = context;
    }

    public bool GuardarImagen(string nombreArchivo, byte[] contenido)
    {
        var imagen = new ImagenAprendiz
        {
            NombreArchivo = nombreArchivo,
            RutaArchivo = contenido // Ajusta según tu modelo
        };
        _context.ImagenesAprendiz.Add(imagen);
        return _context.SaveChanges() > 0;
    }
}