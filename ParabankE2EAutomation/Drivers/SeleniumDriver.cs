using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace ParabankE2EAutomation.Drivers
{
    internal class SeleniumDriver
    {
        private IWebDriver driver;
        private readonly ScenarioContext _scenarioContext;

        public SeleniumDriver(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public IWebDriver Setup()
        {
            // Setup the WebDriver Manager for Chrome
            new DriverManager().SetUpDriver(new ChromeConfig());

            // Configure ChromeOptions for performance logging
            var chromeOptions = new ChromeOptions();
            chromeOptions.SetLoggingPreference("performance", LogLevel.All); // Enable performance logs
            chromeOptions.AddArgument("--start-maximized"); // Start browser maximized

            // Initialize the ChromeDriver with the options
            driver = new ChromeDriver(chromeOptions);

            // Maximize the browser for better visibility (optional as we already set this)
            driver.Manage().Window.Maximize();
            Console.WriteLine("Browser Setup Complete");

            // Add the driver to the ScenarioContext for access in step definitions
            _scenarioContext.Set(driver, "WebDriver");

            return driver;
        }

        public void TearDown()
        {
            // Quit the driver if it's not null
            if (driver != null)
            {
                driver.Quit();
                Console.WriteLine("Browser Closed");
            }
        }
    }
}
