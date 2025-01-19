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
using PolygonTests.SeleniumTests.POM;
using HistoricalData = YahooFinanceUI.POM.HistoricalData;


namespace YahooFinanceUI.Tests
{
    [AllureNUnit]
    [AllureEpic("Historical Data Validation")]
    public class HistoricalDataTests :BaseTest
    {
        private static Dictionary<string, HistoricalData> historicalDataCache = new Dictionary<string, HistoricalData>();
        DateTime date;
        private async Task<HistoricalData> GetHistoricalData(string ticker)
        {
            if (!historicalDataCache.ContainsKey(ticker))
            {
                Console.WriteLine($"Fetching data for ticker: {ticker}");
                PolygonSteps polygonSteps = new PolygonSteps(PolygonClient);
               date = polygonSteps.GetPreviousBusinessDay();

                var polygonResponseData = await polygonSteps.GetStockOpenCloseDataByDatePolygonIO(ticker, date.ToString("yyyy-MM-dd"));
                Assert.That(polygonResponseData, Is.Not.Null, $"Failed to fetch data for ticker: {ticker}");

                var data = new HistoricalData
                {
                    Date = date.ToString(),
                  //  Symbol = polygonResponseData.Symbol,
                    Open = polygonResponseData.Open.ToString(),
                    Close = polygonResponseData.Close.ToString(),
                    Volume = polygonResponseData.Volume.ToString(),
                };

                historicalDataCache[ticker] = data;
            }

            return historicalDataCache[ticker];
        }



        [Test, TestCaseSource(typeof(DataProvider), nameof(DataProvider.TickersToTest))]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureStory("Verify the stock's open rate matches expected data from Polygon.IO API")]
        public async Task ValidateOpenRate(string ticker, string _)
        {
            try
            {
                var historicalDataFromAPI = await GetHistoricalData(ticker);
                quoteLookup.LookupQuote(ticker);
                stockData.NanigateToHistoricalData();
                POM.HistoricalData siteHistoricalData = stockData.GetStockDataByDate(ticker, date);
                Assert.That(historicalDataFromAPI.Open.ToString(), Is.EqualTo(siteHistoricalData.Open),
                    $"Open Rate mismatch: Expected {siteHistoricalData.Open}, but got {historicalDataFromAPI.Open}");
            }
            catch (Exception ex) 
            {
                Assert.Fail( ex.Message );
            }
            
        }

        [Test, TestCaseSource(typeof(DataProvider), nameof(DataProvider.TickersToTest))]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureStory("Verify the stock's close rate  matches expected data from Polygon.IO API")]
        public async Task ValidateCloseRate(string ticker, string _)
        {
            try
            {
                var historicalDataFromAPI = await GetHistoricalData(ticker);
                quoteLookup.LookupQuote(ticker);
                stockData.NanigateToHistoricalData();
                POM.HistoricalData siteHistoricalData = stockData.GetStockDataByDate(ticker, date);
                Assert.That(historicalDataFromAPI.Close.ToString(), Is.EqualTo(siteHistoricalData.Close),
                    $"Open Rate mismatch: Expected {siteHistoricalData.Close}, but got {historicalDataFromAPI.Close}");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test, TestCaseSource(typeof(DataProvider), nameof(DataProvider.TickersToTest))]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureStory("Verify the stock's trading volume  matches expected data from Polygon.IO API")]
        public async Task ValidateStockVolume(string ticker, string _)
        {
            try
            {
                var historicalDataFromAPI = await GetHistoricalData(ticker);
                quoteLookup.LookupQuote(ticker);
                stockData.NanigateToHistoricalData();
                POM.HistoricalData siteHistoricalData = stockData.GetStockDataByDate(ticker, date);
                Assert.That(historicalDataFromAPI.Volume.ToString(), Is.EqualTo(siteHistoricalData.Volume),
                    $"Open Rate mismatch: Expected {siteHistoricalData.Volume}, but got {historicalDataFromAPI.Volume}");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }



    }
}
