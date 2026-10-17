using Application.Interfaces.TransferenciaExterna;
using Application.UseCases;
using Infrastructure.Antifraude.DataAccess;
using Infrastructure.CamaraCompensacion.DataAccess;
using Infrastructure.CoreBancario.DataAccess;
using Infrastructure.Sms.DataAccess;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyección de dependencias: interfaz -> implementación mock
builder.Services.AddScoped<ICuentasReader, CuentasReader>();
builder.Services.AddScoped<ICuentasWriter, CuentasWriter>();
builder.Services.AddScoped<ISaldosReader, SaldosReader>();
builder.Services.AddScoped<ISaldosWriter, SaldosWriter>();
builder.Services.AddScoped<IMovimientosReader, MovimientosReader>();
builder.Services.AddScoped<IMovimientosWriter, MovimientosWriter>();
builder.Services.AddScoped<IRegistrarMovimientoWriter, RegistrarMovimientoWriter>();
builder.Services.AddScoped<IEvaluarFraudeReader, EvaluarFraudeReader>();
builder.Services.AddScoped<IEnviarNotificacionWriter, EnviarNotificacionWriter>();

builder.Services.AddScoped<ITransExternaUseCase, TransExternaUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorMiddleware>();

app.UseAuthorization();
app.MapControllers();

app.Run();