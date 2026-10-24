using Domain.Entities;

namespace Application.Interfaces.TransferenciaExterna
{
    public interface IMovimientosWriter
    {
        Task<bool> RegistrarAsync(Transferencia transferencia);
    }
}