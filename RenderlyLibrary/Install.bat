@echo off
echo Installing OpenTK via NuGet...
echo.

REM Check if dotnet CLI is available
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo "dotnet CLI not found. Please install .NET SDK first."
    pause
    exit /b
)

REM Install OpenTK in the current directory/project
dotnet add package OpenTK --version 4.8.0

echo.
echo OpenTK installation complete!
pause
