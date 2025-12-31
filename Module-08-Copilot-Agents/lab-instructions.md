# Lab 8: Agent-Based Development Workflow

**Duration:** 30 minutes

## Prerequisites

Before starting, ensure you have: 
- Visual Studio 2022 or VS Code with C# extension
- .NET 8 SDK installed
- GitHub Copilot extension enabled
- A new or existing ASP.NET Core Web API project

## Setup (5 minutes)

### Step 1: Create Your Project

```bash
# Create a new ASP.NET Core Web API project
dotnet new webapi -n UserManagementAPI
cd UserManagementAPI

# Create project folders
mkdir Services
mkdir Models
mkdir Repositories
mkdir Exceptions
mkdir Tests
```

### Step 2: Install Required NuGet Packages

```bash
# Main project packages
dotnet add package BCrypt.Net-Next
dotnet add package Microsoft.Extensions.Logging

# Create test project
dotnet new xunit -n UserManagementAPI.Tests
cd UserManagementAPI.Tests

# Test project packages
dotnet add package Moq
dotnet add package FluentAssertions
dotnet add reference ../UserManagementAPI.csproj

cd ..
```

### Step 3:  Verify Copilot is Active

1. Open your project in VS Code or Visual Studio
2. Open Copilot Chat (`Ctrl+Alt+I` in VS Code or `Ctrl+/` in Visual Studio)
3. Type: "Hello, are you ready?" to verify Copilot responds

---

## Lab Objectives

- Use Copilot in different agent roles
- Apply multi-agent collaboration
- Improve code through agent workflows
- Practice agent-to-agent thinking

---

## Exercise 1: Developer Agent (10 minutes)

### Goal
Implement a user registration system using Copilot as your development assistant.

### Step 1: Open Copilot Chat
- **VS Code:** Press `Ctrl+Alt+I` or click the chat icon
- **Visual Studio:** Press `Ctrl+/` or click the Copilot Chat window

### Step 2: Provide Context and Prompt

Copy this complete prompt into Copilot Chat:

```
Context: I'm building an ASP.NET Core 8.0 Web API application

Task: As a developer, implement a user registration system in C#

Requirements:
- Email validation using regex pattern
- Password strength validation (minimum 8 characters, must contain uppercase, lowercase, number, and special character)
- Password hashing using BCrypt. Net-Next
- Async methods for all operations
- Proper exception handling
- Dependency injection ready
- Check for duplicate users

Please generate: 
1. UserRegistrationService. cs - Main service class with RegisterAsync method
2. RegisterUserRequest.cs - DTO with validation
3. IUserRepository.cs - Repository interface
4. User.cs - User entity model

Use best practices for . NET 8 and include XML documentation comments.
```

### Step 3: Review Generated Code

Copilot will generate code for all four files. Review each one: 
- Check that async/await patterns are used
- Verify BCrypt is used for password hashing
- Confirm email validation regex is present
- Look for proper exception handling

### Step 4: Create Files and Add Code

1. **Create `Models/User.cs`** and paste the generated User entity code
2. **Create `Models/RegisterUserRequest.cs`** and paste the DTO code
3. **Create `Repositories/IUserRepository.cs`** and paste the interface code
4. **Create `Services/UserRegistrationService.cs`** and paste the service code

### Step 5: Build and Fix Any Errors

```bash
dotnet build
```

If there are compilation errors, ask Copilot: 
```
Fix the compilation errors in this code:
[paste the code with errors]
```

### Step 6: Document Your Work

Create a file called `exercise1-developer-notes.md` and document:
- What code was generated
- Any modifications you made
- Initial observations about code quality

**✅ Checkpoint:** You should now have 4 working C# files with a complete registration system. 

---

## Exercise 2: Reviewer Agent (10 minutes)

### Goal
Use Copilot as a code reviewer to identify issues and improvements.

### Step 1: Open a New Copilot Chat Session

Click "New Chat" or clear your previous conversation to start fresh.

### Step 2: Provide Your Code for Review

