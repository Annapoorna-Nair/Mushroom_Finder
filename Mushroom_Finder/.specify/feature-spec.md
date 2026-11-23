# Feature Specification: Mushroom Identifier

**Feature Branch**: `001-mushroom-identifier`
**Created**: 2026-11-23
**Status**: Draft
**Input**: User description: "Create a web application that allows users to identify mushrooms by selecting color, location, cap shape, height, and width characteristics. Display the questions on the left and identification results on the right. Store the submitted information in memory for the current user. The application must run locally in Visual Studio 2026 and be deployable to Azure."

## User Scenarios & Testing

### User Story 1 - Submit Mushroom Characteristics (Priority: P1)

As a user, I want to describe a mushroom using its observable characteristics so that the application can identify likely matching mushrooms.

**Why this priority**: Collecting the initial characteristics is required for every identification attempt.

**Independent Test**: A user can open the Identifier page, select valid values for every initial characteristic, submit the form, and verify that the application processes the submission.

**Acceptance Scenarios**:

1. **Given** the Identifier page is displayed, **When** the user views the form, **Then** the form displays controls for color, location, cap shape, height, and width.
2. **Given** the user is completing the form, **When** the user selects one or two colors, **Then** the selected colors are shown as the current color choices.
3. **Given** the user has selected valid values for every initial characteristic, **When** the user submits the form, **Then** the application evaluates the submitted characteristics.
4. **Given** the user has not selected a required characteristic, **When** the user submits the form, **Then** the application displays a clear validation message identifying the missing selection.

---

### User Story 2 - Provide Additional Characteristics for Ambiguous Results (Priority: P1)

As a user, I want to provide additional mushroom characteristics when the initial answers identify two or more possible mushrooms so that the application can narrow the results.

**Why this priority**: Additional characteristics are necessary when the initial identification is ambiguous.

**Independent Test**: A test dataset can be configured so that the initial submission produces two or more candidates. The user can then complete the additional fields and submit them for refined identification.

**Acceptance Scenarios**:

1. **Given** the user has submitted valid characteristics, **When** matching results are available, **Then** the results are displayed on the right side of the page.
2. **Given** the submitted characteristics do not match a known mushroom, **When** the identification is completed, **Then** the results area displays a clear no-match message.
3. **Given** the results are displayed, **When** the user reviews them, **Then** the selected characteristics remain visible on the left side of the page.
4. **Given** the page is viewed on a narrow screen, **When** the form and results are displayed, **Then** the content remains usable and the two areas may stack vertically without loss of functionality.

---

### User Story 3 - Preserve the Current User's Submission (Priority: P2)

As a user, I want my submitted characteristics to remain available during my session so that I can review or resubmit them without losing my selections.

**Why this priority**: Preserving the submission supports comparison and correction while keeping the first version of the application simple.

**Independent Test**: A user can submit characteristics, reload or revisit the Identifier page during the same active session, and verify that the latest submission is available to the application for that user.

**Acceptance Scenarios**:

1. **Given** the user has submitted valid characteristics, **When** the user revisits the Identifier page during the same active session, **Then** the latest submitted characteristics remain associated with that user.
2. **Given** the user submits a new set of characteristics, **When** the submission succeeds, **Then** the new values replace the user's previous in-memory submission.
3. **Given** the application restarts or the user's session expires, **When** the user returns to the Identifier page, **Then** the previous in-memory submission is no longer required to be available.

---

### Edge Cases

- The user selects no colors.
- The user selects more than two colors.
- The user selects duplicate colors.
- The user submits the form with one or more required fields missing.
- A submitted value is not one of the supported options.
- The submitted characteristics produce no matching mushroom.
- The user submits the same characteristics multiple times.
- The user opens the Identifier page in separate browser sessions.
- The in-memory data is unavailable after an application restart, deployment, or process recycle.
- The application is deployed to multiple Azure instances and a subsequent request is handled by a different instance.
- The user accesses the page from a narrow viewport or keyboard-only navigation.
- An unexpected identification or storage error occurs.

## Requirements

### Functional Requirements

- **FR-001**: The system MUST provide an Identifier page for submitting mushroom characteristics.
- **FR-002**: The Identifier page MUST provide a color control that allows the user to select a minimum of one and a maximum of two colors.
- **FR-003**: The supported color options MUST be:
  - Red
  - Orange
  - Yellow
  - Green
  - Blue
  - Purple
  - Pink
  - White
  - Brown
  - Black
  - Gray
- **FR-004**: The Identifier page MUST provide a location control that allows exactly one selection.
- **FR-005**: The supported location options MUST be:
  - On a tree
  - On the ground
  - On a tree root
  - On a dead tree
