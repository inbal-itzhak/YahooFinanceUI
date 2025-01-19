using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinanceUI.POM
{
    public class HistoricalData
    {
        private double _close;
        private double _open;
        private double _high;
        private double _low;
        
        public string Date { get; set; }
        public string Open 
        {
            get => _open.ToString("F2");
            set => _open = double.Parse(value);
        }
        public string High 
        {
            get => _high.ToString("F2");
            set => _high = double.Parse(value);
        }
        public string Low
        {
            get => _low.ToString("F2");
            set => _low = double.Parse(value);
        }
        public string Close
        {
            get => _close.ToString("F2");
            set => _close = double.Parse(value);
        }
        public string AdjClose { get; set; }
        public string Volume { get; set; }

    }
}
