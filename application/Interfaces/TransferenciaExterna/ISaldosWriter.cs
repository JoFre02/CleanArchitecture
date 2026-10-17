using Domain.Entities;

namespace Application.Interfaces.TransferenciaExterna
{
    public interface ISaldosWriter
    {
        Task ActualizarAsync(Saldo saldo);
    }
}