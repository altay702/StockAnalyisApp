using System;
using System.Collections.Generic;

namespace StockAnalysisApp.Utilities
{
    public static class MathHelper
    {
        // Example min or max function
        public static double Max(IEnumerable<double> vals)
        {
            double m=double.MinValue;
            foreach(var v in vals) if(v>m) m=v;
            return m;
        }
    }
}
