using System;
using System.Collections.Generic;

namespace StockAnalysisApp.Crypto
{
    public static class CryptoServices
    {
        // Simple random data generator for demonstration 
        public static List<CryptoAsset> GenerateRandomAssets()
        {
            var list = new List<CryptoAsset>();
            var rnd = new Random();
            // Let's make 5 sample cryptos
            var names = new string[]{"BTC","ETH","BNB","XRP","SOL"};
            foreach(var sym in names)
            {
                double price = rnd.Next(10,50000) + rnd.NextDouble();
                double cap   = price * rnd.Next(500000,2000000);
                long vol     = rnd.Next(100000,10000000);
                list.Add(new CryptoAsset{ Symbol=sym, Price=price, MarketCap=cap, Volume=vol });
            }
            return list;
        }
    }
}
