---
description: "Dependency-ordered implementation tasks for the Community Forum"
---

# Tasks: Community Forum

**Input**: Design documents from `specs/001-community-forum/`

**Prerequisites**: `plan.md`, `spec.md`, `research.md`, `data-model.md`, and `quickstart.md`

**Testing**: Use the manual acceptance checks in `quickstart.md`; no test project or test framework is currently planned.

**Organization**: Tasks are grouped by user story and ordered by dependency. Application code is not changed by this task-list document.

## Phase 1: Setup

**Purpose**: Verify the existing project baseline; reuse the current .NET 8 Razor Pages application.

- [X] T001 Run the baseline build and record any pre-existing failures for `Mushroom_Finder/Mushroom_Finder.csproj`.

---

## Phase 2: Foundational

**Purpose**: Prepare the shared, presentation-only example content consumed by all three user stories.

- [X] T002 Select or create two project-owned or public-domain/CC0 mushroom photos at `Mushroom_Finder/wwwroot/images/community-forum-id-request.jpg` and `Mushroom_Finder/wwwroot/images/community-forum-sharing.jpg`; record each source and usage-rights basis in `Mushroom_Finder/wwwroot/images/community-forum-attributions.md`.
- [X] T003 Define the static forum-post model and the two example entries in `Mushroom_Finder/Pages/Shared/Forum.cshtml.cs`, using `specs/001-community-forum/data-model.md` as the canonical definition for `ForumPost.Category`, `ForumPost.Title`, `ForumPost.AuthorDisplayName`, `ForumPost.DisplayDate`, `ForumPost.Body`, `ForumPost.PhotoUrl`, `ForumPost.PhotoAltText`, `ForumPost.PhotoSourceAndRights`, and `ForumPost.ShowSafetyDisclaimer`.

**Checkpoint**: Shared example data and its required photo provenance are ready before page stories begin.

---

## Phase 3: User Story 1 - Browse the community feed (Priority: P1)

**Goal**: Show a complete, readable combined feed to signed-out visitors at `/Forum`.

**Independent Test**: While signed out, open `/Forum` from the shared Forum navigation. Confirm the heading, intro, location guidance, controls, and both sample categories appear; each card shows its required content, usable photo/alt text, and applicable safety disclaimer.

### Implementation

- [X] T004 [US1] Implement the `/Forum` route and semantic page shell, location guidance, category controls, and post-card markup in `Mushroom_Finder/Pages/Shared/Forum.cshtml`; render `ForumPost.Category`, `ForumPost.Title`, `ForumPost.AuthorDisplayName`, `ForumPost.DisplayDate`, `ForumPost.Body`, and the applicable optional photo fields from the page model, and do not add authentication, posting, or discussion actions.
- [X] T005 [US1] Add the page-scoped visual hierarchy and card styling in `Mushroom_Finder/Pages/Shared/Forum.cshtml.css`, consistent with the nature-inspired design guidance.

### Manual Validation

- [X] T006 [US1] Run the initial-feed, signed-out-route, post-content, image-alt-text, and safety-message checks in `specs/001-community-forum/quickstart.md`.

**Checkpoint**: The combined feed can be browsed independently without filtering.

---

## Phase 4: User Story 2 - Filter by post category (Priority: P1)

**Goal**: Filter the feed by “All posts,” “ID Request,” and “Just Sharing,” with recoverable empty states and accessible feedback.

**Independent Test**: Select each filter and verify only matching posts remain; verify an empty feed and an empty selected category show the specified messages, “All posts” restores the combined feed, the result is announced, and focus stays on the selected control.

### Implementation

- [X] T007 [US2] Add filter data hooks and full-feed/category-specific empty-state and status-announcement markup in `Mushroom_Finder/Pages/Shared/Forum.cshtml`.
- [X] T008 [US2] Implement in-page filtering, programmatic selected state, empty-state transitions, and result-count or empty-state announcements without moving focus in `Mushroom_Finder/wwwroot/js/forum.js`.

### Manual Validation

- [ ] T009 [US2] Run the category-filter, empty-state, return-to-all, announcement, and focus-retention checks in `specs/001-community-forum/quickstart.md`.

**Checkpoint**: Filtering and its empty/recovery states work independently over the supplied in-memory examples.

---

## Phase 5: User Story 3 - Browse across devices and input methods (Priority: P2)

**Goal**: Keep the feed responsive, keyboard-operable, perceivable without color, and readable at narrow widths.

**Independent Test**: At the reference browser/assistive-technology combinations, use only the keyboard and screen reader, and inspect 320, 767, 768, and 1280 CSS-pixel widths. Verify contrast, focus, selected-state cues, announcement behavior, and no overflow for the specified long-text fixtures.

### Implementation

- [X] T010 [US3] Implement the responsive layout in `Mushroom_Finder/Pages/Shared/Forum.cshtml.css`: stack below 768 CSS pixels, use two columns at 768 CSS pixels and above, and wrap the specified long text without horizontal overflow.
- [X] T011 [US3] Complete the accessibility treatment in `Mushroom_Finder/Pages/Shared/Forum.cshtml` and `Mushroom_Finder/Pages/Shared/Forum.cshtml.css`: ensure accessible control names, visible keyboard focus, non-color category/selected cues, and the specified WCAG 2.2 AA contrast thresholds.

### Manual Validation

