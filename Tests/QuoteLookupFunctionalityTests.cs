using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.NUnit;
using YahooFinanceUI.POM;
using PolygonTests.APITests.PolygonAPI;
using Allure.Net.Commons;
using Allure.NUnit.Attributes;

namespace YahooFinanceUI.Tests
{
    [AllureNUnit]
    [AllureEpic("Quote Lookup functionality")]
    public class QuoteLookupFunctionalityTests : BaseTest
    {
        private static Dictionary<string, StockData> stockDataCache = new Dictionary<string, StockData>();
        
        private async Task<StockData> GetStockQuoteData(string ticker)
        {
            if (!stockDataCache.ContainsKey(ticker))
            {
                Console.WriteLine($"Fetching data for ticker: {ticker}");
                PolygonSteps polygonSteps = new PolygonSteps(PolygonClient);
                var polygonResponseData = await polygonSteps.GetStockCompanyData(ticker);

                Assert.That(polygonResponseData, Is.Not.Null, $"Failed to fetch data for ticker: {ticker}");

                var stockInfo = new StockData
                {
                    Ticker = polygonResponseData.results.Ticker,
                    Name = polygonResponseData.results.Name,
                    Exchange = polygonResponseData.results.Primary_Exchange,
                    Currency = polygonResponseData.results.Currency_Name
                };

                stockDataCache[ticker] = stockInfo;
            }

            return stockDataCache[ticker];
        }


        [Test, Description("Search for a stock by its ticker symbol"),Category("Quote Lookup"),
            TestCaseSource(typeof(DataProvider), nameof(DataProvider.TickersToTest))]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("Lookup Quote")]
        [AllureStory("General quote lookup")]
        
        public void LookupQuote(string ticker, string _)
        {
                quoteLookup.LookupQuote(ticker);
                string stockSymbol = stockData.GetStockSymbol(ticker);
                Assert.That(stockSymbol, Is.EqualTo(ticker.ToUpper()), $"Stock symbol mismatch, expected: {ticker.ToUpper()}," +
                $" actual: {stockSymbol}");
        }

        [Test, Description("Verify the stock name matches expected data from Polygon.IO API"), Category("Quote Lookup"),
            TestCaseSource(typeof(DataProvider), nameof(DataProvider.TickersToTest))]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("Lookup Quote")]
        [AllureStory("General quote lookup")]
        public async Task VerifyStockName(string ticker, string _) 
        {
            var stockDataFromAPI =  GetStockQuoteData(ticker);
            quoteLookup.LookupQuote(ticker);
            var stockName = stockPage.GetStockName(ticker);
            Assert.That(stockName, Is.EqualTo(stockDataFromAPI.Result.Name).IgnoreCase,$"Stock name mismatch, expected: {stockDataFromAPI.Result.Name}," +
                $" actual: {stockName}");

        }

        [Test, Description("Verify the stock exchange name matches the expected data from Polygon.IO API"), Category("Quote Lookup"),
           TestCaseSource(typeof(DataProvider), nameof(DataProvider.TickersToTest))]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("Lookup Quote")]
        [AllureStory("General quote lookup")]
        public async Task VerifyStockExchange(string ticker, string _)
        {
            var stockDataFromAPI = GetStockQuoteData(ticker);
            quoteLookup.LookupQuote(ticker);
            var exchange = stockPage.GetPrimaryExchangeName(ticker);
            Assert.That(exchange, Is.EqualTo(stockDataFromAPI.Result.Exchange).IgnoreCase, $"Stock exchnage name mismatch, expected: {stockDataFromAPI.Result.Exchange}," +
               $" actual: {exchange}");
        }

        [Test, Description("Verify the stock currency matches the expected data from Polygon.IO API"), Category("Quote Lookup"),
         TestCaseSource(typeof(DataProvider), nameof(DataProvider.TickersToTest))]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("Lookup Quote")]
        [AllureStory("General quote lookup")]
        public async Task VerifyStockCurrency(string ticker, string _)
        {
            var stockDataFromAPI = GetStockQuoteData(ticker);
            quoteLookup.LookupQuote(ticker);
            var currency = stockPage.GetStockCurrencyName(ticker);
            Assert.That(currency, Is.EqualTo(stockDataFromAPI.Result.Currency).IgnoreCase, $"Stock name mismatch, expected: {stockDataFromAPI.Result.Currency}," +
               $" actual: {currency}");
        }


        [Test, Description("Search for stocks using the company name"), Category("Quote Lookup"),
            TestCaseSource(typeof(DataProvider), nameof(DataProvider.TickersToTest))]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("Lookup Quote")]
        [AllureStory("General quote lookup")]
        public async Task SearchQuoteByCompanyName(string ticker, string companyName)
        {
            var stockDataFromAPI = GetStockQuoteData(ticker);
            quoteLookup.LookupQuote(companyName);
            string stockSymbol = stockData.GetStockSymbol(ticker);
            Assert.That(stockSymbol, Is.EqualTo(ticker.ToUpper()), $"Stock symbol mismatch, expected: {ticker.ToUpper()}," +
                $" actual: {stockSymbol}");
        }




    }
}
