                using TechTalk.SpecFlow;
                using Daraz.Automation.BDD.Hooks;
                using Daraz.Automation.BDD.Pages;
                using Daraz.Automation.BDD.Utils;
                using System.Runtime.CompilerServices;

            namespace Daraz.Automation.BDD.Steps
                {
                    [Binding]
                    public class RegistrationPageSteps
                    {
                    private readonly RegistrationPage _registrationPage;

                        public RegistrationPageSteps()
                        {

                            _registrationPage = new RegistrationPage(Hook.driver!);
                        }

                [Then(@"Click on the ""(.*)"" button and verify that the SignUp window is opened\.?")]
                        public void ThenClickSignUpAndVerify(string buttonName)
                        {
                            _registrationPage.ClickSignUp();
                            Thread.Sleep(2000);
                            _registrationPage.VerifySignUpWindowOpened();
                            Thread.Sleep(2000);
                        }

                [Then(@"Fill in the registration form with valid Phone Number")]
                public void ThenFillInTheRegistrationForm()
                {

                    string mobileData = Utils.JsonReader.GetTestData("phoneNumber");
                    _registrationPage.EnterMobileNumber(mobileData);
                }

                [Then(@"Check the terms and conditions checkbox")]
                public void AndCheckTheTermsAndConditionsCheckbox()
                {
                    _registrationPage.CheckTermsAndConditions();
                    Thread.Sleep(1000);
                }

                [Then(@"Submit the registration form")]
            public void ThenSubmitTheRegistrationForm()
            {       
                _registrationPage.ClickSubmit();
                Thread.Sleep(3000);
                _registrationPage.HandleUnusualTrafficSlider();
            }

                    }
                }