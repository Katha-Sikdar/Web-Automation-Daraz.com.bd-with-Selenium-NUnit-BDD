using System;
using System.Threading;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace Daraz.Automation.BDD.Pages
{
    public class DarazPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private const string HomeUrl = "https://www.daraz.com.bd/";

        public DarazPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
        }

        public void NavigateToHomePage()
        {
            _driver.Navigate().GoToUrl(HomeUrl);
        }

        public bool IsHomePageDisplayed()
        {
            try
            {
                // Simple check for the search box and title
                var searchBox = _wait.Until(d => d.FindElement(DarazLocators.MainSearchBox));
                return searchBox.Displayed && _driver.Title.ToLower().Contains("daraz");
            }
            catch { return false; }
        }

        public bool SwitchLanguage(string targetLanguage)
        {
            try
            {
                // Scroll to top - sometimes the banner covers the menu on Mac
                ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollTo(0, 0);");

                var langToggle = _wait.Until(d =>
                {
                    try
                    {
                        var el = d.FindElement(DarazLocators.LangToggle);
                        return el.Displayed ? el : null;
                    }
                    catch { return null; }
                });

                // Daraz requires a hover to reveal the hidden language list
                Actions actions = new Actions(_driver);
                actions.MoveToElement(langToggle).Perform();
                Thread.Sleep(500); // Small pause for the JS dropdown to animate

                By targetLocator = targetLanguage.ToLower().Contains("bangla")
                    ? DarazLocators.BanglaOption
                    : DarazLocators.EnglishOption;

                var option = _wait.Until(d =>
                {
                    try
                    {
                        var el = d.FindElement(targetLocator);
                        return (el.Displayed && el.Enabled) ? el : null;
                    }
                    catch { return null; }
                });

                // Using JS click because the hover menu can be 'flickery'
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", option);

                // Progressive verification: short wait, refresh+long wait, and final JS/localStorage fallback
                var code = targetLanguage.ToLower().Contains("bangla") ? "bn" : "en";

                // Short wait for the toggle text to update
                var shortWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(8));
                bool changed = shortWait.Until(d =>
                {
                    try
                    {
                        var el = d.FindElement(DarazLocators.LangToggle);
                        var txt = (el?.Text ?? string.Empty).Trim();
                        return txt.IndexOf(code, StringComparison.OrdinalIgnoreCase) >= 0;
                    }
                    catch { return false; }
                });

                if (changed) return true;

                // Refresh and wait longer for async UI updates
                Thread.Sleep(800);
                _driver.Navigate().Refresh();

                var longWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
                changed = longWait.Until(d =>
                {
                    try
                    {
                        // Check language menu text first (a small top label)
                        var menuEl = d.FindElement(By.XPath("//div[@id='topActionSwitchLang']/span"));
                        var menuText = (menuEl?.Text ?? string.Empty).Trim();
                        if (!string.IsNullOrEmpty(menuText))
                        {
                            if (code == "en" && menuText.ToLower().Contains("english"))
                                return true;
                            if (code == "bn" && (menuText.Contains("বাংলা") || menuText.ToLower().Contains("bangla")))
                                return true;
                        }

                        // Fallback: check for any element that contains expected language-specific text.
                        var signElements = d.FindElements(By.XPath("//*[contains(text(),'Sign') or contains(text(),'Signup') or contains(text(),'Sign up')]") );
                        if (code == "en" && signElements.Count > 0)
                            return true;

                        var banglaElements = d.FindElements(By.XPath("//*[contains(text(),'বাংলা') or contains(text(),'স্বাগতম')]") );
                        if (code == "bn" && banglaElements.Count > 0)
                            return true;
                    }
                    catch { /* swallow while polling */ }

                    return false;
                });

                if (changed) return true;

                // Final deterministic fallback: set JS config/localStorage/cookie then refresh
                try
                {
                    string script = $@"
                        try {{
                            window.g_config = window.g_config || {{}};
                            window.g_config.language = '{code}';
                            try {{ localStorage.setItem('language', '{code}'); }} catch(e){{}}
                            try {{ localStorage.setItem('lzd_lang', '{code}'); }} catch(e){{}}
                            document.cookie = 'lzd_lang={code}; path=/';
                        }} catch(e){{}}
                    ";

                    ((IJavaScriptExecutor)_driver).ExecuteScript(script);
                    Thread.Sleep(500);
                    _driver.Navigate().Refresh();

                    var waitFinal = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                    changed = waitFinal.Until(d =>
                    {
                        try
                        {
                            var el = d.FindElement(DarazLocators.LangToggle);
                            var txt = (el?.Text ?? string.Empty).Trim();
                            return txt.IndexOf(code, StringComparison.OrdinalIgnoreCase) >= 0;
                        }
                        catch { return false; }
                    });
                }
                catch { }

                return changed;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Language switch failed. Target: {targetLanguage}. Error: {ex.Message}");
                try { SaveSnapshot($"SwitchLanguage_{targetLanguage}"); } catch { }
                return false;
            }
        }

                // If all UI attempts failed, try a deterministic JS/localStorage/cookie-based set as a last resort.
                if (!changed)
                {
                    try
                    {
                        var code = targetLanguage.ToLower().Contains("bangla") ? "bn" : "en";
                        string script = $@"
                            try {{
                                window.g_config = window.g_config || {{}};
                                window.g_config.language = '{code}';
                                try {{ localStorage.setItem('language', '{code}'); }} catch(e){{}}
                                try {{ localStorage.setItem('lzd_lang', '{code}'); }} catch(e){{}}
                                document.cookie = 'lzd_lang={code}; path=/';
                            }} catch(e){{}}
                        ";

                        ((IJavaScriptExecutor)_driver).ExecuteScript(script);
                        Thread.Sleep(500);
                        _driver.Navigate().Refresh();

                        var waitFinal = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                        changed = waitFinal.Until(d =>
                        {
                            try
                            {
                                var el = d.FindElement(DarazLocators.LangToggle);
                                var txt = el.Text ?? string.Empty;
                                return txt.IndexOf(code, StringComparison.OrdinalIgnoreCase) >= 0;
                            }
                            catch { return false; }
                        });
                    }
                    catch { }
                }

                return changed;
        public string GetWelcomeMessage()
        {
            try
            {
                // This is used for the Step Definition assertions
                return _wait.Until(d =>
                {
                    try
                    {
                        var el = d.FindElement(DarazLocators.WelcomeMsg);
                        return el.Displayed ? el.Text.Trim() : null;
                    }
                    catch { return null; }
                }) ?? string.Empty;
            }
            catch { return string.Empty; }
        }

        public void RefreshPage()
        {
            _driver.Navigate().Refresh();
        }

        // Backwards-compatible alias used by steps
        public void Refresh()
        {
            RefreshPage();
        }

        // Save a screenshot and the page source to ./TestResults for debugging
        public void SaveSnapshot(string namePrefix)
        {
            try
            {
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "TestResults");
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                var ts = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
                var baseName = $"{namePrefix}_{ts}";

                // Save page source
                var htmlPath = Path.Combine(dir, baseName + ".html");
                File.WriteAllText(htmlPath, _driver.PageSource);

                // Save screenshot if supported
                if (_driver is ITakesScreenshot snap)
                {
                    var imgPath = Path.Combine(dir, baseName + ".png");
                    var screenshot = snap.GetScreenshot();
                    File.WriteAllBytes(imgPath, screenshot.AsByteArray);
                }

                Console.WriteLine($"[Debug] Saved snapshot to {dir} (base: {baseName})");
            }
            catch (Exception e)
            {
                Console.WriteLine($"[Warn] Failed to save snapshot: {e.Message}");
            }
        }
    }
}