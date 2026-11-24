# Feature Specification: Community Forum

**Feature Branch**: `Forum_trinhcl`
**Created**: 2026-11-18
**Status**: Draft
**Input**: Improve the Mushroom Finder Community Forum page so users can browse identification requests and shared mushroom posts through a clearly organized interface.

## Existing Page Context

The current `Mushroom_Finder/Pages/Shared/Forum.cshtml` is a two-column scaffold: a sidebar with “ID Request” and “Just Sharing” buttons plus location guidance, and a feed containing one incomplete post placeholder. The buttons do not currently filter content, and forum-specific styling is not defined in the current page stylesheet. The page uses the shared site navigation and centered content container from `Mushroom_Finder/Pages/Shared/_Layout.cshtml`, with Bootstrap and `Mushroom_Finder/wwwroot/css/site.css` loaded.

## Clarifications

### Session 2026-11-18

- Q: Should this browse-focused feature include a “View discussion” action that takes users beyond the feed? (OQ-001) → A: Exclude the action; keep post content readable in the feed.
- Q: Should the two supplied example posts show sample mushroom photos even though photos are optional for other posts? → A: The initial design called for photos; the current presentation phase uses a text-only sample feed. Photos remain optional for future integration.
- Q: When a visitor changes a category, how should a screen-reader user be informed that the feed has updated? → A: Announce the updated result count or empty state without moving focus from the selected category control.
- Q: Where should the identification and edibility disclaimer appear in the forum? → A: Show it on every identification-request post and on shared posts only when their content makes an identification or edibility claim.

## User Scenarios & Testing

### User Story 1 - Browse the community feed (Priority: P1)

As a visitor to the Community Forum, I want to see identification requests and shared mushroom posts together so that I can browse recent community content.

**Why this priority**: A combined, readable feed is the core value of the page.

**Independent Test**: Load the text-only sample feed and verify that both categories and their post information are visible without selecting a filter.

**Acceptance Scenarios**:

1. **Given** the forum page is opened, **When** it loads, **Then** the page heading, introductory text, location guidance, category controls, and posts from both categories are visible.
2. **Given** a post is shown in the feed, **When** the user reads its card, **Then** its category, title, author, date, and body are distinguishable.
3. **Given** a post has a mushroom photo, **When** it is displayed, **Then** the image has appropriate alternative text.
4. **Given** a visitor is signed out, **When** they open `/Forum` from the shared Forum navigation, **Then** the forum loads without requiring sign-in.
5. **Given** the current text-only sample feed is displayed, **When** a visitor browses either example post, **Then** no mushroom illustration is rendered and its text content remains readable, while optional photo fields remain available for future integration.

### User Story 2 - Filter by post category (Priority: P1)

As a visitor, I want to filter the feed by identification requests or shared posts so that I can focus on the content I am interested in.

**Why this priority**: Separating questions from observations makes the feed easier to browse.

**Independent Test**: Load example posts from both categories, select each category control, and verify that only matching posts remain; then restore the combined feed.

**Acceptance Scenarios**:

1. **Given** the combined feed is shown, **When** the user selects “ID Request,” **Then** only identification-request posts are shown and the selected category is conveyed visually and programmatically.
2. **Given** the combined feed is shown, **When** the user selects “Just Sharing,” **Then** only shared posts are shown and the selected category is conveyed visually and programmatically.
3. **Given** either category is selected, **When** the user selects “All posts,” **Then** posts from both categories are shown and “All posts” is conveyed as selected.
4. **Given** a screen-reader user changes the selected category, **When** the feed updates, **Then** the resulting post count or empty state is announced without moving focus from the selected category control.

### User Story 3 - Browse the forum across devices and input methods (Priority: P2)

As a visitor using a keyboard, screen reader, or narrow screen, I want the forum controls and posts to remain accessible and usable so that I can browse the same content.

**Why this priority**: Category filtering and post content must remain available regardless of input method or viewport.

**Independent Test**: Navigate the page using only a keyboard and inspect it at narrow and wide viewport widths; verify controls, labels, focus, and content remain usable.

**Acceptance Scenarios**:

1. **Given** the user navigates with a keyboard, **When** they reach a category control, **Then** it can be operated and has a visible focus indicator.
2. **Given** the forum is displayed at a 320 CSS-pixel viewport width, **When** the user browses the page, **Then** the sidebar stacks above the feed and no horizontal page scrolling is needed.
3. **Given** category controls and labels are displayed, **When** color is unavailable or not perceived, **Then** their text still communicates the category and selected state.
4. **Given** the viewport is 767 CSS pixels wide, **When** the forum is displayed, **Then** the sidebar stacks above the feed; **Given** the viewport is 768 CSS pixels wide, **When** the forum is displayed, **Then** the sidebar and feed use a two-column layout.

