    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using OpenQA.Selenium.Interactions;
    using SeleniumExtras.WaitHelpers;
    using NUnit.Framework;

    namespace Daraz.Automation.BDD.Pages
    {
        public class DarazPage
        {
            private readonly IWebDriver _driver;
            private readonly WebDriverWait _wait;

            public DarazPage(IWebDriver driver)
            {
                _driver = driver;
                _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
            }

            public void NavigateToHomePage() => _driver.Navigate().GoToUrl("https://www.daraz.com.bd/");

            public void AssertHomePageVisible()
            {
                var search = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.MainSearchBox));
                Assert.That(search.Displayed, Is.True, "Homepage failed to load.");
            }

            public void OpenLanguageMenu()
            {
                var menu = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.languageSelectDrpdwn));
                new Actions(_driver).MoveToElement(menu).Perform();
                menu.Click(); 
            }

            public void SelectLanguageFromPopup(string langCode)
            {
                By targetButton = langCode == "bn" ? DarazLocators.BanglaOption : DarazLocators.EnglishOption;
                var languageBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(targetButton));
                languageBtn.Click();
                _wait.Until(ExpectedConditions.StalenessOf(languageBtn));
            }
        }
    }