using Booksy.Common.Models;
using Booksy.Common.Specifications;
using Booksy.Models.Entities.Books;
using Booksy.Models.Enums;
using System.Linq.Expressions;

namespace Booksy.Features.Reviews.Queries.Specifications;

/// <summary>
/// Specification for querying reviews with search, filter, and sort support
/// </summary>
public class GetReviewsSpecification : Specification<Review>
{
    public GetReviewsSpecification(SearchFilter filter)
    {
        // Always include related data
        AddInclude(r => r.Book);
        AddInclude(r => r.User);

        // Apply search filter
        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
        {
            var searchTerm = filter.SearchTerm.Trim().ToLower();
            Criteria = r =>
                r.Comment.ToLower().Contains(searchTerm) ||
                r.Book.Title.ToLower().Contains(searchTerm) ||
                r.User.Email.ToLower().Contains(searchTerm);
        }

        // Base criteria: not deleted
        AddCriteria(r => !r.IsDeleted);

        // Apply additional filters
        if (filter.FilterCriteria != null)
        {
            if (filter.FilterCriteria.TryGetValue("bookId", out var bookIdObj) && Guid.TryParse(bookIdObj.ToString(), out var bookId))
            {
                AddCriteria(r => r.BookId == bookId);
            }

            if (filter.FilterCriteria.TryGetValue("userId", out var userIdObj) && userIdObj is string userId)
            {
                AddCriteria(r => r.UserId == userId);
            }

            if (filter.FilterCriteria.TryGetValue("minRating", out var minRatingObj) && int.TryParse(minRatingObj.ToString(), out var minRating))
            {
                AddCriteria(r => r.Rating >= minRating);
            }

            if (filter.FilterCriteria.TryGetValue("maxRating", out var maxRatingObj) && int.TryParse(maxRatingObj.ToString(), out var maxRating))
            {
                AddCriteria(r => r.Rating <= maxRating);
            }

            if (filter.FilterCriteria.TryGetValue("status", out var statusObj))
            {
                // Try to parse as ReviewStatus enum
                if (Enum.TryParse<ReviewStatus>(statusObj.ToString(), out var reviewStatus))
                {
                    AddCriteria(r => r.Status == reviewStatus);
                }
            }
        }

        // Apply sorting
        var sortFields = filter.ParseSortBy();
        if (sortFields.Count == 0)
        {
            // Default sort: newest first
            AddOrderByDescending(r => r.CreatedAt);
        }
        else
        {
            // Apply first sort field
            var firstSort = sortFields[0];
            Expression<Func<Review, object>>? orderExpression = firstSort.Field.ToLower() switch
            {
                "rating" => r => r.Rating,
                "createdat" => r => r.CreatedAt,
                "updatedat" => r => r.UpdatedAt,
                "booktitle" => r => r.Book.Title,
                _ => r => r.CreatedAt
            };

            if (orderExpression != null)
            {
                if (firstSort.IsDescending)
                    AddOrderByDescending(orderExpression);
                else
                    AddOrderBy(orderExpression);

                // Apply additional sort fields
                for (int i = 1; i < sortFields.Count; i++)
                {
                    var sort = sortFields[i];
                    Expression<Func<Review, object>>? thenOrderExpression = sort.Field.ToLower() switch
                    {
                        "rating" => r => r.Rating,
                        "createdat" => r => r.CreatedAt,
                        "updatedat" => r => r.UpdatedAt,
                        "booktitle" => r => r.Book.Title,
                        _ => r => r.CreatedAt
                    };

                    if (thenOrderExpression != null)
                        AddThenOrderBy(thenOrderExpression, sort.IsDescending);
                }
            }
        }

        // Apply pagination
        SetPaging(filter.PageNumber, filter.PageSize);
    }

    /// <summary>
    /// Specification for getting reviews by book ID
    /// </summary>
    public GetReviewsSpecification(Guid bookId, SearchFilter? filter = null)
    {
        AddInclude(r => r.Book);
        AddInclude(r => r.User);

        Criteria = r => r.BookId == bookId && !r.IsDeleted;

        if (filter != null)
        {
            if (filter.FilterCriteria != null)
            {
                if (filter.FilterCriteria.TryGetValue("minRating", out var minRatingObj) && int.TryParse(minRatingObj.ToString(), out var minRating))
                {
                    AddCriteria(r => r.Rating >= minRating);
                }

                if (filter.FilterCriteria.TryGetValue("maxRating", out var maxRatingObj) && int.TryParse(maxRatingObj.ToString(), out var maxRating))
                {
                    AddCriteria(r => r.Rating <= maxRating);
                }
            }

            var sortFields = filter.ParseSortBy();
            if (sortFields.Count == 0)
            {
                AddOrderByDescending(r => r.CreatedAt);
            }
            else
            {
                var firstSort = sortFields[0];
                Expression<Func<Review, object>>? orderExpression = firstSort.Field.ToLower() switch
                {
                    "rating" => r => r.Rating,
                    "createdat" => r => r.CreatedAt,
                    _ => r => r.CreatedAt
                };

                if (orderExpression != null)
                {
                    if (firstSort.IsDescending)
                        AddOrderByDescending(orderExpression);
                    else
                        AddOrderBy(orderExpression);
                }
            }

            SetPaging(filter.PageNumber, filter.PageSize);
        }
        else
        {
            AddOrderByDescending(r => r.CreatedAt);
        }
    }

    /// <summary>
    /// Specification for getting a single review by ID
    /// </summary>
    public GetReviewsSpecification(Guid reviewId, bool isById = true)
    {
        if (isById)
        {
            AddInclude(r => r.Book);
            AddInclude(r => r.User);

            Criteria = r => r.Id == reviewId && !r.IsDeleted;
        }
    }
}
