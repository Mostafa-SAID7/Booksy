namespace Booksy.Features.Reviews.DTOs;

/// <summary>
/// DTO for creating a new review
/// </summary>
public class ReviewCreateRequest
{
    public Guid BookId { get; set; }

    public int Rating { get; set; }  // 1-5 stars

    public string? Comment { get; set; }

    public string ReviewerName { get; set; } = string.Empty;
}
