using Domain.Entities;

namespace Application.Interfaces.TransferenciaExterna
{
    public interface ISaldosReader
    {
        Task<Saldo?> ObtenerPorCuentaAsync(string numeroCuenta);
    }
}