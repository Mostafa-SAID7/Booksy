using Microsoft.EntityFrameworkCore;

namespace Booksy.Common.Specifications;

/// <summary>
/// Applies a Specification to an IQueryable to produce the final database query
/// </summary>
/// <typeparam name="T">Entity type being queried</typeparam>
public class SpecificationEvaluator<T> where T : class
{
    /// <summary>
    /// Apply specification to query
    /// </summary>
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, Specification<T> specification)
    {
        var query = inputQuery;

        // Apply criteria (WHERE clause)
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        // Apply includes
        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        // Apply string-based includes
        query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        // Apply ordering
        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        // Apply secondary ordering
        if (specification.OrderBy != null || specification.OrderByDescending != null)
        {
            foreach (var (keySelector, isDescending) in specification.ThenOrderBy)
            {
                query = isDescending
                    ? ((IOrderedQueryable<T>)query).ThenByDescending(keySelector)
                    : ((IOrderedQueryable<T>)query).ThenBy(keySelector);
            }
        }

        // Apply pagination
        if (specification.IsPagingEnabled)
        {
            if (specification.Skip.HasValue)
            {
                query = query.Skip(specification.Skip.Value);
            }

            if (specification.Take.HasValue)
            {
                query = query.Take(specification.Take.Value);
            }
        }

        return query;
    }

    /// <summary>
    /// Get query without pagination (for count operations)
    /// </summary>
    public static IQueryable<T> GetQueryWithoutPaging(IQueryable<T> inputQuery, Specification<T> specification)
    {
        var query = inputQuery;

        // Apply criteria (WHERE clause)
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        // Apply includes
        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        // Apply string-based includes
        query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
}
