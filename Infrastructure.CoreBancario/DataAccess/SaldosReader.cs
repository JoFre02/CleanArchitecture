using Application.Interfaces.TransferenciaExterna;
using Domain.Entities;
using Infrastructure.CoreBancario.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CoreBancario.DataAccess
{
    public class SaldosReader : ISaldosReader
    {
        private readonly CoreBancarioDbContext _context;
        public SaldosReader(CoreBancarioDbContext context) => _context = context;

        public Task<Saldo?> ObtenerPorCuentaAsync(string numeroCuenta) =>
            _context.Saldos.FirstOrDefaultAsync(s => s.NumeroCuenta == numeroCuenta);
    }
}