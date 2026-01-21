using System;
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

        // Verify by Title AND visibility of the unique Logo
        public bool IsHomePageDisplayed()
        {
            try
            {
                bool isTitleCorrect = (_driver.Title ?? string.Empty).Contains("Online Shopping in Bangladesh", StringComparison.OrdinalIgnoreCase);
                // Use a local locator to avoid any compile-time locator resolution issues
                var logoBy = By.XPath("//img[@alt='Daraz']");
                bool isLogoVisible = _wait.Until(d => d.FindElement(logoBy)).Displayed;

                return isTitleCorrect && isLogoVisible;
            }
            catch
            {
                return false;
            }
        }

        public void ChangeLanguageToBangla()
        {
            // Click the language toggle and pick Bangla
            var switcher = _wait.Until(d => d.FindElement(DarazLocators.LangToggle));
            switcher.Click();

            // Click the Bangla (BN) option
            var bangla = _wait.Until(d => d.FindElement(DarazLocators.BanglaOption));
            bangla.Click();
        }

        public string GetHelpCenterText()
        {
            // Get text from a known element to verify language change
            return _wait.Until(d => d.FindElement(DarazLocators.WelcomeMsg)).Text;
        }
    }
}