- [ ] T012 [P] [US3] Run the complete keyboard, NVDA, responsive-width, long-text, and contrast checks in `specs/001-community-forum/quickstart.md` using current stable Chrome on Windows with NVDA.
- [ ] T013 [P] [US3] Run the complete keyboard, VoiceOver, responsive-width, long-text, and contrast checks in `specs/001-community-forum/quickstart.md` using current stable Safari on macOS with VoiceOver.

**Checkpoint**: The page remains usable with keyboard and assistive technology at the documented viewport sizes.

---

## Phase 6: Polish and Final Verification

**Purpose**: Verify the complete feature, scope boundaries, and delivery readiness.

- [ ] T014 Run `dotnet build Mushroom_Finder/Mushroom_Finder.csproj` and all applicable checks in `specs/001-community-forum/quickstart.md`; confirm photo provenance is recorded and no database, authentication, posting, or new dependency work was introduced.

## Dependencies and Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: T001 has no prerequisites.
- **Foundational (Phase 2)**: T002 and T003 follow the baseline build; T003 uses the asset paths and provenance established by T002.
- **User Story 1 (Phase 3)**: Depends on T003; T004 precedes T005, then T006 validates the completed feed.
- **User Story 2 (Phase 4)**: Depends on User Story 1; T007 establishes the DOM states consumed by T008, then T009 validates behavior.
- **User Story 3 (Phase 5)**: Depends on User Story 2; T010 and T011 refine the established page, then T012 and T013 validate independently in separate browser/assistive-technology environments.
- **Final Verification (Phase 6)**: T014 follows all desired stories, T018 design and functional regression verification, and the separate reference-matrix validations.
- **Visual Convergence (Phase 7)**: T015 establishes the revised text-only documentation scope before T016 updates the visual design and T017 refines post presentation; T018 then performs design and functional regression checks before T014 final verification.

### User Story Dependencies

- **User Story 1 (P1)**: Follows the shared example-content foundation; no dependency on another user story.
- **User Story 2 (P1)**: Depends on User Story 1's rendered cards and controls.
- **User Story 3 (P2)**: Depends on the filter controls and behavior from User Story 2 so the accessibility and responsive checks cover the complete experience.

### Parallel Opportunities

- No implementation tasks are marked parallel because they share the page, model, or dependent asset paths.
- After T011, T012 and T013 can run in parallel in their separate browser/assistive-technology environments.

### Parallel Execution Examples

- **User Story 1**: No safe intra-story parallel execution; T004 defines the markup consumed by T005, and T006 validates both.
- **User Story 2**: No safe intra-story parallel execution; T007 defines the DOM states consumed by T008, and T009 validates the behavior.
- **User Story 3**: After T011, run the two independent reference-environment checks concurrently:

```text
Task: T012 Chrome + NVDA validation
Task: T013 Safari + VoiceOver validation
```

---

## Implementation Strategy

### MVP First

1. Complete T001–T003 to verify the existing project and prepare presentation-only example content.
2. Complete User Story 1 (T004–T006) as the MVP browseable feed.
3. Validate the signed-out `/Forum` route, required card content, example photos, and applicable safety disclaimer before proceeding.

### Incremental Delivery

1. Add User Story 2 after the combined feed; validate filtering and both empty-state recovery paths independently.
2. Add User Story 3 after filtering; validate the full page in the documented reference matrix and viewport sizes.
3. Complete T015–T018 visual-convergence work in order: documentation first, then design and post-presentation updates, then responsive and functional regression verification.
4. Preserve T009, T012, and T013 as the separate manual validation responsibilities for filtering/empty states, Chrome with NVDA, and Safari with VoiceOver.
5. Complete T014 final build and scope verification after T018 before considering the feature ready.

## Notes

- `[P]` tasks operate independently in separate environments or files; other tasks are ordered because they share files or depend on preceding markup/data.
- Manual acceptance checks are used instead of adding a test framework, consistent with `plan.md`.
- Keep all implementation changes within the paths named by the plan and tasks; do not alter database, authentication, or unrelated shared-page behavior.

## Phase 7: Convergence

**Purpose**: Align the documentation and presentation of the existing forum with the revised text-only sample-feed design while retaining optional image support for future database integration.

- [X] T015 Update VSDD documentation in `specs/001-community-forum/spec.md`, `specs/001-community-forum/plan.md`, `specs/001-community-forum/data-model.md`, and `specs/001-community-forum/quickstart.md` to reflect a text-only sample feed for this phase; review and update the data model only if needed and preserve optional photo fields for future database integration.
- [X] T016 Improve the Forum visual design in `Mushroom_Finder/Pages/Shared/Forum.cshtml` and `Mushroom_Finder/Pages/Shared/Forum.cshtml.css` to match the original dark-brown, cream, and olive-green mockup with rounded cards and a compact sidebar.
- [X] T017 Refine post presentation in `Mushroom_Finder/Pages/Shared/Forum.cshtml` and `Mushroom_Finder/Pages/Shared/Forum.cshtml.css` by removing sample image rendering without deleting image assets or model fields, fixing horizontal overflow, and keeping text content readable.
- [ ] T018 Complete design and functional regression checks for the revised Forum at 320px, 767px, 768px, and 1280px; confirm category filtering still works and that post information and applicable safety disclaimers remain visible. Keep T009, T012, and T013 as separate manual validation responsibilities for filtering/empty states, Chrome with NVDA, and Safari with VoiceOver.
