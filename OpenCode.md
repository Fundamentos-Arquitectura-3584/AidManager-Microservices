## OpenCode.md

### Commands:

```bash
dotnet build
dotnet test
dotnet lint # Assuming there's a linting tool configured
dotnet test <ProjectName> <TestName> # Run a single test
```

### Code Style Guidelines:

*   **Language:** C#
*   **Naming Conventions:** PascalCase for classes and methods, camelCase for variables.
*   **Imports:** Use explicit imports to avoid namespace collisions.
*   **Formatting:** Follow .NET coding conventions (e.g., use Visual Studio's default formatting).
*   **Types:** Use explicit types instead of `var` when the type is not immediately obvious.
*   **Error Handling:** Use try-catch blocks for exception handling. Log exceptions with meaningful messages.
*   **Asynchronous Programming:** Use `async` and `await` for asynchronous operations.
*   **Configuration:** Use `appsettings.json` for configuration settings.
*   **Dependency Injection:** Use dependency injection for loose coupling.

### Notes:

*   This codebase uses .NET.
