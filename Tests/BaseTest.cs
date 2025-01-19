using Allure.Net.Commons;
using Microsoft.Extensions.Configuration;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using YahooFinanceUI.POM;


namespace YahooFinanceUI.Tests
{
    public class BaseTest
    {
        public IWebDriver driver;
        public EntryMessage entryMessage;
        public QuoteLookup quoteLookup;
        public StockHistoricalData stockData;
        public StockDataMenu stockDataMenu;
        public StockPage stockPage;
        public StockChart stockChart;
        public string polygonApiKey;
        public BasePage basePage;
        public string baseUrl;
        public RestClient PolygonClient;
        private static bool allureEnvWritten;

        public static readonly IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(path: "appsettings.json").Build();


        [OneTimeSetUp]
        public void Setup()
        {
            baseUrl = config["BaseURL:baseUrl"];
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = baseUrl;
            entryMessage = new EntryMessage(driver);
            quoteLookup = new QuoteLookup(driver);
            stockData = new StockHistoricalData(driver);
            stockPage = new StockPage(driver);
            stockChart = new StockChart(driver);
            stockDataMenu = new StockDataMenu(driver);
            entryMessage.ClickOnAcceptAllCookies();
            basePage = new BasePage(driver);

            var polygonOptions = new RestClientOptions("https://api.polygon.io/");
            PolygonClient = (RestClient)new RestClient(polygonOptions).AddDefaultHeader(KnownHeaders.Accept, MediaTypeNames.Application.Json);

            if (!allureEnvWritten)
            {
                
                new XElement("environment",
                new XElement("parameter",
                new XElement("key", "OS"),
                new XElement("value", RuntimeInformation.OSDescription)),
                new XElement("parameter",
                new XElement("key", "browser"),
                new XElement("value", ((ChromeDriver)driver).Capabilities.GetCapability("browserName"))),
                new XElement("parameter",
                new XElement("key", "browser.version"),
                new XElement("value", ((ChromeDriver)driver).Capabilities.GetCapability("browserVersion"))))
                .Save("..\\..\\..\\allure-results/environment.xml");
                allureEnvWritten = true;
            }
        }

        [TearDown]
        public void TakeScreenshotOnFailure()
        {
            if(TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                var screenshotBytes = screenshot.AsByteArray;
                var fileName = $"screenshot_{Guid.NewGuid()}.jpeg";
                var tempFilePath = Path.Combine(Path.GetTempPath(), fileName);
                File.WriteAllBytes(tempFilePath, screenshotBytes);
                AllureApi.AddAttachment(fileName, "image/jpeg", tempFilePath);
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}

