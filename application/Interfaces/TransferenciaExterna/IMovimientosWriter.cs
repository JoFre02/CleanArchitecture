using Domain.Entities;

namespace Application.Interfaces.TransferenciaExterna
{
    public interface IMovimientosWriter
    {
        Task RegistrarAsync(Transferencia transferencia);
    }
}