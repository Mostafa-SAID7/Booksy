using Booksy.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace Booksy.Security
{
    /// <summary>
    /// Centralized validation extensions to eliminate duplicate validation logic
    /// Used across all handlers and services
    /// </summary>
    public static class ValidationContextExtensions
    {
        /// <summary>
        /// Validate entity exists, throw NotFoundException if not
        /// </summary>
        public static T ValidateEntityExists<T>(
            this T? entity,
            string entityType,
            object id,
            ILogger? logger = null) where T : class
        {
            if (entity == null)
            {
                logger?.LogWarning("{EntityType} not found with ID: {Id}", entityType, id);
                throw new NotFoundException($"{entityType} with ID '{id}' not found");
            }
            return entity;
        }

        /// <summary>
        /// Validate string is not null or empty
        /// </summary>
        public static string ValidateNotEmpty(
            this string? value,
            string fieldName,
            ILogger? logger = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                logger?.LogWarning("{FieldName} is empty or null", fieldName);
                throw new ValidationException(new Dictionary<string, string[]> { { fieldName, new[] { $"{fieldName} cannot be empty" } } });
            }
            return value;
        }

        /// <summary>
        /// Validate string length within range
        /// </summary>
        public static string ValidateLength(
            this string value,
            string fieldName,
            int minLength,
            int maxLength,
            ILogger? logger = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                logger?.LogWarning("{FieldName} is empty", fieldName);
                throw new ValidationException(new Dictionary<string, string[]> { { fieldName, new[] { $"{fieldName} cannot be empty" } } });
            }

            if (value.Length < minLength || value.Length > maxLength)
            {
                logger?.LogWarning("{FieldName} length {Length} not in range [{Min}, {Max}]",
                    fieldName, value.Length, minLength, maxLength);
                throw new ValidationException(new Dictionary<string, string[]> { { fieldName, new[] { $"{fieldName} must be between {minLength} and {maxLength} characters" } } });
            }
            return value;
        }

        /// <summary>
        /// Validate GUID is not empty
        /// </summary>
        public static Guid ValidateNotEmpty(
            this Guid id,
            string fieldName,
            ILogger? logger = null)
        {
            if (id == Guid.Empty)
            {
                logger?.LogWarning("{FieldName} is empty GUID", fieldName);
                throw new ValidationException(new Dictionary<string, string[]> { { fieldName, new[] { $"{fieldName} cannot be empty" } } });
            }
            return id;
        }

        /// <summary>
        /// Validate date range (start before end)
        /// </summary>
        public static (DateTime start, DateTime end) ValidateDateRange(
            this (DateTime start, DateTime end) dates,
            string fieldName,
            ILogger? logger = null)
        {
            if (dates.start >= dates.end)
            {
                logger?.LogWarning("{FieldName}: Start date {Start} >= End date {End}",
                    fieldName, dates.start, dates.end);
                throw new BusinessException($"Start date must be before end date");
            }
            return dates;
        }

        /// <summary>
        /// Validate numeric value is positive
        /// </summary>
        public static decimal ValidatePositive(
            this decimal value,
            string fieldName,
            ILogger? logger = null)
        {
            if (value <= 0)
            {
                logger?.LogWarning("{FieldName} value {Value} is not positive", fieldName, value);
                throw new ValidationException(new Dictionary<string, string[]> { { fieldName, new[] { $"{fieldName} must be greater than 0" } } });
            }
            return value;
        }

        /// <summary>
        /// Validate numeric value in range
        /// </summary>
        public static int ValidateRange(
            this int value,
            string fieldName,
            int min,
            int max,
            ILogger? logger = null)
        {
            if (value < min || value > max)
            {
                logger?.LogWarning("{FieldName} value {Value} not in range [{Min}, {Max}]",
                    fieldName, value, min, max);
                throw new ValidationException(new Dictionary<string, string[]> { { fieldName, new[] { $"{fieldName} must be between {min} and {max}" } } });
            }
            return value;
        }

        /// <summary>
        /// Validate item doesn't already exist (for duplicates)
        /// </summary>
        public static T? ThrowIfExists<T>(
            this T? existingEntity,
            string entityType,
            object identifier,
            ILogger? logger = null) where T : class
        {
            if (existingEntity != null)
            {
                logger?.LogWarning("{EntityType} already exists with identifier: {Identifier}", entityType, identifier);
                throw new ConflictException($"{entityType} with this information already exists");
            }
            return null;
        }

        /// <summary>
        /// Validate user owns resource or is admin
        /// </summary>
        public static void ValidateOwnershipOrAdmin(
            this string resourceOwnerId,
            string currentUserId,
            bool isAdmin,
            string resourceType,
            ILogger? logger = null)
        {
            if (resourceOwnerId != currentUserId && !isAdmin)
            {
                logger?.LogWarning(
                    "Unauthorized access attempt: User {UserId} tried to access {ResourceType} owned by {OwnerId}",
                    currentUserId, resourceType, resourceOwnerId);
                throw new AuthorizationException($"You do not have permission to access this {resourceType}");
            }
        }

        /// <summary>
        /// Validate user is in required role
        /// </summary>
        public static void ValidateRole(
            this IEnumerable<string> userRoles,
            string requiredRole,
            ILogger? logger = null)
        {
            if (!userRoles.Contains(requiredRole))
            {
                logger?.LogWarning("User does not have required role: {RequiredRole}", requiredRole);
                throw new AuthorizationException($"This operation requires {requiredRole} role");
            }
        }
    }
}
