Feature: Daraz Full Purchase Workflow

  @EndToEnd
  Scenario: Complete purchase flow from language change to checkout
    Given I navigate to Daraz website
  Then Verify the home page is displayed
  And I change language From "English" to "Bangla" and verify
    # And I change language back to "English" and verify
    # When I register a new account
    # And I log in with my credentials
    # And I select category "Electronics" and add a product to cart
    # And I select category "Fashion" and add another product to cart
    # Then I proceed to checkout
    # And I place the order