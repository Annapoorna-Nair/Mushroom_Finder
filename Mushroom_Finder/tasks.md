# Implementation Tasks: Mushroom Search

**Status:** Proposed
**Source documents:** `Mushroom_Finder/spec.md`, `Mushroom_Finder/plan.md`, `Mushroom_Finder/constitution.md`
**Target:** .NET 8 ASP.NET Core Razor Pages
**Implementation gate:** Do not execute these tasks until the user explicitly issues `speckit.implement`.

## 1. Task Conventions and Dependencies

- Complete tasks in order unless a task explicitly states that it may run in parallel.
- Keep `Pages/Search_cs.cshtml` and its current route; do not rename it to `Search.cshtml`.
- Preserve the existing Search page CSS classes and styling in `wwwroot/css/site.css`.
- Keep search and filter evaluation in the browser; do not add query-string or browser-storage persistence.
- Use a strongly typed in-memory catalog for the initial three mushrooms.
- Use xUnit for the new standard .NET test project unless an existing repository convention requires MSTest.
- Use Razor Page integration tests for server-rendered behavior; use the manual browser checklist in Section 5.4 for client-side interaction and accessibility verification.
- Do not use Playwright, Selenium, or another browser-automation framework.

## 2. Preparation and Test Infrastructure

### T001 — Confirm implementation scope and baseline

- [ ] Re-read `spec.md`, `plan.md`, and `constitution.md` before implementation.
- [ ] Confirm the current page files are `Pages/Search_cs.cshtml` and `Pages/Search_cs.cshtml.cs`.
- [ ] Confirm the existing Search CSS classes in `wwwroot/css/site.css` are the styling contract.
- [ ] Record the baseline solution build/test state before feature changes.
- [ ] Verify no unrelated files or features are included in the work.

**Depends on:** None

### T002 — Create the automated test project

- [ ] Add a .NET 8 xUnit test project, such as `Mushroom_Finder.Tests`.
- [ ] Add the standard test SDK, xUnit, xUnit runner, and coverage dependencies required by the repository’s supported setup.
- [ ] Add a project reference to `Mushroom_Finder.csproj`.
- [ ] Add `Microsoft.AspNetCore.Mvc.Testing` for Razor Page integration tests.
- [ ] Add the test project to the solution.
- [ ] Confirm the new test project can discover and run a minimal test.

**Depends on:** T001

## 3. Domain Model and Catalog Service

### T003 — Add the strongly typed mushroom model

- [ ] Create `Models/Mushroom.cs`.
- [ ] Define the mushroom display name, image path, image alternative text, Region collection, Season collection, and user-facing display values needed by the page.
- [ ] Represent Regions and Seasons as read-only collections suitable for exact membership checks.
- [ ] Preserve the current display wording, including `Late Summer – Fall` and `Fall – Early Winter` where applicable.
- [ ] Use safe, meaningful names and nullable annotations consistent with the project.
- [ ] Avoid embedding filtering or UI/DOM behavior in the model.

**Depends on:** T002

### T004 — Add the catalog abstraction

- [ ] Create `Services/IMushroomCatalog.cs`.
- [ ] Define a read-only catalog operation that supports the current in-memory implementation and future replacement by another data source.
- [ ] Document or encode the empty-catalog behavior required by the specification.
- [ ] Keep the interface independent of Razor Page presentation concerns.

**Depends on:** T003

### T005 — Add the in-memory catalog implementation

- [ ] Create `Services/InMemoryMushroomCatalog.cs`.
- [ ] Add Lions Mane, Wood Blewit, and Amethyst Deceiver with the Regions, Seasons, image paths, alt text, and display values from `spec.md`.
- [ ] Return deterministic read-only data.
- [ ] Ensure callers cannot mutate the service’s internal catalog through the returned value.
- [ ] Return an empty collection safely when the catalog contains no entries.
- [ ] Do not implement name, Region, or Season filtering in the service; filtering remains client-side.

**Depends on:** T004

### T006 — Register the catalog service

- [ ] Update `Program.cs` to register `IMushroomCatalog` with `InMemoryMushroomCatalog` using an appropriate lifetime.
- [ ] Preserve existing Identity, Entity Framework, routing, static-file, and authorization registrations.
- [ ] Confirm the application can resolve the service through dependency injection.

**Depends on:** T005

## 4. Razor Page and Client Interaction

### T007 — Update the Search PageModel

- [ ] Update `Pages/Search_cs.cshtml.cs` to inject `IMushroomCatalog`.
- [ ] Add a strongly typed, read-only page property for the catalog entries.
- [ ] Load catalog data during the existing GET request.
- [ ] Treat null or empty catalog results as an empty collection.
- [ ] Handle catalog failures predictably using the application’s supported diagnostic logging.
- [ ] Expose only the safe no-results state to users when catalog loading fails; never expose exception details or sensitive diagnostics.
- [ ] Keep `Search_csModel` and the current page route unchanged.
- [ ] Keep filtering and DOM logic out of the PageModel.

