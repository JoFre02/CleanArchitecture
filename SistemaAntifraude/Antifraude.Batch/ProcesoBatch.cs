using Antifraude.Batch.Datos;
using Antifraude.Batch.Modelos;
using Antifraude.Batch.Perfil;
using Microsoft.EntityFrameworkCore;

namespace Antifraude.Batch
{
    // Orquesta una corrida del batch: leer datos, calcular perfiles y guardarlos.
    public class ProcesoBatch
    {
        private static readonly TimeSpan HistorialAnalizado = TimeSpan.FromDays(90);

        private readonly AntifraudeBatchDbContext _context;
        private readonly CalculadoraPerfil _calculadora;

        public ProcesoBatch(AntifraudeBatchDbContext context, CalculadoraPerfil calculadora)
        {
            _context = context;
            _calculadora = calculadora;
        }

        public async Task EjecutarAsync(DateTime ahora)
        {
            Console.WriteLine($"[Batch] Inicio de corrida: {ahora:yyyy-MM-dd HH:mm:ss}");

            // Paso 1: leer cuentas activas y movimientos de los últimos 90 días (solo lectura).
            var cuentas = await _context.Cuentas.AsNoTracking()
                .Where(c => c.Activa)
                .Select(c => c.NumeroCuenta)
                .ToListAsync();

            var desde = ahora - HistorialAnalizado;
            var movimientos = await _context.Movimientos.AsNoTracking()
                .Where(m => m.Fecha >= desde && m.Fecha <= ahora)
                .Select(m => new { m.CuentaOrigen, m.Monto, m.Fecha })
                .ToListAsync();

            // Agrupamos por cuenta una sola vez, en vez de consultar la BD por cada cuenta.
            var movimientosPorCuenta = movimientos.ToLookup(m => m.CuentaOrigen, m => new Movimiento(m.Monto, m.Fecha));

            // Paso 2: traer los perfiles existentes para actualizarlos en lugar de duplicarlos.
            var perfiles = await _context.PerfilesRiesgo.ToDictionaryAsync(p => p.NumeroCuenta);

            // Paso 3: calcular el perfil de cada cuenta (aquí está el "lote").
            foreach (var cuenta in cuentas)
            {
                var perfil = _calculadora.Calcular(new ContextoCuenta(cuenta, movimientosPorCuenta[cuenta].ToList(), ahora));

                if (!perfiles.TryGetValue(cuenta, out var fila))
                {
                    fila = new PerfilRiesgoCuentaTable { NumeroCuenta = cuenta };
                    _context.PerfilesRiesgo.Add(fila);
                }

                fila.NivelBase = perfil.NivelBase;
                fila.UmbralMedio = perfil.UmbralMedio;
                fila.UmbralAlto = perfil.UmbralAlto;
                fila.Motivos = perfil.Motivos.Count == 0 ? null : string.Join("; ", perfil.Motivos);
                fila.FechaCalculo = ahora;

                Console.WriteLine($"  {cuenta}: {perfil.NivelBase} (puntaje {perfil.Puntaje}) | Medio desde S/ {perfil.UmbralMedio} | Alto desde S/ {perfil.UmbralAlto}");
                foreach (var motivo in perfil.Motivos)
                    Console.WriteLine($"      - {motivo}");
            }

            // Paso 4: guardar todo junto en una sola transacción.
            await _context.SaveChangesAsync();
            Console.WriteLine($"[Batch] Fin: {cuentas.Count} perfiles actualizados");
        }
    }
}
