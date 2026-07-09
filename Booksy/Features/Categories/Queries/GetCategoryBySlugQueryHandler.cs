using AutoMapper;
using Booksy.Core.Interfaces;
using Booksy.Features.Categories.DTOs;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Categories.Queries
{
    /// <summary>
    /// Handler for retrieving a category by slug
    /// </summary>
    public class GetCategoryBySlugQueryHandler : IQueryHandler<GetCategoryBySlugQuery, CategoryResponse>
    {
        private readonly IRepository<Models.Entities.Books.Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryBySlugQueryHandler(
            IRepository<Models.Entities.Books.Category> categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryResponse> Handle(GetCategoryBySlugQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetOneAsync(
                c => c.Slug.ToLower() == request.Slug.ToLower() && !c.IsDeleted);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category with slug '{request.Slug}' not found");
            }

            return _mapper.Map<CategoryResponse>(category);
        }
    }
}
