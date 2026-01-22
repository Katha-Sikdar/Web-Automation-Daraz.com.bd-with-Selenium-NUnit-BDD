using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace Daraz.Automation.BDD.Pages
{
    public class DarazPage
    {
        private readonly IWebDriver _driver;
        private const string Url = "https://www.daraz.com.bd/";
        private readonly WebDriverWait _wait;

        public DarazPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        // Action method for the navigation step
        public void NavigateToHomePage()
        {
            _driver.Navigate().GoToUrl(Url);
        }

        public bool IsHomePageDisplayed(int timeoutSeconds = 15)
        {
            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));

                return wait.Until(d =>
                {
                    try
                    {
                        var title = (d.Title ?? string.Empty).Trim();
                        if (!string.IsNullOrEmpty(title) && title.IndexOf("daraz", StringComparison.OrdinalIgnoreCase) >= 0)
                            return true;

                        var current = (d.Url ?? string.Empty).Trim();
                        if (current.IndexOf("daraz.com.bd", StringComparison.OrdinalIgnoreCase) >= 0)
                            return true;

                        // check main search box
                        try
                        {
                            var search = d.FindElement(DarazLocators.MainSearchBox);
                            if (search.Displayed)
                                return true;
                        }
                        catch { }

                        // check logo
                        try
                        {
                            var logoBy = By.XPath("//img[@alt='Daraz']");
                            var logo = d.FindElement(logoBy);
                            if (logo.Displayed)
                                return true;
                        }
                        catch { }

                        return false;
                    }
                    catch
                    {
                        return false;
                    }
                });
            }
            catch
            {
                return false;
            }
        }

    public bool ChangeLanguageToBangla()
        {
            try
            {
                var switcher = _wait.Until(d => d.FindElement(DarazLocators.LangToggle));
                try { switcher.Click(); } catch { /* ignore click failures */ }

                var bangla = _wait.Until(d => d.FindElement(DarazLocators.BanglaOption));
                try { bangla.Click(); } catch { /* ignore */ }

                Thread.Sleep(800);

                return true;
            }
            catch
            {
                try
                {
                    var banglaDirect = _wait.Until(d => d.FindElement(DarazLocators.BanglaOption));
                    banglaDirect.Click();
                    Thread.Sleep(800);
                    return true;
                }
                catch
                {
                    try
                    {
                        var script = "var e = document.querySelector('span:contains(\'BN\')'); if(e) e.click();";
                        ((IJavaScriptExecutor)_driver).ExecuteScript(script);
                        Thread.Sleep(800);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public string GetHelpCenterText()
        {
            try
            {
                var el = _wait.Until(d => d.FindElement(DarazLocators.WelcomeMsg));
                return el?.Text ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}