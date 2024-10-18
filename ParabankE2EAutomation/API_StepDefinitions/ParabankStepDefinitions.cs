using Bogus;
using DotNetEnv;
using NUnit.Framework;
using ParabankE2EAutomation.Model;
using ParabankE2EAutomation.Support;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace ParabankE2EAutomation.API_StepDefinitions
{
    [Binding]
    public class ParabankStepDefinitions
    {
        private ApiHelper _client;
        ScenarioContext _scenarioContext;
        FeatureContext _featureContext;

        
        public ParabankStepDefinitions(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;   
            _featureContext = featureContext;

            _scenarioContext.TryGetValue("APIClient", out ApiHelper client);
            _client = client;
        }

        [Given(@"Generate Test Registeration User Data")]
        public void GivenGenerateTestRegisterationUserData()
        {
            var faker = new Faker();
            var registrationData = new RegistrationData
            {
                FirstName = faker.Name.FirstName(),
                LastName = faker.Name.LastName(),
                Address = faker.Address.StreetAddress(),
                City = faker.Address.City(),
                State = faker.Address.State(),
                ZipCode = faker.Address.ZipCode(),
                PhoneNumber = faker.Phone.PhoneNumber(),
                SSN = faker.Random.Replace("###-##-####"), // Generates a random SSN format
                Username = Env.GetString("username") + DateTime.Now.ToString("ddMMyyHHmmss"), // Or faker.Internet.UserName()
                Password = Env.GetString("password") // Or faker.Internet.Password()
            };

            // Store registration data in context
            _featureContext.Add("RegistrationData", registrationData);
        }


        [When(@"I register a new user with the following details")]
        public async Task WhenIRegisterANewUserWithTheFollowingDetails()
        {
            _featureContext.TryGetValue("RegistrationData", out RegistrationData registrationData);

            var registrationParams = new Dictionary<string, string>
            {
                { "customer.firstName", registrationData.FirstName },
                { "customer.lastName", registrationData.LastName },
                { "customer.address.street", registrationData.Address },
                { "customer.address.city", registrationData.City },
                { "customer.address.state", registrationData.State },
                { "customer.address.zipCode", registrationData.ZipCode },
                { "customer.phoneNumber", registrationData.PhoneNumber },
                { "customer.ssn", registrationData.SSN },
                { "customer.username", registrationData.Username },
                { "customer.password", registrationData.Password },
                { "repeatedPassword", registrationData.Password }
            };

            RestResponse _response = await _client.GetRequestWithQueryParams("/register.htm", registrationParams);

            _scenarioContext.Add("Response", _response);
        }

        [Then(@"the registration response should be successful")]
        public void ThenTheRegistrationResponseShouldBeSuccessful()
        {
            _scenarioContext.TryGetValue("Response", out RestResponse response);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Then(@"the response status code should be 200")]
        public void ThenTheResponseStatusCodeShouldBe200()
        {
            _scenarioContext.TryGetValue("Response", out RestResponse response);
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [Given(@"I have the login endpoint for Parabank")]
        public void GivenIHaveTheLoginEndpointForParabank()
        {
            _scenarioContext.TryGetValue("APIClient", out ApiHelper client);
            _client = client;
        }

        [When(@"I login using Registered username and password")]
        public async Task WhenILoginUsingUsernameAndPassword()
        {
            _featureContext.TryGetValue("RegistrationData", out RegistrationData registrationData);
            var registrationParams = new Dictionary<string, string>
            {
                { "username", registrationData.Username },
                { "password", registrationData.Password }
            };
          
            RestResponse _response = await _client.PostRequestWithQueryParams("/login.htm", registrationParams);
            _scenarioContext.Add("Response", _response);
        }

        [Then(@"the login response should be successful")]
        public void ThenTheLoginResponseShouldBeSuccessful()
        {
            _scenarioContext.TryGetValue("Response", out RestResponse _response);
            Assert.IsNotNull(_response);
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }
    }

}
