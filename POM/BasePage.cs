using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.NUnit;
using Allure.NUnit.Attributes;

namespace YahooFinanceUI.POM
{
    public class BasePage
    {
        public BasePage(IWebDriver driver)
        {
            Driver = driver;

        }

        public IWebDriver Driver { get; set; }

        public void NavigateTo(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        [AllureStep("Click on {0}")]
        public void ClickElement(IWebElement element) 
        {
            element.Click();
        }
        [AllureStep("type {1} in {0}")]
        public void FillText(IWebElement element, string text) 
        {
            element.Clear();
            element.SendKeys(text);
        }
       
        public string GetElementText(IWebElement element)
        {
           return element.Text;
        }

        public double ParseStringToDouble(string value) 
        {
            return double.Parse(value);

        }

      
    }
}
