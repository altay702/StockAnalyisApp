@echo off
title -Made by Altay702
chcp 65001
cls
echo                             █████  ███    ██  █████  ██      ██    ██ ███████ ███████ ██████  
echo                            ██   ██ ████   ██ ██   ██ ██       ██  ██  ██      ██      ██   ██ 
echo                            ███████ ██ ██  ██ ███████ ██        ████   ███████ █████   ██████  
echo                            ██   ██ ██  ██ ██ ██   ██ ██         ██         ██ ██      ██   ██ 
echo                            ██   ██ ██   ████ ██   ██ ███████    ██    ███████ ███████ ██   ██ 
echo                                          Made by github.com/altay702
echo.
echo 1) Stocks 
echo 2) Crypto
echo.
set /p choice="Enter your choice: "

if "%choice%"=="1" (
    color 5
    echo Cleaning old build...
    dotnet clean
    if ERRORLEVEL 1 (
        echo Clean failed.
        pause
        exit /b
    )
    echo Building Stock Program...
    dotnet build /p:StartupObject=StockAnalysisApp.Program /p:Configuration=Debug
    if ERRORLEVEL 1 (
        echo Build failed.
        pause
        exit /b
    )
    echo Running Stock Analysis...
    dotnet bin\Debug\net6.0\StockAnalysisApp.dll
    color 07
    echo Cleaning after run...
    dotnet clean
    pause
    exit /b
) else if "%choice%"=="2" (
    color 5
    echo Cleaning old build...
    dotnet clean
    if ERRORLEVEL 1 (
        echo Clean failed.
        pause
        exit /b
    )
    echo Building Crypto Program...
    dotnet build /p:StartupObject=StockAnalysisApp.CryptoProgram /p:Configuration=Debug
    if ERRORLEVEL 1 (
        echo Build failed.
        pause
        exit /b
    )
    echo Running Crypto Analysis...
    dotnet bin\Debug\net6.0\StockAnalysisApp.dll
    color 07
    echo Cleaning after run...
    dotnet clean
    pause
    exit /b
) else (
    echo Invalid selection. Exiting.
    pause
    exit /b
)
