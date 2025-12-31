# Lab 2: Advanced Copilot Features in Action

**Duration:** 30 minutes  
**Difficulty:** Intermediate

## Lab Objectives

In this lab, you will:
1. Use Copilot Chat for advanced code tasks
2. Perform code refactoring with Copilot assistance
3. Optimize code for performance and readability
4. Use Copilot for design feedback
5. Generate comprehensive documentation
6. Understand and work around Copilot limitations

## Prerequisites

- Completion of Module 1 and Lab 1
- GitHub Copilot and Copilot Chat enabled
- Visual Studio or VS Code with C# support
- .NET SDK 6.0 or higher installed
- Sample project files (provided in starter-code/)

## Lab Structure

### Exercise 1: Advanced Chat Commands (5 minutes)

**Objective:** Master Copilot Chat slash commands with C# examples

#### Task 1: Using /explain
1. Open `starter-code/ComplexAlgorithm.cs`
2. Select the `FindOptimalPath` method
3. In Copilot Chat, type: `/explain this method`
4. Review the explanation

**Example File:**
```csharp
public class PathFinder
{
  public List<int> FindOptimalPath(int[,] grid, int start, int end)
  {
    // Complex pathfinding logic here
  }
}
```

#### Task 2: Using /fix
1. Open `starter-code/BuggyCode.cs`
2. Try to build the code (it has bugs)
3. Select the problematic method
4. Type: `/fix this code`
5. Apply the suggested fixes and verify with `dotnet build`

#### Task 3: Using /tests
1. Open `starter-code/UserService.cs`
2. Select the `CreateUser` method
3. Type: `/tests for this method including edge cases using xUnit`
4. Save the generated tests to `UserServiceTests.cs`

#### Task 4: Using /doc
1. Open `starter-code/ApiHandler.cs`
2. Select a method without XML documentation
3. Type: `/doc with C# XML documentation format`
4. Add the generated documentation

**Deliverable:** Document your findings in `exercise1-chat-commands.md`

---

### Exercise 2: Code Refactoring (10 minutes)

**Objective:** Improve code quality using Copilot

#### Part A: Simplify Complex Methods

