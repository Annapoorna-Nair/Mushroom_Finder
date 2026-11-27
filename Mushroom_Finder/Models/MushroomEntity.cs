using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Mushroom_Finder.Models;

public sealed class MushroomEntity
{
    [Key]
    public int MushroomId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Family { get; set; }

    [MaxLength(200)]
    public string? ToxicityClass { get; set; }

    [Precision(18, 2)]
    public decimal? MinCapDiameter { get; set; }

    [Precision(18, 2)]
    public decimal? MaxCapDiameter { get; set; }

    [MaxLength(100)]
    public string? CapShape { get; set; }

    [MaxLength(100)]
    public string? CapSurface { get; set; }

    [MaxLength(100)]
    public string? CapColor { get; set; }

    [MaxLength(100)]
    public string? GillAttachment { get; set; }

    [Precision(18, 2)]
    public decimal? MinStemHeight { get; set; }

    [Precision(18, 2)]
    public decimal? MaxStemHeight { get; set; }

    [MaxLength(100)]
    public string? StemRoot { get; set; }

    [MaxLength(100)]
    public string? StemColor { get; set; }

    [MaxLength(200)]
    public string? Habitat { get; set; }

    [MaxLength(100)]
    public string? Season { get; set; }

    [MaxLength(200)]
    public string? Region { get; set; }

    [MaxLength(500)]
    public string? ImagePath { get; set; }

    [MaxLength(500)]
    public string? ImageAltText { get; set; }

    [MaxLength(200)]
    public string? ScientificName { get; set; }

    public string? Description { get; set; }

    [MaxLength(500)]
    public string? SourceReference { get; set; }
}
