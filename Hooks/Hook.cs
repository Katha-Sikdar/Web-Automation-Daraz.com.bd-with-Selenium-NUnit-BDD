using System;
using System.IO;
using Allure.Net.Commons;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using WebDriverManager.DriverConfigs.Impl;

namespace Daraz.Automation.BDD.Hooks
{
    [Binding]
    public class Hook
    {
        public static IWebDriver? driver;
        private readonly ScenarioContext _scenarioContext;

        public Hook(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            ChromeOptions options = new ChromeOptions();
            
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddArgument("--no-sandbox");

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [AfterStep]
        public void AfterStep()
        {
            if (_scenarioContext.TestError != null && driver != null)
            {
                try
                {
                    string stepName = _scenarioContext.StepContext.StepInfo.Text;
                    byte[] content = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
                
                    AllureApi.AddAttachment($"Failed: {stepName}", "image/png", content);
                    
                    string screenshotDir = "/Users/katha/Desktop/Automation Assessment/Web-Automation-Daraz.com.bd-with-Selenium-NUnit-BDD/ScreenShot";
                    if (!Directory.Exists(screenshotDir)) Directory.CreateDirectory(screenshotDir);

                    string filePath = Path.Combine(screenshotDir, $"Error_{DateTime.Now:HHmmss}.png");
                    File.WriteAllBytes(filePath, content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Hook Error] Screenshot capture failed: {ex.Message}");
                }
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (driver != null)
            {
                driver.Quit();
                //driver = null;
            }
        }
    }
}
