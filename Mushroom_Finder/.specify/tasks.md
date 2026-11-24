---

description: "Implementation tasks for the Mushroom Identifier feature"

# Tasks: Mushroom Identifier

**Input**: `.specify/feature-spec.md`, `.specify/memory/constitution.md`

**Project**: .NET 8 ASP.NET Core Razor Pages application in `Mushroom_Finder/`

**Prerequisites**: Approved feature specification and implementation plan. Application implementation MUST NOT begin until the user explicitly issues `speckit.implement`.

## Task Format

- `[P]`: Can be completed in parallel with other `[P]` tasks in the same phase.
- `[US1]`–`[US5]`: Maps the task to a feature user story.
- Tests MUST be written before the implementation they verify and MUST initially fail.
- All implementation tasks are blocked until `speckit.implement` is issued.

## Phase 1: Setup

**Purpose**: Establish the test structure and feature organization without changing application behavior.

- [ ] T001 Confirm the approved feature specification and identify all requirements from `FR-001` through `FR-048`.
- [ ] T002 [P] Create the test project structure at `tests/Mushroom_Finder.Tests/` using the repository's existing .NET conventions.
- [ ] T003 [P] Add the test project to the solution and configure it to target .NET 8.
- [ ] T004 [P] Create feature directories for domain models, services, and session handling under `Mushroom_Finder/Features/Identifier/`.
- [ ] T005 [P] Define the test data location and format for mushroom matches, images, edibility classifications, safety guidance, Poison Control links, and forum links.
- [ ] T006 Document the local and Azure configuration values required by the Identifier feature without adding secrets to source control.

---

## Phase 2: Foundational Infrastructure

**Purpose**: Implement shared infrastructure required by all user stories.

- [ ] T007 [P] Create the initial mushroom characteristic models in `Mushroom_Finder/Features/Identifier/Models/MushroomCharacteristics.cs` and `Mushroom_Finder/Features/Identifier/Models/AdditionalMushroomCharacteristics.cs`.
- [ ] T008 [P] Create result and safety models in `Mushroom_Finder/Features/Identifier/Models/IdentificationResult.cs`, `Mushroom_Finder/Features/Identifier/Models/MushroomMatch.cs`, and `Mushroom_Finder/Features/Identifier/Models/SafetyGuidance.cs`.
- [ ] T009 [P] Create the user-session submission model in `Mushroom_Finder/Features/Identifier/Models/UserSessionSubmission.cs`.
- [ ] T010 [P] Create validation tests for supported colors, locations, cap shapes, heights, widths, gills, stem shapes, and clustering values in `tests/Mushroom_Finder.Tests/Features/Identifier/Models/CharacteristicValidationTests.cs`.
- [ ] T011 Create the supported-option definitions and centralized validation service in `Mushroom_Finder/Features/Identifier/Services/IdentifierValidationService.cs`.
- [ ] T012 Create failing unit tests for invalid values, missing required values, duplicate colors, fewer than one color, and more than two colors in `tests/Mushroom_Finder.Tests/Features/Identifier/Services/IdentifierValidationServiceTests.cs`.
- [ ] T013 Implement validation behavior required by `FR-012` through `FR-015` and `FR-024`.
- [ ] T014 Create the mushroom data provider contract in `Mushroom_Finder/Features/Identifier/Services/IMushroomDataProvider.cs`.
- [ ] T015 Create the identification service contract in `Mushroom_Finder/Features/Identifier/Services/IIdentifierService.cs`.
- [ ] T016 Create failing unit tests for zero-match, single-match, and multiple-match identification outcomes in `tests/Mushroom_Finder.Tests/Features/Identifier/Services/IdentifierServiceTests.cs`.
- [ ] T017 Implement the identification service in `Mushroom_Finder/Features/Identifier/Services/IdentifierService.cs`.
- [ ] T018 Create the session-submission service contract in `Mushroom_Finder/Features/Identifier/Services/IUserSessionSubmissionStore.cs`.
- [ ] T019 Create failing isolation tests proving that one user's submission cannot be returned for another user in `tests/Mushroom_Finder.Tests/Features/Identifier/Services/UserSessionSubmissionStoreTests.cs`.
- [ ] T020 Implement the in-memory user-session submission store in `Mushroom_Finder/Features/Identifier/Services/UserSessionSubmissionStore.cs`.
- [ ] T021 Register the Identifier services and in-memory storage in `Mushroom_Finder/Program.cs`.
- [ ] T022 Configure the session or user-scoping mechanism required by the approved plan and ensure it is compatible with local execution and the selected Azure hosting configuration.
- [ ] T023 Add safe logging and exception handling for identification and session-storage failures without logging submitted sensitive data.

