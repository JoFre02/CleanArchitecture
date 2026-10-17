using Application.Interfaces.TransferenciaExterna;
using Domain.Entities;

namespace Infrastructure.CoreBancario.DataAccess
{
    public class MovimientosReader : IMovimientosReader
    {
        public Task<List<Transferencia>> ObtenerPorCuentaAsync(string numeroCuenta, DateTime fecha)
        {
            return Task.FromResult(new List<Transferencia>());
        }
    }
}