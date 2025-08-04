@echo off
echo Iniciando aplicación de Tienda de Celulares...
echo.

echo 1. Iniciando Backend (.NET API)...
start "Backend API" cmd /k "cd BackendIphoneStore\BackendIphoneStore && dotnet run"

echo 2. Esperando 10 segundos para que el backend inicie...
timeout /t 10 /nobreak > nul

echo 3. Iniciando Frontend (Angular)...
start "Frontend Angular" cmd /k "cd FrontendIphoneStore && npm start"

echo.
echo ¡Aplicación iniciada!
echo Backend: https://localhost:7000
echo Frontend: http://localhost:4200
echo.
echo Usuario por defecto:
echo Email: admin@iphone.com
echo Password: admin123
echo.
pause