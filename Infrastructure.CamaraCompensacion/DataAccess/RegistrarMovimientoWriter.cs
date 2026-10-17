using Application.Interfaces.TransferenciaExterna;
using Domain.Entities;

namespace Infrastructure.CamaraCompensacion.DataAccess
{
    public class RegistrarMovimientoWriter : IRegistrarMovimientoWriter
    {
        public Task<bool> EnviarACamaraCompensacionAsync(Transferencia transferencia)
        {
            return Task.FromResult(true);
        }
    }
}