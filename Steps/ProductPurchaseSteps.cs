using TechTalk.SpecFlow;
using Daraz.Automation.BDD.Hooks;
using Daraz.Automation.BDD.Pages;
using System;
using System.Threading;

namespace Daraz.Automation.BDD.Steps
{
    [Binding]
    public class ProductPurchaseSteps
    {
        private readonly ProductpurchasePage _productPurchasePage;

        public ProductPurchaseSteps()
        {
            _productPurchasePage = new ProductpurchasePage(Hook.driver!);
        }

        [Then(@"click on the Cart icon and verify that the Cart page is opened")]
        public void ClickCartIconAndVerify()
        {
            _productPurchasePage.ClickCartIconAndVerifyCartPage();
            Thread.Sleep(2000);

        }

    [Then(@"Click on the Category Dropdown ""(.*)"" and select a product and add to Cart")]
    public void ClickCategoryDropdownAndSelect(string categoryType)
    {
        _productPurchasePage.ClickCategoryDropdownAndSelectCategory(categoryType);
    }
}
    }
