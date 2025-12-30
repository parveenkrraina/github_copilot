# Lab 7: Multi-Language Development

**Duration:** 30 minutes

## Objectives
- Use Copilot effectively in C#
- Use Copilot effectively in JavaScript
- Apply language-agnostic patterns
- Adapt prompts across languages

## Exercise 1: C# Development (10 min)

### Step 1: Create ASP.NET Core Controller
1. Create a new file: `UserController.cs`
2. Use this four-pillar prompt with Copilot:
    - **Intent:** Create an API controller for user management
    - **Context:** ASP.NET Core 6+, RESTful endpoints
    - **Constraints:** GET, POST, PUT endpoints; validate email format; return appropriate HTTP status codes
    - **Examples:** GET /api/users → 200 with user list; POST with invalid email → 400
3. Review Copilot's generated controller structure
4. Verify endpoints are properly decorated with `[HttpGet]`, `[HttpPost]`, etc.

### Step 2: Build Service Layer with Dependency Injection
1. Create `IUserService.cs` interface
2. Create `UserService.cs` implementation
3. Prompt Copilot: "Create a UserService class that implements IUserService with dependency injection for database access. Follow SOLID principles."
4. Verify constructor accepts `IUserRepository` parameter
5. Implement business logic methods (GetAll, Create, Update)

### Step 3: Add Entity Framework Queries
1. Create `User.cs` entity model
2. Create `ApplicationDbContext.cs`
3. Prompt: "Write Entity Framework queries for User CRUD operations using async/await. Include filtering by status."
4. Test queries compile without errors

### Step 4: Write xUnit Tests
1. Create `UserServiceTests.cs`
2. Prompt: "Generate xUnit tests for UserService methods. Include Moq for mocking IUserRepository."
3. Implement 3-4 test cases (happy path, null input, validation failure)
4. Run tests: `dotnet test`

---

## Exercise 2: JavaScript Development (10 min)

### Step 1: Create Express.js Routes
1. Create `routes/userRoutes.js`
2. Use four-pillar prompt:
    - **Intent:** Create Express routes for user API
    - **Context:** Express 4.x, async handlers
    - **Constraints:** GET, POST, PUT endpoints; email validation; error handling middleware
    - **Examples:** GET /api/users → 200 with array; POST invalid → 400 with error message
3. Review generated route definitions
4. Verify proper HTTP methods and route parameters

### Step 2: Build Service Layer
1. Create `services/userService.js`
2. Prompt: "Create a UserService class with methods: getAll(), create(userData), update(id, data). Include input validation."
3. Implement business logic separate from routes
4. Export as module

### Step 3: Add Database Queries
1. Create `db/userQueries.js` (or use ORM like Prisma/Sequelize)
2. Prompt: "Write database queries for User CRUD using [your DB choice]. Include filtering by active status."
3. Use parameterized queries for security
4. Test query syntax

### Step 4: Write Jest Tests
1. Create `__tests__/userService.test.js`
2. Prompt: "Generate Jest tests for userService. Mock database calls using jest.mock()."
3. Implement 3-4 test cases
4. Run: `npm test`

---

## Exercise 3: Cross-Language Patterns (5 min)

### Task 1: Factory Pattern
1. Implement in C#: `UserFactory.cs` with `CreateUser()` method
2. Implement in JavaScript: `userFactory.js` with exported `createUser()` function
3. Compare structure—notice language idioms differ, but concept is identical

### Task 2: Repository Pattern
1. C#: `IUserRepository.cs` interface + `UserRepository.cs` implementation
2. JavaScript: `userRepository.js` with methods like `getById()`, `save()`
3. Observe: Both abstract data access, use dependency injection, achieve same goal

### Task 3: Observer Pattern
1. C#: Use `IObserver<T>` or event handlers
2. JavaScript: Use callback/event emitter pattern
3. Prompt both: "Implement observer pattern to notify when user is created"

---

## Exercise 4: Prompt Adaptation (5 min)

### Step 1: Write a Generic Prompt
Choose a feature (e.g., "user email validation"). Write a language-agnostic four-pillar prompt:
```
Intent: Validate user email format
Context: User registration form validation
Constraints: Must reject empty strings, invalid formats; return true/false
Example: validate('user@example.com') → true; validate('invalid') → false
```

### Step 2: Adapt to C#
Modify prompt: "In C#, write an async method ValidateUserEmailAsync(email) using regex..."
Paste into Copilot in C# file. Record output.

### Step 3: Adapt to JavaScript
Modify prompt: "In JavaScript, write a function validateUserEmail(email) using regex or email validation library..."
Paste into Copilot in `.js` file. Record output.

### Step 4: Adapt to Python
Modify prompt: "In Python, write a function validate_user_email(email)..."
Compare the three outputs. Note: **same logic, different syntax**.

---

## Deliverables

1. **C# API Implementation**
    - `UserController.cs` with 3+ endpoints
    - `UserService.cs` with business logic
    - `UserServiceTests.cs` with passing tests

2. **JavaScript API Implementation**
    - `routes/userRoutes.js` with 3+ routes
    - `services/userService.js` with business logic
    - `__tests__/userService.test.js` with passing tests

3. **Cross-Language Pattern Examples**
    - Factory, Repository, Observer patterns in both languages
    - Side-by-side code comparison document

4. **Adapted Prompts Documentation**
    - Original four-pillar prompt
    - C# adaptation + Copilot output
    - JavaScript adaptation + Copilot output
    - Python adaptation + Copilot output
    - Brief analysis: "What changed? What stayed the same?"

---

## Key Takeaways

- **Same prompt works across languages** — The four-pillar structure transcends syntax
- **Copilot adapts to language conventions** — It uses C# async/await, JavaScript Promises, Python conventions automatically
- **Focus on concepts, not syntax** — Describe *what* you want (validation, service layer, factory), not *how* in specific syntax
- **Universal patterns translate well** — Design patterns apply everywhere; implementation details vary
- **Language idioms matter** — Copilot respects each language's best practices (SOLID for C#, functional patterns for JavaScript)

