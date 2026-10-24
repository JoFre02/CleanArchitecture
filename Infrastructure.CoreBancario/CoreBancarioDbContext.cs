using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CoreBancario.DataModels
{
    public class CoreBancarioDbContext : DbContext
    {
        public CoreBancarioDbContext(DbContextOptions<CoreBancarioDbContext> options) : base(options) { }

        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Saldo> Saldos { get; set; }
        public DbSet<Transferencia> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuenta>(e =>
            {
                e.ToTable("Cuentas");
                e.HasKey("NumeroCuenta");
                e.Property(c => c.Segmento).HasConversion<string>();
            });

            modelBuilder.Entity<Saldo>(e =>
            {
                e.ToTable("Saldos");
                e.HasKey("NumeroCuenta");
            });

            modelBuilder.Entity<Transferencia>(e =>
            {
                e.ToTable("Movimientos");
                e.HasKey("CodigoOperacion");
            });
        }
    }
}