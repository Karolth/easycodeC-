using System;

namespace Easycode
{
    public class Material
    {
        private readonly AppDbContext _context;

        public Material(AppDbContext context)
        {
            _context = context;
        }

        public bool RegistrarMaterial(string nombre, string referencia, string marca, string observaciones, int idTipoMaterial, int idUsuario, int idAprendiz)
        {
            var material = new MaterialEntidad
            {
                Nombre = nombre,
                Referencia = referencia,
                Marca = marca,
                Observaciones = observaciones,
                IdTipoMaterial = idTipoMaterial,
                IdUsuario = idUsuario,
                IdAprendiz = idAprendiz
            };
            _context.Materiales.Add(material);
            return _context.SaveChanges() > 0;
        }
    }
}