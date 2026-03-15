# Code Review

## Scope
This review is based on the current application code inspected in controllers, models, and the game front-end files.

## Performance Errors (Sorted by Impact)

### 1) `OrderBy(Guid.NewGuid())` for random word selection in DB queries (**Critical**)
- Location: `HangMan/Controllers/GameController.cs` (`GetRandomWord`)
- Problem: Random selection uses `OrderBy(x => Guid.NewGuid())` (and may run twice due to fallback), which forces expensive sorting and poor query performance as the `Words` table grows.
- Why it hurts performance: Full-sort randomization is one of the most expensive ways to pick one row.
- Potential solution: Replace full random sorting with a more scalable approach such as querying a filtered count, generating a random offset, and selecting a row with `Skip(...).Take(1)`, or using a database-specific random function only when the dataset is small.

### 2) Full table materialization without paging for dictionary pages (**High**)
- Location: `HangMan/Controllers/GameInformationController.cs` (`Dictionary`, `FilterDictionary`)
- Problem: `ToList()` loads full result sets with `Include(w => w.Category)` and no pagination.
- Why it hurts performance: Large memory use and long response time for bigger datasets.
- Potential solution: Add paging with `Skip` and `Take`, return only the current page of records, and consider projecting only the fields required by the view.

### 3) Case-insensitive filtering done with `ToUpper().Contains(...)` on database columns (**High**)
- Location: `HangMan/Controllers/GameInformationController.cs` (`FilterDictionary`, `AddWord`)
- Problem: Applying `ToUpper()` to DB columns can reduce index usage and increases per-row processing.
- Why it hurts performance: Slower scans for text filtering on larger tables.
- Potential solution: Use database-collation-aware comparisons, `EF.Functions.Like`, or normalized searchable columns instead of wrapping the database column in `ToUpper()`.

### 4) Synchronous EF Core DB operations in request handlers (**Medium**)
- Location: `HangMan/Controllers/GameInformationController.cs`, `HangMan/Controllers/GameController.cs`
- Problem: Uses sync calls like `ToList()`, `SaveChanges()`, `FirstOrDefault()` in web request paths.
- Why it hurts performance: Blocks request threads under load and reduces throughput compared to async EF operations.
- Potential solution: Convert request-path data access to async equivalents such as `ToListAsync()`, `FirstOrDefaultAsync()`, and `SaveChangesAsync()`.

### 5) Repeated linear membership checks in game loop (`includes` on arrays) (**Low**)
- Location: `HangMan/wwwroot/js/hangman.js`
- Problem: `correct.includes(...)` and `incorrect.includes(...)` are called repeatedly; cost grows with guess history.
- Why it hurts performance: Minor currently, but avoidable by using `Set` for membership checks.
- Potential solution: Replace `correct` and `incorrect` arrays with `Set` objects for constant-time membership checks, while converting to arrays only when display formatting requires it.

#### Performance Section Summary
The largest bottlenecks are database-side: random row selection strategy, full-list loading, and non-index-friendly text filtering. These will dominate latency as data volume grows.

---

## Best-Practice and Code-Quality Gaps

### A) Persistence logic issues (functional correctness)
- `GameOverAsync` updates user stats but never persists (`SaveChanges`/`UpdateAsync` missing).
- `PlayerSettings` POST modifies preferences but does not call `SaveChanges`.
- Impact: state changes may be lost.
- Potential solution: Persist changes explicitly by calling the appropriate async save/update methods before returning from each action.

### B) Controller and DI style conventions
- Private fields are mutable and not using conventional names like `_context`, `_userManager`.
- Fields should be `readonly` when assigned only in constructor.
- Potential solution: Rename injected fields to conventional private names and mark them `readonly` to clarify intent and reduce accidental reassignment.

### C) Unused and incorrect `using` directives
- Examples: `Mysqlx.Expr`, `Microsoft.VisualStudio.TestPlatform.CommunicationUtilities` are not needed.
- Impact: unnecessary clutter and reduced maintainability.
- Potential solution: Remove unused imports and rely on automatic code cleanup or analyzer rules to keep using directives minimal.

### D) Magic string used as control flag
- Location: `AddWord` action uses `"NewCategory1234567898765432345678765432134567876543"`.
- Impact: brittle logic and difficult maintenance.
- Potential solution: Replace the magic string with a named constant, enum-backed option, or explicit form field that clearly represents the new-category workflow.

### E) Naming and typo issues
- Example: `"GameInfomation"` typo in `return View("PlayerSettings", "GameInfomation")`.
- Impact: readability issues and possible runtime/route confusion.
- Potential solution: Correct typos and align controller, action, variable, and view names with consistent project naming conventions.

### F) JavaScript robustness and safety
- `hangman.js` assumes `#game-root` always exists; no null guard.
- `innerHTML` is used where `textContent` is safer for plain text rendering.
- Potential solution: Add a guard clause when the root element is missing and prefer `textContent` for plain text output to reduce unnecessary HTML parsing and lower XSS risk.

### G) Formatting consistency
- Multiple files show inconsistent spacing and brace formatting (extra spaces, uneven style).
- Impact: lower readability and maintainability.
- Potential solution: Apply a shared formatting standard through `.editorconfig`, IDE formatting rules, and automated cleanup during commits or builds.

#### Best-Practice Section Summary
Main quality gaps are around persistence correctness, naming/style consistency, and avoidable brittleness (magic strings, unused imports, and UI script assumptions).

---

## Overall Summary
The app’s primary performance risks are concentrated in database query patterns (random selection, full result loading, and non-index-friendly filtering). The most important quality issues are missing persistence calls and inconsistent coding standards. Addressing these areas will significantly improve scalability, reliability, and maintainability.
- Potential solution: Prioritize fixing database query patterns first, then address persistence correctness, and finally standardize naming, formatting, and defensive coding practices across the project.
