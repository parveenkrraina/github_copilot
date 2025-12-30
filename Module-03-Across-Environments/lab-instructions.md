# Lab 3: Using Copilot Across Different Environments

**Duration:** 30 minutes  
**Difficulty:** Intermediate

## Lab Objectives

- Use GitHub Copilot effectively in your IDE
- Master inline suggestions vs Copilot Chat
- Practice CLI Copilot commands
- Understand when to use each interface
- Avoid common misuse patterns

## Prerequisites

- IDE installed (VS Code, Visual Studio, or JetBrains)
- GitHub Copilot extension installed
- GitHub CLI installed (optional)
- Copilot subscription active

## Exercise 1: IDE Mastery (10 minutes)

### Task 1: Inline Suggestions
1. Create a new file `calculator.js`
2. Type: `// Function to calculate compound interest`
3. Press Enter and let Copilot suggest
4. Try `Alt+]` to see next suggestions
5. Press `Ctrl+Enter` to see all suggestions
6. Accept the best one

### Task 2: Keyboard Shortcuts
Practice these shortcuts 10 times each:
- Accept suggestion: `Tab`
- Reject: `Esc`
- Next: `Alt+]`
- Previous: `Alt+[`
- All suggestions: `Ctrl+Enter`

### Task 3: Context Awareness
1. Open 3 related files (model, controller, service)
2. Create a new function in one file
3. Notice how Copilot uses context from other files
4. Document your observations

**Deliverable:** `exercise1-shortcuts-mastery.md`

## Exercise 2: Chat vs Inline (8 minutes)

### Scenario 1: New Function (Use Inline)
Write a function to validate credit card numbers using inline suggestions only.

### Scenario 2: Refactoring (Use Chat)
Take a complex function and use Chat to refactor it step by step.

### Scenario 3: Documentation (Use Chat)
Use `/doc` command to document your code.

**Deliverable:** Document when inline worked better vs when chat was superior

## Exercise 3: CLI Copilot (7 minutes)

Install and use GitHub CLI Copilot:

```bash
# Install extension
gh extension install github/gh-copilot

# Try these commands
gh copilot suggest "find all large files"
gh copilot explain "git reset --hard HEAD~1"
gh copilot suggest "compress folder to tar.gz"
```

**Deliverable:** List 5 commands you generated and tested

## Exercise 4: Decision Making (5 minutes)

For each task, identify the best interface:

1. "Write a sorting algorithm" → ?
2. "Explain this regex pattern" → ?
3. "Generate unit tests" → ?
4. "Find processes using port 8080" → ?
5. "Refactor this 200-line function" → ?

**Deliverable:** `exercise4-interface-decisions.md`

## Key Takeaways

- Inline for coding, Chat for thinking, CLI for terminal
- Master keyboard shortcuts for efficiency
- Context is key - keep relevant files open
- Use the right tool for each task

## Next Module

[Module 4: Management & Customization](../Module-04-Management-Customization/lab-instructions.md)
