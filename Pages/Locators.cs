using OpenQA.Selenium;

namespace Daraz.Automation.BDD.Pages
{
    public static class DarazLocators
    {
        // Language
        public static readonly By LangToggle = By.Id("topActionLangSwitcher");
        public static readonly By BanglaOption = By.XPath("//span[contains(text(),'BN')]");
        public static readonly By EnglishOption = By.XPath("//span[contains(text(),'EN')]");
        public static readonly By WelcomeMsg = By.Id("topActionCustomText"); // Used to verify lang change

        // Auth
        public static readonly By SignupLink = By.XPath("//a[text()='Signup / Member Login']");
        public static readonly By RegisterTab = By.ClassName("login-title");
        public static readonly By LoginEmailInput = By.XPath("//input[@type='text']");
        public static readonly By LoginPassInput = By.XPath("//input[@type='password']");
        public static readonly By LoginBtn = By.XPath("//button[contains(@class, 'next-btn-primary')]");

        // Categories & Products
        public static readonly By CategoryMenu = By.Id("q"); // Just a placeholder, adjust to Daraz Sidebar
        public static readonly By FirstCategory = By.XPath("//li[@id='Level_1_Category_No1']");
        public static readonly By SecondCategory = By.XPath("//li[@id='Level_1_Category_No2']");
        public static readonly By AddToCartBtn = By.XPath("//span[text()='Add to Cart']");
        
        // Checkout
        public static readonly By CartIcon = By.ClassName("cart-icon");
        public static readonly By CheckoutBtn = By.XPath("//button[text()='PROCEED TO CHECKOUT']");
        public static readonly By PlaceOrderBtn = By.XPath("//button[text()='Place Order']");
    }
}