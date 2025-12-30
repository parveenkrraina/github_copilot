# Lab 8: Agent-Based Development Workflow

**Duration:** 30 minutes

## Objectives
- Use Copilot in different agent roles
- Apply multi-agent collaboration
- Improve code through agent workflows
- Practice agent-to-agent thinking

## Exercise 1: Developer Agent (5 min)
As a developer, implement a feature:
```
"As a developer, implement a user registration system
with email validation and password hashing."
```

## Exercise 2: Reviewer Agent (5 min)
As a reviewer, critique the code:
```
"As a code reviewer, analyze this registration code for:
- Security issues
- Best practices
- Potential bugs
- Improvements"
```

## Exercise 3: Developer Response (5 min)
As a developer, address feedback:
```
"As a developer, address these review comments:
[paste feedback]"
```

## Exercise 4: QA Agent (5 min)
As QA, create test suite:
```
"As a QA engineer, create comprehensive tests 
including edge cases and security scenarios."
```

## Exercise 5: Complete Workflow (10 min)
Implement a feature using all agents:
1. Developer: Implement password reset
2. Reviewer: Review implementation
3. Developer: Fix issues
4. QA: Create tests
5. Security: Security audit
6. Documentation: Create docs

**Deliverables:**
- Code from each agent perspective
- Documentation of agent interactions
- Final refined implementation
- Complete test suite

## Key Takeaways
- Different roles provide different insights
- Multi-agent review catches more issues
- Iteration improves quality
- Agent workflows mirror real processes


### Exercise 1: Developer Agent
1. Open Copilot Chat in your IDE
2. Copy the developer prompt into the chat window
3. Add context: "I'm building a Node.js/Express application"
4. Copilot will generate registration code with validation and hashing
5. Save the generated code to `user-registration.js`
6. Test it locally: `node user-registration.js`

### Exercise 2: Reviewer Agent
1. Copy your generated registration code into Copilot Chat
2. Paste the reviewer prompt
3. Copilot will identify:
    - Missing input sanitization
    - Weak password requirements
    - Missing error handling
4. Document each finding in a `review-findings.md` file
5. Note which issues are critical vs. nice-to-have

### Exercise 3: Developer Response
1. In Copilot Chat, use `/fix` command on the code
2. Paste the review findings
3. Ask Copilot to address each issue systematically
4. Compare old vs. new implementation
5. Save the improved code to `user-registration-v2.js`
6. Test edge cases: empty fields, invalid emails, weak passwords

### Exercise 4: QA Agent
1. Start a new Copilot Chat session
2. Paste the improved registration code
3. Use the QA prompt to generate tests
4. Copilot should generate Mocha/Jest tests covering:
    - Valid registration
    - Invalid emails
    - Weak passwords
    - Duplicate accounts
    - SQL injection attempts
5. Save tests to `user-registration.test.js`
6. Run: `npm test` to validate all tests pass

### Exercise 5: Complete Workflow
1. Choose a feature (password reset recommended)
2. Follow steps 1-4 for each agent in sequence
3. Create a `workflow-log.md` documenting:
    - Developer's implementation
    - Reviewer's findings
    - Developer's fixes
    - QA's test coverage
    - Security concerns addressed
4. Final deliverable: Working code with 100% test coverage