using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Orders.DTOs;
using Booksy.Models.Entities.Books;
using Booksy.Models.Entities.Orders;
using Booksy.Models.Entities.Users;
using Booksy.Models.Enums;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Orders.Commands;

/// <summary>
/// Handler for creating an order
/// </summary>
public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, OrderResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderResponse> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        // Get user's cart
        var cart = await _unitOfWork.Carts.GetOneAsync(c => c.UserId == request.UserId);

        if (cart == null || !cart.Items.Any())
        {
            throw new BusinessException($"Cart is empty for user {request.UserId}");
        }

        // Create order
        var order = new Order
        {
            UserId = request.UserId,
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            TransactionStatus = TransactionStatus.Pending
        };

        // Add order items from cart
        foreach (var cartItem in cart.Items)
        {
            var book = await _unitOfWork.Books.GetOneAsync(b => b.Id == cartItem.BookId);
            if (book == null)
            {
                throw new NotFoundException($"Book with ID {cartItem.BookId} not found");
            }

            // Verify sufficient stock
            if (book.Stock < cartItem.Quantity)
            {
                throw new BusinessException($"Insufficient stock for book '{book.Title}'. Available: {book.Stock}, Requested: {cartItem.Quantity}");
            }

            var orderItem = new OrderItem
            {
                BookId = cartItem.BookId,
                Quantity = cartItem.Quantity,
                Price = (int)book.Price,
                Order = order
            };

            order.OrderItems.Add(orderItem);

            // Reduce stock
            book.Stock -= cartItem.Quantity;
            _unitOfWork.Books.Update(book);
        }

        // Clear cart
        cart.Items.Clear();
        _unitOfWork.Carts.Update(cart);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Save order and commit books
        await _unitOfWork.Orders.AddAsync(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<OrderResponse>(order);
    }
}
