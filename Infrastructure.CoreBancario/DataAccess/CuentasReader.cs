using Application.Interfaces.TransferenciaExterna;
using Domain.Entities;
using Infrastructure.CoreBancario.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CoreBancario.DataAccess
{
    public class CuentasReader : ICuentasReader
    {
        private readonly CoreBancarioDbContext _context;
        public CuentasReader(CoreBancarioDbContext context) => _context = context;

        public Task<Cuenta?> ObtenerPorNumeroAsync(string numeroCuenta) =>
            _context.Cuentas.FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta);
    }
}