using Mushroom_Finder.Models;

namespace Mushroom_Finder.Services;

public interface IMushroomCatalog
{
    IReadOnlyList<Mushroom> GetAll();
}
