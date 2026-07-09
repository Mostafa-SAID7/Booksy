using AutoMapper;
using Booksy.Core.Interfaces;
using Booksy.Features.Tags.DTOs;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Tags.Queries
{
    /// <summary>
    /// Handler for retrieving a tag by slug
    /// </summary>
    public class GetTagBySlugQueryHandler : IQueryHandler<GetTagBySlugQuery, TagResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTagBySlugQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TagResponse> Handle(GetTagBySlugQuery request, CancellationToken cancellationToken)
        {
            var tag = await _unitOfWork.Tags.GetOneAsync(
                t => t.Slug.ToLower() == request.Slug.ToLower() && !t.IsDeleted);

            if (tag == null)
            {
                throw new KeyNotFoundException($"Tag with slug '{request.Slug}' not found");
            }

            return new TagResponse
            {
                Id = tag.Id,
                Name = tag.Name,
                Slug = tag.Slug,
                Description = tag.Description,
                BookCount = tag.Books?.Count ?? 0,
                CreatedAt = tag.CreatedAt,
                UpdatedAt = tag.UpdatedAt
            };
        }
    }
}
