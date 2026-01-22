Feature: Daraz Full Purchase Workflow

  @EndToEnd
  Scenario: A Complete purchase flow from language change to checkout
    Given Daraz website navigation
    Then Verify the home page is displayed or not
    And Change language From "English" to "Bangla" and verify
    Then Change the site language back from "Bangla" to "English" and verify that the language is updated
