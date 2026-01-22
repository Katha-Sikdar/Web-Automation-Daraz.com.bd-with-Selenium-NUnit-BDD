    Feature: Daraz SignUp Workflow

      @EndToEnd
      Scenario: Register a new user account on Daraz website
        Given I navigate to Daraz website
        Then Verify the home page is displayed
        Then Click on the "SignUp" button
