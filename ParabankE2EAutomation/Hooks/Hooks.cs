using ParabankE2EAutomation.Drivers;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using System.IO;

namespace ParabankE2EAutomation.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario("@UI")]
        public void BeforeScenario()
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env");
            DotNetEnv.Env.Load(fullPath);
            
            // Driver init
            SeleniumDriver seleniumDriver = new SeleniumDriver(_scenarioContext);
            _driver = seleniumDriver.Setup();

            _scenarioContext.Set(_driver, "WebDriver");
        }

        [AfterScenario("@UI")]
        public void AfterScenario()
        {
            try
            {
                //Closing
                var driver = _scenarioContext.Get<IWebDriver>("WebDriver");
                driver?.Quit();
                Console.WriteLine("Browser Closed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during teardown: {ex.Message}");
            }
        }
    }
}
