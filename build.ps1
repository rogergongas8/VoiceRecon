# Script simple para compilar y ejecutar VoiceRecon

Write-Host "==================================" -ForegroundColor Cyan
Write-Host "  VoiceRecon - Build & Run" -ForegroundColor Cyan
Write-Host "==================================" -ForegroundColor Cyan
Write-Host ""

# Compilar
Write-Host "Compilando proyecto..." -ForegroundColor Yellow
dotnet build --configuration Release

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "✓ Compilación exitosa!" -ForegroundColor Green
    Write-Host ""
    Write-Host "Ejecutando aplicación..." -ForegroundColor Yellow
    
    # Ejecutar
    Start-Process ".\bin\Release\net8.0-windows\VoiceRecon.exe"
    
    Write-Host "✓ Aplicación iniciada!" -ForegroundColor Green
} else {
    Write-Host ""
    Write-Host "✗ Error en la compilación" -ForegroundColor Red
}
