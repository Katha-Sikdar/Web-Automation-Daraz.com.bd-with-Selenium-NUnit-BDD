using Daraz.Automation.BDD.Hooks;
using Daraz.Automation.BDD.Pages;
using TechTalk.SpecFlow;

namespace Daraz.Automation.BDD.Steps
{
    [Binding]
    public class DarazFlowSteps
    {
        [Given(@"I navigate to Daraz website")]
        public void GivenINavigateToDarazWebsite()
        {
            // Construct the page object after the Hook has initialized the driver
            var darazPage = new DarazPage(Hook.driver!);
            darazPage.NavigateToHomePage();
        }
    }
}