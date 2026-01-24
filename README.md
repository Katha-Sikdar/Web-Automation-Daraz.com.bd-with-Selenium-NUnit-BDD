# Web Automation on Daraz – End-to-End Purchase Flow
Using Selenium WebDriver with C#, NUnit, BDD Framework(Specflow)

## What is Automation?
In simple terms, Automation is the use of technology to perform tasks with reduced human intervention. In the world of Software Quality Assurance (SQA), it specifically refers to using software tools to execute tests, compare actual outcomes with predicted outcomes, and report the results.

## Importance of Automation - 
As an SQA engineer, you know that software moves fast. Manual testing alone often can't keep up with modern development cycles (like Agile or DevOps). Here is why automation is essential:
  - Speed and Efficiency
  - High Accuracy and Consistency
  - Better Test Coverage
  - Regression Testing
  - Cost-Effectiveness (In the Long Run)
  - Faster Feedback for Developers

## Why Selenium with C# NUnit, BDD Framework (Gherkin/Specflow)?
  - Selenium WebDriver: An open source tool for automating web browsers. It simulates real user interactions clicking, typing, and navigating to validate UI behavior.
  - NUnit: A powerful .NET unit-testing framework that provides
      - structure through annotations (Setup/Teardown)
      - handles assertions
      - supports parallel execution for faster results
      - easy integration with reporting tools (Allure)
  This combination is ideal for building scalable and maintainable web automation frameworks.

## Technologies Used
  - Selenium WebDriver
  - C# (.NET)
  - NUnit Framework
  - BDD Framework: SpecFlow
  - Visual Studio
  - ChromeDriver
  - Allure Report

## How to Run This Project
  - Clone the repository
  - git clone
  - Open the solution
  - Open the .sln file in Visual Studio
  - Restore dependencies
  - NuGet packages will restore automatically (or use dotnet restore)
  - Run tests From Visual Studio Test Explorer OR dotnet Test OR can run using "tags"
    - dotnet test --filter "TestCategory=homepage"
    - dotnet test --filter "TestCategory=SignUp"
    - dotnet test --filter "TestCategory=login"
    - dotnet test --filter "TestCategory=productpurchase"
      
## Test Scenario
  - Website: https://www.daraz.com.bd/

## Automation Featured Test Flow 
  - Localization Testing (Language Switch)
      - Given I navigate to Daraz website
      - Then Verify the home page is displayed
      - Then Click on the Language Selection Dropdown and Select "Bangla" from the language options
      - Then Change the site language back from "Bangla" to "English" and verify that the language is updated.
        
  - Authentication Flow -
      - Given I navigate to Daraz website
      - Then Verify the home page is displayed
      - Then Click on the "SignUp" button and verify that the SignUp window is opened.
      - And Fill in the registration form with valid Phone Number
      - And Check the terms and conditions checkbox
      - Then Submit the registration form
        
 - Authorization Flow
      -  Given I navigate to Daraz website
      -  Then Click on the "Login" button and verify that the Login window is opened.
      -  And Fill in the Login form with valid Email Address and Password
      -  Then Submit the Login form and verify successful login by checking
        
 - Shopping, Cart Management and Checkout
      -  Given I navigate to Daraz website
      -  Then Click on the "Login" button and verify that the Login window is opened.
      -  And Fill in the Login form with valid Email Address and Password
      -  Then Submit the Login form and verify successful login by checking
      -  Then click on the Cart icon and verify that the Cart page is opened
      -  Then Click on the Category Dropdown "First" and select a product and add to Cart
      -  And Click on the Category Dropdown "Second" and select a product and add to Cart
      -  Then Proceed to Checkout from the Cart page
      -  Then Fill in the Delivery Information form with valid details and Proceed To Pay
  
## Test Case Checklist 
  - Verify Daraz homepage loads successfully
  - Verify language can be changed from English to Bangla
  - Verify language can be changed back from Bangla to English
  - Verify user registration works with valid information
  - Verify login works with registered credentials
  - Verify user can select a product category
  - Verify user can add a product to the cart
  - Verify user can add products from multiple categories
  - Verify cart contains selected products
  - Verify user can proceed to checkout
  - Verify user can proceed to buy/place order

## Project Structure 
```
├── Data                # External test data (JSON/Excel)
├── Features            # Gherkin .feature files (User Stories)
├── Hooks               # Setup and Teardown logic
├── Pages               # Page Objects (Locators and Methods)
├── Reader              # Configuration and file readers
├── ScreenShot          # Failure screenshots
├── Steps               # Step Definition implementations
├── allure-report       # Generated test execution reports
├── Daraz.Automation.BDD.csproj
└── specflow.json       # SpecFlow configuration

```

## Screenshot  - 
<img width="1470" height="956" alt="Screenshot 2026-01-24 at 18 02 31" src="https://github.com/user-attachments/assets/dfb95738-4fa6-4b6a-a4f0-5d75f81a02f3" />
<img width="1470" height="956" alt="Screenshot 2026-01-24 at 18 02 17" src="https://github.com/user-attachments/assets/4bccc33f-2feb-45b3-bb85-c4928ba1d4c0" />

## Video Recording - 







  
