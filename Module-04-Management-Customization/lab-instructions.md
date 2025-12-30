# Lab 4: Customization and Template Creation

**Duration:** 25 minutes

## Objectives
- Customize Copilot settings for your workflow
- Create reusable prompt templates
- Understand organizational policies
- Build a personal template library

## Exercise 1: IDE Customization (5 min)
Configure Copilot settings in VS Code (version 1.107+) with current features:

### Step 1: Access Copilot Settings
1. Open VS Code (version 1.107 or later)
2. Click the Copilot icon in the Status Bar (bottom right)
3. Select **Set up Copilot** to sign in with your GitHub account
4. Open Settings (Ctrl+,) and search for "@ext:GitHub.copilot" to access Copilot extension settings

### Step 2: Configure Inline Suggestions
1. Search for "Copilot: Inline Suggest Show" in Settings
2. Set inline suggestion behavior:
   - **Enabled** (default) - Get ghost text suggestions as you type
   - **Disabled** - Only show suggestions on manual trigger
3. Adjust "Copilot: Inline Suggest Delay" (recommended: 100-200ms)
4. **Test:** Start typing `function factorial(` in a new .js file and observe ghost text suggestions

### Step 3: Configure Suggestion Behavior
1. Search for "Copilot: Suggestion" settings
2. Set max visible suggestions (recommended: 3-5)
3. Enable/disable for specific file types:
   - Enable: `.js`, `.ts`, `.cs`, `.py`
   - Disable: `.md`, `.txt`, `.json` (config files)
4. Configure "Code Autocompletion" settings
5. **Test:** Open different file types and verify Copilot activation

### Step 4: Configure Keyboard Shortcuts
1. Go to Keyboard Shortcuts (Ctrl+K Ctrl+S)
2. Search for "copilot" to view all available commands
3. Set your preferred shortcuts for:
   - **Accept inline suggestion** (default: Tab)
   - **Dismiss suggestion** (default: Escape)
   - **Open Copilot Chat** (default: Ctrl+Alt+I)
   - **Trigger inline completion** (Ctrl+I for editor inline chat)
   - **Request Copilot fix** (available in code editors)
4. Choose shortcuts that don't conflict with existing bindings
5. **Deliverable:** Document your custom shortcut configuration

### Step 5: Customize with Custom Instructions
1. Create a `.copilot-instructions.md` file at project root
2. Add your coding style guidelines:
   ```markdown
   ---
   applyTo: "**"
   ---
   # My Coding Style
   - Use arrow functions for components
   - Prefer const over let
   - Always include TypeScript types
   - Use descriptive variable names
   - Follow Repository pattern for data access
   ```
