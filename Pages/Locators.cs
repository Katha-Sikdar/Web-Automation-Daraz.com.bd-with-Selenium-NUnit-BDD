
         using OpenQA.Selenium;

         namespace Daraz.Automation.BDD.Pages
         {
            public static class DarazLocators
            {

   //Homepage Locators
               public static By languageSelectDrpdwn=By.XPath("//div[@id='topActionSwitchLang']");
               public static readonly By BanglaOption = By.XPath("//div[@data-lang=\"bn\"]");
               public static readonly By EnglishOption = By.XPath("//div[@data-lang=\"en\"]");
               public static readonly By LangToggle = By.XPath("//div[@class=\"top-links-item white\"][3]");
               public static readonly By MainSearchBox = By.Id("q");

               //SignUp Part Locators
               public static By signUpButton = By.XPath("//a[text()='Sign Up']");
               public static By modalCheckBox = By.XPath("//label[contains(@class,'iweb-checkbox-lazada')]");

               public static By mobileInput = By.XPath("//input[@placeholder='Please enter your phone number']");
               public static By termsAndConditionsCheckbox = By.XPath("//label[contains(@class,'iweb-checkbox-lazada')]");
               public static By submitButton = By.XPath("//button[@type='button']");

               //Login Part Locators
               public static By loginButton = By.XPath("//a[text()='Login']");
               public static By inputEmail = By.XPath("//input[@placeholder='Please enter your Phone or Email']");
               public static By inputPassword = By.XPath("//input[@placeholder='Please enter your password']");
               public static By btnSubmit = By.XPath("//button[@type='button']");
               public static By profileId = By.XPath("//span[@id='myAccountTrigger']");
               //public static readonly By profileXPath = By.XPath($"//*[contains(text(), '{expectedName}')]"); 
               public static readonly By loginWindowAssertion = By.XPath("//button[@type='button' and text()='LOGIN']");

               //product purchase locators
               public static readonly By cartIcon = By.XPath("//span[@class='cart-icon-daraz']");
               public static readonly By clickCategoryDrpDwn = By.XPath("//span[text()='Categories']");
               public static readonly By categoryOne = By.XPath("//span[text()='TV & Home Appliances']");
               public static readonly By subCategoryOne = By.XPath("//span[text()='Kitchen Appliances']");
               public static readonly By firstItem = By.XPath("//span[text()='Singer Deep Freezer']");
               public static readonly By firstItemAddToCart = By.XPath("//a[@title='Singer Chest Freezer']");
              // public static readonly By selectFirstItem = By.XPath("(//div[@data-qa-locator='general-products']//div[@data-qa-locator='product-item'])[1]");
               public static readonly By categoryTwo = By.XPath("(//a[contains(@class,'card-categories-li')])[2]");
               public static readonly By btnAddtoCart = By.XPath("//button//span[text()='Add to Cart']");
      //   IWebElement CartMessage => driver.FindElement(By.XPath("//span[@class='cart-message-text']"));
      //   IWebElement btnCross => driver.FindElement(By.XPath("//i[@class='next-icon next-icon-close next-icon-small']"));


                     }
                             
    }