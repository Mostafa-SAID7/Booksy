namespace Booksy.Common.Services;

/// <summary>
/// Centralized service for slug generation and validation
/// Eliminates duplicate slug logic across create/update handlers
/// Ensures slugs are unique per entity type and URL-safe
/// 
/// Best Practices:
/// - Always use this service for slug generation
/// - Don't manually check for slug uniqueness
/// - Provide entity type to check uniqueness in context
/// - Use excludeId when updating to allow same slug
/// </summary>
public interface ISlugService
{
    /// <summary>
    /// Generate a unique slug for an entity
    /// Automatically appends counter if slug already exists
    /// </summary>
    Task<string> GenerateUniqueSlugAsync(
        IUnitOfWork unitOfWork,
        string input,
        Type entityType,
        Guid? excludeId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Check if slug is unique for entity type
    /// Returns true if unique, false if exists
    /// </summary>
    Task<bool> IsSlugUniqueAsync(
        IUnitOfWork unitOfWork,
        string slug,
        Type entityType,
        Guid? excludeId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all slugs for an entity type
    /// Used for batch uniqueness checking
    /// </summary>
    Task<HashSet<string>> GetExistingSlugsAsync(
        IUnitOfWork unitOfWork,
        Type entityType,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Validate slug format (URL-safe characters only)
    /// Throws BusinessException if invalid format
    /// </summary>
    void ValidateSlugFormat(string slug);

    /// <summary>
    /// Resolve slug to entity ID
    /// Returns entity ID if found, throws if not found
    /// </summary>
    Task<Guid> ResolveSlugToIdAsync(
        IUnitOfWork unitOfWork,
        string slug,
        Type entityType,
        CancellationToken cancellationToken = default);
}
