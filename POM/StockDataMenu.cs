using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinanceUI.POM
{
    public class StockDataMenu : BasePage
    {
        public StockDataMenu(IWebDriver driver) : base(driver)
        {
        }

        [AllureStep("Click on menu item {0}")]
        public void CLickOnMenuItem(string menuItemName)
        {
            List<IWebElement> menuItems = Driver.FindElements(By.CssSelector(".ellipsis.yf-1e6z5da")).ToList();
            foreach (IWebElement menuItem in menuItems)
            {
                if (GetElementText(menuItem).Contains(menuItemName))
                {
                   ClickElement(menuItem);
                }
            }
        }
    }
}
