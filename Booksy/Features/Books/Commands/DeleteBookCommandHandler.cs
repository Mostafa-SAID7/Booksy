using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Infrastructure.Search;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;
using MediatR;

namespace Booksy.Features.Books.Commands;

/// <summary>
/// Handler for deleting a book.
/// Removes the document from Elasticsearch after the DB deletion.
/// </summary>
public class DeleteBookCommandHandler : ICommandHandler<DeleteBookCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBookSearchService _search;

    public DeleteBookCommandHandler(IUnitOfWork unitOfWork, IBookSearchService search)
    {
        _unitOfWork = unitOfWork;
        _search     = search;
    }

    public async Task<Unit> Handle(
        DeleteBookCommand request,
        CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetOneAsync(b => b.Id == request.Id);
        if (book == null)
            throw new NotFoundException($"Book with ID {request.Id} not found");

        _unitOfWork.Books.Delete(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _search.RemoveBookAsync(book.Id, cancellationToken);

        return Unit.Value;
    }
}