Copy this reviewer prompt into Copilot Chat: 

```
Act as a senior C# code reviewer. Review this user registration code for: 

Security Issues:
- Password handling vulnerabilities
- Injection attacks
- Data exposure risks

Best Practices:
- SOLID principles adherence
- Async/await patterns
- Exception handling patterns
- Null reference safety

Code Quality:
- Naming conventions
- Code organization
- XML documentation completeness

Potential Bugs:
- Edge cases not handled
- Race conditions
- Memory leaks

Here is the UserRegistrationService. cs code:
[paste your UserRegistrationService.cs code here]

Provide a detailed review with specific line references and severity ratings (Critical, High, Medium, Low).
```

### Step 3: Analyze the Review

Copilot will provide feedback.  Common issues it might find:
- Missing input sanitization
- Weak email validation regex
- No rate limiting
- Missing logging
- Synchronous operations in async methods
- No transaction handling
- Missing null checks
- Hard-coded configuration values

### Step 4: Document Review Findings

Create a file `exercise2-review-findings.md`:

```markdown
# Code Review Findings

## Critical Issues
1. [Issue description]
   - Location: [file and line]
   - Fix: [recommended fix]

## High Priority Issues
1. [Issue description]
   - Location: [file and line]
   - Fix: [recommended fix]

## Medium Priority Issues
[Continue listing...]

## Low Priority / Enhancements
[Continue listing...]

## Positive Observations
- [What was done well]
```

### Step 5: Ask Follow-up Questions

If you need clarification on any review point, ask Copilot:
```
Can you explain why [specific issue] is a security concern?
What's the recommended pattern for [specific issue]?
Show me an example of how to fix [specific issue].
```

**✅ Checkpoint:** You should have a documented list of 5-10 issues to address.

---

## Exercise 3: Developer Response (10 minutes)

### Goal
Address the reviewer's feedback by refactoring your code.

### Step 1: Prioritize Issues

Review your `exercise2-review-findings.md` and identify: 
- Must fix (Critical/High)
- Should fix (Medium)
- Nice to have (Low)

### Step 2: Use Copilot to Fix Issues

In Copilot Chat, use this prompt:

```
As a C# developer, refactor this UserRegistrationService to address these review findings:

Issues to fix:
1. [Paste issue #1 from your review findings]
2. [Paste issue #2 from your review findings]
3. [Continue with all Critical and High priority issues]

Additional requirements:
- Add ILogger for logging
- Add custom exceptions (UserAlreadyExistsException, InvalidEmailException)
- Add null checks with ArgumentNullException
- Use configuration for password policy
- Add XML documentation for all public methods

Here is the current code:
[paste your UserRegistrationService.cs]

Please provide the refactored code with all issues addressed.
```

### Step 3: Review Refactored Code

Copilot will provide improved code. Check that:
- All critical issues are addressed
- Code is more readable
- New features (logging, exceptions) are added
- XML documentation is complete

### Step 4: Create Custom Exception Classes

Ask Copilot to generate exceptions: 

```
Create custom exception classes for the user registration system:
1. UserAlreadyExistsException
2. InvalidEmailException
3. WeakPasswordException

Each should: 
- Inherit from appropriate base exception
- Include constructors for message and inner exception
- Include XML documentation
- Follow . NET exception best practices
```

### Step 5: Save Refactored Code

1. Create `Exceptions/UserAlreadyExistsException.cs` (and other exceptions)
2. Update `Services/UserRegistrationService.cs` with the refactored version
3. Save the old version as `Services/UserRegistrationService.v1.cs` for comparison

### Step 6: Build and Test

```bash
dotnet build
```

### Step 7: Document Changes

Create `exercise3-refactoring-notes.md`:

```markdown
# Refactoring Changes

## Issues Addressed
- [List each issue and how it was fixed]

## New Features Added
- Custom exceptions
- Logging
- Configuration

## Code Comparison
### Before:  [describe old approach]
### After: [describe new approach]

## Improvements Measured
- Security:  [improvements]
- Maintainability: [improvements]
- Testability: [improvements]
```

