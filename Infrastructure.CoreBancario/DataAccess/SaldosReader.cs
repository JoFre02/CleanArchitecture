using Application.Interfaces.TransferenciaExterna;
using Domain.Entities;

namespace Infrastructure.CoreBancario.DataAccess
{
    public class SaldosReader : ISaldosReader
    {
        public Task<Saldo?> ObtenerPorCuentaAsync(string numeroCuenta)
        {
            var saldo = new Saldo
            {
                NumeroCuenta = numeroCuenta,
                Monto = 20000
            };

            return Task.FromResult<Saldo?>(saldo);
        }
    }
}