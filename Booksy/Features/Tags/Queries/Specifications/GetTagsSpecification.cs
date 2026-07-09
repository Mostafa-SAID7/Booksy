using Booksy.Common.Models;
using Booksy.Common.Specifications;
using Booksy.Models.Entities.Books;
using System.Linq.Expressions;

namespace Booksy.Features.Tags.Queries.Specifications;

/// <summary>
/// Specification for querying tags with search, filter, and sort support
/// </summary>
public class GetTagsSpecification : Specification<Tag>
{
    public GetTagsSpecification(SearchFilter filter)
    {
        // Always include related data
        AddInclude(t => t.Books);

        // Base criteria: not deleted
        Criteria = t => !t.IsDeleted;

        // Apply search filter
        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
        {
            var searchTerm = filter.SearchTerm.Trim().ToLower();
            AddCriteria(t =>
                t.Name.ToLower().Contains(searchTerm) ||
                t.Description.ToLower().Contains(searchTerm));
        }

        // Apply sorting
        var sortFields = filter.ParseSortBy();
        if (sortFields.Count == 0)
        {
            // Default sort: newest first
            AddOrderByDescending(t => t.CreatedAt);
        }
        else
        {
            // Apply first sort field
            var firstSort = sortFields[0];
            Expression<Func<Tag, object>>? orderExpression = firstSort.Field.ToLower() switch
            {
                "name" => t => t.Name,
                "bookcount" => t => t.Books.Count,
                "createdat" => t => t.CreatedAt,
                "updatedat" => t => t.UpdatedAt,
                _ => t => t.CreatedAt
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
                    Expression<Func<Tag, object>>? thenOrderExpression = sort.Field.ToLower() switch
                    {
                        "name" => t => t.Name,
                        "bookcount" => t => t.Books.Count,
                        "createdat" => t => t.CreatedAt,
                        "updatedat" => t => t.UpdatedAt,
                        _ => t => t.CreatedAt
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
    /// Specification for getting a single tag by ID with includes
    /// </summary>
    public GetTagsSpecification(Guid tagId)
    {
        AddInclude(t => t.Books);

        Criteria = t => t.Id == tagId && !t.IsDeleted;
    }
}
