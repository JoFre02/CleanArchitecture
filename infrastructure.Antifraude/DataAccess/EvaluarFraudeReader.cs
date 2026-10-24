using Application.Interfaces.TransferenciaExterna;
using Domain.Enums;
using Infrastructure.Antifraude.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Antifraude.DataAccess
{
    // Adaptador hacia el Sistema Antifraude (batch independiente).
    // No calcula reglas: solo lee el perfil que el batch publicó y lo aplica según el contrato.
    public class EvaluarFraudeReader : IEvaluarFraudeReader
    {
        // El batch corre cada 5 minutos; si el perfil es más viejo, el batch podría estar caído.
        private static readonly TimeSpan VigenciaPerfil = TimeSpan.FromMinutes(30);

        // Política del contrato cuando no hay perfil confiable: pedir OTP y aplicar los límites globales.
        private static readonly PerfilRiesgoCuentaTable PerfilPorDefecto = new()
        {
            NivelBase = NivelRiesgo.Medio,
            UmbralMedio = 10000,
            UmbralAlto = 50000
        };

        private readonly AntifraudeDbContext _context;
        private readonly TimeProvider _reloj;

        public EvaluarFraudeReader(AntifraudeDbContext context, TimeProvider reloj)
        {
            _context = context;
            _reloj = reloj;
        }

        public async Task<NivelRiesgo> EvaluarAsync(string cuentaOrigen, decimal monto)
        {
            var perfil = await _context.PerfilesRiesgo.AsNoTracking()
                .FirstOrDefaultAsync(p => p.NumeroCuenta == cuentaOrigen);

            var limiteVigencia = _reloj.GetLocalNow().DateTime - VigenciaPerfil;
            if (perfil is null || perfil.FechaCalculo < limiteVigencia)
                perfil = PerfilPorDefecto;

            NivelRiesgo nivelPorMonto =
                monto >= perfil.UmbralAlto ? NivelRiesgo.Alto :
                monto >= perfil.UmbralMedio ? NivelRiesgo.Medio :
                NivelRiesgo.Bajo;

            // Nivel final = el mayor de los dos (el enum está ordenado Bajo < Medio < Alto).
            return (NivelRiesgo)Math.Max((int)perfil.NivelBase, (int)nivelPorMonto);
        }
    }
}
