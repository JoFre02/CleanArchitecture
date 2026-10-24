using Application.Adapters;
using Application.Executors;

namespace Application.Interfaces.TransferenciaExterna
{
    public interface ITransExternaUseCase
    {
        Task<Result<TransferenciaResponse>> ExecuteAsync(TransferenciaRequest request, TraceIdentifierAdapter trace);
    }

}