**Checkpoint**: Validation, matching, safety metadata, and user-session storage are independently testable before the Razor Page is implemented.

---

## Phase 3: User Story 1 - Submit Initial Characteristics (Priority: P1)

**Goal**: Allow users to submit valid initial mushroom characteristics.

**Independent Test**: Submit valid and invalid initial forms and verify validation and processing behavior.

### Tests First

- [ ] T024 [P] [US1] Create Razor Page integration tests for rendering the Identifier page in `tests/Mushroom_Finder.Tests/Features/Identifier/IdentifierPageInitialFormTests.cs`.
- [ ] T025 [P] [US1] Create integration tests for valid initial submissions, missing fields, invalid values, and invalid color counts in `tests/Mushroom_Finder.Tests/Features/Identifier/IdentifierPageInitialSubmissionTests.cs`.
- [ ] T026 [P] [US1] Create accessibility-oriented tests verifying labels, validation summaries, and keyboard-operable controls in `tests/Mushroom_Finder.Tests/Features/Identifier/IdentifierPageAccessibilityTests.cs`.

### Implementation

- [ ] T027 [US1] Create the Identifier Page model in `Mushroom_Finder/Pages/Identifier.cshtml.cs`.
- [ ] T028 [US1] Implement the initial characteristic binding, validation, and POST handler in `Mushroom_Finder/Pages/Identifier.cshtml.cs`.
- [ ] T029 [US1] Create the initial characteristic form in `Mushroom_Finder/Pages/Identifier.cshtml`.
- [ ] T030 [US1] Add labels, validation messages, validation summary, and safe user-facing error handling to `Mushroom_Finder/Pages/Identifier.cshtml`.
- [ ] T031 [US1] Ensure the initial form renders the exact supported option lists defined in `FR-003`, `FR-005`, `FR-007`, `FR-009`, and `FR-011`.
- [ ] T032 [US1] Preserve valid initial submissions through the user-session submission store.
- [ ] T033 [US1] Run the User Story 1 tests and verify that the initial form works independently.

**Checkpoint**: A user can submit valid initial characteristics and receive either a result, no-match message, or follow-up form.

---

## Phase 4: User Story 2 - Refine Ambiguous Results (Priority: P1)

**Goal**: Request and process gills, stem shape, and clustering characteristics only when the initial result contains two or more candidates.

**Independent Test**: Submit characteristics that produce multiple candidates, complete the follow-up form, and verify that the result set is narrowed or remains multiple.

### Tests First

- [ ] T034 [P] [US2] Create integration tests proving the follow-up form is displayed when the initial result contains two or more candidates in `tests/Mushroom_Finder.Tests/Features/Identifier/IdentifierPageAmbiguousResultTests.cs`.
- [ ] T035 [P] [US2] Create integration tests proving the follow-up form is not required for zero or one initial candidate in `tests/Mushroom_Finder.Tests/Features/Identifier/IdentifierPageConditionalFormTests.cs`.
- [ ] T036 [P] [US2] Create tests for follow-up validation and all supported gills, stem shape, and clustering values in `tests/Mushroom_Finder.Tests/Features/Identifier/IdentifierPageFollowUpValidationTests.cs`.
- [ ] T037 [P] [US2] Create tests for follow-up submissions that produce one result, multiple results, or no result in `tests/Mushroom_Finder.Tests/Features/Identifier/IdentifierPageRefinedResultsTests.cs`.

### Implementation

- [ ] T038 [US2] Add follow-up characteristic binding and validation to `Mushroom_Finder/Pages/Identifier.cshtml.cs`.
- [ ] T039 [US2] Add conditional follow-up form rendering to `Mushroom_Finder/Pages/Identifier.cshtml`.
- [ ] T040 [US2] Render the exact gills options required by `FR-019`.
- [ ] T041 [US2] Render the exact stem shape options required by `FR-021`.
- [ ] T042 [US2] Render the exact clustering options required by `FR-023`.
- [ ] T043 [US2] Implement refined matching using the initial and additional characteristics.
- [ ] T044 [US2] Store the complete submission after valid follow-up processing.
- [ ] T045 [US2] Run the User Story 2 tests and verify that conditional questioning works independently.

**Checkpoint**: Ambiguous initial results request the additional characteristics and produce a refined outcome.

---

## Phase 5: User Story 3 - Display Identification and Safety Results (Priority: P1)

**Goal**: Display the result image, name, edibility classification, safety guidance, and unresolved-result guidance.

**Independent Test**: Verify the UI for edible, poisonous, unsure, no-match, and still-multiple outcomes.

### Tests First

