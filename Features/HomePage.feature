          Feature: Daraz HomePage Workflow

            @homepage
            Scenario: HomePage - Workflow from language change to checkout
              Given I navigate to Daraz website
              Then Verify the home page is displayed
              Then Click on the Language Selection Dropdown and Select "Bangla" from the language options
              Then Change the site language back from "Bangla" to "English" and verify that the language is updated.

            
