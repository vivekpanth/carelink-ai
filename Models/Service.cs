using System.ComponentModel.DataAnnotations;

namespace CareLink.Models;

public class Service
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Category { get; set; } = string.Empty; // Health, Mental Health, Housing, Food, Employment, Legal

    [Required, MaxLength(200)]
    public string Address { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Suburb { get; set; } = string.Empty;

    [MaxLength(50)]
    public string State { get; set; } = string.Empty;

    [Phone]
    public string? Phone { get; set; }

    [Url]
    public string? Website { get; set; }

    public string? OpeningHours { get; set; }

    public bool IsAIRecommended { get; set; }
    public double AiMatchScore { get; set; }

    public bool IsFree { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string? CreatedByUserId { get; set; }

    // Tags for AI matching
    public string Tags { get; set; } = string.Empty;
}
