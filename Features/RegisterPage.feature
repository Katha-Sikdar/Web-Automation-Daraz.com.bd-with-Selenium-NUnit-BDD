        Feature: Daraz SignUp Workflow

          @SignUp
          Scenario: Register a new user account on Daraz website
            Given I navigate to Daraz website
            Then Verify the home page is displayed
            Then Click on the "SignUp" button and verify that the SignUp window is opened.
            And Fill in the registration form with valid Phone Number
            And Check the terms and conditions checkbox
           Then Submit the registration form
