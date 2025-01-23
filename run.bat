@echo off
title Analyser github.com/altay702
chcp 65001
color 5
cls
echo                             █████  ███    ██  █████  ██      ██    ██ ███████ ███████ ██████  
echo                            ██   ██ ████   ██ ██   ██ ██       ██  ██  ██      ██      ██   ██ 
echo                            ███████ ██ ██  ██ ███████ ██        ████   ███████ █████   ██████  
echo                            ██   ██ ██  ██ ██ ██   ██ ██         ██         ██ ██      ██   ██ 
echo                            ██   ██ ██   ████ ██   ██ ███████    ██    ███████ ███████ ██   ██ 
echo                                          Made by github.com/altay702
echo 1) Stocks
echo 2) Crypto
echo.
set /p choice="Enter your choice: "

if "%choice%"=="1" (
    echo Building and Running Stocks Program...
    dotnet build /p:StartupObject=StockAnalysisApp.Program /p:Configuration=Debug
    if ERRORLEVEL 1 (
        echo Build failed.
        pause
        exit /b
    )
    echo.
    echo Running StockAnalysisApp...
    dotnet bin\Debug\net6.0\StockAnalysisApp.dll
) else if "%choice%"=="2" (
    echo Building and Running Crypto Program...
    dotnet build /p:StartupObject=StockAnalysisApp.CryptoProgram /p:Configuration=Debug
    if ERRORLEVEL 1 (
        echo Build failed.
        pause
        exit /b
    )
    echo.
    echo Running StockAnalysisApp...
    dotnet bin\Debug\net6.0\StockAnalysisApp.dll
) else (
    echo Invalid selection. Exiting.
)
