using Booksy.Common.Models;
using Booksy.Common.Specifications;
using Booksy.Models.Entities.Books;
using System.Linq.Expressions;

namespace Booksy.Features.Books.Queries.Specifications;

/// <summary>
/// Specification for querying books with search, filter, and sort support
/// </summary>
public class GetBooksSpecification : Specification<Book>
{
    public GetBooksSpecification(SearchFilter filter)
    {
        // Always include related data
        AddInclude(b => b.Category);
        AddInclude(b => b.Author);
        AddInclude(b => b.Tags);

        // Apply search filter
        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
        {
            var searchTerm = filter.SearchTerm.Trim().ToLower();
            Criteria = b =>
                b.Title.ToLower().Contains(searchTerm) ||
                b.Description.ToLower().Contains(searchTerm) ||
                b.Author.Name.ToLower().Contains(searchTerm);
        }

        // Base criteria: not deleted
        AddCriteria(b => !b.IsDeleted);

        // Apply additional filters
        if (filter.FilterCriteria != null)
        {
            if (filter.FilterCriteria.TryGetValue("categoryId", out var categoryIdObj) && Guid.TryParse(categoryIdObj.ToString(), out var categoryId))
            {
                AddCriteria(b => b.CategoryId == categoryId);
            }

            if (filter.FilterCriteria.TryGetValue("authorId", out var authorIdObj) && Guid.TryParse(authorIdObj.ToString(), out var authorId))
            {
                AddCriteria(b => b.AuthorId == authorId);
            }

            if (filter.FilterCriteria.TryGetValue("minPrice", out var minPriceObj) && decimal.TryParse(minPriceObj.ToString(), out var minPrice))
            {
                AddCriteria(b => b.Price >= minPrice);
            }

            if (filter.FilterCriteria.TryGetValue("maxPrice", out var maxPriceObj) && decimal.TryParse(maxPriceObj.ToString(), out var maxPrice))
            {
                AddCriteria(b => b.Price <= maxPrice);
            }

            if (filter.FilterCriteria.TryGetValue("inStock", out var inStockObj) && bool.TryParse(inStockObj.ToString(), out var inStock) && inStock)
            {
                AddCriteria(b => b.Stock > 0);
            }
        }

        // Apply sorting
        var sortFields = filter.ParseSortBy();
        if (sortFields.Count == 0)
        {
            // Default sort: newest first
            AddOrderByDescending(b => b.CreatedAt);
        }
        else
        {
            // Apply first sort field
            var firstSort = sortFields[0];
            Expression<Func<Book, object>>? orderExpression = firstSort.Field.ToLower() switch
            {
                "title" => b => b.Title,
                "price" => b => b.Price,
                "stock" => b => b.Stock,
                "createdat" => b => b.CreatedAt,
                "updatedat" => b => b.UpdatedAt,
                _ => b => b.CreatedAt
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
                    Expression<Func<Book, object>>? thenOrderExpression = sort.Field.ToLower() switch
                    {
                        "title" => b => b.Title,
                        "price" => b => b.Price,
                        "stock" => b => b.Stock,
                        "createdat" => b => b.CreatedAt,
                        "updatedat" => b => b.UpdatedAt,
                        _ => b => b.CreatedAt
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
    /// Specification for getting a single book by ID with includes
    /// </summary>
    public GetBooksSpecification(Guid bookId)
    {
        AddInclude(b => b.Category);
        AddInclude(b => b.Author);
        AddInclude(b => b.Tags);
        AddInclude(b => b.Reviews);

        Criteria = b => b.Id == bookId && !b.IsDeleted;
    }

    /// <summary>
    /// Specification for getting books by category ID
    /// </summary>
    public GetBooksSpecification(Guid categoryId, SearchFilter? filter = null)
    {
        AddInclude(b => b.Category);
        AddInclude(b => b.Author);
        AddInclude(b => b.Tags);

        Criteria = b => b.CategoryId == categoryId && !b.IsDeleted;

        // Apply search if provided
        if (filter != null)
        {
            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                var searchTerm = filter.SearchTerm.Trim().ToLower();
                AddCriteria(b =>
                    b.Title.ToLower().Contains(searchTerm) ||
                    b.Description.ToLower().Contains(searchTerm));
            }

            var sortFields = filter.ParseSortBy();
            if (sortFields.Count == 0)
            {
                AddOrderByDescending(b => b.CreatedAt);
            }
            else
            {
                var firstSort = sortFields[0];
                Expression<Func<Book, object>>? orderExpression = firstSort.Field.ToLower() switch
                {
                    "title" => b => b.Title,
                    "price" => b => b.Price,
                    "stock" => b => b.Stock,
                    "createdat" => b => b.CreatedAt,
                    _ => b => b.CreatedAt
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
            AddOrderByDescending(b => b.CreatedAt);
        }
    }
}
