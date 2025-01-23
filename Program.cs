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

            Console.WriteLine("\n=== Real-Time Top 10 Stocks ===");
            await FetchAndPrintTop10();
            Console.WriteLine("\nDone (Stocks). Press any key to exit.");
            Console.ReadKey();
        }

        static async Task FetchAndPrintTop10()
        {
            var symbols = new string[] { "AAPL","MSFT","GOOGL","AMZN","TSLA","NVDA","META","BRK-B","JPM","V" };
            var url = "https://query1.finance.yahoo.com/v7/finance/quote?symbols=" + string.Join(",", symbols);

            using var client = new HttpClient();
            // A longer or different User-Agent might help with HTTP request
            client.DefaultRequestHeaders.Add("User-Agent", "C# StockAnalysisApp/2.0 (https://example.com)");

            for(int attempt=1; attempt<=3; attempt++)
            {
                try
                {
                    Console.WriteLine($"(Attempt {attempt}) Fetching top 10 stocks...");
                    var response = await client.GetAsync(url);

                    if(!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"HTTP {response.StatusCode}, waiting 5s...");
                        await Task.Delay(5000);
                        continue;
                    }

                    // Success
                    var json = await response.Content.ReadAsStringAsync();
                    var doc = JsonDocument.Parse(json);
                    var results = doc.RootElement
                                     .GetProperty("quoteResponse")
                                     .GetProperty("result");

                    foreach(var item in results.EnumerateArray())
                    {
                        var sym = item.GetProperty("symbol").GetString();
                        var shortName = item.TryGetProperty("shortName", out var snVal) ? snVal.GetString() : sym;
                        var price = item.TryGetProperty("regularMarketPrice", out var pVal) ? pVal.GetDouble() : 0;
                        var cap = item.TryGetProperty("marketCap", out var cVal) ? cVal.GetDouble() : 0;
                        Console.WriteLine($"{sym,-6} {shortName,-25} Price={price,10:F2}  MarketCap={cap,15:F0}");
                    }
                    return; // done if success
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}, waiting 5s...");
                    await Task.Delay(5000);
                }
            }

            Console.WriteLine("Failed after 3 attempts. Skipping stock data.");
        }
    }
}
