using System;
using System.Collections.Generic;

namespace StockAnalysisApp.Analysis
{
    public static class TechnicalIndicators
    {
        public static List<double> SMA(List<double> prices, int period)
        {
            var r=new List<double>();
            for(int i=0;i<prices.Count;i++)
            {
                if(i<period-1) r.Add(double.NaN);
                else
                {
                    double sum=0; for(int j=0;j<period;j++) sum+=prices[i-j];
                    r.Add(sum/period);
                }
            }
            return r;
        }
        public static List<double> EMA(List<double> prices, int period)
        {
            var r=new List<double>();
            double k=2.0/(period+1), prev=0;
            for(int i=0;i<prices.Count;i++)
            {
                if(i<period-1){r.Add(double.NaN);continue;}
                if(i==period-1)
                {
                    double s=0; for(int j=0;j<period;j++) s+=prices[j];
                    prev=s/period; r.Add(prev); 
                }
                else
                {
                    double val=(prices[i]-prev)*k+prev;
                    r.Add(val); prev=val;
                }
            }
            return r;
        }
    }
}
