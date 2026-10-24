using Microsoft.EntityFrameworkCore;

namespace Antifraude.Batch.Datos
{
    public class AntifraudeBatchDbContext : DbContext
    {
        public AntifraudeBatchDbContext(DbContextOptions<AntifraudeBatchDbContext> options) : base(options) { }

        public DbSet<CuentaTable> Cuentas { get; set; }
        public DbSet<MovimientoTable> Movimientos { get; set; }
        public DbSet<PerfilRiesgoCuentaTable> PerfilesRiesgo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CuentaTable>(e =>
            {
                e.ToTable("Cuentas");
                e.HasKey(c => c.NumeroCuenta);
                e.Property(c => c.Activa).HasColumnName("EstaActiva");
            });

            modelBuilder.Entity<MovimientoTable>(e =>
            {
                e.ToTable("Movimientos");
                e.HasKey(m => m.CodigoOperacion);
                e.Property(m => m.Monto).HasColumnName("MontoTransferido");
                e.Property(m => m.Fecha).HasColumnName("FechaOperacion");
            });

            modelBuilder.Entity<PerfilRiesgoCuentaTable>(e =>
            {
                e.ToTable("PerfilRiesgoCuenta");
                e.HasKey(p => p.NumeroCuenta);
                e.Property(p => p.NivelBase).HasConversion<string>().HasMaxLength(5);
                e.Property(p => p.UmbralMedio).HasPrecision(18, 2);
                e.Property(p => p.UmbralAlto).HasPrecision(18, 2);
                e.Property(p => p.Motivos).HasMaxLength(500);
            });
        }
    }
}
