# Lab 1: Building Strong Prompting Habits

**Duration:** 30 minutes  
**Difficulty:** Beginner

## Lab Objectives

In this lab, you will:
1. Understand how Copilot uses context
2. Compare outputs with different levels of commenting
3. Practice writing effective prompts
4. Use role-based and constraint-based prompts
5. Iteratively refine Copilot outputs

## Prerequisites

- GitHub Copilot enabled in your IDE (VS Code, Visual Studio, or JetBrains)
- Node.js or any programming environment installed
- Basic JavaScript or TypeScript knowledge

## Lab Structure

This lab is divided into 4 exercises:

### Exercise 1: Understanding Context (10 minutes)

**Objective:** Observe how Copilot behaves with different levels of context.

#### Part A: No Comments
1. Create a new file: `exercise1-no-comments.js`
2. Start typing: `function calculate`
3. Observe the suggestions Copilot provides
4. Accept one suggestion and note what it does

#### Part B: Minimal Comments
1. Create a new file: `exercise1-minimal-comments.js`
2. Add a minimal comment:
```javascript
// calculate shipping
function calculate
```
3. Observe the suggestions
4. Compare with Part A - Are they more specific?

#### Part C: Well-Structured Comments
1. Create a new file: `exercise1-structured-comments.js`
2. Add a well-structured comment using the four-pillar approach:
```javascript
// Intent: Calculate shipping cost for an e-commerce order
// Context: Support multiple shipping zones (domestic, international)
// Constraints:
// - Base cost: $5 domestic, $15 international
// - Add $1.50 per pound of weight
// - Free shipping for orders over $100
// Examples:
// calculateShipping('domestic', 3, 50) -> 9.5
// calculateShipping('international', 2, 120) -> 0 (free shipping)
function calculateShipping
```
3. Let Copilot generate the function
4. Test the generated function

#### Deliverable:
Create a file `exercise1-observations.md` documenting:
- What suggestions you received in each scenario
- How specificity improved with better comments
- Which approach produced the best code

---

### Exercise 2: Poor vs Effective Prompts (10 minutes)

**Objective:** Practice identifying and improving prompts.

#### Part A: Poor Prompts
Try to get useful code from these poor prompts:

1. **Vague Prompt:**
```javascript
// get data
```

2. **Missing Context:**
```javascript
// validate
```

3. **No Constraints:**
```javascript
// save to database
```

#### Part B: Improve the Prompts
Rewrite each prompt using the four-pillar structure:

1. **Improved Prompt 1:**
```javascript
// Intent: Fetch user data from REST API
// Context: Node.js application using axios library
// Constraints:
// - Endpoint: https://api.example.com/users/{id}
// - Include authorization header with JWT token
// - Handle network errors and 404 responses
// - Return user object or null
// Examples:
// fetchUserData(123, 'token') -> {id: 123, name: 'John', email: '...'}
// fetchUserData(999, 'token') -> null (user not found)
```

2. **Your turn:** Improve the "validate" prompt
3. **Your turn:** Improve the "save to database" prompt

#### Deliverable:
Create a file `exercise2-improved-prompts.js` with your improved prompts and generated code.

---

### Exercise 3: Role-Based Prompts (5 minutes)

**Objective:** Use role-based prompting to influence code generation.

#### Tasks:
Create three versions of a password validation function using different roles:

1. **Security Engineer Role:**
```javascript
// As a security engineer, create a password validation function
// Requirements:
// - Minimum 12 characters
// - Must include uppercase, lowercase, numbers, and special characters
// - Check against common password list
// - No sequential characters (abc, 123)
// - No repeated characters (aaa, 111)
function validatePasswordSecure
```

2. **UX Designer Role:**
```javascript
// As a UX designer, create a password validation function
// Requirements:
// - Provide helpful, user-friendly error messages
// - Return specific feedback for each failed rule
// - Include a strength indicator (weak/medium/strong)
// - Suggest improvements to user
function validatePasswordUX
```

