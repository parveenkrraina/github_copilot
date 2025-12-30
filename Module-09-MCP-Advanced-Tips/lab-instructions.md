# Lab 9: MCP Concepts and Future-Ready Skills

**Duration:** 60 minutes

## Objectives
- Understand MCP (Model Context Protocol) principles and architecture
- Apply MCP-style thinking to current Copilot workflows
- Create structured specifications for design-to-code workflows
- Build reusable prompt templates for future AI capabilities

---

## Exercise 1: MCP Concepts & Rich Context (20 min)

### Task 1.1: Understanding MCP Layers (5 min)
MCP has three core layers: Resources, Tools, and Prompts.

**Step 1:** Read the MCP concept
- **Resources:** Structured data (schemas, configs, docs)
- **Tools:** Actions that modify state (queries, deployments)
- **Prompts:** Reusable instruction templates

**Step 2:** In your IDE, create a file `mcp-example.md`:
```markdown
# My First MCP Concept

## Resources (Data Layer)
- User database schema
- API documentation
- Design tokens from Figma

## Tools (Action Layer)
- Query executor for database
- API endpoint tester
- Component renderer

## Prompts (Instruction Layer)
- "Generate CRUD operations for [resource]"
- "Create tests for [endpoint]"
```

**Step 3:** Verify you've identified all three layers.

### Task 1.2: Convert Vague Prompts to MCP-Style (10 min)

**Step 1:** For each vague prompt below, add Resources, Tools, and Prompts structure.

**Vague Prompt 1:** "Create an API client"

**Step 2:** Rewrite with MCP structure:
```javascript
/*
MCP STRUCTURE:

Resources:
- /docs/api-endpoints.md (base URL, auth method, rate limits)
- /schemas/response-models.ts (User, Order, Product types)
- /config/environment.json (API_KEY, API_URL)

Tools:
- httpClient.get(endpoint, headers)
- httpClient.post(endpoint, body, headers)
- logger.request(method, endpoint, duration)

Prompt:
"Create an API client that:
1. Uses provided response models (schemas)
2. Implements auth headers from config
3. Logs all requests with httpClient.request()
4. Returns typed responses matching /schemas/"
*/
```

**Step 3:** Apply this pattern to:
1. "Validate form data" → add validation rules, error messages, edge cases
2. "Generate report" → add data source, format requirements, output location

**Deliverable:** Save all three rewritten prompts in `exercise-1-prompts.md`

---

## Exercise 2: Figma-to-Code Workflow (20 min)

### Task 2.1: Extract Figma Design Information (7 min)

**Step 1:** Create a new file `figma-login-spec.md`

**Step 2:** Document design structure in 3 sections:

**Section A: Component Hierarchy**
```markdown
## Component Hierarchy

Login Page (1200px × 800px)
├── Header Container (1200px × 100px)
│   ├── Logo Image (48px × 48px, top-left padding 20px)
│   └── Navigation Links (text, top-right)
├── Main Content (centered, 400px × 600px)
│   ├── Title "Sign In" (Heading1, #1A1A1A)
│   ├── Form Container
│   │   ├── Email Input Field
│   │   │   ├── Label "Email"
│   │   │   ├── Input (border-radius 8px)
│   │   │   └── Error Text (optional, color #FF3B30)
│   │   ├── Password Input Field (same structure)
│   │   ├── "Remember Me" Checkbox
│   │   └── "Forgot Password?" Link (color #007AFF)
│   └── Submit Button (full-width, background #007AFF)
└── Footer (1200px × 60px, text-align center)
```

**Step 3:** Verify hierarchy has proper indentation and all nested components.

