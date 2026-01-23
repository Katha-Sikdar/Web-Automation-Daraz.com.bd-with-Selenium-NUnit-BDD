    Feature: Product Purchase Workflow with a Registered User

      @productpurchase
      Scenario: Login on Daraz website with a registered user account and purchase a product
        Given I navigate to Daraz website
        Then Click on the "Login" button and verify that the Login window is opened.
        And Fill in the Login form with valid Email Address and Password
        Then Submit the Login form and verify successful login by checking 
        Then click on the Cart icon and verify that the Cart page is opened
        Then Click on the Category Dropdown "First" and select a product and add to Cart
        And Click on the Category Dropdown "Second" and select a product and add to Cart
        Then Proceed to Checkout from the Cart page
        Then Fill in the Delivery Information form with valid details and Proceed To Pay
        # And Fill in the Shipping Information form with valid details
        # Then Select a Payment Method and complete the purchase
        # Then Verify that the order confirmation page is displayed with order details