3. This applies to all chat requests automatically
4. **Reference:** [Custom Instructions Documentation](https://code.visualstudio.com/docs/copilot/customization/custom-instructions)

## Exercise 2: Create Prompt Templates (10 min)
Build templates for each use case using the four-pillar structure, compatible with current Copilot Chat and Agents (December 2025).

### Template 1: Autonomous Coding with Agent
1. **Use Case:** Complex multi-step development tasks
2. **Template prompt:**
   ```
   // Agent Task: Build a feature with autonomous execution
   // Task: Create a node.js web app for [specific domain]
   // Requirements:
   // - Generate code across multiple files
   // - Install necessary dependencies automatically
   // - Follow current code structure patterns
   // - Make the app modern and responsive
   // Success Criteria: App runs without errors, functional features work
   ```
3. **How to use:** Open Chat (Ctrl+Alt+I), select "Agent" from agent picker, paste and customize
4. Test the agent's autonomous execution capabilities
5. Refine requirements based on generated structure

### Template 2: Editor Inline Chat
1. **Use Case:** Refactoring or explaining code in context
2. **Template prompt:**
   ```
   // Intent: Refactor or explain selected code
   // Context: [Framework/library being used]
   // Task: Refactor this [component/function/service] to:
   // - Improve readability
   // - Reduce nesting depth
   // - Use modern syntax patterns
   // - Maintain current functionality
   // Preserve: Input/output types, error handling
   ```
3. **How to use:** Select code → Press Ctrl+I → Type prompt → Review changes
4. Test with real code from your project

### Template 3: Code Analysis & Review
1. **Use Case:** Security, performance, and code quality analysis
2. **Template prompt:**
   ```
   // Intent: Analyze code for issues
   // Analysis Type: [security/performance/maintainability]
   // Focus Areas:
   // - Identify potential [security vulnerabilities/performance bottlenecks]
   // - Suggest improvements for [specific concern]
   // - Consider: [framework best practices, current standards]
   // Provide: Specific issues with line references and solutions
   ```
3. **How to use:** Chat (Ctrl+Alt+I) or inline chat (Ctrl+I on code selection)
4. Apply suggested fixes using Copilot's code change capability

### Template 4: Smart Actions
1. **Use Case:** Generate commit messages, PR descriptions, documentation
2. **Template prompt:**
   ```
   // Intent: Generate documentation/commit message
   // Type: [JSDoc comments/commit message/PR description]
   // Context: Changes made in [files]
   // Style: [detailed/concise], include [examples/breaking changes]
   // Format: Follow [existing style/standard format]
   ```
3. **How to use:** Use Copilot Smart Actions from command palette (Ctrl+Shift+P)
4. Customize generated output as needed

### Template 5: Language Model Selection
1. **New Feature:** Switch between AI models for different tasks
2. **When to use different models:**
   - Fast models: Quick inline suggestions, real-time feedback
   - Reasoning models: Complex problem-solving, architecture decisions
   - Specialized models: Domain-specific tasks
3. **How to use:** Click model dropdown in Chat view and select appropriate model
4. **Test:** Compare outputs from different models for the same prompt

**Deliverable:** `prompt-templates.md` with all five current-version templates documented

## Exercise 3: Team Standards with Custom Instructions (5 min)
Document your team's coding standards using Copilot's custom instructions feature (new in December 2025 version).

### Step 1: Define Core Coding Style
1. Document variable naming rules:
   - camelCase for variables/functions (e.g., `getUserData`)
   - PascalCase for classes/components (e.g., `UserCard`)
   - UPPER_SNAKE_CASE for constants (e.g., `MAX_RETRIES`)
   - Prefix for private members if applicable (e.g., `_privateMethod`)
2. Create style guidelines that Copilot will follow:
    ```markdown
    # Team Coding Standards
    
    ## Naming Conventions
    - Use camelCase for variables and functions
    - Use PascalCase for classes and React components
    - Use UPPER_SNAKE_CASE for constants
    - Avoid single-letter variable names (except loop counters)
    
    ✓ Good: const userName = 'John'
    ✗ Bad: const user_name = 'John' or const u = 'John'
    ```

### Step 2: Define Error Handling & Logging Patterns
1. Document your team's error handling approach:
    - Try-catch blocks for synchronous code
    - Promise rejection handling for async code
    - Logging strategy using structured logging
    - Custom error classes with context information
2. Add to custom instructions:
    ```markdown
    ## Error Handling
    - Use try-catch for async operations
    - Include context in error logs: { error, context, userId }
    - Create custom error classes extending Error
    - Always provide user-friendly error messages
    ```

### Step 3: Define AI-Assisted Documentation Requirements
1. Specify what Copilot should generate:
    - Function JSDoc with parameter types and return values
    - Practical usage examples
    - Side effect documentation
    - TypeScript interface definitions where applicable
2. Add documentation standards:
    ```markdown
    ## Documentation
    - All functions must have JSDoc comments
    - Include @param @returns @throws @example tags
    - Add usage examples for complex functions
    - Keep comments current with code changes
    ```

### Step 4: Create `.copilot-instructions.md` File
1. Create at your project root with content:
    ```markdown
    ---
    applyTo: "**"
    ---
    # Team Coding Standards
    
    ## Code Style
    - Use arrow functions for components and callbacks
    - Prefer const over let; never use var
    - Always include TypeScript types
    - Use descriptive variable names
    - Follow Repository pattern for data access
    
    ## Error Handling
    - Wrap async operations in try-catch
    - Log errors with context information
    - Create custom Error classes
    - Provide meaningful error messages
    
    ## Documentation
    - Write JSDoc for all public functions
    - Include usage @example tags
    - Document TypeScript interfaces
    - Add comments for complex logic
    
    ## Testing
    - Write unit tests alongside code
    - Use Arrange-Act-Assert pattern
    - Mock external dependencies
    - Aim for 80%+ code coverage
    ```
2. Commit to version control so all team members share standards
3. Copilot automatically applies these instructions to all chat requests

### Step 5: Validate with Copilot Chat
1. Open Copilot Chat (Ctrl+Alt+I)
2. Ask: "Generate a function to fetch user data following our team standards"
3. Verify the generated code follows your custom instructions
4. Update `.copilot-instructions.md` if output doesn't match expectations

**Deliverable:** `.copilot-instructions.md` file in project root with team standards

## Exercise 4: Custom Agents & Tools Integration (5 min)\nLeverage Copilot's custom agents and MCP server tools for team workflows (December 2025 features).

## Exercise 4: Custom Agents & Tools Integration (5 min)
Leverage Copilot's custom agents and MCP server tools for team workflows (December 2025 features).

### Step 1: Understand Available Agents
1. Open Copilot Chat (Ctrl+Alt+I)
2. Click the agent picker (dropdown at top) to see available agents:
    - **Chat Agent:** Ask questions, get explanations
    - **Edit Agent:** Make code changes across files
    - **Agent:** Autonomous task execution with multi-step planning
3. Review which agent suits each type of work

### Step 2: Create a Custom Agent for Your Workflow
1. Access custom agent configuration:
    - Open Command Palette (Ctrl+Shift+P)
    - Search for "Copilot: Create Agent" or check Settings
2. Define your custom agent:
    ```json
    {
      "name": "Architecture Agent",
      "description": "Specializes in planning and architectural discussions",
      "instructions": "Focus on system design, scalability, and patterns. Ask clarifying questions about requirements.",
      "tools": ["codebase-analyzer", "documentation-search"]
    }
    ```
3. Specify which tools the agent can access
4. Add custom instructions for the agent's specific role

### Step 3: Configure MCP Servers for Enhanced Tools
1. **New Feature:** Extend Copilot with Model Context Protocol (MCP) servers
2. Add MCP tools for specialized tasks:
    - Database query tools
    - External API connections
    - Git repository operations
    - Specialized domain tools
3. In VS Code settings, add MCP server configuration:
    ```json
    "[copilot]": {
      "mcp_servers": [
         {
            "name": "database-tools",
            "url": "http://localhost:3000"
         }
      ]
    }
    ```
4. Test MCP tool availability in Agent mode

### Step 4: Build Team Template Repository
1. Create organized template library structure:
    ```
    templates/
    ├── agents/
    │   ├── architect-agent.md
    │   ├── qa-agent.md
    │   └── security-agent.md
    ├── prompts/
    │   ├── api-design.md
    │   ├── testing-strategy.md
    │   └── refactoring.md
    ├── instructions/
    │   └── .copilot-instructions.md (symlink to project root)
    └── README.md
    ```
2. Document each agent's:
    - Purpose and use cases
    - Custom instructions
    - Recommended tools to enable
    - Example prompts
3. Create README with agent selection guide:
    ```markdown
    ## When to Use Each Agent
    
    | Agent | Best For | Example |
    |-------|----------|---------|
    | Chat | Understanding code | "Explain this authentication flow" |
    | Edit | Code changes | "Add error handling" |
    | Architecture | Design decisions | "Design a microservices approach" |
    | Security | Vulnerability detection | "Find security issues" |
    ```

### Step 5: Optimize Language Model Selection
1. **New Feature:** Choose models based on task complexity
2. Configure model preferences:
    - Fast models for real-time suggestions
    - Reasoning models for complex architecture decisions
    - Specialized models for specific domains
3. Document team guidance:
    ```markdown
    ## Language Model Selection
    
    - Quick code suggestions: Use fastest model
    - API design review: Use reasoning model
    - Security analysis: Use specialized security model
    - Component refactoring: Use reasoning model
    ```
4. Test different models on same prompt to compare quality
5. Add model selection to relevant agent instructions

**Deliverable:**
- Custom agent definition for your team workflow
- MCP configuration file with enabled tools
- Team agent selection guide in README
- Template repository structure with all agent types
- Model selection guidelines document

## Key Takeaways
- Customization improves workflow
- Templates save time and ensure consistency
- Team standards guide Copilot behavior
- Reusable templates increase productivity
