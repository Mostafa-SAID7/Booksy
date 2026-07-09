using AutoMapper;
using Booksy.Core.Interfaces;
using Booksy.Features.Tags.DTOs;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Tags.Queries
{
    /// <summary>
    /// Handler for retrieving a tag by ID
    /// </summary>
    public class GetTagByIdQueryHandler : IQueryHandler<GetTagByIdQuery, TagResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTagByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TagResponse> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
        {
            // Get tag by ID
            var tag = await _unitOfWork.Tags.GetByIdAsync(request.Id);

            if (tag == null || tag.IsDeleted)
            {
                throw new KeyNotFoundException($"Tag with ID {request.Id} not found");
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
