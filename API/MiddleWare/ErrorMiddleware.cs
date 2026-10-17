using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var respuesta = new
                {
                    Exitoso = false,
                    Mensaje = "Ocurrió un error inesperado",
                    Detalle = ex.Message
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(respuesta));
            }
        }
    }
}