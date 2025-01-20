# Yahoo Finance UI Automation Project

## Overview
This project automates UI testing for the Yahoo Finance website using Selenium and the Page Object Model (POM) design pattern.
The tests validate key functionalities of the website and compare data against the Polygon.IO API.
While the API does not provide real-time data, it serves as a reference for verification.

---

## Table of Contents
- [Features](#features)
- [Technologies Used](#technologies-used)
- [POM Classes](#pom-classes)
- [Test Scenarios](#test-scenarios)
- [Known Issues](#known-issues)
- [Setup and Installation](#setup-and-installation)
- [Usage](#usage)
- [Reporting](#reporting)


---

## Features
- Automated testing of Yahoo Finance's key functionalities:
  - Quote Lookup and verification of stock details.
  - Historical data validation for stocks.
  - Chart data span verification for various timeframes.
- Data-driven testing using a custom DataProvider.
- Integration with Allure reporting for test results and categorization.

---

## Technologies Used
- **C#** - Programming language for test development.
- **Selenium WebDriver** - Web automation framework.
- **Page Object Model (POM)** - Design pattern for test code structure.
- **Allure.Net.Commons** - Reporting tool for detailed test reports.
- **Polygon.IO API** - Used for data comparison (non-real-time).

---

## POM Classes
### Type Classes
1. **HistoricalData**: Represents historical stock data.
2. **StockData**: Represents stock information.

### Element and Action Classes
1. **BasePage**: Contains common methods and properties shared across pages.
2. **EntryMessage**: Handles the "Accept Cookies" entry message.
3. **QuoteLookup**: Manages stock quote lookup functionality.
4. **StockChart**: Interacts with stock chart elements.
5. **StockDataMenu**: Handles the stock data menu, to navigate on the stock page.
6. **StockHistoricalData**: Represents the stock-specific historical data elements.
7. **StockPage**: Represents the stock-specific page elements and actions.

---

## Test Scenarios

### Quote Lookup Functionality Tests
- **LookupQuote**: Search for a stock by its ticker symbol.
- **VerifyStockName**: Verify the stock name matches expected data from Polygon.IO API.
- **VerifyStockExchange**: Verify the stock exchange name matches the expected data from Polygon.IO API.
- **VerifyStockCurrency**: Verify the stock currency matches the expected data from Polygon.IO API.
- **SearchQuoteByCompanyName**: Search for stocks using the company name.

### Historical Data Tests
- **ValidateOpenRate**: Verify the stock's open rate matches expected data from Polygon.IO API.
- **ValidateCloseRate**: Verify the stock's close rate  matches expected data from Polygon.IO API.
- **ValidateStockVolume**: Verify the stock's trading volume  matches expected data from Polygon.IO API.

### Chart Data Tests
- **VerifyChartDataOneDay**: Validate the chart data for a one-day time span is correct.
- **VerifyChartDataFiveDays**: Validate the chart data for five days time span is correct.
- **VerifyChartDataOneMonth**: Validate the chart data for one month time span is correct.
- **VerifyChartDataSixMonths**: Validate the chart data for six months time span is correct.
- **VerifyChartDataOneYear**: Validate the chart data for one year time span is correct.
- **VerifyChartDataFiveYears**: Validate the chart data for five years time span is correct.

---

## Known Issues
- **Data Mismatch**: The data from Polygon.IO API does not always match the data displayed on the Yahoo Finance website. This is due to differences in naming conventions or the timing of updates.
  - Examples:
    - Some **ValidateStockVolume** tests fail because the stock volume data differs.
    - Some **VerifyStockName** tests fail due to inconsistencies like "Corp" vs. "Corporation" in the stock names.
- These issues are acknowledged and documented in the Allure reports for transparency.

---

## Setup and Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-repo/YahooFinanceUITests.git
   ```

2. Install dependencies using NuGet:
   - Selenium WebDriver
   - SeleniumExtras.WaitHelpers
   - Microsoft.Extensions.Configuration.Json
   - Microsoft.Extensions.Configuration.Abstractions
   - Allure.Net.Commons
   - Allure.NUnit

3. Configure the project:
   - Update `appsettings.json` with the base URL and browser configuration.

4. Ensure you have .NET installed:
   ```bash
   dotnet --version
   ```

---

## Usage

1. Open the solution in Visual Studio.
2. Build the project to ensure all dependencies are installed.
3. Run the tests using the Test Explorer or CLI:
   ```bash
   dotnet test
   ```
4. Generate Allure reports:
   ```bash
   allure serve allure-results
   ```

---


## Reporting
This project uses Allure.Net.Commons for detailed reporting. Reports include:
- Test execution results
- Categories and labels for better organization

To view reports, run:
```bash
allure serve allure-results
```
