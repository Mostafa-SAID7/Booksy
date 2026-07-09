namespace Booksy.Features.Reviews.DTOs;

/// <summary>
/// DTO for updating a review
/// </summary>
public class ReviewUpdateRequest
{
    public int Rating { get; set; }  // 1-5 stars

    public string? Comment { get; set; }
}
