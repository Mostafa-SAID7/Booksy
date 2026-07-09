using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;
using MediatR;

namespace Booksy.Features.Books.Commands;

/// <summary>
/// Handler for updating a book
/// </summary>
public class UpdateBookCommandHandler : ICommandHandler<UpdateBookCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }



    public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {

        // Get existing book

        var book = await _unitOfWork.Books.GetOneAsync(b => b.Id == request.Id);

        if (book == null)

        {

            throw new NotFoundException($"Book with ID {request.Id} not found");

        }



        // Verify author exists

        var author = await _unitOfWork.Authors.GetOneAsync(a => a.Id == request.AuthorId);

        if (author == null)

        {

            throw new NotFoundException($"Author with ID {request.AuthorId} not found");

        }



        // Verify category exists

        var category = await _unitOfWork.Categories.GetOneAsync(c => c.Id == request.CategoryId);

        if (category == null)

        {

            throw new NotFoundException($"Category with ID {request.CategoryId} not found");

        }



        // Check if new ISBN conflicts with another book

        var existingBooks = await _unitOfWork.Books.GetAsync();

        if (existingBooks.Any(b => b.Id != request.Id && b.ISBN.Equals(request.ISBN, StringComparison.OrdinalIgnoreCase)))

        {

            throw new ConflictException($"A book with ISBN '{request.ISBN}' already exists");

        }



        // Update book

        book.Title = request.Title;

        book.Price = request.Price;

        book.Stock = request.Stock;

        book.Description = request.Description;

        book.CategoryId = request.CategoryId;

        book.AuthorId = request.AuthorId;

        book.CoverImageUrl = request.CoverImageUrl;

        book.ISBN = request.ISBN;



        // Save changes

        _unitOfWork.Books.Update(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);



        return Unit.Value;

    }

}




