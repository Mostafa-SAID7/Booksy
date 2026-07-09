using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Books;
using MediatR;

namespace Booksy.Features.Inventory.Commands;

/// <summary>
/// Handler for restocking items
/// </summary>
public class RestockItemCommandHandler : ICommandHandler<RestockItemCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public RestockItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RestockItemCommand request, CancellationToken cancellationToken)
    {
        // Get the book
        var book = await _unitOfWork.Books.GetByIdAsync(request.BookId);
        if (book == null)
        {
            throw new NotFoundException($"Book with ID {request.BookId} not found");
        }

        // Restock the item
        book.Stock += request.Quantity;

        // Save changes
        _unitOfWork.Books.Update(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
