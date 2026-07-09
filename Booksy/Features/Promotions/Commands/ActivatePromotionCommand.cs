using Booksy.Core.Interfaces;
using MediatR;

namespace Booksy.Features.Promotions.Commands;

/// <summary>
/// Command to activate a promotion
/// </summary>
public class ActivatePromotionCommand : ICommand<Unit>
{
    public Guid PromotionId { get; set; }
    public bool IsActive { get; set; }
}