**Section B: Design Tokens**
```markdown
## Design Tokens

Colors:
- Primary Blue: #007AFF
- Error Red: #FF3B30
- Text Dark: #1A1A1A
- Text Light: #A9A9A9
- Background White: #FFFFFF
- Border Gray: #E5E5EA

Typography:
- Heading1: 32px, bold, #1A1A1A
- Body: 16px, regular, #1A1A1A
- Caption: 14px, regular, #A9A9A9

Spacing:
- xs: 4px
- sm: 8px
- md: 16px
- lg: 24px
- xl: 32px

Radius:
- sm: 4px
- md: 8px
- lg: 12px
```

**Section C: Component States**
```markdown
## Component States

Email Input:
- Default: border #E5E5EA, padding 12px, bg white
- Focused: border #007AFF, shadow
- Error: border #FF3B30, error-text visible

Submit Button:
- Default: bg #007AFF, text white
- Hover: bg #0051D5
- Disabled: bg #E5E5EA, text #A9A9A9
- Loading: spinner animation
```

### Task 2.2: Convert Spec to Copilot Prompt (8 min)

**Step 1:** Open a new file `figma-to-react-prompt.md`

**Step 2:** Write a four-pillar prompt (from Module 01):

```javascript
// INTENT
// Convert Figma login design to a reusable React component

// CONTEXT
// Framework: React 18 with TypeScript
// Styling: styled-components
// Available from /utils/:
//   - validateEmail(email): boolean
//   - validatePassword(password): boolean
//   - useFormState() hook
// Design tokens in: /styles/theme.ts

// CONSTRAINTS
// - Component must accept props: onSubmit(email, password), onForgotPassword()
// - Must handle 3 input states: default, focused, error
// - Must show error messages from validation
// - Must be responsive (mobile-first, min 320px)
// - Use design tokens from /styles/theme.ts (no hardcoded colors)
// - Include accessibility: labels, aria-attributes, keyboard nav

// EXAMPLES
// <LoginForm 
//   onSubmit={(e, p) => console.log(e, p)} 
//   onForgotPassword={() => nav('/forgot')} 
// />
// Output when email invalid: <ErrorText>Invalid email format</ErrorText>
```

**Step 3:** Verify prompt includes all four pillars.

**Deliverable:** Save prompt in `figma-to-react-prompt.md`

---

## Exercise 3: Best Practices Audit (12 min)

### Task 3.1: Self-Assess Your Code (8 min)

**Step 1:** Open a recent code file you've written with Copilot help.

**Step 2:** Create audit checklist in `code-audit.md`:

```markdown
# Code Audit Checklist

## Prompt Engineering ✓
- [ ] Prompt included clear intent (what, not how)
- [ ] Context specified: framework, libraries, file paths
- [ ] Constraints listed: validation rules, boundaries, edge cases
- [ ] Examples provided: sample inputs and expected outputs
- [ ] Prompt followed four-pillar structure

## Code Quality
- [ ] Functions have single responsibility
- [ ] Nested callbacks/conditionals reduced to 2 levels max
- [ ] Variable names are descriptive (>3 chars, no single letters except i,j,k)
- [ ] Comments explain "why", not "what"
- [ ] Duplicate code extracted to reusable functions

## Testing Coverage
- [ ] Unit tests cover happy path
- [ ] Edge cases tested (empty, null, invalid input)
- [ ] Error scenarios validated
- [ ] Test descriptions explain business context
- [ ] Test coverage >70%

## Documentation
- [ ] Function signatures include JSDoc/XML comments
- [ ] API endpoints documented with request/response examples
- [ ] README explains setup and usage
- [ ] Complex logic has inline explanations
- [ ] Error messages are actionable

## Security
- [ ] User input validated before processing
- [ ] SQL/database queries use parameterized statements
- [ ] Sensitive data not logged or exposed
- [ ] Authentication/authorization enforced
- [ ] Dependencies checked for vulnerabilities

## Team Collaboration
- [ ] Code follows team style guide
- [ ] Git commit messages are descriptive
- [ ] PR includes context for why changes made
- [ ] No hardcoded secrets or credentials
- [ ] Code review comments addressed
```

**Step 3:** Check off items that pass; note failures.