### Edge Cases

- When the combined feed contains no posts, show a clear empty state rather than a blank feed.
- When a selected category contains no posts, show a category-specific empty state and a way to return to “All posts.”
- When a post has no photo, omit the image or show a neutral placeholder that does not imply a missing or failed identification.
- For long-content acceptance, use a 256-character title, a 2,000-character body, and a 64-character unbroken text sequence; each must wrap without obscuring controls or causing horizontal overflow at 320 CSS pixels. No title or body length limit is introduced.
- When a title does not contain a bracketed general location, display the title as provided; the location guidance does not imply validation or block display.
- Do not require a precise location in titles or expose more location detail than the sample general-region convention.
- Category labels and selected states must remain understandable without color.

### Edge Case Acceptance Scenarios

1. **Given** the entire feed contains no posts, **When** the forum loads, **Then** a clear empty state is displayed instead of a blank feed.
2. **Given** a category is selected and has no matching posts, **When** the feed is displayed, **Then** a category-specific empty state and an “All posts” control are visible; **When** the user selects “All posts,” **Then** the combined feed is restored.
3. **Given** a post has no photo, **When** its card is displayed, **Then** the post content remains readable and no broken image is shown.
4. **Given** a post has a 256-character title, a 2,000-character body, or a 64-character unbroken text sequence, **When** its card is displayed at 320 CSS pixels, **Then** the text wraps within the card and causes no horizontal overflow; no content-length limit is imposed.
5. **Given** a post title has no bracketed general location, **When** the post is displayed, **Then** its title is shown as provided without being rejected or altered.
6. **Given** an identification-request post is displayed, or a shared post names or guesses the specimen's species/genus, claims a mushroom is edible or safe to consume, or recommends consuming or preparing it, **When** the user reads it, **Then** a clear disclaimer states that community discussion does not verify species identification or edibility.

## Requirements

### Functional Requirements

- **FR-001**: The page MUST present a “Community Forum” heading, a concise description, and the existing location guidance.
- **FR-002**: The default feed MUST show posts from both supported categories: “ID Request” and “Just Sharing.”
- **FR-003**: The page MUST provide “All posts,” “ID Request,” and “Just Sharing” browsing controls.
- **FR-004**: Selecting a category MUST show only posts in that category; selecting “All posts” MUST restore posts from both categories.
- **FR-005**: The selected browsing control MUST be visually identifiable and expose its state programmatically.
- **FR-006**: Each post card MUST show its category, title, author display name, date, and body. A photo MAY be included.
- **FR-007**: The page MUST display the provided location guidance about putting a general location in square brackets in the post title. This convention is guidance only; it MUST NOT be presented as a requirement to provide precise location data.
- **FR-008**: The page MUST show a useful empty state when either the entire feed or a selected category has no posts. A category-specific empty state MUST offer a return to the combined feed.
- **FR-009**: Informative photos MUST have descriptive alternative text. Decorative images MUST have empty alternative text.
- **FR-010**: Every identification-request post MUST display a clear disclaimer that community discussion does not verify species identification or edibility. A shared post MUST display the disclaimer if it names or guesses the specimen's species or genus, claims a mushroom is edible or safe to consume, or recommends consuming or preparing it.
- **FR-011**: Below 768 CSS pixels, the page MUST stack the sidebar above the feed. At 768 CSS pixels and above, it MUST use a sidebar-plus-feed layout. At 320 CSS pixels, content MUST remain usable without horizontal page scrolling.
- **FR-012**: Interactive controls MUST be operable by keyboard, have visible focus, and have accessible names. Category meaning and selection MUST NOT rely on color alone. After filtering, assistive technology MUST be informed of the resulting post count or empty state without moving focus from the selected control.
- **FR-013**: Text and applicable non-text UI components and state indicators MUST meet WCAG 2.2 AA contrast thresholds: at least 4.5:1 for normal text, 3:1 for large text (at least 18 pt regular or 14 pt bold), and 3:1 for applicable non-text UI components and state indicators.
- **FR-014**: Post cards MUST present title, metadata, and body as visually distinct information, with long text wrapping within the available card width.
- **FR-015**: The forum MUST be directly reachable at `/Forum`, and the shared Forum navigation link MUST resolve to that route.
- **FR-016**: Forum browsing MUST be available to signed-out visitors. No sign-in, account, or posting flow is included in this feature.
- **FR-017**: The current sample feed MUST NOT display example photos. Optional photo fields remain available for future database integration; any photos introduced then MUST be project-owned or public-domain/CC0 assets with source and usage-rights basis documented alongside the assets.

### Key Entities

