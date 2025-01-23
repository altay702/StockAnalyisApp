# Stock & Crypto Analysis App

A **C# console application** that I made in notepad++ that can analyze **stocks** (synthetic data + Yahoo Finance info) or **crypto** (synthetic random data).  

1. **`Program.cs`** - Main entry point for **Stocks**.  
2. **`CryptoProgram.cs`** - Main entry point for **Crypto**.  
3. **`run.bat`** - Lets you **choose** whether to run Stocks or Crypto option.
4. **Various directories** contain relevant code for **analysis**, **models**, **services**, and **utilities**.

## Requirements

1. **.NET 6+** installed ([Download .NET](https://dotnet.microsoft.com/download)).  
2. **Internet connection** if you want to fetch real-time stock data from Yahoo Finance (some calls are made in `Program.cs`).  
3. **Errors** Sometimes the 401 http request does not work just hope to be lucky.

## How to Build & Run

1. **Open a terminal** in the `StockAnalysisApp` folder.  
2. **Build** the project (optional, the batch file also triggers a build if needed):
   ```bash
   dotnet build
   ```
3. **Run** the batch file:
   ```bash
   run.bat
   ```
4. **Select**:
   - **1** for **Stocks**  
   - **2** for **Crypto**  

--
**Enjoy** exploring both the Stocks and Crypto options!