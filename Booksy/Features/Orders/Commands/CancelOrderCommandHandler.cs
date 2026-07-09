using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Orders;
using Booksy.Models.Enums;
using Booksy.Repositories.IRepositories;
using Booksy.Common.Services;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Booksy.Features.Orders.Commands;

/// <summary>
/// Handler for canceling an order
/// </summary>
public class CancelOrderCommandHandler : ICommandHandler<CancelOrderCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorizationService _authorizationService;
    private readonly ILogger<CancelOrderCommandHandler> _logger;

    public CancelOrderCommandHandler(
        IUnitOfWork unitOfWork,
        IAuthorizationService authorizationService,
        ILogger<CancelOrderCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _authorizationService = authorizationService;
        _logger = logger;
    }

    public async Task<Unit> Handle(
        CancelOrderCommand request,
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
                "Unauthorized cancel attempt: User {UserId} tried to cancel Order {OrderId}",
                request.UserId,
                request.OrderId);
            throw new AuthorizationException($"You are not authorized to cancel this order");
        }

        // Verify order can be canceled
        if (order.Status == OrderStatus.Completed || order.Status == OrderStatus.Canceled)
        {
            throw new BusinessException($"Cannot cancel order with status {order.Status}");
        }

        // Cancel the order
        order.Status = OrderStatus.Canceled;

        // Restore book stock
        foreach (var item in order.OrderItems)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(item.BookId);
            if (book != null)
            {
                book.Stock += item.Quantity;
                _unitOfWork.Books.Update(book);
            }
        }

        // Save through UnitOfWork
        _unitOfWork.Orders.Update(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Order {OrderId} canceled by user {UserId}",
            request.OrderId,
            request.UserId);

        return Unit.Value;
    }
}