- **Forum post (presentation content)**: A sample or displayed entry with a category, title, author display name, date, body, and optional photo with alternative text. This is a UI content shape, not a persistence requirement.
- **Post category (presentation state)**: One of “ID Request” or “Just Sharing,” used to label and filter displayed posts.
- **Selected category (presentation state)**: “All posts,” “ID Request,” or “Just Sharing,” used to determine which post cards are visible and which control is active.

### Success Criteria

#### Measurable Outcomes

- **SC-001**: On initial load with the supplied examples, users can see at least one post from each category without interacting with a filter.
- **SC-002**: Selecting either category shows zero posts from the other category; selecting “All posts” restores both example categories.
- **SC-003**: At 320 CSS pixels the page has no horizontal overflow; at 767 CSS pixels the sidebar is stacked above the feed; at 768 CSS pixels the sidebar and feed are in two columns.
- **SC-004**: All category controls can be reached and operated using only a keyboard, with the selected state available to assistive technology.
- **SC-005**: Every displayed example post exposes its category, title, author, date, and body as readable text.
- **SC-006**: When the feed has no posts, a visible empty state is shown; when a selected category has no matches, its empty state includes an “All posts” control that restores the combined feed when selected.
- **SC-007**: Every displayed identification-request post, and every shared post that names or guesses the specimen's species/genus, claims it is edible or safe to consume, or recommends consuming or preparing it, includes a visible disclaimer that community discussion does not verify species identification or edibility.
- **SC-008**: After a category filter is applied, assistive technology receives the resulting post count or empty-state message, and focus remains on the selected category control.
- **SC-009**: A signed-out visitor can open `/Forum` from the shared Forum navigation without an authentication challenge.
- **SC-010**: All normal text has at least 4.5:1 contrast; large text (at least 18 pt regular or 14 pt bold) and applicable non-text UI components and state indicators have at least 3:1 contrast.
- **SC-011**: At 320 CSS pixels, a 256-character title, 2,000-character body, and 64-character unbroken sequence each display without horizontal overflow.

## Assumptions

- The initial feed content may use static sample posts; server-side post creation and persistence are not required for this feature.
- The site’s existing shared layout and navigation remain in place.
- The two category names remain exactly “ID Request” and “Just Sharing.”
- Dates in the examples are display content; sorting, search, pagination, and moderation behavior are not defined by this feature.
- No authenticated or anonymous posting flow is part of this browse-focused scope.
- Forum browsing is public and does not require sign-in.
- Manual acceptance uses current stable Chrome with NVDA on Windows and current stable Safari with VoiceOver on macOS; this is a validation matrix, not a broader browser-support guarantee.
- The current static sample posts are text-only. Optional photo fields are retained for future database integration; any photos used in that integration must be project-owned or public-domain/CC0 and have source and usage-rights documentation.

## Scope Boundaries

### In Scope

- The forum heading and introductory guidance.
- A responsive sidebar and post feed.
- Category labels, category filtering, and selected states.
- Example identification-request and shared posts.
- Empty states, safety wording, styling guidance, and accessibility requirements.

### Out of Scope

- Authentication, authorization, accounts, and user profiles.
- Database persistence or server-side post creation.
- Submitting, saving, editing, or deleting posts or comments.
- Comment creation, moderation, reactions, and post bookmarking.
- “View discussion” actions and separate post-detail views.
- Search, sorting, and pagination.

## Example Content

### ID Request

- **Title**: `[Pacific Northwest] Brown mushrooms growing on a fallen log`
- **Author/date**: `ForestWalker · 2 days ago`
- **Body**: “Found these in a damp, shaded area. The caps are about 5 cm wide. Can anyone suggest features I should check?”
- **Photo**: Not displayed in the current text-only sample feed. The optional photo fields remain available for future integration; any future photo must have descriptive alternative text and documented project-owned or public-domain/CC0 rights.

### Just Sharing

- **Title**: `[Northern California] A morning walk after the rain`
- **Author/date**: `MossAndMore · 5 hours ago`
- **Body**: “Spotted several clusters along the trail today. I loved the range of shapes and colors.”
- **Photo**: Not displayed in the current text-only sample feed. The optional photo fields remain available for future integration; any future photo must have descriptive alternative text and documented project-owned or public-domain/CC0 rights.

Show the identification and edibility disclaimer on every identification-request post and on shared posts only when they name or guess the specimen's species/genus, claim a mushroom is edible or safe to consume, or recommend consuming or preparing it.

## Visual Design Guidance

- Use the original mockup's dark-brown, cream, and olive-green palette, with readable contrast.
- Use rounded cards, consistent spacing, clear borders or subtle shadows, a strong visual distinction between titles, metadata, and body text, and a compact sidebar that does not stretch unnecessarily.
- Stack the sidebar above the feed below 768 CSS pixels; use the sidebar-plus-feed composition at 768 CSS pixels and wider.
- Use category text labels as well as color, and provide clear hover, pressed, selected, and keyboard-focus states.
