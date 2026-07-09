using Booksy.Core.Interfaces;
using MediatR;

namespace Booksy.Features.Promotions.Commands;

/// <summary>
/// Command to delete a promotion
/// </summary>
public class DeletePromotionCommand : ICommand<Unit>
{
    public Guid PromotionId { get; set; }
}
