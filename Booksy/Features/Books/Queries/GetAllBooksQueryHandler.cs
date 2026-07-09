using AutoMapper;
using Booksy.Common.Models;
using Booksy.Core.Interfaces;
using Booksy.Features.Books.DTOs;
using Booksy.Features.Books.Queries.Specifications;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Books.Queries;

/// <summary>
/// Handler for getting all books with pagination, search, filter, and sort support
/// Uses database-level filtering for performance
/// </summary>
public class GetAllBooksQueryHandler : IQueryHandler<GetAllBooksQuery, PaginatedResponse<BookResponse>>
{
    private readonly IRepository<Book> _bookRepository;
    private readonly IMapper _mapper;

    public GetAllBooksQueryHandler(
        IRepository<Book> bookRepository,
        IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<BookResponse>> Handle(
        GetAllBooksQuery request,
        CancellationToken cancellationToken)
    {
        // Validate filter
        if (!request.Filter.IsValid(out var errors))
        {
            throw new ArgumentException($"Invalid search filter: {string.Join(", ", errors)}");
        }

        // Create specification with search/filter/sort/pagination
        var specification = new GetBooksSpecification(request.Filter);

        // Get paginated results from database
        var (items, totalCount) = await _bookRepository.GetPaginatedAsync(specification);

        // Map to response DTOs
        var bookResponses = _mapper.Map<List<BookResponse>>(items);

        // Return paginated response
        return new PaginatedResponse<BookResponse>(
            bookResponses,
            request.Filter.PageNumber,
            request.Filter.PageSize,
            totalCount);
    }
}
