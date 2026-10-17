using Application.Interfaces.TransferenciaExterna;
using Domain.Entities;

namespace Infrastructure.CoreBancario.DataAccess
{
    public class SaldosWriter : ISaldosWriter
    {
        public Task ActualizarAsync(Saldo saldo)
        {
            return Task.CompletedTask;
        }
    }
}