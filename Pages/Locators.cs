using OpenQA.Selenium;


namespace Daraz.Automation.BDD.Pages
{
   public static class DarazLocators
   {

<<<<<<< HEAD
=======
        public static readonly By DarazLogo = By.XPath("//img[@alt='Daraz']");
        public static readonly By MainSearchBox = By.Id("q");       
        
        // Language
        public static readonly By LangToggle = By.XPath("//div[@class=\"top-links-item white\"][3]");
        public static readonly By BanglaOption = By.XPath("//div[@data-lang=\"bn\"]");
        public static readonly By EnglishOption = By.XPath("//div[@data-lang=\"en\"]");
        public static readonly By WelcomeMsg = By.Id("topActionCustomText");
>>>>>>> 0021c10 ( modified on Change language from english to bangla)

   // Home Page (Logo and Search Box)
       public static readonly By DarazLogo = By.XPath("//img[@alt='Daraz']");
       public static readonly By MainSearchBox = By.Id("q");      
      
       // Language
       public static readonly By LangToggle = By.XPath("//div[@class=\"top-links-item white\"][3]");
       public static readonly By BanglaOption = By.XPath("//div[@data-lang=\"bn\"]");
       public static readonly By EnglishOption = By.XPath("//div[@data-lang=\"en\"]");
       public static readonly By WelcomeMsg = By.Id("topActionCustomText");



   }
}