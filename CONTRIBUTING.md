# Contributing to LuminaNotes

Thank you for considering contributing to LuminaNotes! This document outlines the process and guidelines.

## Development Setup

1. **Prerequisites**
   - Windows 11 22H2+ or Windows 12 Preview
   - Visual Studio 2025 (v17.12+)
   - .NET 9.0 SDK
   - Windows App SDK
   - Ollama (for AI features)

2. **Clone and Build**
   ```bash
   git clone https://github.com/Gzeu/LuminaNotes.git
   cd LuminaNotes
   dotnet restore
   dotnet build
   ```

3. **Run**
   ```bash
   dotnet run --project LuminaNotes.WinUI
   ```

## Branch Strategy

- `main` - Stable production code
- `develop` - Integration branch for features
- `feature/*` - New features (e.g., `feature/graph-view`)
- `fix/*` - Bug fixes
- `docs/*` - Documentation updates

## Commit Convention

Use [Conventional Commits](https://www.conventionalcommits.org/):

- `feat:` - New feature
- `fix:` - Bug fix
- `docs:` - Documentation
- `style:` - Code style (formatting)
- `refactor:` - Code refactoring
- `test:` - Tests
- `chore:` - Build/tooling

Examples:
```
feat: add markdown preview in editor
fix: resolve crash when loading encrypted notes
docs: update README with encryption setup
```

## Pull Request Process

1. Fork the repository
2. Create a feature branch from `develop`
3. Make your changes with clear commits
4. Write/update tests if applicable
5. Update documentation (README, code comments)
6. Submit PR to `develop` branch
7. Wait for code review

## Code Style

- **Naming**: PascalCase for classes/methods, camelCase for variables
- **Async**: Suffix async methods with `Async`
- **Documentation**: XML comments for public APIs
- **MVVM**: Keep ViewModels testable, UI logic in code-behind minimal

## Testing

- Unit tests for Core services
- Integration tests for database operations
- UI tests for critical user flows

Run tests:
```bash
dotnet test
```

## Reporting Issues

Use GitHub Issues with:
- Clear title and description
- Steps to reproduce
- Expected vs actual behavior
- Screenshots/logs if applicable
- Environment (Windows version, .NET version)

## Feature Requests

Open an issue with:
- Problem statement
- Proposed solution
- Alternative approaches
- Impact on existing features

## Code of Conduct

- Be respectful and inclusive
- Provide constructive feedback
- Focus on the code, not the person
- Help newcomers

## License

By contributing, you agree that your contributions will be licensed under the MIT License.
