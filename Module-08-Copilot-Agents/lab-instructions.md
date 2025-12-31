# Lab 8: Agent-Based Development Workflow (C# Version)

**Duration:** 30 minutes

## Objectives
- Use Copilot in different agent roles
- Apply multi-agent collaboration
- Improve code through agent workflows
- Practice agent-to-agent thinking

## Exercise 1: Developer Agent (5 min)
As a developer, implement a feature: 
```
"As a developer, implement a user registration system in C#
with email validation and password hashing using BCrypt or 
ASP.NET Core Identity.  Include input validation and error handling."
```

## Exercise 2: Reviewer Agent (5 min)
As a reviewer, critique the code:
```
"As a code reviewer, analyze this C# registration code for:
- Security issues
- Best practices
- Potential bugs
- Improvements
- SOLID principles adherence"
```

## Exercise 3: Developer Response (5 min)
As a developer, address feedback:
```
"As a C# developer, address these review comments: 
[paste feedback]
Follow . NET coding standards and best practices."
```

## Exercise 4: QA Agent (5 min)
As QA, create test suite:
```
"As a QA engineer, create comprehensive xUnit or NUnit tests 
for this C# registration system including edge cases, 
security scenarios, and integration tests."
```

## Exercise 5: Complete Workflow (10 min)
Implement a feature using all agents:
1. Developer: Implement password reset in C#
2. Reviewer: Review implementation
3. Developer: Fix issues
4. QA: Create xUnit/NUnit tests
5. Security: Security audit
6. Documentation: Create XML docs

**Deliverables:**
- Code from each agent perspective
- Documentation of agent interactions
- Final refined implementation
- Complete test suite with xUnit/NUnit

## Key Takeaways
- Different roles provide different insights
- Multi-agent review catches more issues
- Iteration improves quality
- Agent workflows mirror real processes

## Step-by-Step Guide

### Exercise 1: Developer Agent
1. Open Copilot Chat in your IDE (Visual Studio or VS Code)
2. Copy the developer prompt into the chat window
3. Add context: "I'm building an ASP.NET Core Web API application"
4. Copilot will generate registration code with validation and hashing
5. Create a new C# class file: `UserRegistrationService.cs`
6. Also create supporting classes: 
   - `RegisterUserRequest.cs` (DTO)
   - `UserRepository.cs` (data access)
7. Test it locally: Build and run your project

**Example prompt to use:**
```
Create a C# UserRegistrationService class with:
- Email validation using regex
- Password strength validation (min 8 chars, uppercase, lowercase, number, special char)
- Password hashing using BCrypt. Net-Next
- Async methods
- Exception handling
- Dependency injection ready
```

### Exercise 2: Reviewer Agent
1. Copy your generated C# registration code into Copilot Chat
2. Paste the reviewer prompt
3. Copilot will identify: 
    - Missing input sanitization
    - Async/await best practices
    - Exception handling patterns
    - Missing null checks
    - Password policy enforcement
4. Document each finding in a `review-findings.md` file
5. Note which issues are critical vs. nice-to-have

**Example reviewer prompt:**
```
Review this C# code as a senior . NET developer:
[paste your UserRegistrationService code]

Check for:
- Async/await patterns
- Exception handling
- Null reference safety
- Security vulnerabilities
- SOLID principles
- Dependency injection
- Unit testability
```

### Exercise 3: Developer Response
1. In Copilot Chat, use `/fix` command on the code
2. Paste the review findings
3. Ask Copilot to address each issue systematically
4. Compare old vs. new implementation
5. Save the improved code to `UserRegistrationService.v2.cs`
6. Test edge cases: 
   - Null/empty fields
   - Invalid emails
   - Weak passwords
   - SQL injection in username

**Example fix prompt:**
```
Refactor this C# UserRegistrationService to address these issues:
1. Add null checks with ArgumentNullException
2. Use ILogger for logging
3. Add custom exceptions (UserAlreadyExistsException)
4. Improve async patterns
5. Add XML documentation comments
[paste your code]
```

