using System;
using System.IO;
using System.Threading;
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
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
        }

        public void NavigateToHomePage()
        {
            _driver.Navigate().GoToUrl(HomeUrl);
        }

        public bool IsHomePageDisplayed(int timeoutSeconds = 15)
        {
            try
            {
                var waitLocal = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));
                var searchBox = waitLocal.Until(d => d.FindElement(DarazLocators.MainSearchBox));
                return searchBox.Displayed && _driver.Title.ToLower().Contains("daraz");
            }
            catch { return false; }
        }

        // UI-based change to Bangla (kept for coverage/fallback)
        public bool ChangeLanguageToBangla()
        {
            try
            {
                var langToggle = _wait.Until(d => d.FindElement(DarazLocators.LangToggle));
                var actions = new Actions(_driver);
                actions.MoveToElement(langToggle).Perform();
                var bnOption = _wait.Until(d => d.FindElement(DarazLocators.BanglaOption));
                bnOption.Click();
                // wait briefly for UI
                Thread.Sleep(500);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Debug] ChangeLanguageToBangla failed: {ex.Message}");

                // Fallback: try to click via JS
                try
                {
                    ((IJavaScriptExecutor)_driver).ExecuteScript("var e=document.querySelector('[data-lang=\\\"bn\\\"]'); if(e) e.click();");
                    Thread.Sleep(800);

                    // quick verification using localStorage/cookie
                    try
                    {
                        var checkScript = @"
                            try {
                                var c = '';
                                try { c = localStorage.getItem('lzd_lang') || localStorage.getItem('language') || (window.g_config && window.g_config.language) || ''; } catch(e) { }
                                if (c) return c;
                                var m = document.cookie.match(/(^|;)\s*lzd_lang=([^;]+)/);
                                return m ? m[2] : '';
                            } catch(e) { return ''; }
                        ";

                        var res = ((IJavaScriptExecutor)_driver).ExecuteScript(checkScript) as string ?? string.Empty;
                        if (!string.IsNullOrEmpty(res) && res.IndexOf("bn", StringComparison.OrdinalIgnoreCase) >= 0)
                            return true;
                    }
                    catch { }
                }
                catch { }

                // Last resort: deterministic setter
                try
                {
                    return SetLanguageDeterministic("Bangla");
                }
                catch { return false; }
            }
        }

        // Deterministic setter: set localStorage/window.g_config and cookie then refresh and verify
        public bool SetLanguageDeterministic(string targetLanguage)
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

                try
                {
                    ((IJavaScriptExecutor)_driver).ExecuteScript(script);
                }
                catch { /* ignore script execution errors and continue to refresh/fallback */ }

                Thread.Sleep(500);
                _driver.Navigate().Refresh();

                try
                {
                    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                    bool ok = wait.Until(d =>
                    {
                        try
                        {
                            var checkScript = @"
                                try {
                                    var c = '';
                                    try { c = localStorage.getItem('lzd_lang') || localStorage.getItem('language') || (window.g_config && window.g_config.language) || ''; } catch(e) { }
                                    if (c) return c;
                                    var m = document.cookie.match(/(^|;)\s*lzd_lang=([^;]+)/);
                                    return m ? m[2] : '';
                                } catch(e) { return ''; }
                            ";

                            var res = ((IJavaScriptExecutor)d).ExecuteScript(checkScript) as string ?? string.Empty;
                            return !string.IsNullOrEmpty(res) && res.IndexOf(code, StringComparison.OrdinalIgnoreCase) >= 0;
                        }
                        catch { return false; }
                    });

                    if (ok) return true;
                }
                catch { /* timed out waiting for deterministic change */ }

                // Fallback: try UI click on Bangla option
                try
                {
                    var banglaDirect = _wait.Until(d => d.FindElement(DarazLocators.BanglaOption));
                    banglaDirect.Click();
                    Thread.Sleep(800);

                    // verify again
                    try
                    {
                        var verify = ((IJavaScriptExecutor)_driver).ExecuteScript(@"
                            try {
                                var c = '';
                                try { c = localStorage.getItem('lzd_lang') || localStorage.getItem('language') || (window.g_config && window.g_config.language) || ''; } catch(e) { }
                                if (c) return c;
                                var m = document.cookie.match(/(^|;)\s*lzd_lang=([^;]+)/);
                                return m ? m[2] : '';
                            } catch(e) { return ''; }
                        ") as string ?? string.Empty;

                        return !string.IsNullOrEmpty(verify) && verify.IndexOf(code, StringComparison.OrdinalIgnoreCase) >= 0;
                    }
                    catch { /* still failed */ }
                }
                catch { /* no UI fallback available */ }

                // final failure
                try { SaveSnapshot($"SetLanguage_{targetLanguage}"); } catch { }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Debug] Deterministic SetLanguage failed: {ex.Message}");
                try { SaveSnapshot($"SetLanguage_{targetLanguage}"); } catch { }
                return false;
            }
        }

        public string GetWelcomeMessage()
        {
            try
            {
                var element = _wait.Until(d => d.FindElement(DarazLocators.WelcomeMsg));
                return element.Text.Trim();
            }
            catch { return string.Empty; }
        }

        public string GetHelpCenterText()
        {
            try
            {
                var element = _wait.Until(d => d.FindElement(DarazLocators.HelpCenterLink));
                return element.Text.Trim();
            }
            catch { return string.Empty; }
        }

        public void RefreshPage()
        {
            _driver.Navigate().Refresh();
        }

        public void Refresh() => RefreshPage();

        public void SaveSnapshot(string namePrefix)
        {
            try
            {
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "TestResults");
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                var ts = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
                var baseName = $"{namePrefix}_{ts}";

                var htmlPath = Path.Combine(dir, baseName + ".html");
                File.WriteAllText(htmlPath, _driver.PageSource);

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
