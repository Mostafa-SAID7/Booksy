using MediatR;
using Booksy.Core.Interfaces;

namespace Booksy.Features.Reviews.Commands;

/// <summary>
/// Command to update an existing review
/// </summary>
public class UpdateReviewCommand : ICommand<Unit>
{
    public Guid Id { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    /// <summary>
    /// The ID of the user updating the review
    /// Used for ownership validation
    /// </summary>
    public string UserId { get; set; }
}
