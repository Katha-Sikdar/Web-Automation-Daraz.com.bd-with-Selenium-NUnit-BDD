// using System;
// using TechTalk.SpecFlow;
// using OpenQA.Selenium;
// using OpenQA.Selenium.Chrome;
// using WebDriverManager.DriverConfigs.Impl;
// using Allure.Net.Commons;

// namespace Daraz.Automation.BDD.Hooks
// {
//     [Binding]
//     public class Hook
//     {
//     public static IWebDriver? driver;
       
//         [BeforeTestRun]
//         public static void BeforeTestRun()
//         {
//             ChromeOptions options = new ChromeOptions();
//             options.AddArgument("--no-sandbox");
//             options.AddArgument("--disable-dev-shm-usage");

//             try
//             {
//                 new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine("[Hook] WebDriverManager.SetUpDriver warning: " + ex.Message);
//             }

//             driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(2));
//             driver.Manage().Window.Maximize();
//             driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
//         }

//         [AfterTestRun]
//         public static void AfterTestRun()
//         {
//             driver?.Quit();
//             driver = null;
//         }
//         private void TakeScreenshot(string stepName)
// {
//     if (driver is ITakesScreenshot ts)
//     {
//         var screenshot = ts.GetScreenshot().AsByteArray;
        
//         // This line attaches the image specifically to the Allure report
//         AllureLifecycle.Instance.AddAttachment(stepName, "image/png", screenshot);
        
//         // Also keep your file saving logic if you want local copies
//         File.WriteAllBytes(Path.Combine(directoryPath, "fail.png"), screenshot);
//     }
// }

//         }
//     }

using System;
using System.IO;
using Allure.Net.Commons;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using WebDriverManager.DriverConfigs.Impl;

// If you REALLY want the assembly attribute, it must be here (above the namespace)
// But for modern Allure, it's safer to rely on specflow.json.
// [assembly: Allure.SpecFlowPlugin.AllureFeature] 

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
            
            // Human Tip: Helps bypass bot detection on Daraz
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddArgument("--no-sandbox");

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [AfterStep]
        public void AfterStep()
        {
            // Human Logic: Capture the exact state when a step fails
            if (_scenarioContext.TestError != null && driver != null)
            {
                try
                {
                    // Use a unique name for each failed step screenshot
                    string stepName = _scenarioContext.StepContext.StepInfo.Text;
                    byte[] content = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
                    
                    // Attach to Allure
                    AllureApi.AddAttachment($"Failed: {stepName}", "image/png", content);
                    
                    // Save locally on your Mac for quick access
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
                driver = null; // Important to avoid 'Object reference' errors in next test
            }
        }
    }
}
