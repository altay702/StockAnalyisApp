using System;
using System.Collections.Generic;
using System.Linq;
using StockAnalysisApp.Crypto;

namespace StockAnalysisApp.Crypto
{
    public static class CryptoAnalysis
    {
        // Very basic stats for demonstration
        public static double MeanPrice(IEnumerable<CryptoAsset> assets)
        {
            var arr = assets.Select(a => a.Price).ToArray();
            if(arr.Length==0)return 0;
            double sum=0;foreach(var v in arr) sum+=v; 
            return sum/arr.Length;
        }

        public static double MarketCapSum(IEnumerable<CryptoAsset> assets)
        {
            double total=0;
            foreach(var a in assets) total+=a.MarketCap;
            return total;
        }

        // Example correlation between Price and Volume for demonstration
        public static double PriceVolumeCorrelation(List<CryptoAsset> assets)
        {
            if(assets.Count<2) return 0;
            var prices = assets.Select(a=>a.Price).ToList();
            var vols   = assets.Select(a=> (double)a.Volume).ToList();
            return Pearson(prices, vols);
        }

        static double Pearson(List<double> x, List<double> y)
        {
            double mx=Mean(x), my=Mean(y), num=0, denx=0, deny=0;
            for(int i=0;i<x.Count;i++)
            {
                double dx=x[i]-mx, dy=y[i]-my;
                num+=dx*dy; denx+=dx*dx; deny+=dy*dy;
            }
            return (denx==0||deny==0)?0:num/Math.Sqrt(denx*deny);
        }

        static double Mean(IEnumerable<double> vals)
        {
            double s=0;int c=0; 
            foreach(var v in vals){s+=v;c++;}
            return c==0?0:s/c;
        }
    }
}
