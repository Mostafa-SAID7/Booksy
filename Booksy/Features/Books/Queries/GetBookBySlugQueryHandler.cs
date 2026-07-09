using AutoMapper;
using Booksy.Core.Interfaces;
using Booksy.Features.Books.DTOs;
using Booksy.Features.Reviews.DTOs;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Books.Queries
{
    /// <summary>
    /// Handler for retrieving a book by slug
    /// </summary>
    public class GetBookBySlugQueryHandler : IQueryHandler<GetBookBySlugQuery, BookDetailResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBookBySlugQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BookDetailResponse> Handle(GetBookBySlugQuery request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetOneAsync(
                b => b.Slug.ToLower() == request.Slug.ToLower() && !b.IsDeleted);

            if (book == null)
            {
                throw new KeyNotFoundException($"Book with slug '{request.Slug}' not found");
            }

            return new BookDetailResponse
            {
                Id = book.Id,
                Title = book.Title,
                Slug = book.Slug,
                Price = book.Price,
                Discount = book.Discount,
                Description = book.Description,
                Stock = book.Stock,
                CoverImageUrl = book.CoverImageUrl,
                Traffic = book.Traffic,
                ISBN = book.ISBN,
                Quantity = book.Quantity,
                CategoryId = book.CategoryId,
                CategoryName = book.Category?.Name ?? "",
                CategorySlug = book.Category?.Slug ?? "",
                AuthorId = book.AuthorId,
                AuthorName = book.Author?.Name ?? "",
                AuthorSlug = book.Author?.Slug ?? "",
                Reviews = book.Reviews?.Select(r => new ReviewResponse
                {
                    Id = r.Id,
                    ReviewerName = r.ReviewerName,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    Status = (int)r.Status,
                    BookTitle = book.Title,
                    UserEmail = r.User?.Email ?? "",
                    CreatedAt = r.CreatedAt
                }).ToList() ?? new List<ReviewResponse>()
            };
        }
    }
}
