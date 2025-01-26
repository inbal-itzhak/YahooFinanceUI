using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinanceUI.POM
{
    public class EntryMessage : BasePage
    {
        public EntryMessage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement GoToButtom => Driver.FindElement(By.CssSelector("#scroll-down-btn"));
        public IWebElement AcceptAllCookies => Driver.FindElement(By.CssSelector("[name='agree']"));
        public IWebElement RejectAllCookies => Driver.FindElement(By.CssSelector(".btn.secondary.reject-all"));

        public void ClickOnGoToBottomBtn()
        {
            ClickElement(GoToButtom);
        }

        public void ClickOnAcceptAllCookies()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(AcceptAllCookies)).Click();

        }

        public void ClickOnRejectAllCookies()
        {
            ClickElement(RejectAllCookies);
        }
    }
}
