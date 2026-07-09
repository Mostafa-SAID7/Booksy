using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Orders;
using Booksy.Models.Enums;
using Booksy.Repositories.IRepositories;
using MediatR;

namespace Booksy.Features.Orders.Commands;

/// <summary>
/// Handler for canceling an order
/// </summary>
public class CancelOrderCommandHandler : ICommandHandler<CancelOrderCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public CancelOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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

        return Unit.Value;
    }
}




