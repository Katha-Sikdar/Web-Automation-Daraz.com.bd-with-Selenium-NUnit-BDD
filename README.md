# Daraz.Automation.BDD

This repository contains a small SpecFlow + Selenium + NUnit BDD test project for daraz.com.bd.

Quick start (macOS):

1. Ensure .NET SDK (10+) is installed.
2. Ensure Google Chrome is installed and a matching ChromeDriver is available. The project uses WebDriverManager to fetch drivers automatically, but local Chrome/driver version mismatch may cause failures.

Run tests:

```bash
# restore and build
dotnet build

# run tests
dotnet test
```

Notes:
- Do not commit generated SpecFlow code-behind files (`Features/*.feature.cs`). They are generated at build time. This project now ignores them via `.gitignore`.
- If tests fail with a `session not created` error referencing Chrome/ChromeDriver versions, update Chrome or configure a matching driver. On macOS you can upgrade Chrome via Homebrew cask:

```bash
brew update
brew install --cask google-chrome
```

or use WebDriverManager to manage drivers at runtime.

If you want, I can:
- Add a CI workflow that runs tests in a reproducible environment (recommended).
- Tidy Hook.Setup to detect and select exact driver versions automatically.
