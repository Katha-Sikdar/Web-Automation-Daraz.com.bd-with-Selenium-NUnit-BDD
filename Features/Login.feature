    Feature: Daraz Login Workflow with a Registered User

      @SignUp
      Scenario: Login on Daraz website with a registered user account
        Given I navigate to Daraz website
        #Then Verify the home page is displayed
        #Then Click on the "SignUp" button and verify that the SignUp window is opened.
        Then Click on the "Login" button and verify that the Login window is opened.
        And Fill in the Login form with valid Phone Number
        Then Submit the Login form