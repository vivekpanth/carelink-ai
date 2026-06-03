using System.ComponentModel.DataAnnotations;

namespace CareLink.Models;

public class ServiceRequest
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string NeedsDescription { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string? Suburb { get; set; }

    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

    public string Status { get; set; } = "Pending"; // Pending, Matched, Resolved
}
