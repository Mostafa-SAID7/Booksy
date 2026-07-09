using AutoMapper;
using Booksy.Common.Utilities;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Books.DTOs;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Books.Commands;

/// <summary>
/// Handler for creating a new book
/// </summary>
public class CreateBookCommandHandler : ICommandHandler<CreateBookCommand, BookResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBookCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BookResponse> Handle(
        CreateBookCommand request,
        CancellationToken cancellationToken)
    {
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

        // Check if book with same ISBN already exists
        var existingBooks = await _unitOfWork.Books.GetAsync();
        if (existingBooks.Any(b => b.ISBN.Equals(request.ISBN, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ConflictException($"A book with ISBN '{request.ISBN}' already exists");
        }

        // Generate slug if not provided
        string slug = string.IsNullOrWhiteSpace(request.Slug)
            ? SlugGenerator.Generate(request.Title)
            : SlugGenerator.Generate(request.Slug);

        // Ensure slug is unique
        var existingSlugs = existingBooks.Select(b => b.Slug).ToList();
        slug = SlugGenerator.GenerateUnique(slug, existingSlugs);

        // Create new book entity
        var book = new Book
        {
            Title = request.Title,
            Slug = slug,
            Price = request.Price,
            Stock = request.Stock,
            Description = request.Description,
            CategoryId = request.CategoryId,
            AuthorId = request.AuthorId,
            CoverImageUrl = request.CoverImageUrl,
            ISBN = request.ISBN
        };

        // Add to repository and save
        var createdBook = await _unitOfWork.Books.AddAsync(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Map and return response
        return _mapper.Map<BookResponse>(createdBook);
    }
}