**Depends on:** T006

### T008 — Render typed mushroom cards

- [ ] Update `Pages/Search_cs.cshtml` to render cards from the PageModel collection using a Razor `foreach` or equivalent strongly typed iteration.
- [ ] Remove duplicated hard-coded card markup while preserving the current three catalog results.
- [ ] Render safely encoded name, image path, alt text, Region details, and Season details.
- [ ] Preserve the existing CSS classes used by `site.css`.
- [ ] Preserve the visible heading, subtitle, search input, fixed Region options, fixed Season options, and exact no-results text.
- [ ] Ensure empty catalog rendering does not produce invalid or misleading card markup.

**Depends on:** T007

### T009 — Add ARIA and keyboard-accessible markup

- [ ] Give the search input an explicit accessible label and `aria-controls` reference to the results region.
- [ ] Give the Region and Season selectors explicit accessible labels.
- [ ] Give the results region an accessible name or label relationship.
- [ ] Mark the no-results message with appropriate status semantics, including a suitable `role` and `aria-live` behavior.
- [ ] Ensure filtered cards are removed from the accessibility tree when hidden.
- [ ] Keep the controls operable with keyboard navigation.
- [ ] Do not change the visual design solely to add accessibility semantics.

**Depends on:** T008

### T010 — Implement client-side filtering behavior

- [ ] Retain the Search Page’s client-side filtering script or move it only as needed for maintainability without changing the route or styling contract.
- [ ] Normalize and trim the name query.
- [ ] Implement case-insensitive partial-name matching.
- [ ] Treat empty and whitespace-only queries as unrestricted.
- [ ] Match Region and Season using normalized exact value membership rather than unsafe substring matching.
- [ ] Apply name, Region, and Season criteria with AND semantics.
- [ ] Update results on name `input` events and Region/Season `change` events.
- [ ] Hide nonmatching cards visually and from assistive technology.
- [ ] Show the exact no-results message when zero cards match and hide it when results return.
- [ ] Confirm the page starts with all criteria cleared on a fresh load and writes nothing to the URL or browser storage.

**Depends on:** T009

### T011 — Verify styling and responsive behavior

- [ ] Confirm `wwwroot/css/site.css` continues to style the typed cards using the existing class names.
- [ ] Avoid CSS changes unless a required accessibility or responsive issue cannot be addressed through markup.
- [ ] If CSS changes are necessary, limit them to the Search page and document the reason.
- [ ] Confirm the three-column layout and smaller-screen adaptive behavior remain usable.
- [ ] Confirm Region and Season details remain visible beneath each mushroom name.

**Depends on:** T010

## 5. Automated Verification

### 5.1 Service unit tests

### T012 — Test catalog contents and immutability

- [ ] Verify the catalog returns exactly the three initial mushrooms.
- [ ] Verify Lions Mane has North America and Asia Regions and Summer and Fall filter values.
- [ ] Verify Wood Blewit has Europe and North America Regions and Fall and Winter filter values.
- [ ] Verify Amethyst Deceiver has Europe and North America Regions and Summer and Fall filter values.
- [ ] Verify image paths, alternative text, and user-facing Region/Season display values.
- [ ] Verify returned data cannot mutate the catalog’s internal state.
- [ ] Verify repeated reads are deterministic.

**Depends on:** T005, T002

### T013 — Test empty catalog behavior

- [ ] Test an empty catalog implementation or controlled test double.
- [ ] Verify an empty read returns safely without throwing.
- [ ] Verify the PageModel can consume an empty catalog as an empty collection.

**Depends on:** T007, T002

### 5.2 PageModel and Razor Page integration tests

### T014 — Test PageModel dependency injection and GET behavior

- [ ] Test that the PageModel consumes the injected `IMushroomCatalog` abstraction.
- [ ] Test that GET loads the expected typed entries.
- [ ] Test null/empty catalog behavior.
- [ ] Test safe failure handling if the service throws, including no diagnostic leakage in the user-facing result.

**Depends on:** T007, T002

### T015 — Test Razor Page rendering and accessibility contract

- [ ] Use `WebApplicationFactory` or an equivalent ASP.NET Core integration fixture.
- [ ] Verify the current Search route remains available.
- [ ] Verify the heading, subtitle, search input, Region selector, Season selector, and results region render.
- [ ] Verify the three mushroom cards and their Region/Season details render from typed data.
- [ ] Verify fixed Region and Season options are present.
- [ ] Verify image alt text is present and meaningful.
- [ ] Verify `aria-controls`, accessible labels, result-region semantics, and no-results status attributes are rendered.
- [ ] Verify user-provided or catalog-provided text is safely encoded.
- [ ] Verify the inline/client filtering contract is present without attempting to execute JavaScript in the server integration test.

**Depends on:** T008, T009, T002

### 5.3 Client behavior test boundary

### T016 — Document client-side behavior coverage

