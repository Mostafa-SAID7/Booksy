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
}
