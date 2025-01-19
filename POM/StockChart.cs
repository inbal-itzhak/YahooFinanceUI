using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YahooFinanceUI.POM
{
    public class StockChart : BasePage
    {
        public StockChart(IWebDriver driver) : base(driver)
        {
        }
        public IWebElement OneDayChart => Driver.FindElement(By.CssSelector("#tab-1d-qsp"));
        public IWebElement FiveDaysChart => Driver.FindElement(By.CssSelector("#tab-5d-qsp"));
        public IWebElement OneMonthChart => Driver.FindElement(By.CssSelector("#tab-1m"));
        public IWebElement SixMonthsChart => Driver.FindElement(By.CssSelector("#tab-6m"));
        public IWebElement OneYearChart => Driver.FindElement(By.CssSelector("#tab-1y"));
        public IWebElement FiveYearsChart => Driver.FindElement(By.CssSelector("#tab-5y"));


        [AllureStep("Get Stock Chart info")]
        public string GetChartInfo()
        {
          
            List<IWebElement> canvasElements = Driver.FindElements(By.CssSelector("cq-context canvas")).ToList();
            string chartInfo = "";
            if(canvasElements.Count > 0)
            {
                foreach (IWebElement canvasElement in canvasElements)
                {
                    if (canvasElement.GetDomAttribute("aria-label") != null)
                    {
                        chartInfo = canvasElement.GetDomAttribute("aria-label");
                    }
                }
            }
            Console.WriteLine($"chart info is: {chartInfo}");
           
            return chartInfo;
        }
        [AllureStep("Select one day chart")]
        public void SelectOneDayChart()
        {
            ClickElement(OneDayChart);
        }

        [AllureStep("Select five days chart")]
        public void SelectFiveDayChart()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            ClickElement(FiveDaysChart);
        }

    
        [AllureStep("Select one month chart")]
        public void SelectOneMonthChart()
        {
            ClickElement(OneMonthChart);
        }

        [AllureStep("Select six months chart")]
        public void SelectSixMonthChart()
        {
            ClickElement(SixMonthsChart);
        }

        [AllureStep("Select one year chart")]
        public void SelectOneYearChart()
        {
           ClickElement(OneYearChart);
        }

        [AllureStep("Select five years chart")]
        public void SelecFiveYearChart()
        {
           ClickElement(FiveYearsChart);
        }

        public enum TimePeriod
        {
            OneDay,
            FiveDays,
            OneMonth,
            SixMonths,
            OneYear,
            FiveYears,
        }

        [AllureStep("Verify Correct chart open for time period {0}")]
        [Obsolete]
        public bool VerifyCorrectChartOpen(TimePeriod timePeriod)
        {
            IWebElement chartTab;
            string isTabSelected = "false";
            switch (timePeriod)
            {
                case TimePeriod.OneDay:
                    isTabSelected = OneDayChart.GetAttribute("aria-selected");
                    break;
                case TimePeriod.FiveDays:
                   isTabSelected = FiveDaysChart.GetAttribute("aria-selected");
                    break;
                case TimePeriod.OneMonth:
                    isTabSelected = OneMonthChart.GetAttribute("aria-selected");
                    break;
                case TimePeriod.SixMonths:
                    isTabSelected = SixMonthsChart.GetAttribute("aria-selected");
                    break;
                case TimePeriod.OneYear:
                   isTabSelected = OneYearChart.GetAttribute("aria-selected");
                    break;
                case TimePeriod.FiveYears:
                   isTabSelected = FiveYearsChart.GetAttribute("aria-selected");
                    break;
            }

            if (isTabSelected != null && isTabSelected == "true")
            {
                return true;
            }
            return false;
        }

        public int GetDaysTimeRangeFromChartLabel(string chartLabel)
        {
            //string chartLabel = "This 1 minute mountain chart displays price over time for symbol BBAI between 1/16/2025, 4:00:00 AM and 1/17/2025, 4:18:00 AM. The chart also displays Volume Underlay study.";
            // Regular expression to match date and time in the format M/d/yyyy, h:mm:ss tt
            // chartLabel = "This 1 day mountain chart displays price over time for symbol NFLX between 1/19/2024 and 1/28/2025.  The chart also displays Volume Underlay study.";
            string patternWithHours = @"(\d{1,2}/\d{1,2}/\d{4}, \d{1,2}:\d{2}:\d{2} \w{2})";
            string patternDays = @"(\d{1,2}/\d{1,2}/\d{4})";
            try
            {
                Console.WriteLine($"chart label is: {chartLabel}");
                // Find all matches using the regular expression

                var matchesWithHours = Regex.Matches(chartLabel, patternWithHours);
                var matchesDays = Regex.Matches(chartLabel, patternDays);
                string formatWithHours = "M/d/yyyy, h:mm:ss tt";
                string formatDays = "M/d/yyyy";
                CultureInfo culture = CultureInfo.InvariantCulture;
                // Check if two matches are found (start and end time)
                if (matchesWithHours.Count == 2)
                {

                    // Extract the start and end date-times
                    DateTime startTime = DateTime.ParseExact(matchesWithHours[0].Value, formatWithHours, culture);
                    DateTime endTime = DateTime.ParseExact(matchesWithHours[1].Value, formatWithHours, culture);
                    TimeSpan timeRange = (endTime - startTime);
                    Console.WriteLine($"Start Time: {startTime}");
                    Console.WriteLine($"End Time: {endTime}");
                    return (int)timeRange.TotalDays;
                }
                if (matchesDays.Count == 2)
                {
                    DateTime startTime = DateTime.ParseExact(matchesDays[0].Value, formatDays, culture);
                    DateTime endTime = DateTime.ParseExact(matchesDays[1].Value, formatDays, culture);
                    TimeSpan timeRange = (endTime - startTime);
                    Console.WriteLine($"Start Time: {startTime}");
                    Console.WriteLine($"End Time: {endTime}");
                    return (int)timeRange.TotalDays;
                }
                else
                {
                    Console.WriteLine("Unable to extract both start and end times.");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }


       
        public int GetHoursTimeRangeFromChartLabel(string chartLabel)
        {
            //string chartLabel = "This 1 minute mountain chart displays price over time for symbol BBAI between 1/16/2025, 4:00:00 AM and 1/17/2025, 4:18:00 AM. The chart also displays Volume Underlay study.";
            // Regular expression to match date and time in the format M/d/yyyy, h:mm:ss tt
            string pattern = @"(\d{1,2}/\d{1,2}/\d{4}, \d{1,2}:\d{2}:\d{2} \w{2})";
            try
            {
                // Find all matches using the regular expression

                var matches = Regex.Matches(chartLabel, pattern);
                string format = "M/d/yyyy, h:mm:ss tt"; 
                CultureInfo culture = CultureInfo.InvariantCulture;
                // Check if two matches are found (start and end time)
                if (matches.Count == 2)
                {

                    // Extract the start and end date-times
                    DateTime startTime = DateTime.ParseExact(matches[0].Value,format,culture);
                    DateTime endTime = DateTime.ParseExact(matches[1].Value, format, culture);
                    TimeSpan timeRange = (endTime - startTime);
                    Console.WriteLine($"Start Time: {startTime}");
                    Console.WriteLine($"End Time: {endTime}");
                    return (int)timeRange.TotalHours;
                }
                else
                {
                    Console.WriteLine("Unable to extract both start and end times.");
                    return 0;
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
           
        }


        public DateTime ConvertTimeToEasternTime(DateTime time)
        {
            time = time.Kind == DateTimeKind.Unspecified ? time : DateTime.Now;
            TimeZoneInfo et = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime easternTime = TimeZoneInfo.ConvertTime(time, TimeZoneInfo.Local, et);
            return easternTime;
        }


        [AllureStep("Verify chart matches the following data: Stock {0}, selected chart time period {1}")]
        public string GetExpectedChartTimesDescription(string ticker, TimePeriod timePeriod, DateTime lastTradeTime)
        {
            string expctedDescription;
            TimeSpan timeSpan;

            //This 1 minute mountain chart displays price over time for symbol NFLX between 12/16/2024, 5:00:00 PM and 12/17/2024, 8:21:00 AM.  The chart also displays Volume Underlay study.
            switch (timePeriod)
            {
                case TimePeriod.OneDay:
                    timeSpan = TimeSpan.FromDays(1);
                    expctedDescription = $"This 1 minute mountain chart displays price over time for symbol {ticker.ToUpper()} between {lastTradeTime.ToString("M/d/yyyy, h:mm:ss tt").ToUpper()} and {ConvertTimeToEasternTime(DateTime.Now).ToString("M/d/yyyy, h:mm:ss tt").ToUpper()}.  The chart also displays Volume Underlay study.";
                    break;

                case TimePeriod.FiveDays:
                    timeSpan = TimeSpan.FromDays(5);
                    expctedDescription = $"This 1 minute mountain chart displays price over time for symbol {ticker.ToUpper()} between {lastTradeTime.Add(-timeSpan).ToString("M/d/yyyy, h:mm:ss tt").ToUpper()} and {DateTime.Now.ToString("M/d/yyyy, h:mm:ss tt").ToUpper()}.  The chart also displays Volume Underlay study.";
                    break;

                case TimePeriod.OneMonth:
                    TimeSpan daysInMonth = DateTime.Today - DateTime.Now.AddMonths(-1);
                    timeSpan = daysInMonth;
                    expctedDescription = $"This 1 minute mountain chart displays price over time for symbol {ticker.ToUpper()} between {DateTime.Now.Add(-timeSpan).ToString("M/d/yyyy")} and {DateTime.Now.ToString("M/d/yyyy")}.  The chart also displays Volume Underlay study.";
                    break;
                case TimePeriod.SixMonths:
                    TimeSpan daysInSixMonths = DateTime.Today - DateTime.Now.AddMonths(-6);
                    timeSpan = daysInSixMonths;
                    expctedDescription = $"This 1 minute mountain chart displays price over time for symbol {ticker.ToUpper()} between {DateTime.Now.Add(-timeSpan).ToString("MM/dd/yyyy")} and {DateTime.Now.ToString("M/d/yyyy")}.  The chart also displays Volume Underlay study.";
                    break;
                case TimePeriod.OneYear:
                    TimeSpan daysInOneYear = DateTime.Today - DateTime.Now.AddYears(-1);
                    timeSpan = daysInOneYear;
                    expctedDescription = $"This 1 minute mountain chart displays price over time for symbol {ticker.ToUpper()} between {DateTime.Now.Add(-timeSpan).ToString("M/d/yyyy")} and {DateTime.Now.ToString("M/d/yyyy")}.  The chart also displays Volume Underlay study.";
                    break;
                case TimePeriod.FiveYears:
                    TimeSpan daysInFiveYears = DateTime.Today - DateTime.Now.AddYears(-5);
                    timeSpan = daysInFiveYears;
                    expctedDescription = $"This 1 minute mountain chart displays price over time for symbol {ticker.ToUpper()} between {DateTime.Now.Add(-timeSpan).ToString("M/d/yyyy")} and {DateTime.Now.ToString("M/d/yyyy")}.  The chart also displays Volume Underlay study.";
                    break;

                default:
                    expctedDescription = string.Empty;
                    break;
            }
            return expctedDescription;
        }



    }
}
