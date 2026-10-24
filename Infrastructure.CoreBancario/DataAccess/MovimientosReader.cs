using Application.Interfaces.TransferenciaExterna;
using Domain.Entities;
using Infrastructure.CoreBancario.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CoreBancario.DataAccess
{
    public class MovimientosReader : IMovimientosReader
    {
        private readonly CoreBancarioDbContext _context;
        public MovimientosReader(CoreBancarioDbContext context) => _context = context;

        public Task<List<Transferencia>> ObtenerPorCuentaAsync(string numeroCuenta, DateTime fecha) =>
            _context.Movimientos
                .Where(m => m.CuentaOrigen == numeroCuenta && m.Fecha.Date == fecha.Date)
                .ToListAsync();
    }
}