using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinanceUI.POM
{
    public class QuoteLookup : BasePage
    {
        public QuoteLookup(IWebDriver driver) : base(driver)
        {
        }

        public ReadOnlyCollection<IWebElement> DDB => Driver.FindElements(By.CssSelector("[role='listbox']"));
        public IWebElement Symbol => Driver.FindElement(By.CssSelector(".modules-module_quoteSymbol__BGsyF"));

        [AllureStep("Lookup quote for stock {0}")]
        public void LookupQuote(string ticker)
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            NavigateTo("https://finance.yahoo.com/");
            IWebElement formElement = wait.Until(ExpectedConditions.ElementExists(By.CssSelector("form>[aria-label='Quote Lookup']")));
            FillText(formElement, ticker);
            FillText(formElement, Keys.Enter);
            foreach (var linsting in DDB)
            {
                if (GetElementText(Symbol) == ticker.ToUpper())
                {
                    ClickElement(Symbol);
                }
            }
        }
    }
}