**✅ Checkpoint:** You should have refactored code that addresses all critical issues. 

---

## Exercise 4: QA Agent (15 minutes)

### Goal
Create comprehensive unit tests using Copilot as a QA engineer.

### Step 1: Open Copilot Chat

Start a new chat session for the QA agent role.

### Step 2: Generate Test Class

Copy this QA prompt into Copilot Chat: 

```
Act as a QA engineer.  Create comprehensive xUnit tests for this C# UserRegistrationService. 

Test Coverage Requirements: 

Happy Path Tests:
- Successful user registration with valid data
- Password is properly hashed
- User is saved to repository

Validation Tests:
- Invalid email formats (multiple scenarios)
- Null or empty email
- Null or empty password
- Password too short
- Password missing uppercase letter
- Password missing lowercase letter
- Password missing number
- Password missing special character

Business Logic Tests:
- Duplicate user detection
- Case-insensitive email comparison

Exception Tests:
- UserAlreadyExistsException is thrown correctly
- ArgumentNullException for null parameters

Async Tests:
- Verify async behavior
- Test cancellation token handling

Use: 
- xUnit for test framework
- Moq for mocking IUserRepository
- FluentAssertions for assertions
- AAA pattern (Arrange, Act, Assert)

Here is the UserRegistrationService code:
[paste your refactored UserRegistrationService.cs]

Generate a complete test class with at least 15 test methods.
```

### Step 3: Review Generated Tests

Copilot will generate a test class.  Review it for:
- Proper use of Moq for mocking
- FluentAssertions syntax
- Test naming conventions
- Code coverage completeness
- Edge cases

### Step 4: Create Test File

1. Navigate to your test project:  `cd UserManagementAPI.Tests`
2. Create `UserRegistrationServiceTests.cs`
3. Paste the generated test code

### Step 5: Add Additional Test Cases

Ask Copilot for specific edge cases:

```
Add additional xUnit tests for these edge cases: 
1. Email with spaces before/after
2. Email with uppercase letters
3. Password exactly 8 characters (boundary test)
4. Concurrent registration attempts for same email
5. Special characters in email local part
6. Repository throws exception during save
```

### Step 6: Run Tests

```bash
cd UserManagementAPI.Tests
dotnet test
```

### Step 7: Fix Failing Tests

If any tests fail, ask Copilot:
```
This test is failing: 
[paste test method]

Error message:
[paste error]

How do I fix this test?
```

### Step 8: Check Code Coverage (Optional)

```bash
dotnet add package coverlet.collector
dotnet test /p:CollectCoverage=true
```

### Step 9: Document Test Results

Create `exercise4-test-report.md`:

```markdown
# Test Report

## Test Summary
- Total Tests: [number]
- Passed: [number]
- Failed:  [number]
- Code Coverage: [percentage]

## Test Categories
- Happy Path: [number] tests
- Validation:  [number] tests
- Exception Handling: [number] tests
- Edge Cases: [number] tests

## Notable Test Cases
1. [Interesting test case and why it matters]
2. [Another interesting test case]

## Tests That Found Bugs
- [Any issues discovered during testing]

## Coverage Gaps
- [Any scenarios not covered]
```

**✅ Checkpoint:** You should have 15+ passing tests with good code coverage.

---

## Exercise 5: Complete Workflow (25 minutes)

### Goal
Apply all agent roles to implement a password reset feature from scratch.

### Phase 1: Developer Agent - Initial Implementation (8 minutes)

#### Step 1: Define Requirements

Create `password-reset-requirements.md`:
```markdown
# Password Reset Feature Requirements

1. User requests password reset with email
2. System generates secure, unique reset token
3. Token expires after 15 minutes
4. Email sent with reset link
5. User clicks link and provides new password
6. System validates token
7. Token can only be used once
8. New password is hashed and saved
9. All user sessions are invalidated
```

