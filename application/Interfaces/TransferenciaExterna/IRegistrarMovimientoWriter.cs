using Domain.Entities;

namespace Application.Interfaces.TransferenciaExterna
{
    public interface IRegistrarMovimientoWriter
    {
        Task<bool> EnviarACamaraCompensacionAsync(Transferencia transferencia);
    }
}