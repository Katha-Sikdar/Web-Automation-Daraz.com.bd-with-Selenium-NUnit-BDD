        using System;
        using OpenQA.Selenium;
        using OpenQA.Selenium.Support.UI;
        using SeleniumExtras.WaitHelpers;
        using NUnit.Framework;
    using Daraz.Automation.BDD.Hooks;

    namespace Daraz.Automation.BDD.Pages
        {
            public class RegistrationPage
            {
                private readonly IWebDriver _driver;
                private readonly WebDriverWait _wait;

                public RegistrationPage(IWebDriver driver)
                {
                    _driver = driver;
                    _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
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
                    Thread.Sleep(1000);
                
                }

                public void CheckTermsAndConditions()
                {
                    var termsCheckbox = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.modalCheckBox));
                    termsCheckbox.Click();
                    Thread.Sleep(1000);
                }

                // public void ClickSubmit()
                // {
                //     var submitBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.submitButton));
                //     submitBtn.Click();
                    
                //     _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(DarazLocators.submitButton));
                    
                // }

                public void ClickSubmit()
    {
        var submitBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.submitButton));
        submitBtn.Click();
        try 
        {
            var errorToast = new WebDriverWait(Hook.driver!, TimeSpan.FromSeconds(2))
                .Until(ExpectedConditions.ElementIsVisible(DarazLocators.invalidPhoneNumberError));
            throw new Exception("Stopped Test: phone number is invalid!");
        }
        catch (WebDriverTimeoutException)
        {
            _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(DarazLocators.submitButton));
        }
    }
            }
        }