using System;

namespace StockAnalysisApp.Utilities
{
    public static class Logger
    {
        public static void Log(string message)
        {
            // Minimal console logger
            Console.WriteLine($"[LOG] {message}");
        }
    }
}
