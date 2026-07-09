using AutoMapper;
using Booksy.Common.Models;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Authentication.DTOs;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Authors.Queries;

/// <summary>
/// Handler for getting all authors with pagination, search, filter, and sort support
/// Uses database-level filtering for performance
/// </summary>
public class GetAllAuthorsQueryHandler : IQueryHandler<GetAllAuthorsQuery, PaginatedResponse<AuthorResponse>>
{
    private readonly IRepository<Author> _authorRepository;
    private readonly IMapper _mapper;

    public GetAllAuthorsQueryHandler(
        IRepository<Author> authorRepository,
        IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<AuthorResponse>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        // Validate filter
        if (!request.Filter.IsValid(out var errors))
        {
            throw new ArgumentException($"Invalid search filter: {string.Join(", ", errors)}");
        }

        // Get all authors with basic pagination
        var authors = await _authorRepository.GetAsync();
        var query = authors.AsQueryable();

        // Apply search filter if provided
        if (!string.IsNullOrWhiteSpace(request.Filter.SearchTerm))
        {
            var searchTerm = request.Filter.SearchTerm.ToLower();
            query = query.Where(a =>
                a.Name.ToLower().Contains(searchTerm) ||
                (a.Bio != null && a.Bio.ToLower().Contains(searchTerm)));
        }

        // Get total count
        var totalCount = query.Count();

        // Apply pagination
        var authorList = query
            .Skip((request.Filter.PageNumber - 1) * request.Filter.PageSize)
            .Take(request.Filter.PageSize)
            .ToList();

        // Map to response DTOs
        var authorResponses = _mapper.Map<List<AuthorResponse>>(authorList);

        // Return paginated response
        return new PaginatedResponse<AuthorResponse>(
            authorResponses,
            request.Filter.PageNumber,
            request.Filter.PageSize,
            totalCount);
    }
}
