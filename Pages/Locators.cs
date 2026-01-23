
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
            }
         }