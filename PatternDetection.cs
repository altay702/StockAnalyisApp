using System.Collections.Generic;
using StockAnalysisApp.Models;

namespace StockAnalysisApp
{
    public static class PatternDetection
    {
        public static bool DetectHeadShoulders(List<Candle> data)
        {
            for(int i=2; i<data.Count-2; i++)
            {
                double left=data[i-1].Close, mid=data[i].Close, right=data[i+1].Close;
                if(mid>left && mid>right)
                {
                    if(data[i-2].Close<mid && data[i+2].Close<mid) return true;
                }
            }
            return false;
        }
    }
}