### Task 3.2: Identify Improvement Areas (4 min)

**Step 1:** For 3 items marked as failed, write improvement plan:

```markdown
# Improvement Plan

## Issue 1: Nested callbacks (3 levels)
Current: `fetchUser().then(u => fetchOrders(u).then(o => ...))`
Plan: Extract to async/await, break into named functions
Copilot Prompt: "Refactor this callback chain to async/await with named functions"

## Issue 2: Missing unit test
Current: No tests for validateEmail()
Plan: Create test file with 5 scenarios (valid, invalid domain, empty, spaces, special chars)
Copilot Prompt: "Generate unit tests for validateEmail covering these cases: [list]"

## Issue 3: Hardcoded API URL
Current: `const API = "https://api.example.com"`
Plan: Move to environment variables
Copilot Prompt: "Update API_URL to use process.env.REACT_APP_API_URL"
```

**Deliverable:** Save audit and improvement plan in `code-audit.md`

---

## Exercise 4: Design Future Workflow (8 min)

### Task 4.1: Imagine Full AI Integration (8 min)

**Step 1:** Create file `future-workflow.md`

**Step 2:** Write detailed workflow assuming these capabilities:
- Full MCP server for your project
- Copilot workspace with 10K token context
- Custom agents (Developer, Reviewer, QA, Security)
- Real-time design-to-code sync

**Step 3:** Describe workflow in 4 sections:

```markdown
# My Ideal AI-Assisted Workflow

## 1. Design Phase
Timeline: Designer creates in Figma
- Figma design auto-synced to MCP Resources
- Copilot extracts component hierarchy → YAML
- Design tokens auto-published to theme.ts
- Copilot generates Storybook stories from components

## 2. Development Phase
Timeline: Developer uses IDE + Copilot
- Open Copilot Chat, reference @figma-design-spec
- Developer Agent generates React components
- Reviewer Agent checks code quality, suggests improvements
- QA Agent auto-generates unit and integration tests
- Security Agent flags input validation gaps

## 3. Testing Phase
Timeline: Automated validation
- All tests run in MCP environment
- Coverage report auto-generated
- Integration tests validate API contracts
- Visual regression tests compare screenshots
- Results feed back to Developer Agent for fixes

## 4. Deployment Phase
Timeline: Automated checks before merge
- Security scan checks dependencies, secrets
- Performance test ensures bundle size <100KB
- Documentation auto-generated from JSDoc
- Deployment checklist auto-verified
- On merge: auto-deploy to staging

## What This Enables
1. Design-to-production in hours, not days
2. Zero manual testing or documentation
3. Consistent quality across entire team
4. Knowledge sharing through agent interactions
5. More time for creative problem-solving
```

**Deliverable:** Save complete workflow in `future-workflow.md`

---

## Bonus Challenge: Design MCP Server (10 min, optional)

### Task: Document Your Project's MCP Server

**Step 1:** Create `my-mcp-server-spec.md`

**Step 2:** Define what your MCP server would expose:

```markdown
# [Project Name] MCP Server Spec

## Resources (Read-Only Data)

### database-schema
Description: Current PostgreSQL schema with all tables, columns, types
Format: JSON schema definition
Updated: Auto-sync from migrations/

### api-endpoints
Description: All REST endpoints with methods, params, responses
Format: OpenAPI 3.0 YAML
Updated: Auto-sync from code JSDoc

### design-system
Description: Components, colors, typography, spacing from Figma
Format: JSON tokens
Updated: Auto-sync from Figma exports

### test-fixtures
Description: Reusable test data for users, orders, products
Format: JSON arrays
Updated: Manual, reviewed in PR

## Tools (State-Changing Actions)

### query-database
Input: SQL string (parameters auto-escaped)
Output: JSON result set
Use case: Developer Agent needs sample data to write features

### test-endpoint
Input: method, endpoint, headers, body
Output: status, response time, response body
Use case: QA Agent validates API changes

### generate-migration
Input: schema change description
Output: Migration file with up/down scripts
Use case: Developer Agent creates DB changes

### run-tests
Input: test file path or pattern
Output: Pass/fail, coverage, execution time
Use case: QA Agent validates test suite

## Prompts (Reusable Instructions)

### generate-crud-operations
"Generate CRUD operations for [resource] using:
- Database schema from @database-schema
- API patterns from @api-endpoints
- Validation rules from [rules document]"

### refactor-legacy-callback
"Refactor this callback chain to async/await:
[code]
Must use validateEmail() from @utilities
Must log via logger.request() from @utilities"

### create-test-suite
"Generate tests for [component]:
- Use test fixtures from @test-fixtures
- Cover happy path + 3 edge cases
- Achieve >80% coverage"
```

