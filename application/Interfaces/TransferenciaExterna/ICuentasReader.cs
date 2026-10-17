using Domain.Entities;

namespace Application.Interfaces.TransferenciaExterna
{
    public interface ICuentasReader
    {
        Task<Cuenta?> ObtenerPorNumeroAsync(string numeroCuenta);
    }
}