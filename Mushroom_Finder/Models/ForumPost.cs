using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Mushroom_Finder.Models;

public sealed class ForumPost
{
    [Key]
    public int ForumPostId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Category { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Body { get; set; } = string.Empty;

    [Required]
    public string AuthorId { get; set; } = string.Empty;

    public IdentityUser Author { get; set; } = null!;

    public DateTimeOffset CreatedUtc { get; set; } = DateTimeOffset.UtcNow;

    [MaxLength(500)]
    public string? PhotoUrl { get; set; }

    [MaxLength(500)]
    public string? PhotoAltText { get; set; }

    [MaxLength(500)]
    public string? PhotoSourceAndRights { get; set; }

    public bool ShowSafetyDisclaimer { get; set; }
}
