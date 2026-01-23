using TechTalk.SpecFlow;
using Daraz.Automation.BDD.Hooks;
using Daraz.Automation.BDD.Pages;
using System;

namespace Daraz.Automation.BDD.Steps
{
    [Binding]
    public class ProductPurchaseSteps
    {
        private readonly ProductpurchasePage _productPurchasePage;

        public ProductPurchaseSteps()
        {
           // _loginPage = new LoginPage(Hook.driver!);
          //  _productPurchasePage = new ProductpurchasePage(Hook.driver!);
        }

        [Then(@"click on the Cart icon and verify that the Cart page is opened")]
        public void ClickCartIconAndVerify()
        {
            _productPurchasePage.ClickCartIconAndVerifyCartPage();      
        }
    }
}