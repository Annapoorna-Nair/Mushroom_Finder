# Community Forum Plan Research

## Decision: Reuse the Existing Razor Pages Application

**Decision**: Implement the feature in the existing .NET 8 Razor Pages project and keep its current page model/view pairing.

**Rationale**: `Mushroom_Finder.csproj` targets `net8.0`; the forum already exists as `Pages/Shared/Forum.cshtml` with `Forum.cshtml.cs`. The shared layout loads Bootstrap, `site.css`, and `site.js`.

**Alternatives considered**:

- Add a separate frontend or SPA: rejected because the feature is a single page in an established Razor Pages application.
- Add a new UI framework: rejected because local Bootstrap and browser-native capabilities are sufficient.

## Decision: Keep Feed Data Presentation-Only

**Decision**: Provide the two sample posts from the forum page model as static display data. Do not query or change the database.

**Rationale**: The feature specification explicitly excludes persistence and authentication and defines two concrete examples. The current forum page model has no handlers or data access.

**Alternatives considered**:

- Extend the EF Core model or add repositories: rejected as out of scope.
- Add an external feed/API: rejected because no external interface is required.

## Decision: Use In-Page Filtering with Native Browser JavaScript

**Decision**: Filter the rendered sample cards in the browser with a small page-specific script. Keep the selected state and focus on the activated category control, and announce the result count or empty-state text through a non-interruptive accessible status.

**Rationale**: The clarified requirements expect immediate category updates and an announcement without moving focus. A page-specific script avoids adding dependencies or affecting unrelated pages.

**Alternatives considered**:

- Full-page GET navigation for every category: rejected because it can reset focus and does not naturally provide the required in-place announcement behavior.
- A new client-side framework: rejected as unnecessary for two categories and a small static feed.

## Decision: Scope Styling to the Forum Page

**Decision**: Add a scoped Razor stylesheet at `Pages/Shared/Forum.cshtml.css` rather than broad forum rules in `wwwroot/css/site.css`.

**Rationale**: The current `site.css` contains only global defaults, and the existing shared layout uses Razor scoped CSS. Scoped styling minimizes unintended effects on the rest of the site.

**Alternatives considered**:

- Add all forum styles to global `site.css`: rejected unless a style is intentionally shared with other pages.
- Add another CSS framework: rejected because Bootstrap is already bundled.

## Decision: Preserve the Public `/Forum` Route

**Decision**: Keep the user-facing route at `/Forum`, make it reachable from the shared navigation, and allow signed-out visitors to browse without adding authentication flows or changing authentication behavior elsewhere.

**Rationale**: The shared navigation already targets `/Forum`, and the feature is browse-only. The existing page location and bare `@page` directive require route verification and a narrowly scoped adjustment if needed.

**Alternatives considered**:

- Change the navigation to a new route: rejected because `/Forum` is already the expected user-facing URL.
- Move unrelated shared pages: rejected; only the forum route should be affected.

## Decision: Use a Precise Responsive Boundary and Representative Stress Content

**Decision**: Stack the sidebar and feed below 768 CSS pixels and use two columns at 768 CSS pixels and above. Validate long-content wrapping with a 256-character title, 2,000-character body, and 64-character unbroken sequence at 320 CSS pixels; these are test fixtures, not content limits.

**Rationale**: A precise boundary removes interpretation differences and aligns with the existing Bootstrap `md` breakpoint. Representative fixtures make the wrap/no-overflow requirement reproducible without imposing a maximum post length.

## Decision: Document Example-Photo Rights

**Decision**: Use only project-owned or public-domain/CC0 photos for the two supplied examples and record each asset's source and usage-rights basis alongside it.

**Rationale**: The repository does not currently contain suitable mushroom photos, and example imagery must have clear usage rights before implementation.

## Decision: Validate with Existing Tooling and Manual UI Scenarios

**Decision**: Use the existing project build and manual browser/accessibility acceptance checks. Use current stable Chrome with NVDA on Windows and current stable Safari with VoiceOver on macOS as reference combinations, at 320, 767, 768, and 1280 CSS-pixel widths. Do not add a test framework as part of this plan.

**Rationale**: Repository inspection found no test project or test files. The critical outcomes include responsive layout, keyboard focus, screen-reader announcements, contrast, and content review; these need browser-level verification.

**Alternatives considered**:

- Add a browser automation framework: deferred because it would add dependencies and project structure not present in the repository.
- Rely on build success alone: rejected because it cannot validate filtering behavior or accessibility outcomes.