- **FR-006**: The Identifier page MUST provide a cap shape control that allows exactly one selection.
- **FR-007**: The supported cap shape options MUST be:
  - Funnel
  - Cone
  - Bell
  - Half-egg
  - Round
  - Flat
  - Central knob
  - Depression
- **FR-008**: The Identifier page MUST provide a height control that allows exactly one selection.
- **FR-009**: The supported height options MUST be:
  - Tall
  - Short
  - Medium
- **FR-010**: The Identifier page MUST provide a width control that allows exactly one selection.
- **FR-011**: The supported width options MUST be:
  - Thin
  - Fat
  - Medium
- **FR-012**: The system MUST validate all submitted values against the supported option lists.
- **FR-013**: The system MUST reject submissions that contain fewer than one or more than two colors.
- **FR-014**: The system MUST reject submissions missing a location, cap shape, height, or width.
- **FR-015**: The system MUST display clear, user-readable validation messages for invalid or incomplete submissions.
- **FR-016**: The system MUST process a valid submission to determine matching mushroom results.
- **FR-017**: The system MUST display the identification results on the right side of the Identifier page when the page has sufficient display width.
- **FR-018**: The system MUST display the submitted questions and selection controls on the left side of the Identifier page when the page has sufficient display width.
- **FR-019**: The system MUST display a clear no-match message when no mushroom matches the submitted characteristics.
- **FR-020**: The system MUST preserve the submitted characteristics in memory for the current user during the active session.
- **FR-021**: The system MUST associate in-memory submitted characteristics with the correct user session and MUST NOT expose one user's submission to another user.
- **FR-022**: The system MUST replace the current user's previous in-memory submission when a new valid submission is made.
- **FR-023**: The system MUST NOT require in-memory submissions to survive an application restart, process recycle, deployment, or session expiration.
- **FR-024**: The page MUST support keyboard navigation and provide labels or equivalent accessible names for all form controls.
- **FR-025**: The page MUST remain usable on desktop and narrow-screen layouts.
- **FR-026**: The application MUST provide a safe and understandable error message if identification processing fails.
- **FR-027**: The application MUST be runnable locally in Visual Studio 2026.
- **FR-028**: The application MUST be deployable and runnable in Azure without requiring changes to the user-facing identification workflow.

### Key Entities

- **MushroomCharacteristics**: The user's submitted observations, consisting of one or two colors, one location, one cap shape, one height, and one width.
- **MushroomMatch**: A mushroom identification result associated with a set of supported characteristics. It includes the mushroom's display name and any user-facing identification information defined by the application.
- **IdentificationResult**: The result of comparing submitted characteristics against available mushroom matches. It may contain one or more matching mushrooms or a no-match state.
- **UserSessionSubmission**: The latest `MushroomCharacteristics` submission held in memory for a user's active session.

## Success Criteria

### Measurable Outcomes

- **SC-001**: A user can complete and submit all required characteristics in no more than two minutes without assistance.
- **SC-002**: At least 95% of valid submissions display an identification result or no-match message within two seconds under normal local operating conditions.
- **SC-003**: 100% of submitted values are validated against the supported option lists before identification processing.
- **SC-004**: 100% of valid submissions display the questions on the left and the results on the right on supported desktop layouts.
- **SC-005**: 100% of required controls provide an accessible label and an actionable validation message when invalid.
- **SC-006**: A user's in-memory submission is never displayed as another user's submission during the same application execution.
- **SC-007**: The application can be started locally in Visual Studio 2026 and deployed to Azure using the project's documented deployment process.
- **SC-008**: The identification workflow remains usable when the page is viewed on a narrow screen or operated using keyboard navigation.

## Assumptions

- The first version uses an anonymous active browser session to identify the current user; a registration or sign-in workflow is out of scope unless separately specified.
- "Stored in memory" means the latest valid submission is held only for the active user session and is not required to survive application restart, process recycle, deployment, or session expiration.
- The initial identification data source and matching rules will be defined during planning. This specification does not prescribe a particular storage technology or matching algorithm.
- Results will be based on matching the submitted characteristics against the application's available mushroom data.
- The first version does not require image upload or image-recognition functionality.
- The first version does not provide medical, legal, or food-safety advice. Identification results must not be presented as a guarantee that a mushroom is safe to consume.
- Azure deployment configuration, hosting tier, scaling strategy, and production monitoring will be defined during planning.
- If Azure deployment uses multiple application instances, session affinity or another approved session-scoped approach may be required to preserve in-memory submissions.
- The existing .NET 8 Razor Pages project remains the application host.
- The feature will follow the project constitution, including code quality, testing, accessibility, performance, and security requirements.