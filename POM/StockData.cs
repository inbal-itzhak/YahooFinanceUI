using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinanceUI.POM
{
    public class StockData
    {
        public string Ticker { get; set; }
        public string Name { get; set; }

        public string Exchange { get; set; }

        public string Currency { get; set; }

        public string Price { get; set; }

        public string PriceChange { get; set; }

        public string PriceChangePercent {  get; set; }

        public string PostMarketPrice {  get; set; }

        public string tPostPriceChange { get; set; }

        public string PostPriceChangePercent { get; set; }


    }
}