**Deliverable:** Save complete MCP spec in `my-mcp-server-spec.md`

---

## Lab Completion Checklist

- [ ] Completed Exercise 1: MCP concepts (3 rewritten prompts)
- [ ] Completed Exercise 2: Figma workflow (spec + four-pillar prompt)
- [ ] Completed Exercise 3: Code audit (checklist + improvement plan)
- [ ] Completed Exercise 4: Future workflow (4-section document)
- [ ] (Optional) Completed bonus MCP server spec
- [ ] All files saved to `/lab-submissions/`

---

## Key Takeaways

1. **MCP Applies Today:** Structure your prompts with Resources, Tools, Prompts now
2. **Design Specs Enable Automation:** Detailed Figma specs → better code generation
3. **Audits Prevent Debt:** Regular quality reviews catch issues early
4. **Context is Multiplicative:** Each added layer (schema, tokens, examples) improves results 10×
5. **Teams Scale via Templates:** Reusable prompts let everyone generate consistent quality

---

## Reflection Questions

Answer these in a document `reflection.md`:

1. **Prompt Evolution:** How has your prompting changed from Module 01 to now?
   - What patterns work best for you?
   - Which four pillars do you tend to skip?

2. **Immediate Application:** Which 3 techniques will you use in your next project?

3. **Team Leverage:** How will you share these patterns with teammates?

4. **Future Excitement:** What feature from Exercise 4 excites you most?

5. **Course Value:** Rate each module (1-5) and explain your ratings.

---

## 🎉 Course Completion

**Congratulations!** You've completed all 9 modules of the GitHub Copilot Training Course.

### Skills Mastered:
- ✅ Four-pillar prompt engineering (Intent, Context, Constraints, Examples)
- ✅ Advanced Copilot features (/explain, /fix, /tests, /doc, Chat)
- ✅ Multi-environment proficiency (VS Code, Visual Studio, JetBrains, CLI)
- ✅ Organizational customization and team management
- ✅ Real-world developer workflows and automation
- ✅ Comprehensive test generation and validation
- ✅ Multi-language patterns (JavaScript, C#, language-agnostic)
- ✅ Agent-based collaborative workflows (Developer, Reviewer, QA, Security)
- ✅ MCP concepts and future-ready architecture

### Immediate Next Steps:
1. **This Week:** Apply one technique from Exercise 3 (code audit) to your current project
2. **This Month:** Create 3 reusable prompt templates for your team (from Exercise 1)
3. **This Quarter:** Establish code review checklist based on best practices (Module 2)
4. **Ongoing:** Share learnings in team wiki or knowledge base

### Recommended Resources:
- GitHub Copilot Official Docs: https://docs.github.com/en/copilot
- Monthly Copilot feature updates
- Advanced workshops on agents and MCP
- Your team's growing library of working prompts

---

**Thank you for investing 8 hours in mastering AI-assisted development!** 🚀

We'd love to hear your feedback:
- What was most valuable to you?
- Which module should be expanded?
- What real-world challenges are you solving?
- Any topics for future training?

Share feedback with your team lead or submit to [feedback form].

