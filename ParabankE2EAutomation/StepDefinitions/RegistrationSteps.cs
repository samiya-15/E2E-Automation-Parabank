using OpenQA.Selenium;
using ParabankE2EAutomation.PageObjects.Pages;
using TechTalk.SpecFlow;
using FluentAssertions;
using System;
using ParabankE2EAutomation.Model;
using ParabankE2EAutomation.Support;
using DotNetEnv;

namespace ParabankE2EAutomation.StepDefinitions
{
    [Binding]
    public class RegistrationSteps
    {
        private readonly IWebDriver _driver;
        private readonly RegistrationPage registrationPage;
        private readonly FeatureContext _featureContext;

        public RegistrationSteps(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _driver = scenarioContext.Get<IWebDriver>("WebDriver");
            registrationPage = new RegistrationPage(_driver);
            _featureContext = featureContext;
        }

        [Given(@"I am on the registration page")]
        public void GivenIAmOnTheRegistrationPage()
        {
            string url = Env.GetString("URL");
            Console.WriteLine(url);
            _driver.Navigate().GoToUrl(url);
            registrationPage.ClickRegisterLink();
        }

        [When(@"I fill out the registration form with valid details")]
        public void WhenIFillOutTheRegistrationFormWithValidDetails(Table table)
        {
            var row = table.Rows[0];

            var registrationData = new RegistrationData
            {
                FirstName = row["FirstName"],
                LastName = row["LastName"],
                Address = row["Address"],
                City = row["City"],
                State = row["State"],
                ZipCode = row["ZipCode"],
                PhoneNumber = row["PhoneNumber"],
                SSN = row["SSN"],

                // Generate a unique username
                Username = Env.GetString("username") + DateTime.Now.ToString("ddMMyyHHmmss"),
                Password = Env.GetString("password")
            };

            // Store the registration data in FeatureContext for later use
            _featureContext.Add("RegistrationData", registrationData);

            registrationPage.EnterFirstName(registrationData.FirstName);
            registrationPage.EnterLastName(registrationData.LastName);
            registrationPage.EnterAddress(registrationData.Address);
            registrationPage.EnterCity(registrationData.City);
            registrationPage.EnterState(registrationData.State);
            registrationPage.EnterZipCode(registrationData.ZipCode);
            registrationPage.EnterPhoneNumber(registrationData.PhoneNumber);
            registrationPage.EnterSSN(registrationData.SSN);
            registrationPage.EnterUsername(registrationData.Username);
            registrationPage.EnterPassword(registrationData.Password);
            registrationPage.EnterRepeatedPassword(registrationData.Password);
        }

        [When(@"I fill out the registration form with the same details")]
        public void WhenIFillOutTheRegistrationFormWithTheSameDetails()
        {
            _featureContext.TryGetValue("RegistrationData", out RegistrationData registrationData);
            registrationPage.EnterFirstName(registrationData.FirstName);
            registrationPage.EnterLastName(registrationData.LastName);
            registrationPage.EnterAddress(registrationData.Address);
            registrationPage.EnterCity(registrationData.City);
            registrationPage.EnterState(registrationData.State);
            registrationPage.EnterZipCode(registrationData.ZipCode);
            registrationPage.EnterPhoneNumber(registrationData.PhoneNumber);
            registrationPage.EnterSSN(registrationData.SSN);
            registrationPage.EnterUsername(registrationData.Username);
            registrationPage.EnterPassword(registrationData.Password);
            registrationPage.EnterRepeatedPassword(registrationData.Password);
        }


        [When(@"I submit the registration form")]
        public void WhenISubmitTheRegistrationForm()
        {
            registrationPage.SubmitRegistrationForm();
        }

        [Then(@"I should see a confirmation message for successful registration")]
        public void ThenIShouldSeeAConfirmationMessageForSuccessfulRegistration()
        {
            _featureContext.TryGetValue("RegistrationData", out RegistrationData registrationData);

            Utilities.ExplicitWait(_driver, _driver.FindElement(By.CssSelector("h1.title")));
            var welcomeMessage = _driver.FindElement(By.CssSelector("h1.title")).Text;

            welcomeMessage.Should().Contain($"Welcome {registrationData.Username}",
                because: "the user should see a welcome message with their username after successful registration");

            var successMessage = _driver.FindElement(By.CssSelector("#rightPanel > p")).Text;
            successMessage.Should().Contain("Your account was created successfully.",
                because: "the user should see a confirmation message after successful registration");
        }


        [Then(@"I should see an error message for duplicate registration")]
        public void ThenIShouldSeeAnErrorMessageForDuplicateRegistration()
        {
            var errorMessage = _driver.FindElement(By.Id("customer.username.errors")).Text;

            errorMessage.Should().Contain("This username already exists",
                because: "the user should see an error message when trying to register with an existing username");
        }
    }
}
