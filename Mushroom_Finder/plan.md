# Implementation Plan: Mushroom Search

**Status:** Proposed
**Feature:** Mushroom Search
**Target:** .NET 8 ASP.NET Core Razor Pages
**Specification:** `Mushroom_Finder/spec.md`

## 1. Objective

Implement the approved Mushroom Search feature while retaining the existing `Pages/Search_cs.cshtml` filename and route and preserving the existing visual styling in `wwwroot/css/site.css`.

The implementation will move the current mushroom records into a strongly typed in-memory catalog service, expose them through the Razor PageModel, render the cards from typed data, and retain client-side filtering by mushroom name, Region, and Season. The page will provide ARIA-accessible controls and no-results feedback. Automated tests will verify the catalog service and Razor Page behavior.

Implementation must not begin until the user explicitly issues `speckit.implement`, as required by `constitution.md`.

## 2. Governing Requirements

- Target .NET 8 and use the existing ASP.NET Core Razor Pages architecture.
- Keep `Pages/Search_cs.cshtml` and `Pages/Search_cs.cshtml.cs`; do not rename the page or change its route.
- Keep filtering client-side and reset search/filter values on page load.
- Do not persist criteria in query strings, browser storage, or server session state.
- Use a strongly typed in-memory service/model for the initial mushroom catalog.
- Start with the fixed Region options North America, Europe, and Asia.
- Start with the fixed Season options Spring, Summer, Fall, and Winter.
- Structure catalog data so future Region and Season options can become data-driven without redesigning the page contract.
- Treat an empty or unavailable catalog as the existing no-results state.
- Preserve the existing CSS classes and styling in `wwwroot/css/site.css`; only add or adjust styles if a required accessibility or layout issue cannot be addressed through markup.
- Keep PageModel responsibilities limited to request orchestration and page data preparation.
- Keep output safely encoded and avoid exposing diagnostics or sensitive information to users.

## 3. Proposed Design

### 3.1 Strongly typed domain model

Create a `Mushroom` model in a model/domain location consistent with the project structure. The model should contain the data currently represented by markup:

- Display name.
- Search-normalized or searchable name representation, if needed by the view contract.
- Image path.
- Alternative text for the image.
- A collection of Region values.
- A user-facing Region display value, or a presentation-safe way to format the Region collection.
- A collection of Season values.
- A user-facing Season display value, allowing labels such as `Late Summer – Fall` and `Fall – Early Winter` while retaining filterable values.

Prefer immutable or read-only collection properties so catalog entries cannot be modified accidentally by the page. Region and Season values should be represented as values suitable for exact membership checks rather than relying on substring matching in comma-separated strings.

### 3.2 In-memory catalog service

Create an abstraction such as `IMushroomCatalog` and an in-memory implementation such as `InMemoryMushroomCatalog`.

The service should:

- Return the initial three catalog entries: Lions Mane, Wood Blewit, and Amethyst Deceiver.
- Expose read-only data to callers.
- Keep catalog ownership separate from Razor markup.
- Support an empty collection without throwing so the page can show the existing no-results message.
- Be straightforward to replace later with a persistence-backed implementation.
- Avoid unnecessary asynchronous work for the in-memory implementation; if the abstraction is designed for future I/O, use an API that can evolve without blocking operations.

The service should be deterministic and should not apply the client-side search criteria itself; filtering remains a browser interaction for this feature.

### 3.3 Dependency injection

Register the catalog abstraction and in-memory implementation in `Program.cs` with an appropriate lifetime. Use the interface in the PageModel rather than constructing the service directly.

The registration must not alter existing Identity, Entity Framework, routing, static-file, or authorization configuration.

### 3.4 Razor PageModel

Update `Pages/Search_cs.cshtml.cs` to inject the catalog service and expose a strongly typed read-only collection for the page.

The PageModel should:

- Load catalog entries during the existing GET request.
- Provide safe page data to the Razor view.
- Treat a null, empty, or unavailable catalog as an empty collection for the no-results presentation.
- Avoid containing filtering, DOM, or presentation-formatting logic that belongs in the page or client script.
- Preserve the existing `Search_csModel` class and page route.

