using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;

namespace Daraz.Automation.BDD.Pages
{
    public class RegistrationPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public RegistrationPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
        }

        public void ClickSignUp()
        {
            var btn = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.signUpButton));
            btn.Click();
        }

        public void VerifySignUpWindowOpened()
        {
            var registrationForm = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.modalCheckBox));
            Assert.That(registrationForm.Displayed, Is.True, "The Registration form did not load properly.");
        }

        public void EnterMobileNumber(string mobile)
        {
            var phoneInput = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.mobileInput));
            phoneInput.Click();
            phoneInput.Clear();
            phoneInput.SendKeys(mobile);
            

            _wait.Until(d => phoneInput.GetAttribute("value").Length > 0);
        }

        public void CheckTermsAndConditions()
        {
            var termsCheckbox = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.termsAndConditionsCheckbox));
            if (!termsCheckbox.Selected)
            {
                termsCheckbox.Click();
            }
        }

        public void ClickSubmit()
        {
            var submitBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.submitButton));
            submitBtn.Click();
            
            _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(DarazLocators.submitButton));
        }

        public void HandleUnusualTrafficSlider()
        {
            try
            {
                
                var slider = _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("nc_1_n1z")));
                
                if (slider.Displayed)
                {
                    Console.WriteLine("CAPTCHA Detected! Please slide the bar manually.");
                    
                    var longWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
                    longWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("nc_1_n1z")));
                }
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("No slider detected, continuing test");
            }
        }
    }
}