    using TechTalk.SpecFlow;
        using Daraz.Automation.BDD.Hooks;
        using Daraz.Automation.BDD.Pages;

        namespace Daraz.Automation.BDD.Steps
        {
            [Binding]
            public class DarazFlowSteps
            {
                private readonly DarazPage _page;

                public DarazFlowSteps()
                {
                    
                    _page = new DarazPage(Hook.driver);
                }

                [Given(@"I navigate to Daraz website")]
                public void GivenINavigate() => _page.NavigateToHomePage();

                [Then(@"Verify the home page is displayed")]
                public void ThenVerifyHome() => _page.AssertHomePageVisible();

                [Then(@"Click on the Language Selection Dropdown and Select ""(.*)"" from the language options")]
                public void ThenSelectLanguageFromOptions(string languageName)
                {
        
                    string langCode = languageName.ToLower().Contains("bangla") ? "bn" : "en";
                    
                    _page.OpenLanguageMenu();
                    _page.SelectLanguageFromPopup(langCode);
                }

                 [Then(@"Change the site language back from ""Bangla"" to ""English"" and verify that the language is updated.")]
                public void ThenChangeLanguageBack()
                {
                    _page.OpenLanguageMenu();
                    _page.SelectLanguageFromPopup("en");
                }

            }
        }