using Application.Interfaces.TransferenciaExterna;
using Domain.Entities;

namespace Infrastructure.CoreBancario.DataAccess
{
    public class MovimientosWriter : IMovimientosWriter
    {
        public Task<bool> RegistrarAsync(Transferencia transferencia)
        {
            return Task.FromResult(true);
        }
    }
}