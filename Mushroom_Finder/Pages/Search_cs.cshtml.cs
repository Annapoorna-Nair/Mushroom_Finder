using Microsoft.AspNetCore.Mvc.RazorPages;
using Mushroom_Finder.Models;
using Mushroom_Finder.Services;

namespace Mushroom_Finder.Pages
{
    public class Search_csModel : PageModel
    {
        private readonly IMushroomCatalog _mushroomCatalog;
        private readonly ILogger<Search_csModel> _logger;

        public Search_csModel(IMushroomCatalog mushroomCatalog, ILogger<Search_csModel> logger)
        {
            _mushroomCatalog = mushroomCatalog;
            _logger = logger;
        }

        public IReadOnlyList<Mushroom> Mushrooms { get; private set; } = Array.Empty<Mushroom>();

        public void OnGet()
        {
            try
            {
                Mushrooms = _mushroomCatalog.GetAll() ?? Array.Empty<Mushroom>();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Unable to load the mushroom catalog for the Search page.");
                Mushrooms = Array.Empty<Mushroom>();
            }
        }
    }
}
