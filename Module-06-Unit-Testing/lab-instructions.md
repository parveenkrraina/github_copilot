# Lab 6: Test Generation Workshop

**Duration:** 30 minutes

## Objectives
- Master test generation with Copilot for C# and .NET Core
- Create comprehensive test suites with xUnit
- Identify edge cases systematically
- Use Selenium for E2E tests

## Exercise 1: Unit Test Generation (10 min)

**Step 1:** Open your .NET Core project in Visual Studio or VS Code
- Identify a function to test (e.g., `CalculateDiscount()`, `ValidateEmail()`)

**Step 2:** Use Copilot's `/tests` command
- Highlight the function
- Open Copilot Chat and type `/tests`
- Review the generated xUnit test structure

**Step 3:** Add edge cases manually
- Create a new test file (e.g., `CalculateDiscountTests.cs`)
- Add tests for: empty inputs, null values, boundary conditions
- Example: If testing `CalculateDiscount(decimal price, decimal percentage)`, test with 0, negative numbers, large values

**Step 4:** Include negative scenarios
- Prompt Copilot: "Add xUnit tests for invalid inputs and error handling in C# .NET Core"
- Verify tests use `Assert.Throws<ArgumentException>`

**Step 5:** Verify coverage
- Run `dotnet test /p:CollectCoverage=true`
- Ensure the function has 80%+ line coverage
- Add missing test cases if needed

---

## Exercise 2: Test Data Builders (5 min)

**Step 1:** Create a `Builders.cs` file in your test directory

**Step 2:** Generate User builder
- Prompt: "Create a User builder class in C# .NET Core with fluent API that generates realistic test data with Id, Username, Email, Status"
- Use Copilot to generate the builder pattern
- Include sensible defaults and chainable methods

**Step 3:** Generate Order and Product builders
- Repeat Step 2 for OrderBuilder (Id, UserId, Items, Total, CreatedAt)
- Repeat Step 2 for ProductBuilder (Id, Name, Price, Stock)

**Step 4:** Test the builders
- Create sample data: `var user = new UserBuilder().Build(); var order = new OrderBuilder().WithUserId(user.Id).Build();`
- Verify data is consistent and realistic

---

## Exercise 3: Integration Tests (5 min)

**Step 1:** Create `IntegrationTests.cs` file

**Step 2:** Generate API endpoint tests
- Prompt: "Generate xUnit tests for GET /users endpoint using HttpClient in C# .NET Core that test success and error cases"
- Include tests for: 200 response, 404 not found, 500 server error

**Step 3:** Add database interaction tests
- Prompt: "Create xUnit tests in C# .NET Core that verify database calls work correctly with test builders using Entity Framework Core"
- Test insert, update, delete operations

**Step 4:** Add authentication tests
- Prompt: "Generate xUnit tests in C# .NET Core for protected endpoints requiring Bearer token authentication"
- Test: valid token, expired token, missing token scenarios

---

## Exercise 4: Selenium E2E Tests (10 min)

**Step 1:** Install Selenium
```bash
dotnet add package Selenium.WebDriver
dotnet add package Selenium.WebDriver.ChromeDriver
```

**Step 2:** Create `E2ETests.cs` file

**Step 3:** Generate login flow test
- Prompt: "Create a Selenium test in C# .NET Core for user login flow: navigate to login page, enter credentials, verify redirect to dashboard"
- Run: `dotnet test E2ETests.cs`

**Step 4:** Generate shopping cart flow test
- Prompt: "Create a Selenium test in C# .NET Core for adding products to cart and verifying cart updates"

**Step 5:** Generate checkout process test
- Prompt: "Create a Selenium test in C# .NET Core for complete checkout: add item, go to cart, enter shipping, enter payment, confirm order"

**Step 6:** Create page objects
- Prompt: "Generate a LoginPage class in C# .NET Core with element selectors and methods for username input, password input, and login button"
- Generate CartPage and CheckoutPage similarly
- Refactor tests to use page objects for maintainability

---

**Deliverables:**
- Unit test file with 80%+ coverage using xUnit
- `Builders.cs` with User, Order, Product builders
- `IntegrationTests.cs` with API and database tests
- `E2ETests.cs` with 3+ Selenium scenarios using page objects

## Key Takeaways
- `/tests` command accelerates testing in C# .NET Core
- Always include edge cases and error scenarios
- Test data builders improve maintainability and readability
- E2E tests validate complete user workflows
- Page objects make E2E tests easier to maintain
