# Lab 1: Building Strong Prompting Habits

**Duration:** 30 minutes  
**Difficulty:** Beginner

## Prerequisites & Setup

- **VS Code:** Version 1.84+ installed
- **GitHub Copilot Extension:** Install from VS Code Extensions (ID: `GitHub.copilot`)
- **GitHub Account:** Active subscription to GitHub Copilot
- **Node.js:** v16+ installed
- **Internet Connection:** Required for Copilot API calls

### Verify Setup
1. Open VS Code → Extensions (Ctrl+Shift+X / Cmd+Shift+X)
2. Search "GitHub Copilot" and install if missing
3. Sign in: Click Copilot icon → "Sign in to GitHub"
4. Verify status: Copilot icon in status bar should show active (no error icons)

## Lab Objectives

In this lab, you will:
1. Understand how Copilot uses context
2. Compare outputs with different levels of commenting
3. Practice writing effective prompts
4. Use role-based and constraint-based prompts
5. Iteratively refine Copilot outputs

## Lab Structure

This lab is divided into 4 exercises:

### Exercise 1: Understanding Context (10 minutes)

**Objective:** Observe how Copilot behaves with different levels of context.

#### Part A: No Comments
1. Create new file: `File` → `New File` → Save as `exercise1-no-comments.js`
2. Type exactly:
    ```javascript
    function calculate
    ```
3. **Trigger Copilot:** Press `Ctrl+Enter` (Cmd+Enter on Mac) to view all suggestions
4. **Accept suggestion:** Press `Tab` or `Enter` to accept the first suggestion
5. **Document:** Note the function name and parameters Copilot generated

#### Part B: Minimal Comments
1. Create: `File` → `New File` → Save as `exercise1-minimal-comments.js`
2. Type exactly:
    ```javascript
    // calculate shipping
    function calculate
    ```
3. Press `Ctrl+Enter` to view suggestions
4. **Compare:** Is the generated code more specific than Part A?

#### Part C: Well-Structured Comments
1. Create: `File` → `New File` → Save as `exercise1-structured-comments.js`
2. Copy and paste the full comment block:
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
3. Press `Ctrl+Enter` → select suggestion → `Tab` to accept
4. **Test the function:** Add these test cases below:
    ```javascript
    console.log(calculateShipping('domestic', 3, 50));
    console.log(calculateShipping('international', 2, 120));
    ```
5. Run: `node exercise1-structured-comments.js`

#### Deliverable:
Create `exercise1-observations.md` with:
- Suggestions received in each part
- Quality comparison (specificity, completeness)
- Best performing approach

---

### Exercise 2: Poor vs Effective Prompts (10 minutes)

**Objective:** Practice identifying and improving prompts.

#### Part A: Demonstrate Poor Prompts
1. Create: `exercise2-poor.js`
2. Try each vague prompt separately, note results:
    ```javascript
    // get data
    function fetchData
    ```
    ```javascript
    // validate
    function validate
    ```
    ```javascript
    // save to database
    function save
    ```

#### Part B: Rewrite with Four-Pillar Structure
1. Create: `exercise2-improved-prompts.js`
2. For each poor prompt, use this structure:

