using ParabankE2EAutomation.Support;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace ParabankE2EAutomation.PageObjects.Pages
{
    internal class HomePage
    {
        private WebDriver driver;

        public HomePage(WebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//a[@aria-label='Category A']")]
        private IWebElement mnuCategoryA;

        [FindsBy(How = How.XPath, Using = "//a[@aria-label='Category B']")]
        private IWebElement mnuCategoryB;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'In stock')]")]
        private IWebElement chkInStock;

        public void NavigateToCategoryA()
        {
            mnuCategoryA.Click();
        }

        public void NavigateToCategoryB()
        {
            mnuCategoryB.Click();
        }

        public void CheckInStock()
        {
            Utilities.ExplicitWait(driver, chkInStock);
            chkInStock.Click();
        }
    }
}
