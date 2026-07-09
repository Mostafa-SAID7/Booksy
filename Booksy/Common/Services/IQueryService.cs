using Booksy.Models.Entities.Common;
using Booksy.Common.Models;
using Booksy.Common.Specifications;
using System.Linq.Expressions;

namespace Booksy.Common.Services;

/// <summary>
/// Centralized service for common database queries
/// Prevents GetAllAsync() anti-pattern by providing server-side aggregations and filtering
/// 
/// Best Practices:
/// - Always use Specifications for filtering
/// - Aggregations happen at database level, not in-memory
/// - Date range queries have boundaries validation
/// - All results are paginated unless explicitly count/sum
/// </summary>
public interface IQueryService
{
    /// <summary>
    /// Get entities filtered by date range with server-side filtering
    /// Returns only the matched entities (not entire table)
    /// </summary>
    Task<List<T>> GetByDateRangeAsync<T>(
        IUnitOfWork unitOfWork,
        DateTime startDate,
        DateTime endDate,
        Expression<Func<T, DateTime>> dateSelector,
        CancellationToken cancellationToken = default)
        where T : BaseEntity, IAuditableEntity;

    /// <summary>
    /// Count entities matching a predicate (database-level aggregation)
    /// Returns single integer, not entity list
    /// </summary>
    Task<int> CountAsync<T>(
        IUnitOfWork unitOfWork,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
        where T : BaseEntity;

    /// <summary>
    /// Sum decimal values matching a predicate (database-level aggregation)
    /// Used for revenue, totals, etc. Returns single decimal.
    /// </summary>
    Task<decimal> SumAsync<T>(
        IUnitOfWork unitOfWork,
        Expression<Func<T, decimal>> selector,
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
        where T : BaseEntity;

    /// <summary>
    /// Get average value (database-level aggregation)
    /// </summary>
    Task<double> AverageAsync<T>(
        IUnitOfWork unitOfWork,
        Expression<Func<T, int>> selector,
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
        where T : BaseEntity;

    /// <summary>
    /// Get max value (database-level aggregation)
    /// </summary>
    Task<T?> GetMaxAsync<T, TKey>(
        IUnitOfWork unitOfWork,
        Expression<Func<T, TKey>> keySelector,
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
        where T : BaseEntity;

    /// <summary>
    /// Get min value (database-level aggregation)
    /// </summary>
    Task<T?> GetMinAsync<T, TKey>(
        IUnitOfWork unitOfWork,
        Expression<Func<T, TKey>> keySelector,
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
        where T : BaseEntity;

    /// <summary>
    /// Check if any entity matches the predicate (database-level EXISTS query)
    /// Returns boolean, not entity list
    /// </summary>
    Task<bool> AnyAsync<T>(
        IUnitOfWork unitOfWork,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
        where T : BaseEntity;

    /// <summary>
    /// Get paginated results with search and sorting
    /// Combines filtering, pagination, and sorting in one database query
    /// </summary>
    Task<PaginatedResponse<T>> GetPaginatedAsync<T>(
        IUnitOfWork unitOfWork,
        SearchFilter filter,
        Specification<T> specification,
        CancellationToken cancellationToken = default)
        where T : BaseEntity;

    /// <summary>
    /// Group entities by key and get aggregated results
    /// Returns grouped data with counts/sums
    /// </summary>
    Task<Dictionary<TKey, int>> GroupCountAsync<T, TKey>(
        IUnitOfWork unitOfWork,
        Expression<Func<T, TKey>> groupKey,
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
        where T : BaseEntity
        where TKey : notnull;
}
