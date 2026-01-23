// using System;
// using OpenQA.Selenium;
// using OpenQA.Selenium.Support.UI;
// using SeleniumExtras.WaitHelpers;
// using NUnit.Framework;

// namespace Daraz.Automation.BDD.Pages
// {
//     public class ProductpurchasePage
//     {
//         private readonly IWebDriver _driver;
//         private readonly WebDriverWait _wait;

//         // HUMAN PATTERN: The constructor must initialize the driver and wait objects
//         public ProductpurchasePage(IWebDriver driver)
//         {
//             _driver = driver;
//             _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
//         }

//         public void ClickCartIconAndVerifyCartPage()
//         {
//             // Wait for visibility first—more stable than just "clickable" on heavy sites
//             var cartBtn = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.cartIcon));
//             cartBtn.Click();
//              _wait.Until(d => d.Url.Contains("/cart"));

//             // try 
//             // {
//             //     cartBtn.Click();
//             // }
//             // catch (ElementClickInterceptedException)
//             // {
//             //     // FALLBACK: If a popup blocks the icon, force the click via JavaScript
//             //     ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", cartBtn);
//             // }

//             // Verify the URL changed—the most reliable proof of navigation
//            // _wait.Until(d => d.Url.Contains("/cart"));

//             // Verify a checkout button exists. Using a class-based XPath is more robust 
//             // than text-based because it works regardless of language (English/Bengali).
//             // var checkoutBtn = _wait.Until(ExpectedConditions.ElementIsVisible(
//             //     By.XPath("//button[contains(@class, 'checkout') or contains(@class, 'checkout-btn')]")));

//             // Assert.That(checkoutBtn.Displayed, Is.True, "Failed to load the Cart page content.");
//         }
//     }
// }

using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;

namespace Daraz.Automation.BDD.Pages
{
    public class ProductpurchasePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public ProductpurchasePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }

        public void ClickCartIconAndVerifyCartPage()
        {
            var cartBtn = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.cartIcon));
            //var cartBtn = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.cartIcon));
             cartBtn.Click();
             _wait.Until(d => d.Url.Contains("/cart"));
            
        }

        public void ClickCategoryDropdownAndSelectCategory()
        {
            var categoryDrpDwn = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.clickCategoryDrpDwn));
            categoryDrpDwn.Click();

            var category1 = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.categoryOne));
            category1.Click();

            var subCategory1 = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.subCategoryOne));
            
            var actions = new OpenQA.Selenium.Interactions.Actions(_driver);
            actions.MoveToElement(subCategory1).Perform();

            _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.firstItem));

            var firstItem = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.firstItem));
            firstItem.Click();

            var firstItemAddToCart = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.firstItemAddToCart));
            firstItemAddToCart.Click();

            var addItemToCart = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.btnAddtoCart));
            addItemToCart.Click();

            var itemAddedSuccessfully = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.itemAddedSuccessfully));
            Assert.That(itemAddedSuccessfully.Displayed, Is.True, "Item was not added to cart successfully.");

            //var addToCartBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.btnAddtoCart));
            // addToCartBtn.Click();
        }
    }
}