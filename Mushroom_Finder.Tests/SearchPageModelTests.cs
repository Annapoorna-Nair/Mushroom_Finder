using Microsoft.Extensions.Logging.Abstractions;
using Mushroom_Finder.Models;
using Mushroom_Finder.Pages;
using Mushroom_Finder.Services;

namespace Mushroom_Finder.Tests;

public sealed class SearchPageModelTests
{
    [Fact]
    public void OnGet_loads_mushrooms_from_the_injected_catalog()
    {
        var expected = new Mushroom
        {
            Name = "Test Mushroom",
            ImagePath = "~/images/test.jpg",
            ImageAltText = "Test Mushroom",
            Regions = ["europe"],
            RegionDisplay = "Europe",
            Seasons = ["spring"],
            SeasonDisplay = "Spring"
        };
        var page = new Search_csModel(
            new StubMushroomCatalog([expected]),
            NullLogger<Search_csModel>.Instance);

        page.OnGet();

        var actual = Assert.Single(page.Mushrooms);
        Assert.Same(expected, actual);
    }

    [Fact]
    public void OnGet_handles_an_empty_catalog()
    {
        var page = new Search_csModel(
            new StubMushroomCatalog([]),
            NullLogger<Search_csModel>.Instance);

        page.OnGet();

        Assert.Empty(page.Mushrooms);
    }

    [Fact]
    public void OnGet_handles_catalog_failures_without_exposing_the_exception()
    {
        var page = new Search_csModel(
            new ThrowingMushroomCatalog(),
            NullLogger<Search_csModel>.Instance);

        page.OnGet();

        Assert.Empty(page.Mushrooms);
    }

    private sealed class StubMushroomCatalog(IReadOnlyList<Mushroom> mushrooms) : IMushroomCatalog
    {
        public IReadOnlyList<Mushroom> GetAll() => mushrooms;
    }

    private sealed class ThrowingMushroomCatalog : IMushroomCatalog
    {
        public IReadOnlyList<Mushroom> GetAll() => throw new InvalidOperationException("catalog failure");
    }
}
