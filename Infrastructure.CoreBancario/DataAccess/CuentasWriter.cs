using Application.Interfaces.TransferenciaExterna;
using Domain.Entities;
using Infrastructure.CoreBancario.DataModels;

namespace Infrastructure.CoreBancario.DataAccess
{
    public class CuentasWriter : ICuentasWriter
    {
        private readonly CoreBancarioDbContext _context;
        public CuentasWriter(CoreBancarioDbContext context) => _context = context;

        public async Task ActualizarAsync(Cuenta cuenta)
        {
            _context.Cuentas.Update(cuenta);
            await _context.SaveChangesAsync();
        }
    }
}