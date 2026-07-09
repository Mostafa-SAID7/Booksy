using System.Linq.Expressions;

namespace Booksy.Common.Specifications;

/// <summary>
/// Base specification class for implementing the Specification Pattern
/// Allows for reusable, composable query logic
/// </summary>
/// <typeparam name="T">Entity type being queried</typeparam>
public abstract class Specification<T> where T : class
{
    /// <summary>
    /// Main filter criteria (WHERE clause)
    /// </summary>
    public Expression<Func<T, bool>>? Criteria { get; set; }

    /// <summary>
    /// Collection of related entities to include (INCLUDE statements)
    /// </summary>
    public List<Expression<Func<T, object>>> Includes { get; set; } = new();

    /// <summary>
    /// String-based include paths for complex navigation (for ThenInclude)
    /// </summary>
    public List<string> IncludeStrings { get; set; } = new();

    /// <summary>
    /// Primary sort order (ORDER BY ASC)
    /// </summary>
    public Expression<Func<T, object>>? OrderBy { get; set; }

    /// <summary>
    /// Primary sort order (ORDER BY DESC)
    /// </summary>
    public Expression<Func<T, object>>? OrderByDescending { get; set; }

    /// <summary>
    /// Secondary sort orders (for multiple sort fields)
    /// </summary>
    public List<(Expression<Func<T, object>> KeySelector, bool IsDescending)> ThenOrderBy { get; set; } = new();

    /// <summary>
    /// Number of records to take (TAKE/TOP clause)
    /// </summary>
    public int? Take { get; set; }

    /// <summary>
    /// Number of records to skip (SKIP clause)
    /// </summary>
    public int? Skip { get; set; }

    /// <summary>
    /// Whether pagination is enabled
    /// </summary>
    public bool IsPagingEnabled { get; set; }

    /// <summary>
    /// Search term for global search across fields
    /// </summary>
    public string? SearchTerm { get; set; }

    /// <summary>
    /// Additional filter criteria
    /// </summary>
    public Dictionary<string, object>? FilterCriteria { get; set; }

    /// <summary>
    /// Add a filter criterion
    /// </summary>
    public virtual void AddCriteria(Expression<Func<T, bool>> criteria)
    {
        Criteria = Criteria == null
            ? criteria
            : Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    Expression.Invoke(Criteria, criteria.Parameters[0]),
                    Expression.Invoke(criteria, criteria.Parameters[0])),
                criteria.Parameters);
    }

    /// <summary>
    /// Add an include path
    /// </summary>
    public virtual void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    /// <summary>
    /// Add a string-based include path
    /// </summary>
    public virtual void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    /// <summary>
    /// Set ascending sort order
    /// </summary>
    public virtual void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    /// <summary>
    /// Set descending sort order
    /// </summary>
    public virtual void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }

    /// <summary>
    /// Add a secondary sort order
    /// </summary>
    public virtual void AddThenOrderBy(Expression<Func<T, object>> keySelector, bool isDescending = false)
    {
        ThenOrderBy.Add((keySelector, isDescending));
    }

    /// <summary>
    /// Enable pagination with skip and take
    /// </summary>
    public virtual void EnablePaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }

    /// <summary>
    /// Disable pagination
    /// </summary>
    public virtual void DisablePaging()
    {
        Skip = null;
        Take = null;
        IsPagingEnabled = false;
    }

    /// <summary>
    /// Set pagination parameters
    /// </summary>
    public virtual void SetPaging(int pageNumber, int pageSize)
    {
        Skip = (pageNumber - 1) * pageSize;
        Take = pageSize;
        IsPagingEnabled = true;
    }
}
