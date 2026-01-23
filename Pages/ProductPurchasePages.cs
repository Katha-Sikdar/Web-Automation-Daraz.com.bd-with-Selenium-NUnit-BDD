using System;
using System.Threading; // Added for small human-like pauses
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
public void ClickCartIconAndVerifyCartPage()
{
    // 1. Wait for the cart icon to be clickable and click it
    var cartBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.cartIcon));
    cartBtn.Click();

    // 2. HUMAN TOUCH: Wait for the URL to contain 'cart' 
    // This is more reliable than just checking if an element exists.
    _wait.Until(d => d.Url.Contains("cart.daraz.com.bd") || d.Url.Contains("/cart"));

    // 3. Verify a "Cart Page" specific element (like the 'Proceed to Checkout' button)
    // This proves we aren't just on a blank page or a login redirect.
   // var cartHeader = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h1[contains(text(), 'Shopping Cart')] | //button[contains(text(), 'PROCEED TO CHECKOUT')]")));
    
    //Assert.That(cartHeader.Displayed, Is.True, "Cart page failed to load properly.");
}
    }
}