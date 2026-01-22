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
       
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");

            try
            {
                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Hook] WebDriverManager.SetUpDriver warning: " + ex.Message);
            }

            driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(2));
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            driver?.Quit();
            driver = null;
        }
    }
}