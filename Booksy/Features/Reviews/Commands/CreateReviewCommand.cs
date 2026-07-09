using Booksy.Core.Interfaces;
using Booksy.Features.Reviews.DTOs;

namespace Booksy.Features.Reviews.Commands;

/// <summary>
/// Command to create a new review
/// </summary>
public class CreateReviewCommand : ICommand<ReviewDetailResponse>
{
    public Guid BookId { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public string ReviewerName { get; set; } = string.Empty;
}
