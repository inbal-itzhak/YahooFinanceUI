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
    [AllureEpic("Charts Data")]
    public class ChartDataTests :BaseTest
    {
        string ticker = "NFLX";


        [Test, Description("Test 1 day chart data"),Category("Chart Data")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("Chart data verification")]
        [AllureStory("Validate the chart data for a one-day time span is correct")]
        public void VerifyChartDataOneDay()
        {
            quoteLookup.LookupQuote(ticker);
            stockChart.SelectOneDayChart();
            bool isChartOpen = stockChart.VerifyCorrectChartOpen(StockChart.TimePeriod.OneDay);
            Assert.That(isChartOpen, Is.True, "chart is not open");
            string actualChartInfo = stockChart.GetChartInfo();
            int chartActualTimeSpan = stockChart.GetHoursTimeRangeFromChartLabel(actualChartInfo);
            Assert.That(chartActualTimeSpan, Is.GreaterThan(19), $"Time span mismatch, expected: Between 20-72 hours , actual: {chartActualTimeSpan} hours");
            Assert.That(chartActualTimeSpan, Is.LessThan(73), $"Time span mismatch, expected: Between 20-72 hours, actual: {chartActualTimeSpan} hours");
        }


        [Test, Description("Validate the chart data for five days time span is correct"), Category("Chart Data")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("Chart data verification")]
        [AllureStory("Verify Chart Data by time period")]
        public void VerifyChartDataFiveDays()
        {
            quoteLookup.LookupQuote(ticker);
            stockChart.SelectFiveDayChart();
            bool isChartOpen = stockChart.VerifyCorrectChartOpen(StockChart.TimePeriod.FiveDays);
            Assert.That(isChartOpen, Is.True, "chart is not open");
            string actualChartInfo = stockChart.GetChartInfo();
            int chartActualTimeSpan = stockChart.GetDaysTimeRangeFromChartLabel(actualChartInfo);
            Assert.That(chartActualTimeSpan, Is.GreaterThan(2), $"Time span mismatch, expected: Between 3-7 days, actual: {chartActualTimeSpan} days");
            Assert.That(chartActualTimeSpan, Is.LessThan(8), $"Time span mismatch, expected: Between 3-7 days, actual: {chartActualTimeSpan} days");
        }

        [Test, Description("Validate the chart data for one month time span is correct"), Category("Chart Data")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("Chart data verification")]
        [AllureStory("Verify Chart Data by time period")]
        public void VerifyChartDataOneMonth()
        {
            quoteLookup.LookupQuote(ticker);
            stockChart.SelectOneMonthChart();
            bool isChartOpen = stockChart.VerifyCorrectChartOpen(StockChart.TimePeriod.OneMonth);
            Assert.That(isChartOpen, Is.True, "chart is not open");
            //DateTime time = stockChart.GetLastMarketCloseTime();
            // string expectedChartInfo = stockChart.GetExpectedChartTimesDescription(ticker, StockChart.TimePeriod.OneDay, time);
            string actualChartInfo = stockChart.GetChartInfo();
            int chartActualTimeSpan = stockChart.GetDaysTimeRangeFromChartLabel(actualChartInfo);
            Assert.That(chartActualTimeSpan, Is.GreaterThan(27), $"Time span mismatch,  expected: Between 28-33 days, actual: {chartActualTimeSpan} days");
            Assert.That(chartActualTimeSpan, Is.LessThan(34), $"Time span mismatch,  expected: Between 28-33 days, actual: {chartActualTimeSpan}  days");
        }

        [Test, Description("Validate the chart data for six months time span is correct"), Category("Chart Data")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("Chart data verification")]
        [AllureStory("Verify Chart Data by time period")]
        public void VerifyChartDataSixMonths()
        {
            quoteLookup.LookupQuote(ticker);
            stockChart.SelectSixMonthChart();
            bool isChartOpen = stockChart.VerifyCorrectChartOpen(StockChart.TimePeriod.SixMonths);
            Assert.That(isChartOpen, Is.True, "chart is not open");
           // DateTime time = stockChart.GetLastMarketCloseTime();
           //  string expectedChartInfo = stockChart.GetExpectedChartTimesDescription(ticker, StockChart.TimePeriod.OneDay, time);
            string actualChartInfo = stockChart.GetChartInfo();
            int chartActualTimeSpan = stockChart.GetDaysTimeRangeFromChartLabel(actualChartInfo);
            Assert.That(chartActualTimeSpan, Is.LessThan(191), $"Time span mismatch, expected: Between 180-190 days, actual: {chartActualTimeSpan}  days");
            Assert.That(chartActualTimeSpan, Is.GreaterThan(179), $"Time span mismatch, expected: Between 180-190 days , actual: {chartActualTimeSpan}  days");

        }

        [Test, Description("Validate the chart data for one year time span is correct"), Category("Chart Data")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("Chart data verification")]
        [AllureStory("Verify Chart Data by time period")]
        public void VerifyChartDataOneYear()
        {
            quoteLookup.LookupQuote(ticker);
            stockChart.SelectOneYearChart();
            bool isChartOpen = stockChart.VerifyCorrectChartOpen(StockChart.TimePeriod.OneYear);
            Assert.That(isChartOpen, Is.True, "chart is not open");
            string actualChartInfo = stockChart.GetChartInfo();
            int chartActualTimeSpan = stockChart.GetDaysTimeRangeFromChartLabel(actualChartInfo);
            Assert.That(chartActualTimeSpan, Is.LessThan(376), $"Time span mismatch, expected: Between 360-375 days, actual: {chartActualTimeSpan}  days");
            Assert.That(chartActualTimeSpan, Is.GreaterThan(359), $"Time span mismatch, expected: Between 360-375 days, actual: {chartActualTimeSpan} days");
        }

        [Test, Description("Validate the chart data for five years time span is correct"), Category("Chart Data")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("Chart data verification")]
        [AllureStory("Verify Chart Data by time period")]
        public void VerifyChartDataFiveYears()
        {
            quoteLookup.LookupQuote(ticker);
            stockChart.SelecFiveYearChart();
            bool isChartOpen = stockChart.VerifyCorrectChartOpen(StockChart.TimePeriod.FiveYears);
            Assert.That(isChartOpen, Is.True, "chart is not open");
            string actualChartInfo = stockChart.GetChartInfo();
            int chartActualTimeSpan = stockChart.GetDaysTimeRangeFromChartLabel(actualChartInfo);
            Assert.That(chartActualTimeSpan, Is.GreaterThan(999), $"Time span mismatch, expected: Between 1000-1870 days, actual: {chartActualTimeSpan} days");
            Assert.That(chartActualTimeSpan, Is.LessThan(1871), $"Time span mismatch, expected: Between 1000-1870 days, actual: {chartActualTimeSpan}  days");
        }
    }
}
