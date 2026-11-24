using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Mushroom_Finder.Tests;

public sealed class SearchPageIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public SearchPageIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task Search_page_renders_typed_catalog_and_accessible_controls()
    {
        using var response = await _client.GetAsync("/Search_cs");
        var html = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Search some mushrooms", html);
        Assert.Contains("id=\"mushroomSearchInput\"", html);
        Assert.Contains("id=\"regionFilter\"", html);
        Assert.Contains("id=\"seasonFilter\"", html);
        Assert.Contains("aria-controls=\"mushroomGrid\"", html);
        Assert.Contains("role=\"region\"", html);
        Assert.Contains("role=\"status\"", html);
        Assert.Contains("Lions Mane", html);
        Assert.Contains("Wood Blewit", html);
        Assert.Contains("Amethyst Deceiver", html);
        Assert.Contains("Region:</strong> North America, Asia", html);
        Assert.Contains("Season:", html);
        Assert.Contains("Late Summer", html);
        Assert.Contains("Fall", html);
        Assert.Contains("data-region=\"north america,asia\"", html);
        Assert.Contains("data-season=\"summer,fall\"", html);
        Assert.Contains("No mushrooms match your search and filter selection.", html);
    }

    [Fact]
    public async Task Search_page_preserves_the_search_route()
    {
        using var response = await _client.GetAsync("/Search_cs");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
