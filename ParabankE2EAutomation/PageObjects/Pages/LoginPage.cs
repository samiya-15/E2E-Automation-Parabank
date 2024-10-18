using OpenQA.Selenium;

namespace ParabankE2EAutomation.PageObjects.Pages
{
    internal class LoginPage
    {
        private IWebDriver driver;

        // Locators
        private By usernameInput = By.Name("username");
        private By passwordInput = By.Name("password");
        private By loginButton = By.CssSelector("input[type='submit'][value='Log In']");

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void EnterUsername(string username)
        {
            driver.FindElement(usernameInput).SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            driver.FindElement(passwordInput).SendKeys(password);
        }

        public void ClickLoginButton()
        {
            driver.FindElement(loginButton).Click();
        }

        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLoginButton();
        }
    }
}
