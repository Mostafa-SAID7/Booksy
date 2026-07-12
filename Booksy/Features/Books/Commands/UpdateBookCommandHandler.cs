using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Infrastructure.Search;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;
using MediatR;

namespace Booksy.Features.Books.Commands;

/// <summary>
/// Handler for updating a book.
/// Re-indexes the updated document in Elasticsearch after the DB save.
/// </summary>
public class UpdateBookCommandHandler : ICommandHandler<UpdateBookCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBookSearchService _search;

    public UpdateBookCommandHandler(IUnitOfWork unitOfWork, IBookSearchService search)
    {
        _unitOfWork = unitOfWork;
        _search     = search;
    }

    public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetOneAsync(b => b.Id == request.Id);
        if (book == null)
            throw new NotFoundException($"Book with ID {request.Id} not found");

        var author = await _unitOfWork.Authors.GetOneAsync(a => a.Id == request.AuthorId);
        if (author == null)
            throw new NotFoundException($"Author with ID {request.AuthorId} not found");

        var category = await _unitOfWork.Categories.GetOneAsync(c => c.Id == request.CategoryId);
        if (category == null)
            throw new NotFoundException($"Category with ID {request.CategoryId} not found");

        var existingBooks = await _unitOfWork.Books.GetAsync();
        if (existingBooks.Any(b => b.Id != request.Id &&
                                   b.ISBN.Equals(request.ISBN, StringComparison.OrdinalIgnoreCase)))
            throw new ConflictException($"A book with ISBN '{request.ISBN}' already exists");

        book.Title        = request.Title;
        book.Price        = request.Price;
        book.Stock        = request.Stock;
        book.Description  = request.Description;
        book.CategoryId   = request.CategoryId;
        book.AuthorId     = request.AuthorId;
        book.CoverImageUrl = request.CoverImageUrl;
        book.ISBN         = request.ISBN;

        // Attach navigations so the ES document is complete
        book.Author   = author;
        book.Category = category;

        _unitOfWork.Books.Update(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _search.IndexBookAsync(book, cancellationToken);

        return Unit.Value;
    }
}
