        Feature: Daraz Login Workflow with a Registered User

          @login
          Scenario: Login on Daraz website with a registered user account
            Given I navigate to Daraz website
            Then Click on the "Login" button and verify that the Login window is opened.
            And Fill in the Login form with valid Email Address and Password
            Then Submit the Login form and verify successful login by checking 