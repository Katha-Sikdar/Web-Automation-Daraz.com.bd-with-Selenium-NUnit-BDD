using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

namespace Daraz.Automation.BDD.Hooks
{
    [Binding]
    public class Hook
    {
    public static IWebDriver? driver;

        [BeforeScenario]
        public void Setup()
        {
            // Use WebDriverManager to ensure a compatible chromedriver is available.
            // Keep this simple and let WebDriverManager pick the correct driver.
            try
            {
                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Hook] WebDriverManager.SetUpDriver failed: " + ex.Message);
            }

            driver = new ChromeDriver();
            Console.WriteLine("[Hook] ChromeDriver created. Session: " + (driver as OpenQA.Selenium.Remote.RemoteWebDriver)?.SessionId);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [AfterScenario]
        public void TearDown() => driver?.Quit();
    }
}