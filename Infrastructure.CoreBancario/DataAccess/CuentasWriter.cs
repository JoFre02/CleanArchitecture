using Application.Interfaces.TransferenciaExterna;
using Domain.Entities;

namespace Infrastructure.CoreBancario.DataAccess
{
    public class CuentasWriter : ICuentasWriter
    {
        public Task ActualizarAsync(Cuenta cuenta)
        {
            return Task.CompletedTask;
        }
    }
}