The PageModel MUST handle catalog failures predictably by recording supported diagnostic logging and presenting only the safe no-results state required by the specification. Diagnostics MUST NOT be exposed in the user-facing response.

### 3.5 Razor markup

Update `Pages/Search_cs.cshtml` to render the mushroom cards with a `foreach` over the PageModel collection rather than hard-coded repeated cards.

Retain the existing classes used by `site.css`, including:

- `search-page-wrapper`
- `search-banner`
- `search-controls`
- `search-bar-input`
- `filter-row`
- `filter-pill`
- `mushroom-grid`
- `mushroom-card`
- `mushroom-img-box`
- `mushroom-label-box`
- `mushroom-name`
- `mushroom-meta`
- `no-results-msg`

Render safe typed values for image source, alternative text, name, Region details, and Season details. Preserve the current labels and display wording.

Retain the fixed Region and Season options initially. Ensure their values match the normalized values used by the client-side filtering contract.

### 3.6 ARIA and keyboard accessibility

Update markup without changing the existing visual design:

- Give the search input an explicit accessible label and associate it with the result region through `aria-controls`.
- Give each select an explicit accessible label; visible labels may be added if they can use the existing layout without disrupting styling.
- Give the result region an identifiable label or `aria-labelledby` relationship.
- Mark the no-results message as a status, using an appropriate `role` and `aria-live` behavior so changes are announced without overwhelming screen readers.
- Ensure hidden cards are removed from the accessibility tree when filtered out, using a reliable hidden/display mechanism.
- Keep all controls keyboard operable and do not introduce pointer-only interactions.

### 3.7 Client-side filtering

Retain client-side filtering in the Search Page and update it to operate on the rendered typed-card data attributes.

Required behavior:

1. Read and trim the search input.
2. Match names case-insensitively using partial-name matching.
3. Treat an empty or whitespace-only name query as unrestricted.
4. Match Region and Season by normalized exact membership within their associated values, avoiding accidental substring matches.
5. Apply name, Region, and Season criteria with AND semantics.
6. Display a card only when all active criteria match.
7. Hide the card from both visual and assistive-technology presentation when it does not match.
8. Show the exact existing no-results text when the visible result count is zero.
9. Hide the no-results status when one or more cards match.
10. Respond to `input` changes for the name field and `change` events for both selectors.
11. Do not write criteria to the URL or browser storage; page reload returns to the defaults.

Keep the script scoped to the Search Page and avoid assumptions that controls exist on other pages.

## 4. File-Level Work Plan

### Application files to add

- `Mushroom_Finder/Models/Mushroom.cs` — strongly typed mushroom record/model and read-only Region/Season data.
- `Mushroom_Finder/Services/IMushroomCatalog.cs` — catalog service contract.
- `Mushroom_Finder/Services/InMemoryMushroomCatalog.cs` — deterministic initial catalog implementation.

### Application files to modify

- `Mushroom_Finder/Program.cs` — register the catalog service with dependency injection.
- `Mushroom_Finder/Pages/Search_cs.cshtml.cs` — inject the catalog and expose the typed collection to the page.
- `Mushroom_Finder/Pages/Search_cs.cshtml` — render typed cards, preserve fixed filters and CSS classes, and add ARIA markup/client filtering updates.

### Application files to preserve

- `Mushroom_Finder/wwwroot/css/site.css` — retain existing Search page styling; modify only if implementation validation identifies a necessary accessibility or responsive defect.
- Existing database, Identity, layout, and unrelated Razor Pages — no changes are planned.

## 5. Automated Testing Plan

The repository currently has one web project and no dedicated test project. Add a separate .NET 8 test project, for example `Mushroom_Finder.Tests`, rather than placing tests in the web project.

### 5.1 Service unit tests

Add deterministic unit tests for `InMemoryMushroomCatalog` and its contract:

- Returns the expected initial three mushroom entries.
- Returns Lions Mane with North America and Asia Regions and Summer and Fall filter values.
- Returns Wood Blewit with Europe and North America Regions and Fall and Winter filter values.
- Returns Amethyst Deceiver with Europe and North America Regions and Summer and Fall filter values.
- Exposes read-only data or otherwise prevents accidental mutation through the service result.
- Handles repeated reads consistently.

