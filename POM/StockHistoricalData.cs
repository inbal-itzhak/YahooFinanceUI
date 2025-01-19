using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.NUnit.Attributes;

namespace YahooFinanceUI.POM
{
    public class StockHistoricalData : BasePage
    {
        public StockHistoricalData(IWebDriver driver) : base(driver) { }

        [AllureStep("Navigate to stock historical data")]
        public void NanigateToHistoricalData()
        {
            IWebElement historicalData = Driver.FindElement(By.CssSelector("[title='Historical Data']"));
            ClickElement(historicalData);
        }

        [AllureStep("Get stock ({0}) data from date {1}")]
        public HistoricalData GetStockDataByDate(string ticker, DateTime date)
        {
            List<HistoricalData> historicalData = new List<HistoricalData>();
            try
            {
                string siteDate = date.ToString("MMM d yyyy");
                Console.WriteLine($"site date is {siteDate}");
                List<IWebElement> dataRows = Driver.FindElements(By.CssSelector(".table-container tbody>tr")).ToList();
                // checking first 5 rows, since I am only looking for last business day
                for (int i = 0; i < 5; i++)
                {
                    List<string> dataCells = new List<string>();
                    List<IWebElement> cells = dataRows[i].FindElements(By.TagName("td")).ToList();
                    foreach (IWebElement cell in cells)
                    {
                        dataCells.Add(GetElementText(cell).Replace(",",""));
                    }
                    string[] stockDataRow = dataCells.ToArray();
                    historicalData.Add(new HistoricalData
                    {
                        Date = dataCells[0],
                        Open = dataCells[1],
                        High = dataCells[2],
                        Low = dataCells[3],
                        Close = dataCells[4],
                        AdjClose = dataCells[5],
                        Volume = dataCells[6]
                    });
                    Console.WriteLine($"HistoricalData values for row number {i} are: {historicalData[i].Date},{historicalData[i].Open}, {historicalData[i].High},{historicalData[i].Low}" +
                        $", {historicalData[i].Close}, {historicalData[i].AdjClose}, {historicalData[i].Volume} ");
                }

                foreach (HistoricalData data in historicalData)
                {
                    Console.WriteLine($"data.Date.ToString() is {data.Date}");
                    if (data.Date.ToString() == siteDate)
                    {
                        Console.WriteLine($"HistoricalData returned");
                        return data;
                    }
                }
                Console.WriteLine($"HistoricalData not found for {siteDate}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return null; 
            }
        }


        [AllureStep("Get company name for stock {0}")]
        public string CompanyName(string ticker)
        {
            IWebElement companyName = Driver.FindElement(By.CssSelector("h1.yf-xxbei9"));
            return  GetElementText(companyName);
        }

        [AllureStep("Verify stock Symbol is {0}")]
        public string GetStockSymbol(string ticker)
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            IWebElement tickerSymbol = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1.yf-xxbei9")));
            if (tickerSymbol != null)
            {
                string stockNameSymbol = GetElementText(tickerSymbol);
                string stockSymbol = stockNameSymbol.Split('(')[1].Split(')')[0];
                return stockSymbol.ToUpper();
            }
            else
            {
                return "element not found";
            }

        }
        public List<IWebElement> StockDataList()
        {
            List<IWebElement> stockData = Driver.FindElements(By.CssSelector(".value.yf-swtyw6")).ToList();
            return stockData;
        }

        [AllureStep("Get stock ({0}) previous close rate")]
        public string PreviousClose(string ticker)
        {
            var previousCloseElement = Driver.FindElement(By.CssSelector("[data-field='regularMarketPreviousClose'].yf-dudngy"));
            if (previousCloseElement != null)
            {
                return GetElementText(previousCloseElement);
            }
            return string.Empty;
        }

        [AllureStep("Get stock ({0}) open rate")]
        public string OpenRate(string ticker)
        {
            var openRateElement = Driver.FindElement(By.CssSelector("[data-field='regularMarketOpen'].yf-dudngy"));
            if (openRateElement != null)
            {
                return GetElementText(openRateElement);
            }
            return string.Empty;
        }

        [AllureStep("Get stock ({0}) trade volume")]
        public string Volume(string ticker)
        {
            var openRateElement = Driver.FindElement(By.CssSelector("[data-field='regularMarketVolume'].yf-dudngy"));
            if (openRateElement != null)
            {
                return GetElementText(openRateElement);
            }
            return string.Empty;
        }



    }
}
