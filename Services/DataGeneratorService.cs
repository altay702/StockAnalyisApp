using System;
using System.Collections.Generic;
using StockAnalysisApp.Models;

namespace StockAnalysisApp.Services
{
    public static class DataGeneratorService
    {
        public static List<Candle> Generate(int days)
        {
            var list = new List<Candle>();
            var rnd = new Random();
            double prev = 100;
            for(int i=0; i<days; i++)
            {
                double open = prev + (rnd.NextDouble()-0.5)*2;
                double high = open + rnd.NextDouble()*3;
                double low = open - rnd.NextDouble()*3;
                double close = low + rnd.NextDouble()*(high-low);
                var vol = rnd.Next(1000, 5000);
                list.Add(new Candle{Open=open, High=high, Low=low, Close=close, Volume=vol});
                prev=close;
            }
            return list;
        }
    }
}
