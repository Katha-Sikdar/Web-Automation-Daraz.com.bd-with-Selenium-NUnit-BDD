using System;
using Daraz.Automation.BDD.Hooks;
using Daraz.Automation.BDD.Pages;
using FluentAssertions;
using TechTalk.SpecFlow;
using OpenQA.Selenium;


namespace Daraz.Automation.BDD.Steps
{
   [Binding]
   public class DarazFlowSteps
   {
       private readonly DarazPage _darazPage;
       private readonly IWebDriver _driver;


       public DarazFlowSteps()
       {
           _driver = Hook.driver;
           _darazPage = new DarazPage(_driver);
       }


       [Given(@"Daraz website navigation")]
       public void GivenDarazWebsiteNavigation()
       {
           _darazPage.NavigateToHomePage();
       }


       [Then(@"Verify the home page is displayed or not")]
       public void ThenVerifyHomePageDisplay()
       {
           bool isDisplayed = _darazPage.IsHomePageDisplayed();
           isDisplayed.Should().BeTrue("Homepage failed to load or the unique logo was not found.");
       }

       [Then(@"Change language From ""(.*)"" to ""(.*)"" and verify")]
       public void ThenChangeLanguageAndVerify(string fromLanguage, string toLanguage)
       {
           bool isSuccess = false;


           if (toLanguage.Equals("Bangla", StringComparison.OrdinalIgnoreCase))
           {
          
               isSuccess = _darazPage.ChangeLanguageToBangla();
           }
           else if (toLanguage.Equals("English", StringComparison.OrdinalIgnoreCase))
           {
               var englishOption = _driver.FindElement(DarazLocators.EnglishOption);
               englishOption.Click();
               isSuccess = true;
           }


           // Assertion
           isSuccess.Should().BeTrue($"The UI failed to switch the language to {toLanguage}");


           // text change verification
           var welcomeText = _darazPage.GetWelcomeMessage();
          
           if (toLanguage.Equals("Bangla"))
           {
               welcomeText.Should().NotBe("Signup / Login", "The welcome text should be in Bangla now.");
           }
       }
   }
}

