    Feature: Daraz HomePage Workflow

      @EndToEnd
      Scenario: Complete purchase flow from language change to checkout
        Given I navigate to Daraz website
        Then Verify the home page is displayed
        Then Click on the Language Selection Dropdown and Select "Bangla" from the language options
        Then Change the site language back from "Bangla" to "English" and verify that the language is updated.

      
