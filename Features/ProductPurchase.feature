    Feature: Product Purchase Workflow with a Registered User

      @productpurchase
      Scenario: Login on Daraz website with a registered user account and purchase a product
        Given I navigate to Daraz website
        Then Click on the "Login" button and verify that the Login window is opened.
        And Fill in the Login form with valid Email Address and Password
        Then Submit the Login form and verify successful login by checking 
        Then click on the Cart icon and verify that the Cart page is opened
        Then Click on the Category Dropdown and select a product category and added item to the Cart