#### Step 2: Generate Implementation

Ask Copilot: 
```
Context: ASP.NET Core 8.0 Web API, extending existing user management system

Task: As a developer, implement a password reset system

Requirements:
- PasswordResetService class with these methods:
  * GenerateResetTokenAsync(string email)
  * ValidateResetTokenAsync(string token)
  * ResetPasswordAsync(string token, string newPassword)
- Use cryptographically secure random token generation
- Token expiration (15 minutes)
- Single-use tokens
- Email notification (mock for now)
- Password validation reuse from registration
- Async/await throughout

Generate: 
1. PasswordResetService.cs
2. PasswordResetToken.cs (entity)
3. IPasswordResetRepository.cs
4. ResetPasswordRequest.cs (DTO)

Include XML documentation and error handling.
```

#### Step 3: Create Files

Create the following files with generated code:
- `Models/PasswordResetToken.cs`
- `Models/ResetPasswordRequest.cs`
- `Repositories/IPasswordResetRepository.cs`
- `Services/PasswordResetService.cs`

#### Step 4: Build

```bash
dotnet build
```

Fix any compilation errors with Copilot's help.

---

### Phase 2: Reviewer Agent - Code Review (5 minutes)

#### Step 1: Security Review

Ask Copilot: 
```
Act as a security-focused code reviewer. Review this password reset implementation:

[paste PasswordResetService.cs]

Focus on:
1. Token security (randomness, length, entropy)
2. Timing attack vulnerabilities
3. Token storage best practices
4. Rate limiting requirements
5. Email enumeration risks
6. Replay attack prevention

Provide specific security recommendations.
```

#### Step 2: General Code Review

Ask Copilot:
```
Act as a senior C# developer. Review this password reset code:

[paste PasswordResetService.cs]

Check for:
- SOLID principles
- Error handling
- Async patterns
- Resource cleanup
- Edge cases
- Logging
```

#### Step 3: Document Findings

Create `exercise5-review-findings.md` with all issues found. 

---

### Phase 3: Developer Response - Fixes (7 minutes)

#### Step 1: Address Security Issues

Ask Copilot: 
```
Refactor PasswordResetService to address these security concerns:

[paste security issues from review]

Requirements:
- Use RandomNumberGenerator for token generation
- Implement constant-time token comparison
- Add rate limiting attributes
- Prevent email enumeration
- Add audit logging

Current code:
[paste PasswordResetService.cs]
```

#### Step 2: Address Code Quality Issues

Ask Copilot: 
```
Further refactor PasswordResetService to address these issues:

[paste code quality issues from review]

Add:
- ILogger integration
- Custom exceptions
- Configuration for token expiration
- Transaction support
```

#### Step 3: Update Files

Update `Services/PasswordResetService.cs` with refactored code.

---

### Phase 4: QA Agent - Testing (10 minutes)

#### Step 1: Generate Unit Tests

Ask Copilot: 
```
Act as a QA engineer. Create comprehensive xUnit tests for PasswordResetService.

Test Categories: 

Token Generation Tests:
- Unique tokens generated
- Token length and format
- Cryptographically secure

Token Validation Tests:
- Valid token accepted
- Expired token rejected
- Invalid token rejected
- Used token rejected
- Null/empty token handled

Password Reset Tests:
- Successful password reset
- Password validation enforced
- Token invalidated after use
- User notified

Security Tests:
- Rate limiting enforced
- Timing attack resistance
- Concurrent reset attempts

Edge Cases:
- Non-existent email
- Multiple active tokens
- Token cleanup

Use xUnit, Moq, FluentAssertions, and AAA pattern. 

Code to test:
[paste PasswordResetService.cs]

Generate complete test class.
```

#### Step 2: Create Test File

Create `UserManagementAPI.Tests/PasswordResetServiceTests.cs` with generated tests.

#### Step 3: Run Tests

```bash
cd UserManagementAPI.Tests
dotnet test
```

#### Step 4: Fix Failures and Add Missing Tests

