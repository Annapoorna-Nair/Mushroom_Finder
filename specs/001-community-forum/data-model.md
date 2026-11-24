# Community Forum Presentation Model

## Scope

This model describes display data and transient filter state only. It does not define persistence, authentication, identity, or a database schema.

## Forum Post

One card in the forum feed.

| Field | Meaning | Constraint |
|---|---|---|
| `Category` | Post category | Exactly `ID Request` or `Just Sharing`. |
| `Title` | Post title, optionally including a bracketed general location | Display as supplied; absence of a location prefix does not block or rewrite the title. Long-title acceptance uses 256 characters; this is not a content limit. |
| `AuthorDisplayName` | Name shown for the post author | Display text only; not an account or identity key. |
| `DisplayDate` | Date label shown in the card | Presentation text for the examples; no ordering rule is introduced. |
| `Body` | Post description or request | Must wrap within the card width. Long-body acceptance uses 2,000 characters and a 64-character unbroken sequence; these are test fixtures, not content limits. |
| `PhotoUrl` | Optional photo source | May be absent; absence must not render a broken image. Current sample posts omit photos; retain this optional field for future database integration. |
| `PhotoAltText` | Accessible description of an informative photo | Required and descriptive when an informative photo is shown; decorative imagery uses empty alt text. |
| `PhotoSourceAndRights` | Provenance record for a supplied example photo | For future photos, document alongside the asset; photo must be project-owned or public-domain/CC0. Not required for posts without photos. |
| `ShowSafetyDisclaimer` | Whether the card displays the safety disclaimer | True for every `ID Request`; true for `Just Sharing` when it names or guesses the specimen's species/genus, claims it is edible or safe to consume, or recommends consuming or preparing it. |

Two static text-only examples are supplied by the feature: one `ID Request` and one `Just Sharing`. Photo fields remain optional for future database integration; the current example posts have no photo URLs, while any future informative image must have descriptive alt text and documented project-owned or public-domain/CC0 rights.

## Selected Category

Transient page state that controls which cards are visible.

| Value | Visible cards |
|---|---|
| `All posts` | Both categories |
| `ID Request` | Identification-request cards only |
| `Just Sharing` | Shared-post cards only |

The state is single-select. The active control is conveyed visually and programmatically. Applying a selection updates the result count or empty-state announcement without moving focus from the control.

## Feed Empty States

- **Entire feed empty**: Display a clear empty state instead of an empty/blank feed.
- **Selected category has no matches**: Display a category-specific empty state and an “All posts” control that restores the combined feed.

These are presentation states derived from the supplied in-memory post collection and selected category; they do not represent persisted lifecycle states.
