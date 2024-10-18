using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ParabankE2EAutomation.Support
{
    public static class Utilities
    {
        public static void ExplicitWait(IWebDriver driver, By by)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(120)).Until(ExpectedConditions.ElementIsVisible(by));
            }
            catch (Exception)
            {
                Console.WriteLine("Wait Over");
            }
        }

        public static void ExplicitWait(IWebDriver driver, IWebElement element)
        {

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
                wait.Until(ExpectedConditions.ElementToBeClickable(element));
            }
            catch (Exception)
            {
                Console.WriteLine("Wait Over");
            }
        }

        public static void StaticWait(int milisec)
        {
            Thread.Sleep(milisec);
        }
    }
}
