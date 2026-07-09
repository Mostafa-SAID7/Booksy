using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Orders;
using Booksy.Repositories.IRepositories;
using Booksy.Common.Services;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Booksy.Features.Orders.Commands;

/// <summary>
/// Handler for updating order status
/// </summary>
public class UpdateOrderStatusCommandHandler : ICommandHandler<UpdateOrderStatusCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorizationService _authorizationService;
    private readonly ILogger<UpdateOrderStatusCommandHandler> _logger;

    public UpdateOrderStatusCommandHandler(
        IUnitOfWork unitOfWork,
        IAuthorizationService authorizationService,
        ILogger<UpdateOrderStatusCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _authorizationService = authorizationService;
        _logger = logger;
    }

    public async Task<Unit> Handle(
        UpdateOrderStatusCommand request,
        CancellationToken cancellationToken)
    {
        // Get the order
        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
        if (order == null)
        {
            throw new NotFoundException($"Order with ID {request.OrderId} not found");
        }

        // OWNERSHIP VALIDATION: Verify user owns this order
        if (!_authorizationService.CanUserAccessOrder(request.UserId, order.UserId))
        {
            _logger.LogWarning(
                "Unauthorized status update attempt: User {UserId} tried to update Order {OrderId}",
                request.UserId,
                request.OrderId);
            throw new AuthorizationException($"You are not authorized to update this order");
        }

        // Update order status
        order.Status = request.Status;

        // Save through UnitOfWork
        _unitOfWork.Orders.Update(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Order {OrderId} status updated to {Status} by user {UserId}",
            request.OrderId,
            request.Status,
            request.UserId);

        return Unit.Value;
    }
}




