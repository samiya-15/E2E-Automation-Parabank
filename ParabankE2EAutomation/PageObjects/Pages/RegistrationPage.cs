using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParabankE2EAutomation.PageObjects.Pages
{
    public class RegistrationPage
    {
        private readonly IWebDriver driver;

        // Locators
        private By registerLink = By.XPath("//a[text()='Register']");
        private By firstNameInput = By.Id("customer.firstName");
        private By lastNameInput = By.Id("customer.lastName");
        private By addressInput = By.Id("customer.address.street");
        private By cityInput = By.Id("customer.address.city");
        private By stateInput = By.Id("customer.address.state");
        private By zipCodeInput = By.Id("customer.address.zipCode");
        private By phoneNumberInput = By.Id("customer.phoneNumber");
        private By ssnInput = By.Id("customer.ssn");
        private By usernameInput = By.Id("customer.username");
        private By passwordInput = By.Id("customer.password");
        private By repeatedPasswordInput = By.Id("repeatedPassword");
        private By registerButton = By.CssSelector("input[type='submit'][value='Register']");


        public RegistrationPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ClickRegisterLink()
        {
            driver.FindElement(registerLink).Click();
        }

        public void EnterFirstName(string firstName)
        {
            driver.FindElement(firstNameInput).SendKeys(firstName);
        }

        public void EnterLastName(string lastName)
        {
            driver.FindElement(lastNameInput).SendKeys(lastName);
        }

        public void EnterAddress(string address)
        {
            driver.FindElement(addressInput).SendKeys(address);
        }

        public void EnterCity(string city)
        {
            driver.FindElement(cityInput).SendKeys(city);
        }

        public void EnterState(string state)
        {
            driver.FindElement(stateInput).SendKeys(state);
        }

        public void EnterZipCode(string zipCode)
        {
            driver.FindElement(zipCodeInput).SendKeys(zipCode);
        }

        public void EnterPhoneNumber(string phoneNumber)
        {
            driver.FindElement(phoneNumberInput).SendKeys(phoneNumber);
        }

        public void EnterSSN(string ssn)
        {
            driver.FindElement(ssnInput).SendKeys(ssn);
        }

        public void EnterUsername(string username)
        {
            driver.FindElement(usernameInput).SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            driver.FindElement(passwordInput).SendKeys(password);
        }

        public void EnterRepeatedPassword(string repeatedPassword)
        {
            driver.FindElement(repeatedPasswordInput).SendKeys(repeatedPassword);
        }

        public void SubmitRegistrationForm()
        {
            driver.FindElement(registerButton).Click();
        }
    }
}
