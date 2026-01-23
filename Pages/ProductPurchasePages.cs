using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;

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
             cartBtn.Click();
             _wait.Until(d => d.Url.Contains("/cart"));
            
        }

        public void ClickCategoryDropdownAndSelectCategory(string categoryType)
{
    _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(DarazLocators.overlayBackdrop));

    var categoryDrpDwn = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.clickCategoryDrpDwn));
    categoryDrpDwn.Click();


    if (categoryType.ToLower().Contains("first"))
    {
     
        _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.categoryOne)).Click();
        
        var subCat = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.subCategoryOne));
        new Actions(_driver).MoveToElement(subCat).Perform();
        
        var item = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.firstItem));
        item.Click();

        var firstItemAddToCart = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.firstItemAddToCart));
        firstItemAddToCart.Click();

        var addToCartBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.btnAddtoCart));
        addToCartBtn.Click();

         var itemAddedSuccessfully = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.itemAddedSuccessfully));
         Assert.That(itemAddedSuccessfully.Displayed, Is.True, "Item was not added to cart successfully.");

        var dialogCloseBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.dialogBtnClose));
        dialogCloseBtn.Click();
    }
    else
    {
        
        _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.categoryTwo)).Click();

        var subCat2 = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.subCategoryTwo));
        subCat2.Click();
        var item2 = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.secondItem));
        item2.Click();

        var secondItemAddToCart = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.btnAddtoCart));
        secondItemAddToCart.Click();
        var success = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.itemAddedSuccessfully));
        Assert.That(success.Displayed, Is.True, "Item was not added to cart successfully.");    

        var dialogCloseBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.dialogBtnClose));
        dialogCloseBtn.Click();
    }

    CompletePurchaseFlow();
}

private void CompletePurchaseFlow()
{
    var addBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.btnAddtoCart));
    addBtn.Click();
    
    var success = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.itemAddedSuccessfully));
    Assert.That(success.Displayed, Is.True);
}

    }
}