### 5.2 PageModel tests

Test the PageModel with a controlled catalog implementation:

- GET loads the catalog into the page model.
- An empty catalog produces an empty collection without an exception.
- A catalog failure follows the safe no-results behavior and does not expose diagnostic details.
- The PageModel uses the injected abstraction rather than constructing a concrete catalog.

### 5.3 Razor Page integration tests

Use an ASP.NET Core Razor Pages test host or equivalent integration-test fixture to verify:

- The existing Search page route remains available.
- The heading, search input, Region selector, Season selector, result region, and no-results message are rendered.
- The initial page contains the catalog cards and Region/Season details.
- The fixed Region and Season options are present.
- The page includes the required accessible labels, relationships, and status semantics.
- Mushroom values are safely rendered as text/attributes.

### 5.4 Manual browser verification

Verify the client-side DOM behavior manually in a supported browser because the selected test strategy does not use browser-automation frameworks:

- Empty initial criteria display all cards.
- Full and partial name searches are case-insensitive.
- Whitespace-only input behaves as an empty query.
- Region filtering matches multi-Region cards.
- Season filtering matches multi-Season cards.
- Name, Region, and Season criteria use AND semantics.
- Clearing one criterion removes only that restriction.
- A no-match combination hides all cards and shows the exact no-results message.
- Returning to a matching combination hides the no-results message.
- Filtered cards are hidden from assistive technology and the status update is observable.
- Criteria are reset after a fresh page load and are not persisted in URL or browser storage.
- Unusual search input, including HTML markup, script-like text, quotes, and special characters, is treated as text and does not execute, alter the page structure, or expose diagnostics.

Playwright, Selenium, and other browser-automation frameworks are out of scope. Record the browser, viewport sizes, and manual results as completion evidence.

## 6. Validation and Quality Gates

After implementation:

1. Build the solution successfully with .NET 8.
2. Run all service unit tests.
3. Run PageModel and Razor Page integration tests.
4. Complete the manual browser verification and accessibility checklist in Section 5.4; browser automation is not required.
5. Verify the current Search page CSS remains applied and the responsive layout remains usable.
6. Verify keyboard operation for the search input and both selectors.
7. Verify the no-results status is announced appropriately and does not remain visible when results return.
8. Review output encoding, input handling, service failure behavior, and diagnostic logging.
9. Confirm no URL or browser-storage persistence was introduced.
10. Confirm changes remain limited to the approved Mushroom Search scope.

## 7. Risks and Mitigations

- **Filename mismatch:** Keep `Search_cs.cshtml` and `Search_csModel` unchanged to avoid unintended route changes.
- **Substring filter errors:** Store normalized Region and Season values as collections and compare values by membership rather than raw comma-separated substring checks.
- **Accessibility regressions from inline hiding:** Use an explicit hidden/display strategy and test the resulting DOM accessibility state.
- **Test environment complexity:** Keep service/PageModel tests independent of browser infrastructure and verify client-side interaction through the manual browser checklist.
- **Future data source changes:** Keep the page dependent on `IMushroomCatalog` and use read-only typed data so a future persistence implementation can replace the in-memory service.
- **Styling regressions:** Reuse all existing Search CSS class names and avoid unrelated CSS changes.

## 8. Implementation Sequence

1. Add the strongly typed `Mushroom` model.
2. Add the `IMushroomCatalog` abstraction and in-memory implementation.
3. Register the catalog service in `Program.cs`.
4. Update the Search PageModel to consume the service.
5. Convert the Search Razor markup from hard-coded cards to typed rendering.
6. Add ARIA relationships and accessible no-results status semantics.
7. Update client-side filtering and exact multi-value matching while preserving existing styling.
8. Add the test project and service/PageModel tests.
9. Add Razor Page integration and client behavior tests.
10. Build, run tests, and perform accessibility/responsive verification.
11. Review the final changes against `spec.md` and `constitution.md`.

## 9. Out of Scope

- Renaming or duplicating the Search Page route.
- Replacing the in-memory catalog with Entity Framework or another persistence system.
- Adding search URL parameters, browser storage, pagination, sorting, favorites, authentication changes, or mushroom identification functionality.
- Redesigning `site.css` or changing unrelated pages.
