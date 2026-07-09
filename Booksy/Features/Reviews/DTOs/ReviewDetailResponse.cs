using Booksy.Models.Enums;

namespace Booksy.Features.Reviews.DTOs;

/// <summary>
/// DTO for review details response
/// </summary>
public class ReviewDetailResponse
{
    public Guid Id { get; set; }

    public string ReviewerName { get; set; } = string.Empty;

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public ReviewStatus Status { get; set; }

    public string BookTitle { get; set; } = string.Empty;

    public string UserEmail { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
