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
    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
    
    ChromeOptions options = new ChromeOptions();
    options.AddArgument("--no-sandbox");
    options.AddArgument("--disable-dev-shm-usage");

    // Increase the command timeout to 2 minutes to give Daraz time to load on heavy networks
    driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(2));
    
    driver.Manage().Window.Maximize();
    // Also set a page load timeout
    driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
}

        [AfterScenario]
        public void TearDown() => driver?.Quit();
    }
}