- [ ] Record that xUnit/MSTest server tests verify rendered markup and the filtering contract but do not replace browser execution.
- [ ] Map each client behavior requirement to the manual browser checklist in Section 5.4.
- [ ] Keep browser automation frameworks out of the test project.

**Depends on:** T010, T015

### 5.4 Manual browser verification checklist

### T017 — Verify initial page state manually

- [ ] Open the existing Search page route in a supported browser.
- [ ] Confirm all three mushroom cards display on initial load.
- [ ] Confirm every card displays an image, name, Region details, and Season details.
- [ ] Confirm the existing visual styling and layout are preserved.
- [ ] Reload the page and confirm the search input and both selectors reset to their defaults.

**Depends on:** T011, T015

### T018 — Verify name search manually

- [ ] Enter a full mushroom name and confirm only that card remains visible.
- [ ] Enter a partial name and confirm matching cards remain visible.
- [ ] Use mixed-case text and confirm matching is case-insensitive.
- [ ] Enter only spaces and confirm all cards are treated as eligible by name.
- [ ] Clear the input and confirm the name restriction is removed immediately.

**Depends on:** T010, T017

### T019 — Verify Region and Season filters manually

- [ ] Select North America, Europe, and Asia individually and verify the expected cards.
- [ ] Select `All Regions` and confirm the Region restriction is removed.
- [ ] Select Spring, Summer, Fall, and Winter individually and verify the expected cards.
- [ ] Select `All Seasons` and confirm the Season restriction is removed.
- [ ] Combine a name query, Region, and Season and confirm AND semantics.
- [ ] Confirm multi-Region and multi-Season mushrooms match each associated value.

**Depends on:** T018

### T020 — Verify the no-results state manually

- [ ] Enter criteria that match no mushroom.
- [ ] Confirm all cards are hidden.
- [ ] Confirm `No mushrooms match your search and filter selection.` is displayed.
- [ ] Change or clear one criterion and confirm matching cards return.
- [ ] Confirm the no-results message disappears when at least one card matches.

**Depends on:** T019

### T021 — Verify accessibility and responsive behavior manually

- [ ] Navigate to the search input and both selectors using only the keyboard.
- [ ] Confirm visible labels or accessible names identify each control.
- [ ] Use a screen reader or browser accessibility inspection to verify the result region and no-results status are exposed appropriately.
- [ ] Confirm filtered cards are not exposed as visible results to assistive technology.
- [ ] Test narrow viewport widths and confirm controls wrap or adapt without feature-caused horizontal scrolling.
- [ ] Confirm card names and Region/Season details remain readable at narrow widths.
- [ ] Enter unusual search input containing HTML markup, script-like text, quotes, and special characters.
- [ ] Confirm unusual input is treated as text, does not execute, does not alter the page structure, and does not expose diagnostics.

**Depends on:** T020

## 6. Final Validation and Completion

### T022 — Run automated verification

- [ ] Build the full solution in .NET 8.
- [ ] Run all xUnit/MSTest unit tests.
- [ ] Run Razor Page integration tests.
- [ ] Confirm all tests pass deterministically.
- [ ] Review any warnings or failures related to the feature and resolve them before completion.

**Depends on:** T012, T013, T014, T015

### T023 — Complete specification and constitution review

- [ ] Verify every approved requirement in `spec.md` is covered by implementation or a test/checklist item.
- [ ] Verify the `_cs` page filename and route are unchanged.
- [ ] Verify existing `site.css` styling is retained.
- [ ] Verify no URL/browser-storage persistence was added.
- [ ] Verify empty/unavailable catalog behavior uses the existing no-results message.
- [ ] Verify security, output encoding, logging, separation of concerns, and accessibility requirements from `constitution.md`.
- [ ] Verify no unrelated files or behavior were changed.

**Depends on:** T022, T017, T018, T019, T020, T021

### T024 — Record completion evidence

- [ ] Record the final build result.
- [ ] Record automated test results.
- [ ] Record manual browser verification results, including browser and viewport coverage.
- [ ] Record any approved deviations from `spec.md` or `plan.md`.
- [ ] Mark the feature complete only after all quality gates pass.

**Depends on:** T023

## 7. Definition of Done

The Mushroom Search feature is complete only when:

- [ ] The strongly typed in-memory model and service are implemented and registered through DI.
- [ ] `Pages/Search_cs.cshtml` renders the catalog without a route or filename change.
- [ ] Existing Search styling in `wwwroot/css/site.css` remains intact and usable.
- [ ] Name, Region, Season, combined, clearing, and no-results behaviors work client-side.
- [ ] ARIA labels, result semantics, live status behavior, keyboard operation, and hidden-card accessibility behavior are verified.
- [ ] xUnit/MSTest service tests pass.
- [ ] Razor Page integration tests pass.
- [ ] The manual browser checklist in Section 5.4 passes without Playwright or Selenium.
- [ ] The solution builds successfully.
- [ ] The implementation remains within the approved specification and constitution.
