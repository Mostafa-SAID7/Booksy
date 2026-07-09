using AutoMapper;
using Booksy.Core.Interfaces;
using Booksy.Features.Tags.DTOs;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Tags.Queries
{
    /// <summary>
    /// Handler for retrieving tags associated with a book
    /// </summary>
    public class GetTagsByBookIdQueryHandler : IQueryHandler<GetTagsByBookIdQuery, List<TagResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTagsByBookIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TagResponse>> Handle(GetTagsByBookIdQuery request, CancellationToken cancellationToken)
        {
            // Verify book exists
            var book = await _unitOfWork.Books.GetByIdAsync(request.BookId);
            if (book == null || book.IsDeleted)
            {
                throw new KeyNotFoundException($"Book with ID {request.BookId} not found");
            }

            // Get tags associated with the book
            var tags = book.Tags?.Where(t => !t.IsDeleted).ToList() ?? new List<Models.Entities.Books.Tag>();

            var response = tags.Select(t => new TagResponse
            {
                Id = t.Id,
                Name = t.Name,
                Slug = t.Slug,
                Description = t.Description,
                BookCount = t.Books?.Count ?? 0,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            }).ToList();

            return response;
        }
    }
}
