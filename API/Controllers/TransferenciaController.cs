using Application.Adapters;
using Application.Interfaces.TransferenciaExterna;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Channels;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferenciaController : ControllerBase
    {
        private readonly ITransExternaUseCase _transExternaUseCase;

        public TransferenciaController(ITransExternaUseCase transExternaUseCase)
        {
            _transExternaUseCase = transExternaUseCase;
        }

        [HttpPost("externa")]
        public async Task<IActionResult> TransferenciaExterna(
        [FromBody] TransferenciaRequest request,
        [FromHeader(Name = "X-Device-Id")] string? deviceId,
        [FromHeader(Name = "X-Message-Id")] string? messageId,
        [FromHeader(Name = "X-Channel-Id")] string? channelId)
        {
            var trace = new TraceIdentifierAdapter
            {
                DeviceIdentifier = deviceId,
                MessageIdentifier = messageId,
                ChannelIdentifier = channelId
            };
            var result = await _transExternaUseCase.ExecuteAsync(request, trace);

            if (!result.Exitoso)
            {
                return result.CodigoError switch
                {
                    "CUENTA_INVALIDA" => NotFound(result),
                    "SALDO_INSUFICIENTE" => BadRequest(result),
                    "LIMITE_EXCEDIDO" => BadRequest(result),
                    "OTP_REQUERIDO" => StatusCode(428, result), // 428 Precondition Required
                    "RIESGO_ALTO" => StatusCode(403, result),   // Forbidden
                    "RIESGO_MEDIO_OTP_REQUERIDO" => StatusCode(428, result),
                    _ => BadRequest(result)
                };
            }

            return Ok(result);
        }
    }
}