using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace ParabankE2EAutomation.StepDefinitions
{
    [Binding]
    public class MobileResolutionSteps
    {
        private readonly IWebDriver _driver;

        public MobileResolutionSteps(ScenarioContext scenarioContext)
        {
            _driver = scenarioContext.Get<IWebDriver>("WebDriver");
        }

        [When(@"I set the browser window size to ""(.*)""")]
        public void WhenISetTheBrowserWindowSizeTo(string device)
        {
            switch (device.ToLower())
            {
                case "iphone":
                    _driver.Manage().Window.Size = new System.Drawing.Size(375, 812); // iPhone X resolution
                    break;
                case "ipad":
                    _driver.Manage().Window.Size = new System.Drawing.Size(768, 1024); // iPad resolution
                    break;
                case "android":
                    _driver.Manage().Window.Size = new System.Drawing.Size(412, 915); // Generic Android device
                    break;
                case "pixel 4":
                    _driver.Manage().Window.Size = new System.Drawing.Size(411, 869); // Google Pixel 4 resolution
                    break;
                case "pixel 5":
                    _driver.Manage().Window.Size = new System.Drawing.Size(393, 851); // Google Pixel 5 resolution
                    break;
                // Add more Android devices as needed
                default:
                    throw new ArgumentException($"Device '{device}' not supported");
            }
        }


        [Then(@"the website should display correctly for ""(.*)"" resolution")]
        public void ThenTheWebsiteShouldDisplayCorrectlyFor(string device)
        {
            var pageSource = _driver.PageSource;
            pageSource.Should().NotContain("error",
                because: $"the site should render correctly on {device} without errors.");
        }
    }

}
