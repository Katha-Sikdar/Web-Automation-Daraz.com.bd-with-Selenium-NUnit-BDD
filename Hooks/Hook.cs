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
            // Try to detect installed Chrome major version and ask WebDriverManager to fetch matching chromedriver.
            try
            {
                string chromePath = "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome";
                string chromeVersion = string.Empty;
                if (System.IO.File.Exists(chromePath))
                {
                    var psi = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = chromePath,
                        Arguments = "--version",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    using var proc = System.Diagnostics.Process.Start(psi);
                    chromeVersion = proc?.StandardOutput.ReadLine() ?? string.Empty;
                }

                string major = string.Empty;
                if (!string.IsNullOrEmpty(chromeVersion))
                {
                    // chromeVersion looks like: "Google Chrome 143.0.7499.193"
                    var parts = chromeVersion.Split(' ');
                    if (parts.Length >= 3)
                    {
                        major = parts[2].Split('.')[0];
                    }
                }

                if (!string.IsNullOrEmpty(major))
                {
                    // ask WebDriverManager to download matching driver (pass major version)
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), major);
                }
                else
                {
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                }

                driver = new ChromeDriver();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Hook] Error while setting up ChromeDriver via WebDriverManager: " + ex.Message);
                // fallback to default behavior
                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                driver = new ChromeDriver();
            }
            Console.WriteLine("[Hook] ChromeDriver created. Session: " + (driver as OpenQA.Selenium.Remote.RemoteWebDriver)?.SessionId);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [AfterScenario]
        public void TearDown() => driver?.Quit();
    }
}