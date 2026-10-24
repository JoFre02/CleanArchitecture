using Antifraude.Batch;
using Antifraude.Batch.Datos;
using Antifraude.Batch.Perfil;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// Punto de entrada del batch: hace UNA corrida y termina.
// La repetición cada 5 minutos la hace el Programador de tareas (ver programar_tarea.bat).

var configuracion = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

var opciones = new DbContextOptionsBuilder<AntifraudeBatchDbContext>()
    .UseSqlServer(configuracion.GetConnectionString("AntifraudeDb"))
    .Options;

try
{
    using var context = new AntifraudeBatchDbContext(opciones);
    var proceso = new ProcesoBatch(context, new CalculadoraPerfil());
    await proceso.EjecutarAsync(DateTime.Now);
    return 0;
}
catch (Exception ex)
{
    // El código de salida 1 le avisa al .bat (y al Programador de tareas) que la corrida falló.
    Console.Error.WriteLine($"[Batch] ERROR: {ex.Message}");
    return 1;
}
