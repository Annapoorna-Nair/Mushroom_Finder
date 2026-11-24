using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mushroom_Finder.Pages.Shared
{
    public class ForumModel : PageModel
    {
        public IReadOnlyList<ForumPost> Posts { get; } =
        [
            new ForumPost(
                "ID Request",
                "[Pacific Northwest] Brown mushrooms growing on a fallen log",
                "ForestWalker",
                "2 days ago",
                "Found these in a damp, shaded area. The caps are about 5 cm wide. Can anyone suggest features I should check?",
                null,
                null,
                null,
                true),
            new ForumPost(
                "Just Sharing",
                "[Northern California] A morning walk after the rain",
                "MossAndMore",
                "5 hours ago",
                "Spotted several clusters along the trail today. I loved the range of shapes and colors.",
                null,
                null,
                null,
                false)
        ];

        public void OnGet()
        {
        }
    }

    public sealed record ForumPost(
        string Category,
        string Title,
        string AuthorDisplayName,
        string DisplayDate,
        string Body,
        string? PhotoUrl,
        string? PhotoAltText,
        string? PhotoSourceAndRights,
        bool ShowSafetyDisclaimer);
}
