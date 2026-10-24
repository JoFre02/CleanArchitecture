@echo off
REM ============================================================
REM  Registra el batch en el Programador de tareas de Windows
REM  para que se ejecute cada 5 minutos.
REM  Para quitarla: schtasks /Delete /TN "SistemaAntifraude\BatchPerfilesRiesgo" /F
REM ============================================================

schtasks /Create /TN "SistemaAntifraude\BatchPerfilesRiesgo" /TR "%~dp0ejecutar_antifraude.bat /auto"/SC MINUTE /MO 5 /F
