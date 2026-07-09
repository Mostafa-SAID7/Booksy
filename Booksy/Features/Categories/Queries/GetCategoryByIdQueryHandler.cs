using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Categories.DTOs;
using Booksy.Models.Entities.Books;

namespace Booksy.Features.Categories.Queries;

/// <summary>
/// Handler for getting a category by ID
/// </summary>
public class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryResponse>
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(
        IRepository<Category> categoryRepository,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);
        if (category is null)
        {
            throw new NotFoundException("Category", request.Id);
        }

        return _mapper.Map<CategoryResponse>(category);
    }
}