3. **Performance Engineer Role:**
```javascript
// As a performance engineer, create a password validation function
// Requirements:
// - Optimize for speed (process 1000+ passwords/second)
// - Use efficient regex patterns
// - Avoid unnecessary string operations
// - Return boolean only
function validatePasswordPerformance
```

#### Deliverable:
Create a file `exercise3-role-based.js` with all three functions.

---

### Exercise 4: Iterative Refinement (5 minutes)

**Objective:** Learn to refine Copilot output through iteration.

#### Scenario:
You need a function to filter and sort products.

#### Iteration 1 (Basic):
```javascript
// Filter products by category
function filterProducts
```

#### Iteration 2 (Add constraints):
```javascript
// Filter products by category
// Sort by price (low to high)
// Only show in-stock items
function filterProducts
```

#### Iteration 3 (Add examples):
```javascript
// Filter products by category and price range
// Sort by price (ascending) or name (alphabetical)
// Only show in-stock items with minimum rating
// Examples:
// filterProducts(products, 'electronics', 0, 500, 'price', 4.0)
// filterProducts(products, 'books', 0, 50, 'name', 3.5)
function filterProducts
```

#### Iteration 4 (Add context):
```javascript
// E-commerce product filtering for search results page
// Filter products by category and price range
// Sort by price (ascending) or name (alphabetical)  
// Only show in-stock items with minimum rating
// Handle empty results gracefully
// Expected input: Array of product objects with {id, name, category, price, inStock, rating}
// Expected output: Filtered and sorted array
// Examples:
// filterProducts(products, 'electronics', 0, 500, 'price', 4.0) -> [{...}, {...}]
// filterProducts(products, 'books', 0, 50, 'name', 3.5) -> []
function filterProducts
```

#### Task:
1. Try each iteration level
2. Compare the code quality at each stage
3. Identify which iteration produced the best result

#### Deliverable:
Create a file `exercise4-iterations.js` showing all iterations.

---

## Bonus Challenge

Create a prompt for a complex feature using Figma design description:

**Figma Design:**
A dashboard card component showing:
- Header with title and icon
- Main content area with metric value and trend indicator (up/down arrow)
- Footer with timestamp and "View Details" link
- Responsive design with different layouts for mobile/desktop
- Hover effect on the card
- Colors: Primary blue (#007AFF), Success green (#34C759), Danger red (#FF3B30)

**Your Task:**
Write a comprehensive prompt that includes:
- Intent
- Context (React component, TypeScript)
- Constraints (props, styling approach)
- Examples (sample props)

Then let Copilot generate the component.

---

## Lab Completion Checklist

- [ ] Completed Exercise 1: Context comparison
- [ ] Completed Exercise 2: Improved prompts
- [ ] Completed Exercise 3: Role-based prompts
- [ ] Completed Exercise 4: Iterative refinement
- [ ] Created observation notes
- [ ] Attempted bonus challenge

## Key Takeaways

After completing this lab, you should understand:
1. **Context matters:** More context = better suggestions
2. **Structure helps:** Four-pillar approach produces consistent results
3. **Roles influence output:** Different perspectives yield different code
4. **Iteration improves quality:** Refining prompts leads to better code
5. **Examples guide behavior:** Showing expected I/O helps Copilot understand

## Next Module

Proceed to [Module 2: Advanced GitHub Copilot Features](../../Module-02-Advanced-Features/) when ready.

## Troubleshooting

**Copilot not suggesting anything:**
- Make sure Copilot is enabled (check status bar)
- Try pressing `Ctrl+Enter` (or `Cmd+Enter` on Mac) to see all suggestions
- Ensure you have an active internet connection

**Suggestions don't match expectations:**
- Add more context and constraints
- Provide examples
- Be more specific in your intent

**Want to see alternative suggestions:**
- Press `Alt+]` for next suggestion
- Press `Alt+[` for previous suggestion
- Press `Ctrl+Enter` to see all suggestions in a panel
