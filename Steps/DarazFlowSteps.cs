using Daraz.Automation.BDD.Hooks;
using Daraz.Automation.BDD.Pages;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Daraz.Automation.BDD.Steps
{
    [Binding]
    public class DarazFlowSteps
    {
        [Given(@"I navigate to Daraz website")]
        public void GivenINavigateToDarazWebsite()
        {
            var darazPage = new DarazPage(Hook.driver!);
            darazPage.NavigateToHomePage();
        }
        [Then(@"Verify the home page is displayed")]
        public void ThenVerifyTheHomePageIsDisplayed()
        {
            var darazPage = new DarazPage(Hook.driver!);
            bool isDisplayed = darazPage.IsHomePageDisplayed();
            isDisplayed.Should().BeTrue("because the user should be redirected to the Daraz Homepage after navigation.");
        }
        [Given(@"I change language From ""English"" to ""Bangla"" and verify")]
        public void WhenIChangeLanguageFromEnglishToBanglaAndVerify()
        {
            var darazPage = new DarazPage(Hook.driver!);

            darazPage.ChangeLanguageToBangla();

            // Verification: 'Help Center' in Bangla is typically 'সহায়তা কেন্দ্র'
            string helpText = darazPage.GetHelpCenterText();
            helpText.Should().NotBe("Help Center", "because the language should have switched to Bangla.");
        }
    }
}