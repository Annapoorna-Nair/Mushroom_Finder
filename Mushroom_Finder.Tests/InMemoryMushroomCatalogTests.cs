using Mushroom_Finder.Services;

namespace Mushroom_Finder.Tests;

public sealed class InMemoryMushroomCatalogTests
{
    [Fact]
    public void GetAll_returns_the_expected_mushrooms()
    {
        var catalog = new InMemoryMushroomCatalog();

        var mushrooms = catalog.GetAll();

        Assert.Equal(3, mushrooms.Count);
        Assert.Equal("Lions Mane", mushrooms[0].Name);
        Assert.Equal("Wood Blewit", mushrooms[1].Name);
        Assert.Equal("Amethyst Deceiver", mushrooms[2].Name);
    }

    [Fact]
    public void GetAll_preserves_filter_values_and_display_values()
    {
        var catalog = new InMemoryMushroomCatalog();

        var lionsMane = catalog.GetAll()[0];
        var woodBlewit = catalog.GetAll()[1];
        var amethystDeceiver = catalog.GetAll()[2];

        Assert.Equal(["north america", "asia"], lionsMane.Regions);
        Assert.Equal(["summer", "fall"], lionsMane.Seasons);
        Assert.Equal("Late Summer – Fall", lionsMane.SeasonDisplay);
        Assert.Equal(["europe", "north america"], woodBlewit.Regions);
        Assert.Equal(["fall", "winter"], woodBlewit.Seasons);
        Assert.Equal("Fall – Early Winter", woodBlewit.SeasonDisplay);
        Assert.Equal(["europe", "north america"], amethystDeceiver.Regions);
        Assert.Equal(["summer", "fall"], amethystDeceiver.Seasons);
        Assert.Equal("Summer – Fall", amethystDeceiver.SeasonDisplay);
    }

    [Fact]
    public void GetAll_returns_a_read_only_collection()
    {
        var catalog = new InMemoryMushroomCatalog();

        var mushrooms = catalog.GetAll();

        Assert.IsAssignableFrom<IReadOnlyList<Mushroom_Finder.Models.Mushroom>>(mushrooms);
        Assert.False(mushrooms is Mushroom_Finder.Models.Mushroom[]);
        Assert.Same(mushrooms, catalog.GetAll());
    }
}
