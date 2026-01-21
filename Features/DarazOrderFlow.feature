Feature: Daraz Full Purchase Workflow

  @EndToEnd
  Scenario: Complete purchase flow from language change to checkout
    Given I navigate to Daraz website
     Then Verify the home page is displayed
    And I change language From "English" to "Bangla" and verify
