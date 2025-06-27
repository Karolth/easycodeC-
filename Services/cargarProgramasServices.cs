using Microsoft.EntityFrameworkCore;
using TuProyecto.Models;
using System.Globalization;

public class FichaService
{
    private readonly AppDbContext _context;

    public FichaService(AppDbContext context)
    {
        _context = context;
    }

    public List<Programa> ObtenerProgramas()
    {
        return _context.Programas.ToList();
    }

    public int InsertarFicha(string numeroFicha, DateTime fechaInicio, DateTime fechaFin, string jornada, int idPrograma)
    {
        var ficha = new Ficha
        {
            Numficha = numeroFicha,
            FechaInicio = fechaInicio,
            FechaFinal = fechaFin,
            Jornada = jornada,
            IdPrograma = idPrograma
        };
        _context.Fichas.Add(ficha);
        _context.SaveChanges();
        return ficha.IdFicha;
    }

    public void ImportarAprendicesDesdeCsv(Stream archivoStream)
    {
        using (var reader = new StreamReader(archivoStream))
        {
            string line;
            int i = 0;
            while ((line = reader.ReadLine()) != null)
            {
                if (i != 0)
                {
                    var datos = line.Split(';');
                    var nombre = datos[0]?.Trim();
                    var documento = datos[1]?.Trim();
                    var rh = datos[2]?.Trim();

                    if (!string.IsNullOrEmpty(documento))
                    {
                        var aprendiz = _context.Aprendices.FirstOrDefault(a => a.Documento == documento);
                        if (aprendiz == null)
                        {
                            aprendiz = new Aprendiz
                            {
                                Nombre = nombre,
                                Documento = documento,
                                RH = rh
                            };
                            _context.Aprendices.Add(aprendiz);
                        }
                        else
                        {
                            aprendiz.Nombre = nombre;
                            aprendiz.RH = rh;
                            _context.Aprendices.Update(aprendiz);
                        }
                    }
                }
                i++;
            }
            _context.SaveChanges();
        }
    }

    public int? ObtenerIdAprendizPorDocumento(string documento)
    {
        var aprendiz = _context.Aprendices.FirstOrDefault(a => a.Documento == documento);
        return aprendiz?.IdAprendiz;
    }

    public void AsignarAprendizAFicha(int idFicha, int idAprendiz)
    {
        var existe = _context.FichaAprendiz.Any(fa => fa.IdFicha == idFicha && fa.IdAprendiz == idAprendiz);
        if (!existe)
        {
            var fichaAprendiz = new FichaAprendiz
            {
                IdFicha = idFicha,
                IdAprendiz = idAprendiz
            };
            _context.FichaAprendiz.Add(fichaAprendiz);
            _context.SaveChanges();
        }
    }
}