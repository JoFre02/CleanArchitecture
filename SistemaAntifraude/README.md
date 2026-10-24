# Sistema Antifraude (batch)

Sistema independiente que, cada 5 minutos, calcula un **perfil de riesgo por cuenta**
y lo publica en la tabla `PerfilRiesgoCuenta`. El sistema de Transferencias no conoce
este código: solo lee esa tabla desde `Infrastructure.Antifraude`.

## Contrato: tabla `PerfilRiesgoCuenta`

| Columna        | Tipo           | Significado |
|----------------|----------------|-------------|
| `NumeroCuenta` | NVARCHAR(450)  | Cuenta evaluada (PK) |
| `NivelBase`    | `'Bajo'` / `'Medio'` / `'Alto'` | Riesgo propio de la cuenta, sin importar el monto |
| `UmbralMedio`  | DECIMAL(18,2)  | Desde este monto, una transferencia es al menos Medio |
| `UmbralAlto`   | DECIMAL(18,2)  | Desde este monto, una transferencia es Alto |
| `Motivos`      | NVARCHAR(500)  | Explicación del nivel (auditoría) |
| `FechaCalculo` | DATETIME2      | Cuándo se calculó |

**Regla de uso para quien consume el contrato:**

- Nivel final = el **mayor** entre `NivelBase` y el nivel que corresponde al monto según los umbrales.
- Si no existe perfil para la cuenta, o `FechaCalculo` tiene más de 30 minutos, el perfil
  no es confiable: usar `NivelBase = Medio`, `UmbralMedio = 10000`, `UmbralAlto = 50000`.

## Cómo ejecutarlo

1. Crear la tabla: `scripts/01_crear_tabla_PerfilRiesgoCuenta.sql`
2. Revisar la cadena de conexión en `Antifraude.Batch/appsettings.json`
3. Una corrida manual: `ejecutar_antifraude.bat`
4. Programarlo cada 5 minutos: `programar_tarea.bat`
