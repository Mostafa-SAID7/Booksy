using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Books;
using Booksy.Models.Entities.Users;
using Booksy.Repositories.IRepositories;
using Booksy.Security;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Booksy.Features.Carts.Commands;

/// <summary>
/// Handler for adding an item to cart
/// </summary>
public class AddToCartCommandHandler : ICommandHandler<AddToCartCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorizationService _authorizationService;
    private readonly ILogger<AddToCartCommandHandler> _logger;

    public AddToCartCommandHandler(
        IUnitOfWork unitOfWork,
        IAuthorizationService authorizationService,
        ILogger<AddToCartCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _authorizationService = authorizationService;
        _logger = logger;
    }

    public async Task<Unit> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Adding {Quantity} of book {BookId} to cart for user {UserId}",
            request.Quantity,
            request.BookId,
            request.UserId);

        // OWNERSHIP VALIDATION: Verify user is adding to their own cart
        // (Users can only add to their own cart, not others')
        if (string.IsNullOrWhiteSpace(request.UserId))
            throw new BusinessException("User ID is required");

        // Validate input
        if (request.Quantity <= 0)
            throw new BusinessException("Quantity must be positive");
        if (request.BookId == Guid.Empty)
            throw new BusinessException("Book ID is required");

        // Verify book exists
        var book = await _unitOfWork.Books.GetOneAsync(b => b.Id == request.BookId);
        if (book == null)
        {
            _logger.LogWarning("Book not found with ID: {BookId}", request.BookId);
            throw new NotFoundException($"Book with ID {request.BookId} not found");
        }

        // Check stock availability
        if (book.Stock < request.Quantity)
        {
            _logger.LogWarning(
                "Insufficient stock for book {BookId}. Available: {Available}, Requested: {Requested}",
                request.BookId,
                book.Stock,
                request.Quantity);
            throw new BusinessException(
                $"Insufficient stock for book ID {request.BookId}. Available: {book.Stock}, Requested: {request.Quantity}");
        }

        // Get or create cart for user
        var cart = await _unitOfWork.Carts.GetOneAsync(c => c.UserId == request.UserId);

        if (cart == null)
        {
            _logger.LogInformation("Creating new cart for user: {UserId}", request.UserId);
            // Create a new cart if it doesn't exist
            cart = new Cart { UserId = request.UserId };
            await _unitOfWork.Carts.AddAsync(cart);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        // Check if item already in cart
        var existingItem = cart.Items.FirstOrDefault(i => i.BookId == request.BookId);
        if (existingItem != null)
        {
            _logger.LogInformation(
                "Updating quantity for book {BookId} in cart. Old: {OldQuantity}, New: {NewQuantity}",
                request.BookId,
                existingItem.Quantity,
                existingItem.Quantity + request.Quantity);
            
            existingItem.Quantity += request.Quantity;
            _unitOfWork.Carts.Update(cart);
        }
        else
        {
            _logger.LogInformation("Adding new item to cart: BookId {BookId}, Quantity {Quantity}", request.BookId, request.Quantity);
            
            var cartItem = new CartItem { BookId = request.BookId, Quantity = request.Quantity };
            cart.Items.Add(cartItem);
            _unitOfWork.Carts.Update(cart);
        }

        // Save changes
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Item successfully added to cart for user: {UserId}", request.UserId);

        return Unit.Value;
    }
}