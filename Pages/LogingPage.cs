
using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;

namespace Daraz.Automation.BDD.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        }

        public void ClickLogin()
        {
            var btn = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.loginButton));
            btn.Click();
        }

        public void EnterLoginCredentials(string email, string password)
        {
            var emailInput = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.inputEmail));
            
            emailInput.Clear();
            emailInput.SendKeys(email);
            
            var passInput = _driver.FindElement(DarazLocators.inputPassword); 
            passInput.Clear();
            passInput.SendKeys(password);
            Thread.Sleep(500); 
        }


        public void VerifyLoginWindowOpened()
        {
            var loginWindow = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.loginWindowAssertion));
            Assert.That(loginWindow.Displayed, Is.True, 
                $"Verification Failed: Modal '{DarazLocators.loginWindowAssertion}' not displayed");
        }

        public void SubmitLogin()
        {
            var submit = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.btnSubmit));
            
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].click();", submit);
            _wait.Until(d => !d.Url.Contains("login") && !d.Url.Contains("signup"));
        }

        public void loginAssertion()
        {
            var profileElement = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.profileId));
            string actualProfileText = profileElement.Text.Trim();
            string expectedProfileText = "KATHA SIKDAR'S ACCOUNT";
            Assert.That(actualProfileText, Is.EqualTo(expectedProfileText),
                $"Verification Failed: expected '{expectedProfileText}' but found '{actualProfileText}'");
        }

    }
}