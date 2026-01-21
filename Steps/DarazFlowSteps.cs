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
            // Allow a slightly longer wait for the homepage to stabilize
            bool isDisplayed = darazPage.IsHomePageDisplayed(timeoutSeconds: 20);
            isDisplayed.Should().BeTrue("because the user should be redirected to the Daraz Homepage after navigation.");
        }
        [Then(@"I change language From ""(.*)"" to ""(.*)"" and verify")]
        public void WhenIChangeLanguageFromEnglishToBanglaAndVerify(string fromLanguage, string toLanguage)
        {
            var darazPage = new DarazPage(Hook.driver!);
            bool changed = false;

            // Support both directions; currently we have an implementation for Bangla.
            if (toLanguage?.Equals("Bangla", StringComparison.OrdinalIgnoreCase) == true)
            {
                try { changed = darazPage.ChangeLanguageToBangla(); } catch { changed = false; }
            }
            else if (toLanguage?.Equals("English", StringComparison.OrdinalIgnoreCase) == true)
            {
                // try to click the English option
                try
                {
                    var english = Hook.driver!.FindElement(DarazLocators.EnglishOption);
                    english.Click();
                    Thread.Sleep(800);
                    changed = true;
                }
                catch { changed = false; }
            }
            else
            {
                // unsupported language requested
                changed = false;
            }

            changed.Should().BeTrue($"because we expect the language to change to '{toLanguage}'");

            // Verification: 'WelcomeMsg' should change when language updates
            string helpText = darazPage.GetHelpCenterText();
            if (!string.IsNullOrEmpty(helpText))
            {
                helpText.Should().NotBe("Help Center", "because the language should have switched.");
            }
        }
    }
}