- [ ] T046 [P] [US3] Create tests for single edible results, including image, classification, and safe preparation guidance, in `tests/Mushroom_Finder.Tests/Features/Identifier/IdentifierEdibleResultTests.cs`.
- [ ] T047 [P] [US3] Create tests for poisonous results and the configured official Poison Control link in `tests/Mushroom_Finder.Tests/Features/Identifier/IdentifierPoisonousResultTests.cs`.
- [ ] T048 [P] [US3] Create tests for unsure results and the instruction not to eat or interact with the mushroom in `tests/Mushroom_Finder.Tests/Features/Identifier/IdentifierUnsureResultTests.cs`.
- [ ] T049 [P] [US3] Create tests for unresolved multiple results, forum guidance, and the instruction not to interact with the mushroom in `tests/Mushroom_Finder.Tests/Features/Identifier/IdentifierMultipleResultTests.cs`.
- [ ] T050 [P] [US3] Create tests for missing images, missing classifications, missing safety content, and missing external links in `tests/Mushroom_Finder.Tests/Features/Identifier/IdentifierResultFallbackTests.cs`.

### Implementation

- [ ] T051 [US3] Add result presentation properties to `Mushroom_Finder/Pages/Identifier.cshtml.cs`.
- [ ] T052 [US3] Create the right-side results panel in `Mushroom_Finder/Pages/Identifier.cshtml`.
- [ ] T053 [US3] Render an accessible mushroom image with meaningful alternative text or a safe fallback message.
- [ ] T054 [US3] Render edible results with safe preparation guidance and a non-guarantee safety disclaimer.
- [ ] T055 [US3] Render poisonous results with the configured official Poison Control link.
- [ ] T056 [US3] Render unsure results with a clear instruction not to eat or interact with the mushroom.
- [ ] T057 [US3] Render unresolved multiple results with forum guidance and a clear instruction not to interact with or consume the mushroom.
- [ ] T058 [US3] Render no-match and processing-error messages.
- [ ] T059 [US3] Verify that external links use safe, configured destinations and appropriate link text.
- [ ] T060 [US3] Run the User Story 3 tests and verify all result categories independently.

**Checkpoint**: Every supported result category displays appropriate identification and safety information.

---

## Phase 6: User Story 4 - Preserve the Current User's Submission (Priority: P2)

**Goal**: Preserve and isolate the latest submission during the active user session.

**Independent Test**: Submit, revisit, replace, and isolate submissions across separate sessions.

### Tests First

- [ ] T061 [P] [US4] Create tests for restoring the current user's latest submission in `tests/Mushroom_Finder.Tests/Features/Identifier/IdentifierSessionRestoreTests.cs`.
- [ ] T062 [P] [US4] Create tests proving a new valid submission replaces the previous submission in `tests/Mushroom_Finder.Tests/Features/Identifier/IdentifierSessionReplacementTests.cs`.
- [ ] T063 [P] [US4] Create tests proving submissions are not shared across sessions in `tests/Mushroom_Finder.Tests/Features/Identifier/IdentifierSessionIsolationTests.cs`.
- [ ] T064 [P] [US4] Create tests documenting the expected behavior after application restart or session expiration in `tests/Mushroom_Finder.Tests/Features/Identifier/IdentifierSessionLifetimeTests.cs`.

### Implementation

- [ ] T065 [US4] Restore the current user's stored submission when the Identifier Page loads.
- [ ] T066 [US4] Replace the stored submission only after successful validation and processing.
- [ ] T067 [US4] Ensure incomplete or invalid submissions do not overwrite the last valid submission.
- [ ] T068 [US4] Verify the storage behavior for local development and the selected Azure deployment topology.
- [ ] T069 [US4] Run the User Story 4 tests and verify session behavior independently.

**Checkpoint**: The latest valid submission is preserved and isolated for the active user session.

---

## Phase 7: User Story 5 - Accessible and Responsive Experience (Priority: P1)

**Goal**: Provide a consistent, accessible, and responsive two-column Identifier experience.

**Independent Test**: Use the complete workflow on desktop, narrow screens, and keyboard-only navigation.

### Tests First

- [ ] T070 [P] [US5] Create tests for left-side questions and right-side results in `tests/Mushroom_Finder.Tests/Features/Identifier/IdentifierLayoutTests.cs`.
- [ ] T071 [P] [US5] Create tests for narrow-screen stacking behavior and responsive classes in `tests/Mushroom_Finder.Tests/Features/Identifier/IdentifierResponsiveTests.cs`.
- [ ] T072 [P] [US5] Create tests for validation announcements, focus behavior, and accessible names in `tests/Mushroom_Finder.Tests/Features/Identifier/IdentifierAccessibilityTests.cs`.

### Implementation

