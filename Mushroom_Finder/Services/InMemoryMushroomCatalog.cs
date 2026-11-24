using Mushroom_Finder.Models;

namespace Mushroom_Finder.Services;

public sealed class InMemoryMushroomCatalog : IMushroomCatalog
{
    private static readonly IReadOnlyList<Mushroom> Mushrooms = Array.AsReadOnly(
    [
        new Mushroom
        {
            Name = "Lions Mane",
            ImagePath = "~/images/lions-mane.jpg",
            ImageAltText = "Lions Mane",
            Regions = ["north america", "asia"],
            RegionDisplay = "North America, Asia",
            Seasons = ["summer", "fall"],
            SeasonDisplay = "Late Summer – Fall"
        },
        new Mushroom
        {
            Name = "Wood Blewit",
            ImagePath = "~/images/wood-blewit.jpg",
            ImageAltText = "Wood Blewit",
            Regions = ["europe", "north america"],
            RegionDisplay = "Europe, North America",
            Seasons = ["fall", "winter"],
            SeasonDisplay = "Fall – Early Winter"
        },
        new Mushroom
        {
            Name = "Amethyst Deceiver",
            ImagePath = "~/images/amethyst-deceiver.jpg",
            ImageAltText = "Amethyst Deceiver",
            Regions = ["europe", "north america"],
            RegionDisplay = "Europe, North America",
            Seasons = ["summer", "fall"],
            SeasonDisplay = "Summer – Fall"
        }
    ]);

    public IReadOnlyList<Mushroom> GetAll() => Mushrooms;
}
