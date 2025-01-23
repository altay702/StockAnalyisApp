using System;
using StockAnalysisApp.Crypto;
using StockAnalysisApp.Utilities;

namespace StockAnalysisApp
{
    public class CryptoProgram
    {
        public static void Main()
        {
            // Synthetic random crypto data
            Logger.Log($"CryptoProgram v{Config.Version} starting...");
            var assets = CryptoServices.GenerateRandomAssets();

            // Basic stats
            double meanPrice = CryptoAnalysis.MeanPrice(assets);
            double totalMC   = CryptoAnalysis.MarketCapSum(assets);
            double corrPV    = CryptoAnalysis.PriceVolumeCorrelation(assets);

            // Output
            Logger.Log("=== CRYPTO ASSETS ANALYSIS ===");
            Console.WriteLine($"Mean Price : {meanPrice:F2}");
            Console.WriteLine($"Sum MktCap : {totalMC:F2}");
            Console.WriteLine($"Price-Vol Correlation : {corrPV:F2}");
            Console.WriteLine("\nSymbol   Price      MktCap         Volume");
            foreach(var a in assets)
            {
                Console.WriteLine($"{a.Symbol,-6} {a.Price,8:F2} {a.MarketCap,12:F2} {a.Volume,12}");
            }

            Logger.Log("CryptoProgram done. Press any key.");
            Console.ReadKey();
        }
    }
}