Work with Copilot to fix any failing tests and add coverage for gaps.

---

### Phase 5: Documentation Agent (5 minutes)

#### Step 1: Generate API Documentation

Ask Copilot: 
```
Create comprehensive API documentation for the password reset feature:

Include:
1. Feature overview
2. API endpoints (describe what they would be)
3. Request/response examples
4. Error codes and messages
5. Security considerations
6. Usage examples
7. Integration guide

Format as markdown. 

Code reference:
[paste PasswordResetService.cs]
```

#### Step 2: Generate XML Documentation

Ask Copilot: 
```
Add comprehensive XML documentation comments to this code:

[paste PasswordResetService.cs]

Include:
- Summary for class and all methods
- Parameter descriptions
- Return value descriptions
- Exception documentation
- Example usage
- Remarks for security considerations
```

#### Step 3: Create Documentation Files

1. Create `Docs/password-reset-api.md` with API documentation
2. Update `PasswordResetService.cs` with XML comments

---

### Phase 6: Final Deliverables

Create `exercise5-workflow-log.md`:

```markdown
# Complete Workflow Log:  Password Reset Feature

## Phase 1: Developer Agent - Initial Implementation
### What Was Created:
- [List files and key features]

### Key Decisions:
- [Design decisions made]

## Phase 2: Reviewer Agent - Code Review
### Security Issues Found:
1. [Issue and severity]
2. [Issue and severity]

### Code Quality Issues Found:
1. [Issue and priority]
2. [Issue and priority]

## Phase 3: Developer Response - Fixes
### Changes Made:
- [List refactorings]

### Before vs After:
- Security: [improvements]
- Code Quality: [improvements]

## Phase 4: QA Agent - Testing
### Test Coverage:
- Total Tests: [number]
- Categories: [list]
- Coverage: [percentage]

### Bugs Found During Testing:
- [Any issues discovered]

## Phase 5: Documentation
### Documentation Created:
- API documentation
- XML comments
- Usage examples

## Final Metrics
- Files Created: [number]
- Lines of Code: [number]
- Test Coverage: [percentage]
- Issues Found: [number]
- Issues Fixed: [number]

## Key Learnings
1. [What worked well with multi-agent approach]
2. [What could be improved]
3. [Insights about Copilot usage]
```

**✅ Final Checkpoint:** You should have a complete, tested, documented password reset feature. 

---

## Lab Completion Checklist

### Exercise 1: Developer Agent ✓
- [ ] UserRegistrationService.cs created
- [ ] RegisterUserRequest.cs created
- [ ] IUserRepository.cs created
- [ ] User. cs created
- [ ] Code compiles successfully
- [ ] Documentation created

### Exercise 2: Reviewer Agent ✓
- [ ] Code review performed
- [ ] Security issues identified
- [ ] Code quality issues identified
- [ ] Review findings documented
- [ ] Issues prioritized

### Exercise 3: Developer Response ✓
- [ ] Critical issues fixed
- [ ] Custom exceptions created
- [ ] Logging added
- [ ] Code refactored
- [ ] Changes documented
- [ ] Code compiles

### Exercise 4: QA Agent ✓
- [ ] Test class created
- [ ] 15+ test methods written
- [ ] All tests pass
- [ ] Edge cases covered
- [ ] Test report created

### Exercise 5: Complete Workflow ✓
- [ ] Password reset implemented
- [ ] Security review completed
- [ ] Refactoring completed
- [ ] Tests created and passing
- [ ] Documentation generated
- [ ] Workflow log completed

---

## Key Takeaways

### Multi-Agent Benefits
1. **Different Perspectives:** Each agent role catches different issues
2. **Quality Improvement:** Iteration leads to better code
3. **Comprehensive Coverage:** Security, functionality, testing all addressed
4. **Real-World Simulation:** Mirrors actual development processes

### Copilot Best Practices
1. **Be Specific:** Include framework versions, libraries, patterns
2. **Provide Context:** Reference existing code and requirements
3. **Iterate:** Refine prompts based on results
4. **Verify:** Always review generated code
5. **Test:** Generate tests to validate functionality

