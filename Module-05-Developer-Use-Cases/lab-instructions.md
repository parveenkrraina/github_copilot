# Lab 5: Real-World Developer Use Cases

**Duration:** 30 minutes

## Objectives
- Apply Copilot to real development scenarios
- Practice feature development workflow
- Refactor legacy code safely
- Generate comprehensive documentation

## Exercise 1: New Feature Development (10 min)

### Objective
Implement a complete shopping cart feature using the four-pillar prompt structure.

### Step-by-Step Guide

**Step 1: Plan with Copilot Chat**
1. Open Copilot Chat in your IDE
2. Ask: "I need to build a shopping cart feature. What are the key methods I should implement?"
3. Include context: framework (Node.js/Express or C#/.NET), database type
4. Review the suggested architecture

**Step 2: Generate Service Class**
1. Prompt Copilot with Intent, Context, Constraints, and Examples:
    - **Intent:** Create a ShoppingCart service class
    - **Context:** Using Express.js and PostgreSQL, user authentication available
    - **Constraints:** Support add/remove items, calculate totals with tax, validate quantity, prevent negative prices
    - **Examples:** `cart.addItem(userId, productId, quantity)` returns `{success: true, cartTotal: 45.99}`
2. Generate the complete service class
3. Review and save to `shopping-cart-service.js`

**Step 3: Create Tests**
1. In Copilot Chat, ask: `/tests` for your service class
2. Generate test cases covering:
    - Adding items successfully
    - Removing items
    - Edge cases (zero quantity, non-existent products)
3. Save tests to `shopping-cart-service.test.js`
4. Run tests: `npm test` or `dotnet test`

**Step 4: Document API**
1. Use `/doc` command on the service class
2. Generate JSDoc/XML comments for all methods
3. Create a usage guide: "How do I use ShoppingCart in my route handler?"
4. Save documentation to `SHOPPING_CART_API.md`

---

## Exercise 2: Legacy Code Refactoring (10 min)

### Objective
Safely refactor legacy code while maintaining functionality.

### Step-by-Step Guide

**Step 1: Understand with `/explain`**
1. Copy the provided legacy code (callback hell, deep nesting)
2. Select the function and use Copilot's `/explain` command
3. Read the explanation to understand:
    - What the code does
    - Why it's hard to maintain
    - Current patterns (callbacks, nested conditions)

**Step 2: Generate Tests First**
1. Before refactoring, use `/tests` on the original function
2. Create comprehensive tests covering:
    - Happy path (normal inputs)
    - Edge cases (empty arrays, null values)
    - Error scenarios
3. Run tests against original code to verify they pass
4. **This is your safety net for refactoring**

**Step 3: Refactor Incrementally**
1. Prompt Copilot:
    - **Intent:** Refactor callback hell into async/await
    - **Context:** Node.js, existing test suite
    - **Constraints:** Same function signature, same return values
    - **Examples:** Show before/after for 1-2 methods
2. Generate refactored version
3. Compare with original (side-by-side)
4. Implement changes

**Step 4: Verify with Tests**
1. Run your test suite against refactored code
2. All tests must pass (green)
3. If tests fail, use `/fix` to debug
4. Commit with message: "Refactor: Modernize [function] to async/await"

---

## Exercise 3: Debugging Session (5 min)

### Objective
Use Copilot to diagnose and fix a bug efficiently.

### Step-by-Step Guide

**Step 1: Analyze Error with Copilot**
1. Copy the error message and stack trace
2. Paste into Copilot Chat with context:
    - The error message
    - The code where it occurs
    - What you expected vs. what happened
3. Ask: "What's causing this error?"
4. Review Copilot's analysis

**Step 2: Add Debug Logging**
1. Ask Copilot: "Where should I add console.log statements to debug this?"
2. Copilot will suggest strategic logging points
3. Add logs around the suspected bug area
4. Run the code and capture log output

**Step 3: Generate Test Cases**
1. Use `/tests` on the buggy function
2. Include a test case that reproduces the bug
3. Verify the test fails (confirms you've reproduced it)
4. Keep this test for verification after fix

**Step 4: Fix the Bug**
1. Use `/fix` on the problematic function
2. Review the suggested fix
3. Apply the fix to your code
4. Run your reproduction test—it should now pass
5. Run full test suite to ensure no regressions

---

## Exercise 4: Documentation Generation (5 min)

### Objective
Create complete, professional documentation automatically.

### Step-by-Step Guide

**Step 1: Function Documentation**
1. Select a function in your code
2. Use `/doc` command
3. Copilot generates JSDoc (JavaScript) or XML comments (C#)
4. Review for accuracy:
    - Parameter descriptions correct?
    - Return type matches actual output?
    - Example usage provided?
5. Accept or refine the generated comments

**Step 2: API Documentation**
1. In Copilot Chat, prompt:
    - **Intent:** Generate API documentation for ShoppingCart endpoints
    - **Context:** Express.js REST API, authentication via JWT
    - **Constraints:** Include request/response examples, error codes
    - **Examples:** `POST /cart/add-item` returns `{success: true, cartId: 123}`
2. Generate markdown documentation
3. Save to `API.md`

**Step 3: Usage Examples**
1. Prompt: "Create 5 realistic code examples showing how to use [service]"
2. Copilot generates:
    - Basic usage
    - Error handling
    - Advanced scenarios
3. Save to `USAGE_EXAMPLES.md`

**Step 4: README Generation**
1. Ask Copilot: "Generate a README for this feature including: overview, installation, quick start, and troubleshooting"
2. Review and customize as needed
3. Save to `README.md`
4. Verify all code examples are syntactically correct

---

## Deliverables

- ✅ **shopping-cart-service.js** — Complete service implementation
- ✅ **shopping-cart-service.test.js** — Full test coverage (all passing)
- ✅ **refactored-legacy-code.js** — Modernized code with passing tests
- ✅ **bug-fixed-with-tests.js** — Bug fix + reproduction test
- ✅ **SHOPPING_CART_API.md** — API documentation
- ✅ **USAGE_EXAMPLES.md** — Code examples
- ✅ **README.md** — Feature overview and quick start

## Key Takeaways

- **Four-Pillar Structure:** Use Intent + Context + Constraints + Examples in every prompt
- **Test-Driven Refactoring:** Generate tests before refactoring; they validate your changes
- **Chat for Complexity:** Use Copilot Chat for multi-step debugging and planning
- **Documentation as Code:** Use `/doc` to auto-generate professional documentation
- **Iterative Refinement:** Review Copilot's output; refine prompts for better results

