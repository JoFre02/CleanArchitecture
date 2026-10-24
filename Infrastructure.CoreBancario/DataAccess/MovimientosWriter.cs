using Application.Interfaces.TransferenciaExterna;
using Domain.Entities;
using Infrastructure.CoreBancario.DataModels;

namespace Infrastructure.CoreBancario.DataAccess
{
    public class MovimientosWriter : IMovimientosWriter
    {
        private readonly CoreBancarioDbContext _context;
        public MovimientosWriter(CoreBancarioDbContext context) => _context = context;

        public async Task<bool> RegistrarAsync(Transferencia transferencia)
        {
            _context.Movimientos.Add(transferencia);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}