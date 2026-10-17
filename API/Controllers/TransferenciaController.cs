using Application.Adapters;
using Application.Interfaces.TransferenciaExterna;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> TransferenciaExterna([FromBody] TransferenciaRequest request)
        {
            var result = await _transExternaUseCase.ExecuteAsync(request);

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