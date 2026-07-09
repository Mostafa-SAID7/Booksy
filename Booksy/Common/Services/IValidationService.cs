namespace Booksy.Common.Services;

/// <summary>
/// Centralized service for business rule validation
/// Prevents repeated validation logic across handlers
/// Throws standardized exceptions for common validation failures
/// 
/// Best Practices:
/// - Call validation before accessing properties
/// - Don't use null-coalescing for required relationships
/// - Validate input ranges before using values
/// - Check date ranges for business logic errors
/// </summary>
public interface IValidationService
{
    /// <summary>
    /// Validates entity is not null
    /// Throws NotFoundException if null
    /// </summary>
    void ValidateNotNull<T>(T? entity, string entityName) where T : class;

    /// <summary>
    /// Validates required relationship is loaded
    /// Throws BusinessException if null
    /// </summary>
    void ValidateRelationship<T>(T? relationship, string relationshipName) where T : class;

    /// <summary>
    /// Validates collection is not null and has items
    /// Throws BusinessException if empty or null
    /// </summary>
    void ValidateCollection<T>(IEnumerable<T>? collection, string collectionName);

    /// <summary>
    /// Validates value is within acceptable range
    /// Throws BusinessException if out of range
    /// </summary>
    void ValidateRange(int value, int min, int max, string fieldName);

    /// <summary>
    /// Validates decimal value is within acceptable range
    /// Throws BusinessException if out of range
    /// </summary>
    void ValidateRange(decimal value, decimal min, decimal max, string fieldName);

    /// <summary>
    /// Validates start date is before end date and both are reasonable
    /// Throws BusinessException if invalid
    /// </summary>
    void ValidateDateRange(DateTime startDate, DateTime endDate, string fieldName, 
        int maxYearsInPast = 10, int maxYearsInFuture = 10);

    /// <summary>
    /// Validates string is not empty or whitespace
    /// Throws BusinessException if invalid
    /// </summary>
    void ValidateNotEmpty(string? value, string fieldName);

    /// <summary>
    /// Validates email format
    /// Throws BusinessException if invalid format
    /// </summary>
    void ValidateEmail(string? email, string fieldName);

    /// <summary>
    /// Validates value is positive
    /// Throws BusinessException if not positive
    /// </summary>
    void ValidatePositive(int value, string fieldName);

    /// <summary>
    /// Validates value is positive
    /// Throws BusinessException if not positive
    /// </summary>
    void ValidatePositive(decimal value, string fieldName);

    /// <summary>
    /// Validates collection has expected count
    /// Throws BusinessException if mismatch
    /// </summary>
    void ValidateCollectionCount<T>(IEnumerable<T>? collection, int expectedCount, string collectionName);

    /// <summary>
    /// Validates condition is true
    /// Throws BusinessException if false
    /// </summary>
    void ValidateCondition(bool condition, string errorMessage);

    /// <summary>
    /// Validates entity is not soft-deleted
    /// Throws BusinessException if deleted
    /// </summary>
    void ValidateNotDeleted(bool isDeleted, string entityName);
}
