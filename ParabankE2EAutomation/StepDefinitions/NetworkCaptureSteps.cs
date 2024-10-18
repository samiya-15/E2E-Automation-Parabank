using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using TechTalk.SpecFlow;

namespace ParabankE2EAutomation.StepDefinitions
{
    [Binding]
    public class NetworkCaptureSteps
    {
        private readonly IWebDriver _driver;
        private List<object> capturedRequests = new List<object>();
        private readonly ScenarioContext _scenarioContext;
        private readonly string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "networkRequests.json");

        public NetworkCaptureSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = _scenarioContext.Get<IWebDriver>("WebDriver");
        }

        [When(@"I capture all network requests")]
        public void WhenICaptureAllNetworkRequests()
        {
            // Cast the driver to ChromeDriver to use DevTools
            ChromeDriver chromeDriver = (ChromeDriver)_driver;

            // Get performance logs from the driver
            var logs = chromeDriver.Manage().Logs.GetLog("performance");

            foreach (var logEntry in logs)
            {
                // Parse the log entry as JSON
                JObject messageJson = JObject.Parse(logEntry.Message);
                var method = messageJson["message"]?["method"]?.ToString();

                if (method == "Page.frameStartedLoading")
                {
                    // Capture frame started loading event
                    var frameId = messageJson["message"]?["params"]?["frameId"]?.ToString();
                    capturedRequests.Add(new
                    {
                        Event = "Page.frameStartedLoading",
                        FrameId = frameId
                    });
                    Console.WriteLine($"Frame started loading. Frame ID: {frameId}");
                }
                else if (method == "Network.requestWillBeSent")
                {
                    // Capture network request event
                    var url = messageJson["message"]?["params"]?["request"]?["url"]?.ToString();
                    var methodType = messageJson["message"]?["params"]?["request"]?["method"]?.ToString();
                    capturedRequests.Add(new
                    {
                        Event = "Network.requestWillBeSent",
                        Url = url,
                        Method = methodType
                    });
                    Console.WriteLine($"Captured Request: {methodType} {url}");
                }
                else if (method == "Network.responseReceived")
                {
                    // Capture network response event
                    var url = messageJson["message"]?["params"]?["response"]?["url"]?.ToString();
                    var status = messageJson["message"]?["params"]?["response"]?["status"]?.ToString();
                    capturedRequests.Add(new
                    {
                        Event = "Network.responseReceived",
                        Url = url,
                        Status = status
                    });
                    Console.WriteLine($"Captured Response: {status} {url}");
                }
            }

            // Save captured requests to a JSON file
            File.WriteAllText(outputPath, Newtonsoft.Json.JsonConvert.SerializeObject(capturedRequests, Newtonsoft.Json.Formatting.Indented));
            Console.WriteLine($"Network events saved to: {outputPath}");
        }

        [Then(@"I save all captured network requests to a JSON file")]
        public void ThenISaveAllCapturedNetworkRequestsToAJsonFile()
        {
            // Check if there are captured requests and write them to the output path
            if (capturedRequests.Count > 0)
            {
                File.WriteAllText(outputPath, Newtonsoft.Json.JsonConvert.SerializeObject(capturedRequests, Newtonsoft.Json.Formatting.Indented));
                Console.WriteLine($"Network events saved to: {outputPath}");
            }
            else
            {
                Console.WriteLine("No network events captured.");
            }
        }
    }
}
