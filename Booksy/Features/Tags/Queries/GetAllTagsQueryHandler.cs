using AutoMapper;
using Booksy.Common.Models;
using Booksy.Core.Interfaces;
using Booksy.Features.Tags.DTOs;
using Booksy.Features.Tags.Queries.Specifications;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Tags.Queries
{
    /// <summary>
    /// Handler for retrieving all tags with pagination, search, filter, and sort support
    /// Uses database-level filtering for performance
    /// </summary>
    public class GetAllTagsQueryHandler : IQueryHandler<GetAllTagsQuery, PaginatedResponse<TagResponse>>
    {
        private readonly IRepository<Tag> _tagRepository;
        private readonly IMapper _mapper;

        public GetAllTagsQueryHandler(IRepository<Tag> tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<TagResponse>> Handle(
            GetAllTagsQuery request,
            CancellationToken cancellationToken)
        {
            // Validate filter
            if (!request.Filter.IsValid(out var errors))
            {
                throw new ArgumentException($"Invalid search filter: {string.Join(", ", errors)}");
            }

            // Create specification with search/filter/sort/pagination
            var specification = new GetTagsSpecification(request.Filter);

            // Get paginated results from database
            var (items, totalCount) = await _tagRepository.GetPaginatedAsync(specification);

            // Map to response DTOs and include book count
            var tagResponses = items.Select(t => new TagResponse
            {
                Id = t.Id,
                Name = t.Name,
                Slug = t.Slug,
                Description = t.Description,
                BookCount = t.Books?.Count ?? 0,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            }).ToList();

            // Return paginated response
            return new PaginatedResponse<TagResponse>(
                tagResponses,
                request.Filter.PageNumber,
                request.Filter.PageSize,
                totalCount);
        }
    }
}