- [ ] T073 [US5] Add responsive layout styles for the Identifier page in `Mushroom_Finder/wwwroot/css/site.css` or the approved feature-specific stylesheet.
- [ ] T074 [US5] Place initial and follow-up questions on the left and results or next-step guidance on the right at desktop widths.
- [ ] T075 [US5] Ensure the form and result panels stack in a usable order at narrow widths.
- [ ] T076 [US5] Ensure all controls are keyboard operable and all validation and result messages are accessible.
- [ ] T077 [US5] Match existing layout, navigation, typography, and interaction patterns from `Mushroom_Finder/Pages/Shared/_Layout.cshtml`.
- [ ] T078 [US5] Run the User Story 5 tests and manually verify desktop, narrow-screen, and keyboard-only workflows.

**Checkpoint**: The complete Identifier experience is usable, accessible, and visually consistent.

---

## Phase 8: Cross-Cutting Quality, Azure Readiness, and Verification

**Purpose**: Satisfy the constitution's code quality, testing, performance, security, and deployment requirements.

- [ ] T079 [P] Add unit and integration coverage for validation failures, empty results, duplicate submissions, missing result metadata, and processing failures.
- [ ] T080 [P] Review all user-facing safety text to ensure that no result is presented as a guarantee of edibility.
- [ ] T081 [P] Review Razor Page handlers to ensure they orchestrate requests and delegate business logic to services.
- [ ] T082 [P] Review input validation and output rendering for injection, unsafe links, and untrusted user-provided values.
- [ ] T083 [P] Add structured diagnostic logging for failures without logging sensitive session data.
- [ ] T084 [P] Measure normal identification response time and verify the feature meets the two-second target under normal local conditions.
- [ ] T085 [P] Verify that matching does not perform unnecessary repeated work or unbounded data loading.
- [ ] T086 Configure Azure-safe settings for session behavior, external resource URLs, logging, and environment-specific configuration.
- [ ] T087 Verify the feature when Azure uses the selected single-instance or multi-instance deployment topology.
- [ ] T088 Run the complete test suite and resolve all failures.
- [ ] T089 Build the solution in Release configuration and resolve all compiler warnings related to the feature.
- [ ] T090 Verify the feature starts locally in Visual Studio 2026.
- [ ] T091 Verify the documented Azure deployment process without committing secrets or environment-specific credentials.
- [ ] T092 Review the implementation against every functional requirement and acceptance scenario in `.specify/feature-spec.md`.
- [ ] T093 Confirm that no out-of-scope application changes were introduced.

---

## Dependencies and Execution Order

### Phase Dependencies

- **Phase 1: Setup** has no implementation dependency.
- **Phase 2: Foundational Infrastructure** depends on Phase 1 and blocks all user story implementation.
- **Phase 3: User Story 1** depends on Phase 2.
- **Phase 4: User Story 2** depends on Phase 3's initial submission and matching flow.
- **Phase 5: User Story 3** depends on the result contracts from Phases 2–4.
- **Phase 6: User Story 4** depends on the session models and submission handlers from Phases 2–4.
- **Phase 7: User Story 5** depends on the Identifier Page from Phases 3–6.
- **Phase 8: Quality and deployment verification** depends on all required user stories.

### Within Each User Story

1. Write tests first.
2. Confirm the new tests fail for the expected reason.
3. Implement models before services.
4. Implement services before Page Model handlers.
5. Implement Page Model handlers before Razor markup.
6. Verify integration behavior.
7. Run the user story checkpoint tests.

### Parallel Opportunities

- Setup tasks marked `[P]` can run in parallel.
- Independent model and contract tasks in Phase 2 can run in parallel.
- Test tasks within each user story can run in parallel.
- Result-category tests in Phase 5 can run in parallel.
- Cross-cutting review tasks in Phase 8 can run in parallel after feature implementation is complete.

## Implementation Strategy

### MVP

1. Complete Setup and Foundational Infrastructure.
2. Implement User Story 1.
3. Implement the initial matching flow.
4. Validate the P1 MVP independently.
5. Continue with conditional refinement and safety results.

### Incremental Delivery

1. Initial characteristics and validation.
2. Ambiguous-result follow-up questions.
3. Identification results and safety guidance.
4. Session preservation.
5. Responsive and accessible presentation.
6. Azure and quality verification.

## Notes

- These tasks define implementation work only; they do not authorize implementation.
- No task that creates or modifies application code may begin until `speckit.implement` is explicitly issued.
- The existing .NET 8 Razor Pages architecture and project conventions take precedence over assumed paths if the approved implementation plan identifies different locations.
- The in-memory session requirement must be reconciled with the selected Azure deployment topology before implementation.