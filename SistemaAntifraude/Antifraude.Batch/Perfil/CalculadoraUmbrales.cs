using Antifraude.Batch.Modelos;
using Antifraude.Batch.Reglas;

namespace Antifraude.Batch.Perfil
{
    // El batch no conoce el monto de la PRÓXIMA transferencia, así que publica
    // los montos límite a partir de los cuales una operación de esa cuenta es Medio o Alto.
    public static class CalculadoraUmbrales
    {
        // Límites globales del banco: ninguna cuenta puede tener umbrales mayores.
        private const decimal TopeMedio = 10000;
        private const decimal TopeAlto = 50000;

        // Pisos: evitan umbrales absurdamente bajos para quien siempre envía montos pequeños.
        private const decimal PisoMedio = 1000;
        private const decimal PisoAlto = 5000;

        // Cuenta sin historial suficiente: umbrales conservadores.
        private const decimal MedioCuentaNueva = 3000;
        private const decimal AltoCuentaNueva = 10000;

        public static (decimal UmbralMedio, decimal UmbralAlto) Calcular(IReadOnlyList<Movimiento> historial)
        {
            if (historial.Count < ReglaCuentaNueva.MinimoMovimientos)
                return (MedioCuentaNueva, AltoCuentaNueva);

            // Personalizado: 5 veces su promedio ya es inusual, 20 veces es muy inusual.
            decimal promedio = historial.Average(m => m.Monto);
            decimal medio = Math.Clamp(promedio * 5, PisoMedio, TopeMedio);
            decimal alto = Math.Clamp(promedio * 20, PisoAlto, TopeAlto);

            return (Math.Round(medio, 2), Math.Round(alto, 2));
        }
    }
}
