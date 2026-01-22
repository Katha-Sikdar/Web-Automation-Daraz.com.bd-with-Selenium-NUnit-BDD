
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
         
         }
      }