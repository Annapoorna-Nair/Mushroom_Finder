# Feature Specification: Mushroom Search

**Status:** Draft
**Feature:** Search and filter mushrooms
**Application:** Mushroom Finder (.NET 8 ASP.NET Core Razor Pages)

## 1. Overview

The Mushroom Search feature allows users to browse the available mushroom cards and quickly narrow the list by mushroom name, Region, and Season. Each matching card presents the mushroom image, name, Region details, and Season details. When the selected search and filters do not match any mushroom, the page provides a clear no-results message.

The current repository implementation is represented by `Pages/Search_cs.cshtml` and `Pages/Search_cs.cshtml.cs` as named in the workspace. The requested `Pages/Search.cshtml` file does not currently exist; the existing `_cs` filename and route are retained for this feature.

## 2. Goals

- Enable users to find mushrooms by entering part or all of a mushroom name.
- Enable users to filter the displayed mushrooms by Region.
- Enable users to filter the displayed mushrooms by Season.
- Allow name search and both filters to be used together.
- Show Region and Season information directly on every mushroom card.
- Clearly communicate when no mushroom matches the current criteria.
- Maintain the existing Mushroom Finder visual language and responsive Razor Pages experience.

## 3. User Scenarios and Acceptance Criteria

### Scenario A: Browse the available mushrooms

**Given** the user opens the Search page with no search text and the default Region and Season selections
**When** the page finishes loading
**Then** all available mushroom cards are displayed
**And** each card shows an image, mushroom name, Region details, and Season details.

### Scenario B: Search by mushroom name

**Given** the user is on the Search page
**When** the user enters a name or partial name in the name search field
**Then** only cards whose mushroom name contains the entered text are displayed
**And** the matching is case-insensitive
**And** the results update as the search text changes without requiring a separate submit action.

An empty or whitespace-only name search is treated as no name restriction.

### Scenario C: Filter by Region

**Given** the user is on the Search page
**When** the user selects a Region
**Then** only mushrooms associated with that Region are displayed
**And** the Region selection can be combined with a name search or Season selection.

The default `All Regions` selection removes the Region restriction. The available Region values are:

- All Regions
- North America
- Europe
- Asia

### Scenario D: Filter by Season

**Given** the user is on the Search page
**When** the user selects a Season
**Then** only mushrooms associated with that Season are displayed
**And** the Season selection can be combined with a name search or Region selection.

The default `All Seasons` selection removes the Season restriction. The available Season values are:

- All Seasons
- Spring
- Summer
- Fall
- Winter

### Scenario E: Apply multiple criteria

**Given** the user has entered a name and/or selected a Region and Season
**When** the page evaluates the criteria
**Then** a mushroom is displayed only when it satisfies every active criterion
**And** a mushroom associated with multiple Regions or Seasons matches when it includes the selected value.

### Scenario F: Show no results

**Given** the user has entered search criteria or selected filters
**When** no mushroom satisfies all active criteria
**Then** all mushroom cards are hidden
**And** the message `No mushrooms match your search and filter selection.` is displayed in the results area
**And** the message is not displayed when at least one card matches.

### Scenario G: View card details

**Given** a mushroom card is displayed
**Then** the card shows:

- An image with meaningful alternative text.
- The mushroom name.
- A clearly labeled `Region:` value.
- A clearly labeled `Season:` value.

The Region and Season details remain visible beneath the mushroom name and do not require opening another view.

## 4. Functional Requirements

### FR-001: Search page

The application MUST provide a Search page with a visible heading and supporting text explaining that users can find mushrooms by region and season.

### FR-002: Name input

The page MUST provide a labeled text input for searching by mushroom name. The input MUST accept partial names and MUST perform case-insensitive matching. The input MUST be safe for arbitrary user-entered text and MUST not be interpreted as markup or executable code.

### FR-003: Region filter

The page MUST provide a labeled Region selector with an option to show all Regions and options for North America, Europe, and Asia.

### FR-004: Season filter

The page MUST provide a labeled Season selector with an option to show all Seasons and options for Spring, Summer, Fall, and Winter.

### FR-005: Combined filtering

The page MUST apply active name, Region, and Season criteria using AND semantics. Clearing a criterion MUST remove only that criterion's restriction.

### FR-006: Mushroom results

The results area MUST display one card per available mushroom that matches the active criteria. The initial data represented by the current page includes:

| Mushroom | Regions | Seasons | Image |
|---|---|---|---|
| Lions Mane | North America, Asia | Summer, Fall (displayed as Late Summer – Fall) | `~/images/lions-mane.jpg` |
| Wood Blewit | Europe, North America | Fall, Winter (displayed as Fall – Early Winter) | `~/images/wood-blewit.jpg` |
| Amethyst Deceiver | Europe, North America | Summer, Fall (displayed as Summer – Fall) | `~/images/amethyst-deceiver.jpg` |

The display text for Region and Season MUST remain understandable to users even when a mushroom has multiple values.

### FR-007: No-results state

The page MUST provide a dedicated no-results status message. It MUST be hidden when one or more cards match and shown when zero cards match. The status text MUST be clear and actionable enough for the user to understand that changing the search or filters may produce results.

### FR-008: Responsive presentation

The results MUST remain usable on smaller screens. The card grid MAY change from three columns to an adaptive layout, and search controls MAY wrap, but controls and card details MUST remain readable and operable without horizontal scrolling caused by the feature.

