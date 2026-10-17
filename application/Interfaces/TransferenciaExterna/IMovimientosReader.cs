using Domain.Entities;

namespace Application.Interfaces.TransferenciaExterna
{
    public interface IMovimientosReader
    {
        Task<List<Transferencia>> ObtenerPorCuentaAsync(string numeroCuenta, DateTime fecha);
    }
}