1. Open `starter-code/LegacyCode.cs`
2. Find the `ProcessUserData` method (it's overly complex)
3. Prompt Copilot Chat:
```
Refactor this method to:
- Use modern C# (nullable reference types, records)
- Reduce nesting depth
- Improve readability with LINQ
- Add proper exception handling
- Use async/await instead of synchronous calls
```

**Before Example:**
```csharp
public void ProcessUserData(List<User> users)
{
  foreach (var user in users)
  {
    if (user != null)
    {
      if (user.IsActive)
      {
        if (user.Email.Contains("@"))
        {
          // Process...
        }
      }
    }
  }
}
```

4. Compare original with refactored version
5. Verify with: `dotnet build` and run unit tests

#### Part B: Extract Methods (Single Responsibility)

1. Look at the `HandleCheckout` method
2. It violates Single Responsibility Principle
3. Prompt Copilot:
```
This method does multiple things (validates, calculates, saves, sends email)
Extract each responsibility into separate methods:
- ValidateCheckout()
- CalculateTotal()
- SaveOrder()
- SendConfirmationEmail()
Keep the main method as an orchestrator using async/await
```

**After Example:**
```csharp
public async Task ProcessCheckoutAsync(Order order)
{
  ValidateCheckout(order);
  var total = CalculateTotalAsync(order);
  await SaveOrderAsync(order);
  await SendConfirmationEmailAsync(order);
}
```

#### Part C: Apply Design Patterns

1. Open `starter-code/NotificationSystem.cs`
2. Prompt Copilot:
```
Refactor this notification system using the Strategy pattern:
- Support Email, SMS, and Push notifications
- Use interface INotificationStrategy
- Make it easy to add new notification types
- Use dependency injection with IServiceProvider
```

**Deliverable:** Save refactored code to `exercise2-refactored-code.cs`

**Verification Steps:**
```bash
dotnet build
dotnet test
```

---

### Exercise 3: Performance Optimization (7 minutes)

**Objective:** Optimize code for better performance

#### Task 1: Algorithm Optimization

1. Open `starter-code/SlowSearch.cs`
2. Current implementation uses nested loops (O(n²))
3. Prompt Copilot:
```
Optimize this search method:
- Current: O(n²) time complexity using nested loops
- Target: O(n) or O(n log n)
- Use Dictionary or HashSet for lookups
- Handle null/empty edge cases
```

**Before:**
```csharp
public bool UserExists(List<User> users, string email)
{
  foreach (var user in users)
  {
    if (user.Email == email) return true;
  }
  return false;
}
```

**After:**
```csharp
public bool UserExists(HashSet<string> userEmails, string email)
{
  return userEmails.Contains(email); // O(1) lookup
}
```

#### Task 2: Memory Optimization

1. Open `starter-code/FileProcessor.cs`
2. Current code loads entire file into memory
3. Prompt Copilot:
```
Refactor to use StreamReader for processing large files:
- Read file line by line
- Process without loading entire file into memory
- Handle files larger than available RAM
- Add proper using statements
```

**Verification:**
```bash
dotnet build
dotnet run
```

#### Task 3: Database Query Optimization

1. Open `starter-code/DatabaseQueries.cs`
2. Contains N+1 query problem with Entity Framework
3. Prompt Copilot:
```
Optimize these LINQ queries:
- Eliminate N+1 query problem using Include()
- Use eager loading for related entities
- Reduce database roundtrips
- Comment potential indexes needed
```

**Before:**
```csharp
foreach (var user in context.Users)
{
  var orders = context.Orders.Where(o => o.UserId == user.Id).ToList();
}
```

**After:**
```csharp
var users = context.Users.Include(u => u.Orders).ToList();
```

**Deliverable:** Create `exercise3-optimizations.md` documenting:
- Original time/space complexity
- Optimized complexity
- Measured performance improvements

---

### Exercise 4: Design Feedback (5 minutes)

**Objective:** Get architectural and design suggestions

#### Task 1: Architecture Review

1. Open `starter-code/MonolithicService.cs`
2. Prompt Copilot Chat:
```
Review this code architecture:
- Identify tight coupling issues
- Suggest separation of concerns improvements
- Recommend dependency injection patterns
- Propose interface extraction for testability
```

#### Task 2: SOLID Principles Check

1. Use the same file
2. Prompt:
```
Analyze this code against SOLID principles:
- Single Responsibility violations
- Open/Closed Principle opportunities
- Liskov Substitution issues
- Interface Segregation needs
- Dependency Inversion improvements
Provide specific refactoring suggestions
```

#### Task 3: Code Smell Detection

1. Open `starter-code/SmellyCode.cs`
2. Prompt:
```
Identify code smells in this C# file:
- Long methods (should be <20 lines)
- Duplicated code blocks
- Large classes (God Objects)
- Feature envy (excessive dependencies)
Suggest refactoring strategies with examples
```

**Deliverable:** Create `exercise4-design-review.md` with findings and recommendations

---

### Exercise 5: Documentation Generation (5 minutes)

**Objective:** Generate comprehensive C# XML documentation

#### Task 1: Method Documentation

1. Open `starter-code/UndocumentedUtils.cs`
2. For each method, prompt:
```
/doc generate comprehensive C# XML documentation including:
- <summary> description
- <param> for each parameter with type and description
- <returns> with return type
- <example> with code example
- <exception> for thrown exceptions
- <remarks> for important notes
```

**Example:**
```csharp
/// <summary>
/// Validates user email format.
/// </summary>
/// <param name="email">The email address to validate.</param>
/// <returns>True if email is valid; otherwise, false.</returns>
/// <exception cref="ArgumentNullException">Thrown when email is null.</exception>
public bool ValidateEmail(string email) { }
```

#### Task 2: Class Documentation

1. Open `starter-code/UserManager.cs`
2. Prompt:
```
Generate complete class XML documentation:
- <summary> class purpose
- <remarks> design notes
- Document constructor parameters
- Document public methods with full summaries
- Include <example> with usage
```

#### Task 3: API Documentation

1. Open `starter-code/ApiController.cs`
2. Prompt:
```
Generate API endpoint documentation:
- HTTP method and route
- Request parameters and body
- Response format with schema
- Status codes (200, 400, 404, 500)
- Example requests/responses
- Authorization requirements
```

**Deliverable:** Create fully documented versions; verify with `dotnet build`

---

### Exercise 6: Understanding Limitations (3 minutes)

**Objective:** Recognize when Copilot suggestions need verification

#### Task 1: Identify Security Issues

Review these Copilot suggestions and identify problems:

1. **Suggestion 1 - SQL Injection:**
```csharp
public User AuthenticateUser(string username, string password)
{
  string query = $"SELECT * FROM Users WHERE Username = '{username}' 
           AND Password = '{password}'";
  return context.Users.FromSqlRaw(query).FirstOrDefault();
}
```
**Question:** What's the SQL injection vulnerability? How do you fix it?

2. **Suggestion 2 - Weak Password Hashing:**
```csharp
public string HashPassword(string password)
{
  using (var sha256 = System.Security.Cryptography.SHA256.Create())
  {
    var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
    return Convert.ToBase64String(hash);
  }
}
```
**Question:** Why is this insecure? What should you use instead?

3. **Suggestion 3 - Missing Error Handling:**
```csharp
public async Task<User> FetchUserAsync(int id)
{
  var response = await httpClient.GetAsync($"/api/users/{id}");
  return JsonSerializer.Deserialize<User>(await response.Content.ReadAsStringAsync());
}
```
**Question:** What error handling is missing? What exceptions could occur?

**Deliverable:** Create `exercise6-limitations.md` documenting issues and fixes:
- Use parameterized queries (SqlParameter)
- Use bcrypt or PBKDF2 for hashing
- Add try-catch, null checks, and status code validation

---

## Bonus Challenges

### Challenge 1: Complete Service Refactoring

1. Take `starter-code/LegacyService.cs`
2. Refactor completely:
   - Apply modern C# patterns (records, nullable types)
   - Add comprehensive error handling
   - Optimize performance with LINQ
   - Extract interfaces for testability
   - Add full XML documentation
3. Run: `dotnet build && dotnet test`
4. Document changes in `REFACTORING_NOTES.md`

### Challenge 2: Documentation Generation

Generate a complete documentation site:
- README.md
- API.md (with OpenAPI format)
- ARCHITECTURE.md
- CONTRIBUTING.md
- CHANGELOG.md

**Verification:**
```bash
dotnet build /p:DocumentationFile=bin/Debug/net6.0/App.xml
```

### Challenge 3: Performance Benchmarking

1. Create benchmarks using BenchmarkDotNet:
```bash
dotnet add package BenchmarkDotNet
```
2. Compare original vs optimized methods
3. Document actual performance improvements with metrics

**Run Benchmarks:**
```bash
dotnet run -c Release
```

---

## Lab Completion Checklist

- [ ] Completed Exercise 1: Chat commands with C# examples
- [ ] Completed Exercise 2: Refactoring and verified with `dotnet build`
- [ ] Completed Exercise 3: Optimization and measured improvements
- [ ] Completed Exercise 4: Design feedback documented
- [ ] Completed Exercise 5: Full XML documentation added
- [ ] Completed Exercise 6: Security issues identified
- [ ] All code compiles: `dotnet build` succeeds
- [ ] All tests pass: `dotnet test` succeeds
- [ ] Attempted at least one bonus challenge

## Verification Steps for All Exercises

```bash
# Build the project
dotnet build

# Run unit tests
dotnet test

# Check for code analysis warnings
dotnet build /p:EnableNETAnalyzers=true

# Generate documentation file
dotnet build /p:DocumentationFile=bin/Debug/net6.0/Lab2.xml
```

## Key Takeaways

1. **Slash commands accelerate tasks:** Use /explain, /fix, /tests, /doc for C# code
2. **Always verify with dotnet build:** Compilation catches many issues
3. **Security must be verified manually:** Especially for authentication/database code
4. **SOLID principles guide refactoring:** Copilot helps implement them
5. **Documentation is enforced:** C# XML docs improve IntelliSense and code clarity

## Troubleshooting

**Build errors:**
- Verify .NET SDK: `dotnet --version`
- Check target framework in .csproj file
- Restore packages: `dotnet restore`

**Poor Copilot suggestions:**
- Add domain context in prompts
- Reference similar code patterns in your codebase
- Be explicit about C# version (6.0, 7.0, etc.)

**Tests failing:**
- Review Copilot-generated test assertions
- Verify mocking setup matches your code
- Check for side effects in methods being tested

## Next Module

When ready, proceed to [Module 3: GitHub Copilot Across Environments](../../Module-03-Across-Environments/)