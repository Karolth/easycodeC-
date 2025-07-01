using Microsoft.EntityFrameworkCore;

namespace Easycode.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TipoPrograma> TipoProgramas { get; set; }
        public DbSet<Programa> Programas { get; set; }
        public DbSet<Aprendiz> Aprendices { get; set; }
        public DbSet<Ficha> Fichas { get; set; }
        public DbSet<FichaAprendiz> FichaAprendiz { get; set; }
        public DbSet<TipoVehiculo> TipoVehiculos { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<TipoMaterial> TipoMateriales { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<UsuarioRol> UsuarioRoles { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<MovimientoMaterial> MovimientoMateriales { get; set; }
        public DbSet<MovimientoVehiculo> MovimientoVehiculos { get; set; }
        public DbSet<ImagenAprendiz> ImagenesAprendiz { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// Ejemplo de configuración para nombres de tablas y claves foráneas si es necesario
            //modelBuilder.Entity<TipoPrograma>().ToTable("tipoprograma");
            //modelBuilder.Entity<Programa>().ToTable("programa");
            //modelBuilder.Entity<Aprendiz>().ToTable("aprendiz");
            //modelBuilder.Entity<Ficha>().ToTable("ficha");
            //modelBuilder.Entity<FichaAprendiz>().ToTable("fichaaprendiz");
            //modelBuilder.Entity<TipoVehiculo>().ToTable("tipovehiculo");
            //modelBuilder.Entity<Vehiculo>().ToTable("vehiculo");
            //modelBuilder.Entity<TipoMaterial>().ToTable("tipomaterial");
            //modelBuilder.Entity<Usuario>().ToTable("usuario");
            //modelBuilder.Entity<Rol>().ToTable("rol");
            //modelBuilder.Entity<UsuarioRol>().ToTable("usuariorol");
            //modelBuilder.Entity<Material>().ToTable("material");
            //modelBuilder.Entity<Movimiento>().ToTable("movimiento");
            //modelBuilder.Entity<MovimientoMaterial>().ToTable("movimientomaterial");
            //modelBuilder.Entity<MovimientoVehiculo>().ToTable("movimientovehiculo");
            //modelBuilder.Entity<ImagenAprendiz>().ToTable("imagenes_aprendiz");

            // Ejemplo de configuración de claves primarias compuestas o relaciones si lo necesitas
            // modelBuilder.Entity<FichaAprendiz>()
            //     .HasKey(fa => new { fa.IdFicha, fa.IdAprendiz });

            // Puedes agregar más configuraciones aquí si necesitas personalizar las relaciones
        }
    }
}