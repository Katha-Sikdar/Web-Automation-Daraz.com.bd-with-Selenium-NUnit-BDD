using TechTalk.SpecFlow;
using Daraz.Automation.BDD.Hooks;
using Daraz.Automation.BDD.Pages;
using System;

namespace Daraz.Automation.BDD.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly LoginPage _loginPage;

        public LoginSteps()
        {
           
            _loginPage = new LoginPage(Hook.driver!);
        }

        [Then(@"Click on the ""(.*)"" button and verify that the Login window is opened\.")]
        public void ClickLoginAndVerify_(string login)
        {
            _loginPage.ClickLogin();
            Thread.Sleep(2000);
            _loginPage.VerifyLoginWindowOpened();
        }

        [Then(@"Fill in the Login form with valid Email Address and Password")]
        public void LoginFormWithValidEmailAndPassword()
        {

            string email = Utils.JsonReader.GetTestData("email");
            string password = Utils.JsonReader.GetTestData("password");
            _loginPage.EnterLoginCredentials(email, password);
        }

        [Then(@"Submit the Login form and verify successful login by checking")]
        public void SubmitTheLoginForm()
        {
            _loginPage.SubmitLogin();
            _loginPage.loginAssertion();
        }
    }
}