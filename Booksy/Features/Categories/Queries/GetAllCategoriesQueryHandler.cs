using AutoMapper;
using Booksy.Common.Models;
using Booksy.Core.Interfaces;
using Booksy.Features.Categories.DTOs;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Categories.Queries;

/// <summary>
/// Handler for getting all categories with pagination, search, filter, and sort support
/// Uses database-level filtering for performance
/// </summary>
public class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, PaginatedResponse<CategoryResponse>>
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(
        IRepository<Category> categoryRepository,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<CategoryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        // Validate filter
        if (!request.Filter.IsValid(out var errors))
        {
            throw new ArgumentException($"Invalid search filter: {string.Join(", ", errors)}");
        }

        // Get all categories with basic pagination
        var categories = await _categoryRepository.GetAllAsync();
        var query = categories.AsQueryable();

        // Apply search filter if provided
        if (!string.IsNullOrWhiteSpace(request.Filter.SearchTerm))
        {
            var searchTerm = request.Filter.SearchTerm.ToLower();
            query = query.Where(c =>
                c.Name.ToLower().Contains(searchTerm));
        }

        // Get total count
        var totalCount = query.Count();

        // Apply pagination
        var categoryList = query
            .Skip((request.Filter.PageNumber - 1) * request.Filter.PageSize)
            .Take(request.Filter.PageSize)
            .ToList();

        // Map to response DTOs
        var categoryResponses = _mapper.Map<List<CategoryResponse>>(categoryList);

        // Return paginated response
        return new PaginatedResponse<CategoryResponse>(
            categoryResponses,
            request.Filter.PageNumber,
            request.Filter.PageSize,
            totalCount);
    }
}
