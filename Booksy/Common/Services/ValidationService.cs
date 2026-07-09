using Booksy.Core.Exceptions;
using System.Text.RegularExpressions;

namespace Booksy.Common.Services;

/// <summary>
/// Implementation of centralized validation service
/// Eliminates scattered validation logic across handlers
/// Provides consistent error messages and business rule validation
/// </summary>
public class ValidationService : IValidationService
{
    private readonly ILogger<ValidationService> _logger;
    private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    public ValidationService(ILogger<ValidationService> logger)
    {
        _logger = logger;
    }

    public void ValidateNotNull<T>(T? entity, string entityName) where T : class
    {
        if (entity == null)
        {
            _logger.LogWarning($"Validation failed: {entityName} is null");
            throw new NotFoundException($"{entityName} not found or does not exist");
        }
    }

    public void ValidateRelationship<T>(T? relationship, string relationshipName) where T : class
    {
        if (relationship == null)
        {
            _logger.LogWarning($"Validation failed: Required relationship '{relationshipName}' is null");
            throw new BusinessException($"Required relationship '{relationshipName}' is not loaded or does not exist");
        }
    }

    public void ValidateCollection<T>(IEnumerable<T>? collection, string collectionName)
    {
        if (collection?.Any() != true)
        {
            _logger.LogWarning($"Validation failed: Collection '{collectionName}' is empty");
            throw new BusinessException($"{collectionName} cannot be empty");
        }
    }

    public void ValidateRange(int value, int min, int max, string fieldName)
    {
        if (value < min || value > max)
        {
            _logger.LogWarning($"Validation failed: {fieldName} value {value} out of range [{min}, {max}]");
            throw new BusinessException($"{fieldName} must be between {min} and {max}, but got {value}");
        }
    }

    public void ValidateRange(decimal value, decimal min, decimal max, string fieldName)
    {
        if (value < min || value > max)
        {
            _logger.LogWarning($"Validation failed: {fieldName} value {value:C} out of range [{min:C}, {max:C}]");
            throw new BusinessException($"{fieldName} must be between {min:C} and {max:C}, but got {value:C}");
        }
    }

    public void ValidateDateRange(DateTime startDate, DateTime endDate, string fieldName,
        int maxYearsInPast = 10, int maxYearsInFuture = 10)
    {
        // Check that start date is before end date
        if (startDate > endDate)
        {
            _logger.LogWarning($"Validation failed: {fieldName} - start date {startDate:O} is after end date {endDate:O}");
            throw new BusinessException($"{fieldName}: Start date cannot be after end date");
        }

        // Check that dates are not too far in the past
        var minAllowedDate = DateTime.UtcNow.AddYears(-maxYearsInPast);
        if (startDate < minAllowedDate)
        {
            _logger.LogWarning($"Validation failed: {fieldName} - start date {startDate:O} is too far in the past");
            throw new BusinessException($"{fieldName}: Start date cannot be more than {maxYearsInPast} years in the past");
        }

        // Check that dates are not too far in the future
        var maxAllowedDate = DateTime.UtcNow.AddYears(maxYearsInFuture);
        if (endDate > maxAllowedDate)
        {
            _logger.LogWarning($"Validation failed: {fieldName} - end date {endDate:O} is too far in the future");
            throw new BusinessException($"{fieldName}: End date cannot be more than {maxYearsInFuture} years in the future");
        }
    }

    public void ValidateNotEmpty(string? value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            _logger.LogWarning($"Validation failed: {fieldName} is empty or null");
            throw new BusinessException($"{fieldName} cannot be empty or whitespace");
        }
    }

    public void ValidateEmail(string? email, string fieldName)
    {
        ValidateNotEmpty(email, fieldName);

        if (!Regex.IsMatch(email!, EmailPattern, RegexOptions.IgnoreCase))
        {
            _logger.LogWarning($"Validation failed: {fieldName} has invalid email format: {email}");
            throw new BusinessException($"{fieldName} has invalid email format: {email}");
        }
    }

    public void ValidatePositive(int value, string fieldName)
    {
        if (value <= 0)
        {
            _logger.LogWarning($"Validation failed: {fieldName} value {value} is not positive");
            throw new BusinessException($"{fieldName} must be positive (greater than 0), but got {value}");
        }
    }

    public void ValidatePositive(decimal value, string fieldName)
    {
        if (value <= 0)
        {
            _logger.LogWarning($"Validation failed: {fieldName} value {value:C} is not positive");
            throw new BusinessException($"{fieldName} must be positive (greater than 0), but got {value:C}");
        }
    }

    public void ValidateCollectionCount<T>(IEnumerable<T>? collection, int expectedCount, string collectionName)
    {
        if (collection == null)
        {
            _logger.LogWarning($"Validation failed: {collectionName} is null");
            throw new BusinessException($"{collectionName} cannot be null");
        }

        var actualCount = collection.Count();
        if (actualCount != expectedCount)
        {
            _logger.LogWarning($"Validation failed: {collectionName} has {actualCount} items, expected {expectedCount}");
            throw new BusinessException($"{collectionName} should have {expectedCount} items, but has {actualCount}");
        }
    }

    public void ValidateCondition(bool condition, string errorMessage)
    {
        if (!condition)
        {
            _logger.LogWarning($"Validation failed: Condition check failed - {errorMessage}");
            throw new BusinessException(errorMessage);
        }
    }

    public void ValidateNotDeleted(bool isDeleted, string entityName)
    {
        if (isDeleted)
        {
            _logger.LogWarning($"Validation failed: {entityName} is soft-deleted");
            throw new BusinessException($"{entityName} has been deleted and cannot be used");
        }
    }
}
