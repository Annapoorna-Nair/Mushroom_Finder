namespace Mushroom_Finder.Models;

public sealed record Mushroom
{
    public required string Name { get; init; }

    public required string ImagePath { get; init; }

    public required string ImageAltText { get; init; }

    public required IReadOnlyList<string> Regions { get; init; }

    public required string RegionDisplay { get; init; }

    public required IReadOnlyList<string> Seasons { get; init; }

    public required string SeasonDisplay { get; init; }
}
