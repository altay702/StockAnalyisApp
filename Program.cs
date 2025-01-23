using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using StockAnalysisApp.Models;
using StockAnalysisApp.Services;
using StockAnalysisApp.Analysis;

namespace StockAnalysisApp
{
    class Program
    {
        static async Task Main()
        {
            var data = DataGeneratorService.Generate(50);
            var meanClose = StatisticalAnalysis.Mean(data.Select(d => d.Close));
            var varClose = StatisticalAnalysis.Variance(data.Select(d => d.Close));
            var sdClose = Math.Sqrt(varClose);
            var corr = StatisticalAnalysis.VolumePriceCorrelation(data);
            var closes = data.Select(d => d.Close).ToList();
            var sma20 = TechnicalIndicators.SMA(closes, 20);
            var ema20 = TechnicalIndicators.EMA(closes, 20);
            bool hasHnS = PatternDetection.DetectHeadShoulders(data);

            Console.WriteLine("=== Synthetic Stock Analysis ===");
            Console.WriteLine($"Mean: {meanClose:F2}, Var: {varClose:F2}, StdDev: {sdClose:F2}");
            Console.WriteLine($"Volume-Price Corr: {corr:F2}, Head&Shoulders: {hasHnS}");

            Console.WriteLine("\nDay  Open   High   Low   Close  Volume  SMA20   EMA20");
            for(int i=0; i<data.Count; i++)
            {
                var s = double.IsNaN(sma20[i]) ? "" : sma20[i].ToString("F2");
                var e = double.IsNaN(ema20[i]) ? "" : ema20[i].ToString("F2");
                Console.WriteLine($"{i+1,2}  {data[i].Open,6:F2} {data[i].High,6:F2} {data[i].Low,6:F2} {data[i].Close,6:F2} {data[i].Volume,6}  {s,5}  {e,5}");
            }

            // Real-time top 10 stocks
            Console.WriteLine("\n=== Real-Time Top 10 Stocks ===");
            // Increase the wait period to 30 seconds to reduce rate-limit risk
            for(int round=1; round<=3; round++)
            {
                Console.WriteLine($"\nUpdate #{round}");
                await FetchAndPrintTop10();
                await Task.Delay(30000); // wait 30 seconds between updates
            }
            Console.WriteLine("\nDone. Press any key.");
            Console.ReadKey();
        }

        static async Task FetchAndPrintTop10()
        {
            // Some frequently watched stocks
            var symbols = new string[] { "AAPL","MSFT","GOOGL","AMZN","TSLA","NVDA","META","BRK-B","JPM","V" };

            // Use HttpClient with a custom User-Agent
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# StockApp/1.0");

            var url = "https://query1.finance.yahoo.com/v7/finance/quote?symbols="
                      + string.Join(",", symbols);

            try
            {
                // Attempt the request
                var response = await client.GetAsync(url);
                // If not successful, do a quick retry if we hit 429 or other errors
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Got HTTP {(int)response.StatusCode} ({response.ReasonPhrase}). Trying again in 10s...");
                    await Task.Delay(10000); // wait 10 seconds
                    response = await client.GetAsync(url);
                }
                response.EnsureSuccessStatusCode();

                // Parse JSON
                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var results = doc.RootElement
                                 .GetProperty("quoteResponse")
                                 .GetProperty("result");

                // Print each symbol's current price + market cap
                foreach(var item in results.EnumerateArray())
                {
                    var sym = item.GetProperty("symbol").GetString();
                    double price = item.TryGetProperty("regularMarketPrice", out var p) ? p.GetDouble() : 0;
                    double cap = item.TryGetProperty("marketCap", out var c) ? c.GetDouble() : 0;
                    Console.WriteLine($"{sym,-6} Price={price,10:F2}  MarketCap={cap,15:F0}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error fetching top 10 stocks: {ex.Message}");
            }
        }
    }
}
