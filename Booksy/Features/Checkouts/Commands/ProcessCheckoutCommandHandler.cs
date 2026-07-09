using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Orders;
using Booksy.Models.Entities.Users;
using Booksy.Models.Enums;
using Booksy.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Booksy.Features.Checkouts.Commands;

/// <summary>
/// Handler for processing checkout
/// </summary>
public class ProcessCheckoutCommandHandler : ICommandHandler<ProcessCheckoutCommand, CheckoutResultDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ProcessCheckoutCommandHandler> _logger;

    public ProcessCheckoutCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<ProcessCheckoutCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CheckoutResultDto> Handle(ProcessCheckoutCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing checkout for user: {UserId}", request.UserId);

        // Validate user ID
        if (string.IsNullOrWhiteSpace(request.UserId))
            throw new BusinessException("User ID is required");

        // Get user's cart - use GetOneAsync instead of GetAllAsync
        var userCart = await _unitOfWork.Carts.GetOneAsync(c => c.UserId == request.UserId);
        if (userCart == null || !userCart.Items.Any())
        {
            _logger.LogWarning("Cart is empty for user: {UserId}", request.UserId);
            throw new BusinessException("Cart is empty");
        }

        // Calculate total
        decimal total = userCart.Items.Sum(ci => (decimal)ci.Book.Price * ci.Quantity);

        _logger.LogInformation("Checkout total: {Total} for {ItemCount} items", total, userCart.Items.Count);

        // Create order within transaction
        var order = new Order
        {
            UserId = request.UserId,
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            TransactionStatus = TransactionStatus.Pending,
            IsDeleted = false
        };

        // Add order items
        foreach (var cartItem in userCart.Items)
        {
            order.OrderItems.Add(new OrderItem
            {
                BookId = cartItem.BookId,
                Quantity = cartItem.Quantity,
                Price = (int)cartItem.Book.Price,
                TotalPrice = cartItem.Book.Price * cartItem.Quantity
            });
        }

        // Save order
        await _unitOfWork.Orders.AddAsync(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Clear cart
        userCart.Items.Clear();
        _unitOfWork.Carts.Update(userCart);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Order created successfully with ID: {OrderId}", order.Id);

        return new CheckoutResultDto
        {
            OrderId = order.Id,
            OrderNumber = order.Id.ToString(),
            Total = total,
            OrderDate = order.OrderDate
        };
    }
}
