using Antifraude.Batch.Modelos;

namespace Antifraude.Batch.Reglas
{
    // Contrato común de todas las reglas del batch (patrón Strategy).
    // Agregar un factor nuevo = crear una clase nueva, sin tocar las demás.
    public interface IReglaFraude
    {
        ResultadoRegla Evaluar(ContextoCuenta contexto);
    }
}
