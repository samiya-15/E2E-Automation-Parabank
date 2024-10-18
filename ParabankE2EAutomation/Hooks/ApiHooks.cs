using RestSharp;
using System.IO;
using System;
using TechTalk.SpecFlow;
using ParabankE2EAutomation.Support;

namespace ParabankE2EAutomation.Hooks
{
    [Binding]
    public sealed class ApiHooks
    {
        private ApiHelper _apiClient;
        private readonly ScenarioContext _scenarioContext;

        public ApiHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario("@API")]
        public void BeforeScenarioApi()
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env");
            DotNetEnv.Env.Load(fullPath);

            // Initialize the API client for API tests
            _apiClient = new ApiHelper(DotNetEnv.Env.GetString("BaseURL"));
            _scenarioContext.Add("APIClient", _apiClient);
        }

    }
}