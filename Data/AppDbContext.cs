using Microsoft.EntityFrameworkCore;
using Easycode.Models; // Asegúrate de que este espacio de nombres sea correcto y contenga tus modelos.

namespace Easycode.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define tus DbSet aquí, por ejemplo:
        public DbSet<Aprendiz> Aprendices { get; set; }
        public DbSet<FichaAprendiz> FichaAprendiz { get; set; }
        public DbSet<Ficha> Fichas { get; set; }
        public DbSet<Programa> Programas { get; set; }
        public DbSet<TipoPrograma> TipoProgramas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<UsuarioRol> UsuarioRoles { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        // Puedes agregar configuraciones adicionales usando el método OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ejemplo de configuración de una relación muchos a muchos
            modelBuilder.Entity<FichaAprendiz>()
                .HasKey(fa => new { fa.IdAprendiz, fa.IdFicha });

            // Otras configuraciones aquí
        }
    }
}