### FR-009: Accessibility

Search and filter controls MUST have accessible labels. Mushroom images MUST have meaningful alternative text. The no-results status MUST be perceivable by assistive technology when it becomes visible. Keyboard users MUST be able to reach and operate the input and both selectors.

### FR-010: Predictable failure behavior

If the available mushroom collection is empty or cannot be loaded by a future server-side implementation, the page MUST fail safely and present the existing no-results message without exposing diagnostics or sensitive information. The implementation MUST log diagnostics through the application’s supported logging mechanisms where applicable.

## 5. Interaction Rules

1. The page initially uses `All Regions`, `All Seasons`, and an empty name query.
2. Name filtering responds to text input; Region and Season filtering responds to selection changes.
3. Matching is case-insensitive and ignores leading and trailing whitespace in the name query.
4. Region and Season filters match a mushroom when the mushroom has the selected value among its associated values.
5. Active criteria are evaluated together; a result must satisfy all active criteria.
6. The no-results message is shown only when the filtered result count is zero.
7. Clearing the name input or selecting an “All” option immediately restores the corresponding unfiltered results.

## 6. User Interface Requirements

- Use the existing dark-brown search page container, cream search banner, rounded search input, filter pills, and cream mushroom card styling as the visual baseline.
- Keep the heading, search input, Region selector, and Season selector grouped at the top of the page.
- Keep Region and Season values visually associated with the corresponding mushroom name.
- Keep the no-results message visually distinct from mushroom cards and readable against the page background.
- Preserve the existing responsive behavior: the three-column layout may adapt at smaller viewport widths and filter controls may wrap.

## 7. Data and Scope

### In scope

- Searching the available mushroom collection by name.
- Filtering the collection by Region and Season.
- Rendering image, name, Region, and Season details on result cards.
- Updating the result set and no-results status as criteria change.
- Accessible labels, alternative text, keyboard operation, and responsive presentation.

### Out of scope

- Mushroom identification from uploaded images or camera input.
- Creating, editing, or deleting mushroom records.
- Detailed mushroom pages, favorites, ratings, or forum integration.
- Pagination, sorting, or free-form Region and Season creation.
- Authentication or authorization changes.
- Changing the application’s data source unless required by a later implementation plan.

## 8. Quality and Testing Expectations

In accordance with `constitution.md`:

- Automated unit and integration tests MUST cover the catalog service, PageModel behavior, initial rendered markup, and the filtering contract represented by the rendered card data and client-side filtering configuration.
- Automated tests SHOULD cover service contents, PageModel loading and failure behavior, safe rendering, fixed filter options, case-insensitive and partial-name matching requirements, multiple Regions or Seasons on one mushroom, whitespace-only input, and boundary combinations that produce zero results.
- Razor Pages integration tests SHOULD verify that controls and result content are rendered accessibly and that the no-results message is presented correctly.
- Client-side DOM behavior MUST be verified manually in a supported browser, including live filtering, combined criteria, clearing criteria, hidden cards, no-results transitions, keyboard operation, and accessibility announcements. Browser automation frameworks are out of scope.
- The solution MUST build successfully and relevant automated tests MUST pass before implementation is considered complete.
- Any later server-side data access MUST use appropriate asynchronous APIs, retrieve only required data, validate input at the application boundary, and keep page presentation concerns separate from data/infrastructure concerns.

## 9. Success Criteria

The feature is successful when:

1. A user can locate a mushroom by entering its full or partial name.
2. A user can narrow results by Region, by Season, or by both.
3. Every displayed mushroom card includes readable Region and Season details.
4. A query/filter combination with no matches displays the specified no-results message and no result cards.
5. Clearing criteria restores matching cards without a full-page navigation.
6. Controls and result information are usable with keyboard navigation and assistive technology.
7. The presentation remains usable across the existing responsive breakpoints.
8. The implementation can be tested deterministically and complies with the project constitution.

## 10. Implementation Notes for the Subsequent Plan

- The current `Search_cs.cshtml.cs` PageModel contains no request or data-loading logic; the current behavior is implemented in the Razor markup and inline client-side script.
- The implementation will move the mushroom data into a strongly typed in-memory service or model while retaining client-side filtering and resetting criteria on page load.
- The fixed Region and Season options in this specification will be used initially, but the design should allow future options to be derived from data.
- The repository's `Pages/Search_cs.cshtml` filename and current route will be retained; no `Pages/Search.cshtml` alias or rename is required.
- Search and filter selections will not be persisted in query-string parameters or browser storage.
- This document specifies behavior only. No application implementation, PageModel changes, CSS changes, or automated test files are included by this specification task.

## 11. Clarifications

The following decisions were confirmed during clarification:

1. Mushroom data will be represented by a strongly typed in-memory service or model rather than remaining embedded directly in the Razor markup or being moved to the database for this feature.
2. `Pages/Search_cs.cshtml` and its current route will be retained.
3. Search and filter criteria will remain client-side, will not be persisted in the URL or browser storage, and will reset when the page loads.
4. Region and Season selectors will use the fixed options currently defined in the specification, with an implementation structure that can support future data-driven options.
5. An empty or unavailable mushroom collection will use the existing `No mushrooms match your search and filter selection.` message.
