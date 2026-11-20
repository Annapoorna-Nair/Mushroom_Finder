# Community Forum Validation Quickstart

## Prerequisites

- .NET 8 SDK.
- The repository's normal local configuration for the existing ASP.NET Core application.
- A browser with developer tools. A screen reader is needed for assistive-technology announcement checks.

## Build and Run

From the repository root:

```sh
dotnet build Mushroom_Finder/Mushroom_Finder.csproj
dotnet run --project Mushroom_Finder/Mushroom_Finder.csproj
```

Open the application URL printed by `dotnet run`, followed by `/Forum`, while signed out. Confirm the Forum link in the shared navigation reaches the same route without an authentication challenge.

## Reference Validation Matrix

Use the current stable browser and assistive-technology versions available in the test environment:

| Browser and platform | Assistive technology |
|---|---|
| Chrome on Windows | NVDA |
| Safari on macOS | VoiceOver |

This is a reproducible reference matrix, not a broader browser-support guarantee. Run the keyboard, screen-reader, and contrast checks in both combinations; check responsive behavior at 320, 767, 768, and 1280 CSS-pixel viewport widths.

## Acceptance Checks

1. **Initial feed**: “All posts” is selected, and at least one text-only post from each category is visible. Verify category, title, author, date, and body are readable.
2. **Category filters**: Select “ID Request” and then “Just Sharing.” Each selection shows only matching posts, and the active control is visually and programmatically selected. Select “All posts” and verify both categories return.
3. **Announcements and focus**: With a screen reader, change categories. Verify the resulting post count or empty-state text is announced and focus remains on the activated category control.
4. **Full-feed empty state**: With an empty local sample collection, verify a visible empty state appears instead of a blank feed.
5. **Category-empty state**: With sample content that has no posts in the selected category, verify a category-specific message and an “All posts” control; select it and verify the combined feed state returns.
6. **Text-only sample cards**: Verify neither current example renders a mushroom illustration, all post text remains visible, and the optional model photo fields and existing image assets remain available for future integration.
7. **Long and missing-location titles**: Validate a 256-character title, a 2,000-character body, a 64-character unbroken sequence, and a title without `[general location]`. At 320 CSS pixels, content must wrap without overflow; the title without a location prefix must display as supplied. These lengths are test fixtures, not limits.
8. **Responsive layout**: At 320 and 767 CSS pixels, verify the sidebar stacks above the feed and there is no horizontal overflow. At 768 and 1280 CSS pixels, verify the sidebar and feed use two columns.
9. **Keyboard and assistive technology**: In each reference matrix combination, reach and operate every filter using only the keyboard. Verify visible focus, accessible names, exposed selected state, category labels that do not depend on color, result announcements without focus movement, and descriptive alt text for any future informative photos. The current sample posts are text-only.
10. **Contrast**: Verify normal text contrast is at least 4.5:1; large text (at least 18 pt regular or 14 pt bold) and applicable non-text UI components/state indicators are at least 3:1.
11. **Safety messaging**: Verify each identification-request post displays the disclaimer that community discussion does not verify species identification or edibility. Verify a shared post displays it when it names/guesses the specimen's species/genus, claims it is edible or safe to consume, or recommends consuming or preparing it; verify it is not shown for other shared content.
12. **Text-only examples and future photo support**: Verify the current sample posts render without images, that optional photo fields remain available in the presentation model, and that retained image assets are not required to display the feed. Before future integration displays photos, verify each is project-owned or public-domain/CC0, has descriptive alt text, and has its source and usage-rights basis documented alongside the asset.

No test project or automated test runner was found during planning. The project build and these browser-level checks are the planned validation path; do not add test dependencies without a separate scope decision.
