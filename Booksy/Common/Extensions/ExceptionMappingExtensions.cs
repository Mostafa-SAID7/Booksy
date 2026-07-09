using Booksy.Core.Exceptions;

namespace Booksy.Common.Extensions;

/// <summary>
/// Extension methods for standardized exception throwing
/// Centralizes exception logic to ensure consistency across all handlers
/// Prevents different handlers from using different exceptions for same scenario
/// 
/// Usage:
/// - entity.ThrowIfNotFound("Book");
/// - collection.ThrowIfEmpty("Reviews");
/// - condition.ThrowIfTrue("Cannot delete category with books");
/// </summary>
public static class ExceptionMappingExtensions
{
    /// <summary>
    /// Throws NotFoundException if entity is null
    /// </summary>
    public static void ThrowIfNotFound<T>(this T? entity, string entityName) where T : class
    {
        if (entity == null)
            throw new NotFoundException($"{entityName} not found or does not exist");
    }

    /// <summary>
    /// Throws NotFoundException if any item in collection is null
    /// </summary>
    public static void ThrowIfAnyNull<T>(this IEnumerable<T>? items, string collectionName) where T : class
    {
        if (items?.Any(x => x == null) ?? false)
            throw new NotFoundException($"One or more items in {collectionName} collection is null");
    }

    /// <summary>
    /// Throws BusinessException if collection is empty or null
    /// </summary>
    public static void ThrowIfEmpty<T>(this IEnumerable<T>? collection, string collectionName)
    {
        if (collection?.Any() != true)
            throw new BusinessException($"{collectionName} cannot be empty");
    }

    /// <summary>
    /// Throws BusinessException if condition is true
    /// </summary>
    public static void ThrowIfTrue(this bool condition, string errorMessage)
    {
        if (condition)
            throw new BusinessException(errorMessage);
    }

    /// <summary>
    /// Throws BusinessException if condition is false
    /// </summary>
    public static void ThrowIfFalse(this bool condition, string errorMessage)
    {
        if (!condition)
            throw new BusinessException(errorMessage);
    }

    /// <summary>
    /// Throws ConflictException if condition is true
    /// </summary>
    public static void ThrowConflictIf(this bool condition, string conflictMessage)
    {
        if (condition)
            throw new ConflictException(conflictMessage);
    }

    /// <summary>
    /// Throws ValidationException for validation failures
    /// </summary>
    public static void ThrowValidationError(string fieldName, string errorMessage)
    {
        throw new ValidationException(new Dictionary<string, string[]>
        {
            { fieldName, new[] { errorMessage } }
        });
    }

    /// <summary>
    /// Throws ValidationException with multiple errors
    /// </summary>
    public static void ThrowValidationErrors(Dictionary<string, string[]> errors)
    {
        throw new ValidationException(errors);
    }

    /// <summary>
    /// Throws BusinessException if value is null or empty
    /// </summary>
    public static void ThrowIfNullOrEmpty(this string? value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new BusinessException($"{fieldName} cannot be empty or whitespace");
    }

    /// <summary>
    /// Throws BusinessException if value is not positive
    /// </summary>
    public static void ThrowIfNotPositive(this int value, string fieldName)
    {
        if (value <= 0)
            throw new BusinessException($"{fieldName} must be positive (got {value})");
    }

    /// <summary>
    /// Throws BusinessException if value is not positive
    /// </summary>
    public static void ThrowIfNotPositive(this decimal value, string fieldName)
    {
        if (value <= 0)
            throw new BusinessException($"{fieldName} must be positive (got {value:C})");
    }

    /// <summary>
    /// Throws BusinessException if value is out of range
    /// </summary>
    public static void ThrowIfOutOfRange(this int value, int min, int max, string fieldName)
    {
        if (value < min || value > max)
            throw new BusinessException($"{fieldName} must be between {min} and {max} (got {value})");
    }

    /// <summary>
    /// Throws BusinessException if value is out of range
    /// </summary>
    public static void ThrowIfOutOfRange(this decimal value, decimal min, decimal max, string fieldName)
    {
        if (value < min || value > max)
            throw new BusinessException($"{fieldName} must be between {min:C} and {max:C} (got {value:C})");
    }

    /// <summary>
    /// Throws BusinessException if start date is after end date
    /// </summary>
    public static void ThrowIfInvalidDateRange(this DateTime startDate, DateTime endDate, string fieldName)
    {
        if (startDate > endDate)
            throw new BusinessException($"{fieldName}: Start date ({startDate:O}) cannot be after end date ({endDate:O})");
    }

    /// <summary>
    /// Throws BusinessException if entity is soft-deleted
    /// </summary>
    public static void ThrowIfDeleted(this bool isDeleted, string entityName)
    {
        if (isDeleted)
            throw new BusinessException($"{entityName} has been deleted and cannot be used");
    }
}
