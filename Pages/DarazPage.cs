using OpenQA.Selenium;

namespace Daraz.Automation.BDD.Pages
{
    public class DarazPage
    {
        private readonly IWebDriver _driver;
        private const string Url = "https://www.daraz.com.bd/";

        public DarazPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Action method for the navigation step
        public void NavigateToHomePage()
        {
            _driver.Navigate().GoToUrl(Url);
        }
    }
}