using Application.Interfaces.TransferenciaExterna;
using Domain.Entities;
using Infrastructure.CoreBancario.DataModels;

namespace Infrastructure.CoreBancario.DataAccess
{
    public class MovimientosWriter : IMovimientosWriter
    {
        private readonly CoreBancarioDbContext _context;
        public MovimientosWriter(CoreBancarioDbContext context) => _context = context;

        public async Task RegistrarAsync(Transferencia transferencia)
        {
            _context.Movimientos.Add(transferencia);
            await _context.SaveChangesAsync();
        }
    }
}