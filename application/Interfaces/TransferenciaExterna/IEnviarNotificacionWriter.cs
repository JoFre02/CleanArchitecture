namespace Application.Interfaces.TransferenciaExterna
{
    public interface IEnviarNotificacionWriter
    {
        Task EnviarAsync(string destinatario, string mensaje);
    }
}