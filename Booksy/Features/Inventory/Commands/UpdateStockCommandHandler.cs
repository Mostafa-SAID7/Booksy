using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Books;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Booksy.Features.Inventory.Commands;

/// <summary>
/// Handler for updating book stock
/// </summary>
public class UpdateStockCommandHandler : ICommandHandler<UpdateStockCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateStockCommandHandler> _logger;

    public UpdateStockCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<UpdateStockCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating stock for book {BookId} by {Quantity}",
            request.BookId,
            request.Quantity);

        // Validate input
        if (request.BookId == Guid.Empty)
            throw new BusinessException("Book ID is required");

        // Get the book
        var book = await _unitOfWork.Books.GetByIdAsync(request.BookId);
        if (book == null)
        {
            _logger.LogWarning("Book not found with ID: {BookId}", request.BookId);
            throw new NotFoundException($"Book with ID {request.BookId} not found");
        }

        // Update stock
        var previousStock = book.Stock;
        book.Stock += request.Quantity;
        
        if (book.Stock < 0)
        {
            _logger.LogWarning(
                "Stock cannot be negative for book {BookId}. Previous: {Previous}, Change: {Change}",
                request.BookId,
                previousStock,
                request.Quantity);
            throw new BusinessException("Stock cannot be negative");
        }

        // Save changes
        _unitOfWork.Books.Update(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Stock updated successfully for book {BookId}. Previous: {Previous}, New: {New}",
            request.BookId,
            previousStock,
            book.Stock);

        return Unit.Value;
    }
}
