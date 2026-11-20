# Implementation Plan: Community Forum

**Branch**: `Forum_trinhcl` | **Date**: 2026-11-18 | **Spec**: [spec.md](./spec.md)

**Input**: Feature specification in `specs/001-community-forum/spec.md`

## Summary

Improve the existing Razor Pages forum into a responsive, text-only sample feed for the two specified post categories. Keep the example content in a presentation-only model, retain optional photo fields for future database integration, render the feed in the existing Forum page, and use small page-scoped CSS and vanilla JavaScript for category filtering and accessible result announcements. Display the clarified safety disclaimer on identification requests and on shared posts only when they make identification or edibility claims. No persistence, authentication, comment flows, or external contracts are introduced.

## Technical Context

**Language/Version**: C# / .NET 8 (`net8.0`), nullable reference types enabled.

**Primary Dependencies**: ASP.NET Core Razor Pages and the locally bundled Bootstrap assets. Use browser-native JavaScript; add no packages.

**Storage**: None for this feature. Use static sample presentation data; do not add database reads or writes.

**Testing**: No test project or test files were found. Validate with `dotnet build Mushroom_Finder/Mushroom_Finder.csproj` and the manual browser/accessibility scenarios in [quickstart.md](./quickstart.md).

**Target Platform**: Existing ASP.NET Core web application, browsed on desktop and narrow/mobile viewports. Reference manual validation uses current stable Chrome with NVDA on Windows and current stable Safari with VoiceOver on macOS; this matrix does not define a broader support guarantee.

**Project Type**: Single-project Razor Pages web application.

**Performance Goals**: No network-backed feed or large data set is in scope. Filtering the two supplied examples should update immediately without a server round trip.

**Constraints**: Preserve the specified browse-only scope. Keep the current sample feed text-only while retaining optional photo fields for future database integration; any future photos must be project-owned or public-domain/CC0 with documented source and usage rights. Meet the spec's WCAG 2.2 AA contrast ratios, keyboard behavior, screen-reader announcements, and responsive criteria at 320, 767, and 768 CSS pixels. Keep browsing public at `/Forum` without adding authentication flows or changes.

**Scale/Scope**: One forum page, two categories, two text-only example posts, in-page filtering, empty states, optional photo-field support for future integration, and safety messaging. No comments, post creation, authentication, or persistence.

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

The repository's constitution file contains only template placeholders and no ratified project principles or actionable gates. No project-specific constitution constraints can be evaluated. This plan follows the feature's explicit limits: no database work, authentication changes, added package dependencies, or unrelated pages.

**Pre-design gate**: No populated constitution rules to violate; proceed within the feature scope.

**Post-design gate**: Design remains limited to the existing Razor Page, its presentation model, page-scoped styling, and page-specific filtering behavior. No constitution violations identified.

## Project Structure

### Documentation (this feature)

```text
specs/001-community-forum/
├── spec.md
├── plan.md
├── research.md
├── data-model.md
└── quickstart.md
```

No `contracts/` directory is needed: the feature adds no external API or system interface. `tasks.md` is produced by the later Spec-Kit tasks phase, not this plan.

### Source Code (repository root)

```text
Mushroom_Finder/
├── Pages/
│   └── Shared/
│       ├── Forum.cshtml             # Existing page; feed and category controls
│       ├── Forum.cshtml.cs          # Existing page model; sample display data
│       └── Forum.cshtml.css         # Planned page-scoped styles
└── wwwroot/
    └── js/
        └── forum.js                 # Planned page-specific filter behavior
```

`Pages/Shared/_Layout.cshtml`, `wwwroot/css/site.css`, and the empty `wwwroot/js/site.js` are existing shared assets; keep the forum-specific styles and behavior scoped to the feature where possible. No test project currently exists.

**Structure Decision**: Keep the current Forum Razor Page and its page model. Add only a scoped stylesheet and a page-specific JavaScript file if needed by the implementation. Render static example posts from the page model. Ensure the existing page and shared navigation resolve to `/Forum`; make only the in-scope route adjustment needed and do not move unrelated pages.

## Design and Implementation Approach

1. **Route and page model**: Ensure signed-out visitors can reach `/Forum` from shared navigation. Supply the two example posts and category values through presentation-only page-model data.
2. **Feed markup and responsive layout**: Replace the placeholder with semantic text-only sample articles, a heading/intro, category controls, and distinct category labels. Retain conditional optional image markup with meaningful alt text for future use, but do not render images for the current examples. Use a compact sidebar and rounded cards in the dark-brown, cream, and olive-green mockup palette. Stack the sidebar and feed below 768 CSS pixels; use a fixed, compact sidebar plus a min-width-safe feed at 768 CSS pixels and above. Verify long content at the specified test lengths.
3. **Category filtering**: Implement exclusive “All posts,” “ID Request,” and “Just Sharing” selection over the rendered static posts. Update the selected state programmatically and preserve focus on the activated control.
4. **Accessible feedback**: Announce the resulting post count or empty-state message non-interruptively after filtering. Provide explicit full-feed and category-empty states and an “All posts” return control.
5. **Safety and presentation**: Show the disclaimer on every identification-request post and on shared posts only when they name or guess a specimen's species/genus, claim it is edible or safe to consume, or recommend consuming or preparing it. Keep current sample cards image-free while retaining optional image data and conditional markup, and ensure long text wraps without horizontal overflow.
6. **Validation**: Build the existing project and run the manual scenarios from `quickstart.md`, including signed-out route navigation, text-only example rendering, filtering, empty states, 320/767/768 CSS-pixel layouts, keyboard/focus behavior, the reference screen-reader matrix, contrast, future-photo provenance expectations, and safety copy.

**Contracts**: None. This is an internal page presentation feature with no externally consumed API.

## Complexity Tracking

No constitution violations or additional architectural complexity require justification.
