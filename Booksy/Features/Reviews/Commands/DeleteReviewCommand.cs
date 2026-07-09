using MediatR;
using Booksy.Core.Interfaces;

namespace Booksy.Features.Reviews.Commands;

/// <summary>
/// Command to delete a review
/// </summary>
public class DeleteReviewCommand : ICommand<Unit>
{
    public Guid Id { get; set; }

    /// <summary>
    /// The ID of the user deleting the review
    /// Used for ownership validation
    /// </summary>
    public string UserId { get; set; }
}
