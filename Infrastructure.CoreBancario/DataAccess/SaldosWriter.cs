using Application.Interfaces.TransferenciaExterna;
using Domain.Entities;
using Infrastructure.CoreBancario.DataModels;

namespace Infrastructure.CoreBancario.DataAccess
{
    public class SaldosWriter : ISaldosWriter
    {
        private readonly CoreBancarioDbContext _context;
        public SaldosWriter(CoreBancarioDbContext context) => _context = context;

        public async Task ActualizarAsync(Saldo saldo)
        {
            _context.Saldos.Update(saldo);
            await _context.SaveChangesAsync();
        }
    }
}