### Exercise 4: QA Agent
1. Start a new Copilot Chat session
2. Paste the improved registration code
3. Use the QA prompt to generate tests
4. Copilot should generate xUnit tests covering:
    - Valid registration
    - Invalid emails (multiple formats)
    - Weak passwords (multiple scenarios)
    - Duplicate accounts
    - Null/empty inputs
    - SQL injection attempts
5. Save tests to `UserRegistrationServiceTests.cs`
6. Add NuGet packages: `xUnit`, `Moq`, `FluentAssertions`
7. Run: `dotnet test` to validate all tests pass

**Example QA prompt:**
```
Create comprehensive xUnit tests for this C# UserRegistrationService:
[paste your code]

Include tests for:
- Happy path registration
- Invalid email formats
- Password validation (too short, no uppercase, no numbers, etc.)
- Duplicate user detection
- Null parameter handling
- Async behavior
- Exception scenarios

Use Moq for mocking dependencies and FluentAssertions for assertions. 
```

### Exercise 5: Complete Workflow
1. Choose a feature:  **Password Reset** (recommended)
2. Follow steps 1-4 for each agent in sequence
3. Create a `workflow-log.md` documenting:
    - Developer's implementation
    - Reviewer's findings
    - Developer's fixes
    - QA's test coverage
    - Security concerns addressed
4. Final deliverable: Working C# code with 100% test coverage

**Password Reset Feature Requirements:**
- Generate secure reset token
- Send email with reset link
- Validate token (expiration, single use)
- Allow password change
- Hash new password
- Invalidate old sessions

**Example complete workflow:**

#### Step 1 - Developer Agent: 
```
As a C# developer, implement a password reset system with:
- GenerateResetTokenAsync method
- ValidateResetTokenAsync method
- ResetPasswordAsync method
- Token expiration (15 minutes)
- Secure random token generation
- Email notification
```

#### Step 2 - Reviewer Agent:
```
Review this password reset implementation for:
- Token security
- Timing attack vulnerabilities
- Token storage best practices
- Email injection risks
- Rate limiting needs
```

#### Step 3 - Developer Response:
```
Fix these issues in the password reset code:
[paste reviewer feedback]
Use cryptographically secure random for tokens.
Add rate limiting attributes.
```

#### Step 4 - QA Agent:
```
Create xUnit tests for password reset covering:
- Token generation uniqueness
- Token expiration
- Invalid token handling
- Used token rejection
- Rate limiting
- Concurrent reset attempts
```

#### Step 5 - Documentation:
```
Generate XML documentation for the password reset API including:
- Method summaries
- Parameter descriptions
- Exception documentation
- Usage examples
- Security considerations
```

## Setup Requirements

### Required NuGet Packages:
```bash
dotnet add package BCrypt.Net-Next
dotnet add package xUnit
dotnet add package Moq
dotnet add package FluentAssertions
dotnet add package Microsoft.Extensions. Logging
```

### Project Structure:
```
YourProject/
├── Services/
│   ├── UserRegistrationService.cs
│   ├── PasswordResetService.cs
├── Models/
│   ├── RegisterUserRequest.cs
│   ├── ResetPasswordRequest.cs
├── Repositories/
│   ├── IUserRepository.cs
│   ├── UserRepository.cs
├── Exceptions/
│   ├── UserAlreadyExistsException. cs
│   ├── InvalidTokenException.cs
└── Tests/
    ├── UserRegistrationServiceTests.cs
    ├── PasswordResetServiceTests.cs
```

## Tips for Success

1. **Be Specific**: When prompting Copilot, mention C# version, framework (. NET 6/7/8)
2. **Context Matters**: Include relevant using statements and dependencies
3. **Iterate**: Don't expect perfect code first time - refine through agent roles
4. **Test First**: Consider asking QA agent for test cases before implementing
5. **Document**: Use XML comments - Copilot can generate comprehensive documentation

## Troubleshooting

**Issue**:  Copilot generates Node.js code
- **Solution**: Start prompt with "In C# using . NET..."

**Issue**: Missing async/await patterns
- **Solution**:  Explicitly request "async methods returning Task<T>"

**Issue**: Tests don't compile
- **Solution**: Ask Copilot to "fix compilation errors in this xUnit test"

**Issue**: Need more modern C# features
- **Solution**:  Specify "using C# 10 features like record types and pattern matching"
