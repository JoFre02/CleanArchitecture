@echo off
REM ============================================================
REM  Ejecuta UNA corrida del batch antifraude.
REM  Muestra el resultado en pantalla y lo agrega a logs\antifraude.log
REM  Con doble clic se queda abierto al final; el Programador de
REM  tareas lo llama con /auto para que se cierre solo.
REM ============================================================

cd /d "%~dp0"
if not exist logs mkdir logs

set SALIDA=%TEMP%\antifraude_ultima_corrida.log

dotnet run --project Antifraude.Batch\Antifraude.Batch.csproj -c Release > "%SALIDA%" 2>&1
set RESULTADO=%ERRORLEVEL%

type "%SALIDA%"
echo ===== %date% %time% (codigo de salida: %RESULTADO%) ===== >> logs\antifraude.log
type "%SALIDA%" >> logs\antifraude.log

if /i not "%~1"=="/auto" pause
exit /b %RESULTADO%
