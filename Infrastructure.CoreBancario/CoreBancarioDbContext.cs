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
                e.Property(c => c.TitularDni).HasColumnName("NumeroDocumentoTitular");
                e.Property(c => c.Banco).HasColumnName("NombreBanco");
                e.Property(c => c.Activa).HasColumnName("EstaActiva");
                e.Property(c => c.Segmento).HasConversion<string>();
                e.Property(c => c.LimiteDiario).HasColumnName("LimiteTransferenciaDiario").HasPrecision(18, 2);
            });

            modelBuilder.Entity<Saldo>(e =>
            {
                e.ToTable("Saldos");
                e.HasKey("NumeroCuenta");
                e.Property(s => s.Monto).HasColumnName("SaldoDisponible").HasPrecision(18, 2);
            });

            modelBuilder.Entity<Transferencia>(e =>
            {
                e.ToTable("Movimientos");
                e.HasKey("CodigoOperacion");
                e.Property(m => m.Monto).HasColumnName("MontoTransferido").HasPrecision(18, 2);
                e.Property(m => m.Comision).HasColumnName("ComisionCobrada").HasPrecision(18, 2);
                e.Property(m => m.Fecha).HasColumnName("FechaOperacion");
                e.Property(m => m.EsInterbancaria).HasColumnName("EsTransferenciaInterbancaria");
            });
        }
    }
}