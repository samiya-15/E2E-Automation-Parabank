using DotNetEnv;
using FluentAssertions;
using OpenQA.Selenium;
using ParabankE2EAutomation.Model;
using ParabankE2EAutomation.PageObjects.Pages;
using TechTalk.SpecFlow;

[Binding]
public class LoginSteps
{
    private readonly IWebDriver _driver;
    private readonly LoginPage loginPage;
    private readonly FeatureContext _featureContext;

    public LoginSteps(ScenarioContext scenarioContext, FeatureContext featureContext)
    {
        _driver = scenarioContext.Get<IWebDriver>("WebDriver");
        loginPage = new LoginPage(_driver);
        _featureContext = featureContext;
    }

    [Given(@"I am on the login page")]
    public void GivenIAmOnTheLoginPage()
    {
        string url = Env.GetString("URL");
        _driver.Navigate().GoToUrl(url);
    }

    [When(@"I login with the registered username and password")]
    public void WhenILoginWithTheRegisteredUsernameAndPassword()
    {
        _featureContext.TryGetValue("RegistrationData", out RegistrationData registrationData);

        loginPage.Login(registrationData.Username, registrationData.Password);
    }

    [Then(@"I should be logged in successfully")]
    public void ThenIShouldBeLoggedInSuccessfully()
    {
        var pageSource = _driver.PageSource;
        pageSource.Should().Contain("Welcome", because: "the user should be logged in successfully");
    }
}