**Improved Prompt 1:**
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
function fetchUserData
```

3. **Your task:** Create improved versions for "validate" and "save" prompts
4. Accept all generated code and save

#### Deliverable:
`exercise2-improved-prompts.js` with three improved prompts and generated functions

---

### Exercise 3: Role-Based Prompts (5 minutes)

**Objective:** Use role-based prompting to influence code style and priorities.

#### Steps:
1. Create: `exercise3-role-based.js`
2. Add all three role-based prompts sequentially:

**Prompt 1 - Security Engineer:**
```javascript
// As a security engineer, create a password validation function
// Requirements:
// - Minimum 12 characters
// - Must include uppercase, lowercase, numbers, and special characters
// - Check against common password list (simple implementation)
// - No sequential characters (abc, 123)
function validatePasswordSecure
```

**Prompt 2 - UX Designer:**
```javascript
// As a UX designer, create a password validation function
// Requirements:
// - Provide helpful, user-friendly error messages
// - Return specific feedback for each failed rule
// - Include a strength indicator (weak/medium/strong)
// - Suggest improvements to user
function validatePasswordUX
```

**Prompt 3 - Performance Engineer:**
```javascript
// As a performance engineer, create a password validation function
// Requirements:
// - Optimize for speed (process 1000+ passwords/second)
// - Use efficient regex patterns
// - Avoid unnecessary string operations
// - Return boolean only
function validatePasswordPerformance
```

3. Accept all generated functions
4. **Test:** Add test calls at the bottom to verify all three work

#### Deliverable:
`exercise3-role-based.js` with all three functions

---

### Exercise 4: Iterative Refinement (5 minutes)

**Objective:** Refine prompts progressively to improve output quality.

**Scenario:** Filter and sort products

1. Create: `exercise4-iterations.js`
2. **Iteration 1** - Basic prompt:
    ```javascript
    // Filter products by category
    function filterProducts
    ```
    Accept suggestion → Comment as "// Iteration 1"

3. **Iteration 2** - Add constraints:
    ```javascript
    // Filter products by category
    // Sort by price (low to high)
    // Only show in-stock items
    function filterProducts
    ```
    Accept → Comment as "// Iteration 2"

4. **Iteration 3** - Add examples:
    ```javascript
    // Filter products by category and price range
    // Sort by price (ascending) or name (alphabetical)
    // Only show in-stock items with minimum rating
    // Examples:
    // filterProducts(products, 'electronics', 0, 500, 'price', 4.0)
    // filterProducts(products, 'books', 0, 50, 'name', 3.5)
    function filterProducts
    ```
    Accept → Comment as "// Iteration 3"

5. **Iteration 4** - Full context:
    ```javascript
    // E-commerce product filtering for search results page
    // Filter products by category and price range
    // Sort by price (ascending) or name (alphabetical)  
    // Only show in-stock items with minimum rating
    // Handle empty results gracefully
    // Expected input: Array of {id, name, category, price, inStock, rating}
    // Expected output: Filtered and sorted array
    // Examples:
    // filterProducts(products, 'electronics', 0, 500, 'price', 4.0) -> [{...}]
    // filterProducts(products, 'books', 0, 50, 'name', 3.5) -> []
    function filterProducts
    ```
    Accept → Comment as "// Iteration 4"

#### Deliverable:
`exercise4-iterations.js` showing all four iterations with quality progression notes

---

## Bonus Challenge

**Objective:** Apply all techniques to a real component.

Create a prompt for a React dashboard card component:

```javascript
// Intent: Create a reusable dashboard metric card component
// Context: React with TypeScript, Material-UI styling
// Constraints:
// - Display metric title, value, trend indicator (up/down)
// - Show timestamp in footer with "View Details" link
// - Support mobile/desktop responsive layouts
// - Hover effect with shadow enhancement
// - Color props: primary blue, success green, danger red
// Examples:
// <MetricCard title="Revenue" value="$12,450" trend="up" timestamp="2 hours ago" />
// <MetricCard title="Users" value="1,245" trend="down" timestamp="1 hour ago" />
function MetricCard
```

#### Deliverable:
`bonus-metric-card.tsx` with generated component

---

## Verification Checklist (VS Code 1.84+)

- [ ] GitHub Copilot extension installed and active
- [ ] Signed in to GitHub account
- [ ] `Ctrl+Enter` opens suggestion panel
- [ ] `Tab` accepts suggestions without autocomplete interference
- [ ] All 4 exercises completed
- [ ] Generated code runs without errors
- [ ] Observation notes document quality progression

## Troubleshooting

| Issue | Solution |
|-------|----------|
| Copilot icon shows error | Sign out/in: Click Copilot → "Sign out" → "Sign in to GitHub" |
| No suggestions appear | Press `Ctrl+Enter` explicitly; check internet connection |
| Suggestions are generic | Add more context, constraints, and examples |
| Want alternative suggestions | Press `Alt+]` (next) or `Alt+[` (previous) |
| Extension won't load | Reinstall: Extensions → GitHub Copilot → Uninstall → Reload → Reinstall |

## Key Takeaways

1. **Context matters:** Well-commented code → better suggestions
2. **Structure helps:** Intent + Context + Constraints + Examples = consistent results
3. **Roles influence output:** Different perspectives = different code styles
4. **Iteration improves quality:** Refining prompts incrementally produces better code
5. **Examples guide behavior:** Specific I/O examples improve accuracy

