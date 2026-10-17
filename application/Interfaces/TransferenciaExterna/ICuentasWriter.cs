using Domain.Entities;

namespace Application.Interfaces.TransferenciaExterna
{
    public interface ICuentasWriter
    {
        Task ActualizarAsync(Cuenta cuenta);
    }
}