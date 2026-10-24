using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Antifraude.DataModels
{
    public class AntifraudeDbContext : DbContext
    {
        public AntifraudeDbContext(DbContextOptions<AntifraudeDbContext> options) : base(options) { }

        public DbSet<PerfilRiesgoCuentaTable> PerfilesRiesgo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PerfilRiesgoCuentaTable>(e =>
            {
                e.ToTable("PerfilRiesgoCuenta");
                e.HasKey(p => p.NumeroCuenta);
                e.Property(p => p.NivelBase).HasConversion<string>();
                e.Property(p => p.UmbralMedio).HasPrecision(18, 2);
                e.Property(p => p.UmbralAlto).HasPrecision(18, 2);
            });
        }
    }
}
