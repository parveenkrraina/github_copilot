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
- Code editor with Copilot extensions installed
- Sample project files (provided in labs/starter-code/)

## Lab Structure

### Exercise 1: Advanced Chat Commands (5 minutes)

**Objective:** Master Copilot Chat slash commands

#### Task 1: Using /explain
1. Open `starter-code/complex-algorithm.js`
2. Select the `findOptimalPath` function
3. In Copilot Chat, type: `/explain this function`
4. Review the explanation

#### Task 2: Using /fix
1. Open `starter-code/buggy-code.js`
2. Try to run the code (it has bugs)
3. Select the problematic function
4. Type: `/fix this code`
5. Apply the suggested fixes

#### Task 3: Using /tests
1. Open `starter-code/user-service.js`
2. Select the `createUser` function
3. Type: `/tests for this function including edge cases`
4. Save the generated tests to a new file

#### Task 4: Using /doc
1. Open `starter-code/api-handler.js`
2. Select a function without documentation
3. Type: `/doc with JSDoc format`
4. Add the generated documentation

**Deliverable:** Document your findings in `exercise1-chat-commands.md`

---

### Exercise 2: Code Refactoring (10 minutes)

**Objective:** Improve code quality using Copilot

#### Part A: Simplify Complex Functions

1. Open `starter-code/legacy-code.js`
2. Find the `processUserData` function (it's overly complex)
3. Prompt Copilot Chat:
```
Refactor this function to:
- Use modern JavaScript (ES6+)
- Reduce nesting
- Improve readability
- Add proper error handling
- Use async/await instead of callbacks
```
4. Compare the original with the refactored version

#### Part B: Extract Functions

1. Look at the `handleCheckout` function
2. It does too many things (validates, calculates, saves, sends email)
3. Prompt Copilot:
```
This function violates Single Responsibility Principle
Extract each responsibility into separate functions:
- validateCheckout
- calculateTotal
- saveOrder
- sendConfirmationEmail
Keep the main function as an orchestrator
```

#### Part C: Apply Design Patterns

1. Open `starter-code/notification-system.js`
2. Prompt Copilot:
```
Refactor this notification system to use the Strategy pattern
Support email, SMS, and push notifications
Make it easy to add new notification types
```

**Deliverable:** Save refactored code to `exercise2-refactored-code.js`

---

### Exercise 3: Performance Optimization (7 minutes)

**Objective:** Optimize code for better performance

#### Task 1: Algorithm Optimization

1. Open `starter-code/slow-search.js`
2. The current implementation uses nested loops (O(n²))
3. Prompt Copilot:
```
Optimize this search function:
- Current: O(n²) time complexity
- Target: O(n) or O(n log n)
- Use appropriate data structures
- Handle edge cases
```

#### Task 2: Memory Optimization

1. Open `starter-code/file-processor.js`
2. Current code loads entire file into memory
3. Prompt Copilot:
```
Refactor to use streams for processing large files:
- Read file line by line
- Process without loading entire file
- Reduce memory footprint
- Handle files larger than available RAM
```

#### Task 3: Database Query Optimization

1. Open `starter-code/database-queries.js`
2. Contains N+1 query problem
3. Prompt Copilot:
```
Optimize these database queries:
- Eliminate N+1 query problem
- Use JOIN or eager loading
- Reduce number of database roundtrips
- Add appropriate indexes (in comments)
```

**Deliverable:** Create `exercise3-optimizations.md` documenting:
- Original time/space complexity
- Optimized complexity
- Performance improvements

---

### Exercise 4: Design Feedback (5 minutes)

**Objective:** Get architectural and design suggestions

#### Task 1: Architecture Review

1. Open `starter-code/monolithic-service.js`
2. Prompt Copilot Chat:
```
Review this code architecture:
- Identify tight coupling issues
- Suggest separation of concerns improvements
- Recommend better structure for testability
- Propose dependency injection approach
```

#### Task 2: SOLID Principles Check

1. Use the same file
2. Prompt:
```
Analyze this code against SOLID principles:
- Single Responsibility Principle violations
- Open/Closed Principle opportunities
- Dependency Inversion needs
Provide specific refactoring suggestions
```

#### Task 3: Code Smell Detection

1. Open `starter-code/smelly-code.js`
2. Prompt:
```
Identify code smells in this file:
- Long methods
- Duplicated code
- Large classes
- Feature envy
Suggest refactoring strategies
```

**Deliverable:** Create `exercise4-design-review.md` with findings and recommendations

---

### Exercise 5: Documentation Generation (5 minutes)

**Objective:** Generate comprehensive documentation

#### Task 1: Function Documentation

1. Open `starter-code/undocumented-utils.js`
2. For each function, prompt:
```
/doc generate comprehensive JSDoc including:
- Description
- Parameters with types
- Return value
- Examples
- Edge cases
- Exceptions thrown
```

#### Task 2: Class Documentation

1. Open `starter-code/user-manager.js`
2. Prompt:
```
Generate complete class documentation:
- Class description and purpose
- Constructor parameters
- Public methods with full JSDoc
- Usage examples
- Best practices
```

#### Task 3: API Documentation

1. Open `starter-code/api-routes.js`
2. Prompt:
```
Generate API documentation for these endpoints:
- Endpoint path and method
- Request parameters
- Request body schema
- Response format
- Status codes
- Example requests/responses
- Error cases
```

**Deliverable:** Create fully documented versions of all files

---

### Exercise 6: Understanding Limitations (3 minutes)

**Objective:** Recognize when Copilot suggestions need verification

#### Task 1: Identify Potential Issues

Review these Copilot suggestions and identify problems:

1. **Suggestion 1:**
```javascript
// Copilot suggested this for "create user authentication"
function authenticateUser(username, password) {
  const query = `SELECT * FROM users WHERE username = '${username}' 
                 AND password = '${password}'`;
  return db.query(query);
}
```
**Question:** What's wrong? How would you fix it?

2. **Suggestion 2:**
```javascript
// Copilot suggested this for "hash password"
function hashPassword(password) {
  return btoa(password); // Base64 encoding
}
```
**Question:** What's the security issue?

3. **Suggestion 3:**
```javascript
// Copilot suggested this for "fetch user data"
async function fetchUser(id) {
  const response = await fetch(`/api/users/${id}`);
  return response.json();
}
```
**Question:** What error handling is missing?

**Deliverable:** Create `exercise6-limitations.md` documenting issues found

---

## Bonus Challenges

### Challenge 1: Complete Refactoring

Take the entire `starter-code/legacy-app.js` and:
1. Refactor using modern patterns
2. Add comprehensive error handling
3. Optimize performance
4. Add full documentation
5. Extract reusable components

### Challenge 2: Documentation Site

Generate a complete documentation site structure:
- README.md
- API.md
- ARCHITECTURE.md
- CONTRIBUTING.md
- CHANGELOG.md

Use Copilot to generate content for each based on the code.

### Challenge 3: Performance Benchmark

1. Create performance tests for original vs optimized code
2. Document actual performance improvements
3. Use Copilot to generate benchmark code

---

## Lab Completion Checklist

- [ ] Completed Exercise 1: Chat commands
- [ ] Completed Exercise 2: Refactoring
- [ ] Completed Exercise 3: Optimization
- [ ] Completed Exercise 4: Design feedback
- [ ] Completed Exercise 5: Documentation
- [ ] Completed Exercise 6: Limitations
- [ ] Attempted at least one bonus challenge

## Key Takeaways

1. **Slash commands are powerful:** Use /explain, /fix, /tests, /doc for specific tasks
2. **Refactoring is iterative:** Start with small improvements, build up
3. **Always verify suggestions:** Especially for security and performance
4. **Documentation saves time:** Let Copilot handle boilerplate docs
5. **Know the limitations:** Copilot is a tool, not a replacement for thinking

## Troubleshooting

**Chat not responding:**
- Check internet connection
- Reload VS Code/IDE
- Verify Copilot subscription is active

**Poor suggestions:**
- Add more context in your prompt
- Reference existing code patterns
- Be more specific about requirements

**Code doesn't work:**
- Review for security issues
- Check for missing error handling
- Verify against your environment
- Test thoroughly

## Next Module

When ready, proceed to [Module 3: GitHub Copilot Across Environments](../Module-03-Across-Environments/lab-instructions.md)
