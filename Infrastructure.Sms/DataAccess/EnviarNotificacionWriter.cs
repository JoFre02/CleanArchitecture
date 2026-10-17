using Application.Interfaces.TransferenciaExterna;

namespace Infrastructure.Sms.DataAccess
{
    public class EnviarNotificacionWriter : IEnviarNotificacionWriter
    {
        public Task EnviarAsync(string destinatario, string mensaje)
        {
            Console.WriteLine($"[SMS Mock] Para: {destinatario} - Mensaje: {mensaje}");
            return Task.CompletedTask;
        }
    }
}