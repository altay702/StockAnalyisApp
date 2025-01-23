using System;
using System.Collections.Generic;
using System.Linq;
using StockAnalysisApp.Models;

namespace StockAnalysisApp.Analysis
{
    public static class StatisticalAnalysis
    {
        public static double Mean(IEnumerable<double> vals)
        {
            double sum=0; int c=0;
            foreach(var v in vals){sum+=v;c++;}
            return c==0?0:sum/c;
        }
        public static double Variance(IEnumerable<double> vals)
        {
            var arr = vals.ToArray();
            if(arr.Length<2)return 0;
            double m=Mean(arr), s=0;
            foreach(var a in arr) s+=(a-m)*(a-m);
            return s/(arr.Length-1);
        }
        public static double VolumePriceCorrelation(List<Candle> data)
        {
            if(data.Count<2)return 0;
            var rets=new List<double>(); var vols=new List<double>();
            for(int i=1;i<data.Count;i++)
            {
                double ret=(data[i].Close - data[i-1].Close)/data[i-1].Close;
                rets.Add(ret); vols.Add(data[i].Volume);
            }
            return Pearson(rets, vols);
        }
        static double Pearson(List<double> x, List<double> y)
        {
            double mx=Mean(x), my=Mean(y);
            double num=0, denx=0, deny=0;
            for(int i=0;i<x.Count;i++)
            {
                double dx=x[i]-mx, dy=y[i]-my;
                num+=dx*dy; denx+=dx*dx; deny+=dy*dy;
            }
            return (denx==0||deny==0)?0:num/Math.Sqrt(denx*deny);
        }
    }
}
