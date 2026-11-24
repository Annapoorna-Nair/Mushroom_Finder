	# Mushroom Finder Constitution

## Core Principles

### I. Code Quality and Maintainability

Code MUST be clear, focused, and maintainable. Implementations MUST follow established C# and ASP.NET Core conventions, use meaningful names, minimize duplication, and apply appropriate separation of concerns. Razor Pages handlers MUST remain focused on request orchestration, with business logic placed in appropriate services.

Changes MUST preserve existing behavior unless the specification explicitly defines a behavior change. Unnecessary complexity, speculative abstractions, and unrelated modifications are prohibited.

### II. Testing Standards

All new functionality MUST include appropriate automated tests. Tests MUST cover normal behavior, validation failures, error handling, and relevant boundary conditions.

Unit tests SHOULD verify isolated business logic. Integration tests MUST be used for important interactions involving Razor Pages, persistence, routing, authentication, or external dependencies. Tests MUST be deterministic, independently repeatable, and pass before implementation is considered complete.

A defect correction MUST include a regression test when practical.

### III. Consistent User Experience

The application MUST provide a consistent and accessible user experience across all Razor Pages. Pages SHOULD use consistent layouts, navigation, terminology, validation behavior, error presentation, status messages, and responsive styling.

User-facing text MUST be clear and actionable. Forms MUST provide appropriate labels, validation messages, and accessible feedback. Existing interaction patterns MUST be reused unless the specification explicitly requires a new pattern.

### IV. Performance and Resource Efficiency

Features MUST be implemented with appropriate performance characteristics for their expected usage. Database queries MUST retrieve only required data, avoid unnecessary round trips, and use suitable filtering, ordering, and pagination.

The application MUST avoid blocking asynchronous operations, unbounded data loading, unnecessary allocations, and repeated expensive work. Performance-sensitive behavior MUST be measured when its impact is uncertain. New performance requirements MUST define measurable targets where appropriate.

### V. Secure and Reliable Defaults

Input MUST be validated at application boundaries. User-provided data MUST NOT be trusted, and output MUST be rendered safely. Sensitive information MUST NOT be written to logs or exposed in user-facing errors.

Application failures MUST be handled predictably, with useful diagnostic information recorded through appropriate logging while presenting safe, understandable messages to users. Configuration and secrets MUST be supplied through supported configuration mechanisms rather than committed to source control.

## Technology and Architectural Constraints

The application targets .NET 8 and uses ASP.NET Core Razor Pages. New functionality MUST prioritize the existing project architecture and established patterns.

Implementations SHOULD use asynchronous APIs for I/O-bound work, dependency injection for application services, and strongly typed models for page data and form binding. Persistence, external services, and other infrastructure concerns MUST remain separated from page presentation concerns.

## Development Workflow and Quality Gates

Development MUST follow Spec-Driven Development using GitHub Spec Kit:

1. Establish or update the constitution.
2. Define the feature specification.
3. Produce the implementation plan.
4. Break the plan into tasks.
5. Implement only after the user explicitly issues `speckit.implement`.

Before implementation is complete:

- The solution MUST build successfully.
- Automated tests MUST pass.
- New behavior MUST match the approved specification.
- User experience and accessibility requirements MUST be verified.
- Relevant performance and security considerations MUST be reviewed.
- Changes MUST remain within the scope of the approved feature.

No application code or implementation artifacts may be created or modified before `speckit.implement` is issued.

## Governance

This constitution governs feature specifications, plans, tasks, implementation, and reviews. When a conflict exists, this constitution takes precedence unless it is explicitly amended.

Constitution amendments MUST document the reason for the change and its effect on existing work. Any implementation that violates a principle MUST include a documented justification in the relevant specification or plan.

**Version**: 1.0.0 | **Ratified**: 2026-11-23 | **Last Amended**: 2026-11-23
