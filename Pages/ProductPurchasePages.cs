using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;
using WebDriverManager.Services.Impl;

namespace Daraz.Automation.BDD.Pages
{
    public class ProductpurchasePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public ProductpurchasePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
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
}

private void CompletePurchaseFlow()
{
    var addBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.btnAddtoCart));
    addBtn.Click();
    
    var success = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.itemAddedSuccessfully));
    Assert.That(success.Displayed, Is.True);
}
    
public void ProceedToCheckout()
{
    var cartBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.cartIcon));
    cartBtn.Click();
    var selectAllchkBox = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.selectAllchkBox));
    selectAllchkBox.Click();

    var proceedToCheckoutButton = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.proceedToCheckoutButton));
    proceedToCheckoutButton.Click();
    Thread.Sleep(2000);

    var proceedToPayBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(DarazLocators.proceedToPayBtn));
    proceedToPayBtn.Click();
}  

public void FillDeliveryInformationForm(string FullName,string PhoneNumber, string Building, string Colony, string Address)
{
    var fullNameInput = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.fullNameInput));
    fullNameInput.Clear();
    fullNameInput.SendKeys(FullName);
    
    var phoneNumberInput = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.phoneNumberInput));
    phoneNumberInput.Clear();
    phoneNumberInput.SendKeys(PhoneNumber);

    var buildingInput = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.buildingInput));
    buildingInput.Clear();
    buildingInput.SendKeys(Building);

    var colonyInput = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.colonyInput));
    colonyInput.Clear();
    colonyInput.SendKeys(Colony);

    var addressInput = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.addressInput));
    addressInput.Clear();
    addressInput.SendKeys(Address);

    var regionDrpDwnBtn = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.regionInput));
    regionDrpDwnBtn.Click();
    Thread.Sleep(2000);
    ArrowDown();

    var cityDrpDwnBtn = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.cityInput));
    cityDrpDwnBtn.Click();
    ArrowDown();

    var areaDrpDwn = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.areaInput));
    areaDrpDwn.Click();
    ArrowDown();

    var homeButton = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.homeButton));
    homeButton.Click();

    var saveButton = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.saveButton));
    saveButton.Click();

    var proceedToPayButton = _wait.Until(ExpectedConditions.ElementIsVisible(DarazLocators.proceedToPayButton));
    proceedToPayButton.Click();

    }

    public void ArrowDown()
        {
            new Actions(_driver)
        .Pause(TimeSpan.FromMilliseconds(500))
        .SendKeys(Keys.ArrowDown)
        .Pause(TimeSpan.FromMilliseconds(200))
        .SendKeys(Keys.Enter)
        .Perform();
        }

    
    }
}
