using OpenQA.Selenium;
using Allure.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.NUnit.Attributes;
using System.Linq.Expressions;

namespace YahooFinanceUI.POM
{
    public class StockPage : BasePage
    {
        public StockPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement ExchangeData => Driver.FindElement(By.CssSelector("span.exchange.yf-wk4yba"));
        IWebElement StockName => Driver.FindElement(By.CssSelector("h1.yf-xxbei9"));
        public IWebElement StockPrice => Driver.FindElement(By.CssSelector("[data-testid='qsp-price']"));
        public IWebElement PriceChange => Driver.FindElement(By.CssSelector("[data-testid='qsp-price-change']"));
        public IWebElement PriceChangePercent => Driver.FindElement(By.CssSelector("[data-testid='qsp-price-change-percent']"));
        public IWebElement PostPrice => Driver.FindElement(By.CssSelector("[data-testid='qsp-post-price']"));
        public IWebElement PostPriceChange => Driver.FindElement(By.CssSelector("[data-testid='qsp-post-price-change']"));
        public IWebElement PostPriceChangePercent => Driver.FindElement(By.CssSelector("[data-testid='qsp-post-price-change-percent']"));


        public IWebElement GetExchangeData(string ticker)
        {
           
            //".exchange.yf-wk4yba span"

            return ExchangeData;
        }

        [AllureStep("Get stock {0} traded Exchange data")]
        public string GetPrimaryExchangeName(string ticker) 
        {
            IWebElement exchangeElement = GetExchangeData(ticker);
            string exchange = GetElementText(exchangeElement.FindElements(By.TagName("span"))[0]);

            if (exchange.Contains("NasdaqGS") || exchange.Contains("NasdaqGM") || exchange.Contains("NasdaqCM"))
            {
                return "XNAS";
            }
            if (exchange.Contains("NYSE"))
            {
                return "XNYS";
            }
            return exchange;
        }

        [AllureStep("Get stock {0} currency data")]
        public string GetStockCurrencyName(string ticker)
        {
            IWebElement exchangeElement = GetExchangeData(ticker);
            List<IWebElement> els= exchangeElement.FindElements(By.TagName("span")).ToList();
            if (els.Count> 0)
            {
                foreach (IWebElement el in els)
                {
                    Console.WriteLine(el.Text);
                }
            }
            else
            {
                Console.WriteLine(els.Count);
            }
                
           
            string currency = GetElementText(exchangeElement.FindElements(By.TagName("span"))[2]);

           return currency.ToLower();
        }

        [AllureStep("Get stock name for symbol {0}")]
        public string GetStockName(string ticker)
        {
         
           string fullStockName =  GetElementText(StockName);
            string stockName = System.Text.RegularExpressions.Regex.Replace(fullStockName, @"\s\([^)]+\)$", "");
            stockName = stockName.Replace(",", "");//.TrimEnd('.');

            return stockName;
        }

        [AllureStep("Get stock {0} price")]
        public double GetstockPrice(string ticker)
        {
            string price = GetElementText(StockPrice);
            return ParseStringToDouble(price);
        }

        [AllureStep("Get stock {0} price change in cost")]
        public string GetStockPriceChange(string ticker)
        {
           
            return GetElementText(PriceChange);
        }

        [AllureStep("Get stock {0} price change in percents")]
        public string GetStockPriceChangePercent(string ticker)
        {
           
            return GetElementText(PriceChangePercent);
        }

        [AllureStep("Get stock {0} post market price")]
        public double GetPostMarketPrice(string ticker)
        {
           
           return ParseStringToDouble(PostPrice.Text);
        }


        [AllureStep("Get stock {0} post market price change in cost")]
        public string GetPostPriceChange(string ticker)
        {
            return GetElementText(PostPriceChange);
        }

        [AllureStep("Get stock {0} post market price change in percent")]
        public string GettPostPriceChangePercent(string ticker)
        {
            return GetElementText(PostPriceChangePercent);
        }

    }
}