### Agent Roles Learned
- **Developer:** Implementation focus
- **Reviewer:** Quality and security focus
- **QA:** Testing and edge cases
- **Security:** Threat modeling
- **Documentation:** Communication and usage

---

## Troubleshooting Guide

### Issue:  Copilot generates incomplete code
**Solution:** 
```
Continue the previous response and complete the [ClassName] implementation.
```

### Issue: Code doesn't compile
**Solution:**
```
Fix the compilation errors in this code:
[paste code]
Show me what changes are needed. 
```

### Issue: Tests fail
**Solution:**
```
This test is failing:
[paste test]
Error:  [paste error]
Explain why and provide a fix.
```

### Issue: Need to update existing code
**Solution:**
```
Update this existing code: 
[paste old code]

To include these changes:
[list changes]

Preserve existing functionality. 
```

### Issue: Copilot generates wrong language
**Solution:**
Start every prompt with "In C# using . NET 8..." or reference C# files with `#file: `.

---

## Additional Resources

### Useful Copilot Prompts

**Code Generation:**
```
Generate a C# [type] that [description] with [requirements]
```

**Code Review:**
```
Review this C# code for [aspects] and provide [type of feedback]
```

**Refactoring:**
```
Refactor this code to [improvements] while maintaining [constraints]
```

**Testing:**
```
Create xUnit tests for [code] covering [scenarios] using [tools]
```

**Documentation:**
```
Generate [doc type] for [code] including [sections]
```

### Keyboard Shortcuts

**VS Code:**
- Open Copilot Chat:  `Ctrl+Alt+I`
- Inline suggestions: `Tab` to accept
- Next suggestion: `Alt+]`
- Previous suggestion: `Alt+[`

**Visual Studio:**
- Open Copilot Chat: `Ctrl+/`
- Accept suggestion: `Tab`
- View alternatives: `Alt+.`

---

## Submission

Submit the following files in a folder named `Lab8-Agent-Workflow-[YourName]`:

### Documentation Files: 
1. `exercise1-developer-notes.md`
2. `exercise2-review-findings.md`
3. `exercise3-refactoring-notes.md`
4. `exercise4-test-report.md`
5. `exercise5-workflow-log.md`

### Code Files - Registration System:
6. `Models/User.cs`
7. `Models/RegisterUserRequest.cs`
8. `Repositories/IUserRepository.cs`
9. `Services/UserRegistrationService.cs`
10. `Exceptions/UserAlreadyExistsException.cs`
11. `Tests/UserRegistrationServiceTests.cs`

### Code Files - Password Reset:
12. `Models/PasswordResetToken.cs`
13. `Models/ResetPasswordRequest.cs`
14. `Repositories/IPasswordResetRepository.cs`
15. `Services/PasswordResetService.cs`
16. `Tests/PasswordResetServiceTests. cs`
17. `Docs/password-reset-api.md`

---

## Grading Rubric

| Category | Points | Criteria |
|----------|--------|----------|
| **Exercise 1** | 15 | Complete implementation, compiles, documented |
| **Exercise 2** | 15 | Thorough review, issues identified and prioritized |
| **Exercise 3** | 15 | Issues addressed, refactored code, documented |
| **Exercise 4** | 20 | 15+ tests, all passing, good coverage |
| **Exercise 5** | 30 | Complete workflow, all phases documented |
| **Documentation** | 5 | Clear, complete, well-organized |
| **Total** | **100** | |

---

## Success Criteria

You've successfully completed this lab if you can:
- ✅ Use Copilot to generate production-quality C# code
- ✅ Apply different agent roles to improve code quality
- ✅ Identify and fix security vulnerabilities
- ✅ Create comprehensive test suites
- ✅ Document code and workflows effectively
- ✅ Complete a full feature using multi-agent workflow

**Congratulations on completing Lab 8!  🎉**
