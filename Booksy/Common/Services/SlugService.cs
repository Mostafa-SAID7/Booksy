using Booksy.Common.Utilities;
using Booksy.Core.Exceptions;
using Booksy.Models.Entities.Books;
using System.Text.RegularExpressions;

namespace Booksy.Common.Services;

/// <summary>
/// Implementation of centralized slug service
/// Eliminates duplicate slug generation logic across handlers
/// Provides single source of truth for slug rules
/// </summary>
public class SlugService : ISlugService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SlugService> _logger;

    public SlugService(IUnitOfWork unitOfWork, ILogger<SlugService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<string> GenerateUniqueSlugAsync(
        IUnitOfWork unitOfWork,
        string input,
        Type entityType,
        Guid? excludeId = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input cannot be empty", nameof(input));

        ValidateSlugFormat(input);

        // Generate base slug
        var baseSlug = SlugGenerator.Generate(input);
        var slug = baseSlug;
        var counter = 1;

        _logger.LogInformation($"Generating unique slug for {entityType.Name}: base={baseSlug}");

        // Keep appending counter until unique
        while (!await IsSlugUniqueAsync(unitOfWork, slug, entityType, excludeId, cancellationToken))
        {
            slug = $"{baseSlug}-{counter}";
            counter++;

            // Prevent infinite loops (max 1000 attempts)
            if (counter > 1000)
            {
                _logger.LogError($"Could not generate unique slug after 1000 attempts for {entityType.Name}");
                throw new BusinessException($"Could not generate unique slug for {entityType.Name}");
            }
        }

        _logger.LogInformation($"Generated unique slug: {slug}");
        return slug;
    }

    public async Task<bool> IsSlugUniqueAsync(
        IUnitOfWork unitOfWork,
        string slug,
        Type entityType,
        Guid? excludeId = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return false;

        _logger.LogDebug($"Checking slug uniqueness: {slug} for {entityType.Name}");

        var existingSlugs = await GetExistingSlugsAsync(unitOfWork, entityType, cancellationToken);

        // If we're updating (excludeId provided), remove the old slug from check
        if (excludeId.HasValue)
        {
            existingSlugs = existingSlugs.Where(s => s != slug).ToHashSet();
        }

        var isUnique = !existingSlugs.Contains(slug);
        _logger.LogDebug($"Slug {slug} is {(isUnique ? "unique" : "NOT unique")}");

        return isUnique;
    }

    public async Task<HashSet<string>> GetExistingSlugsAsync(
        IUnitOfWork unitOfWork,
        Type entityType,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug($"Retrieving existing slugs for {entityType.Name}");

        var slugs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // Map entity types to repository access
        if (entityType == typeof(Category))
        {
            var categories = await unitOfWork.Categories.GetAllAsync();
            slugs = new HashSet<string>(categories.Select(c => c.Slug).Where(s => !string.IsNullOrWhiteSpace(s))!, StringComparer.OrdinalIgnoreCase);
        }
        else if (entityType == typeof(Book))
        {
            var books = await unitOfWork.Books.GetAllAsync();
            slugs = new HashSet<string>(books.Select(b => b.Slug).Where(s => !string.IsNullOrWhiteSpace(s))!, StringComparer.OrdinalIgnoreCase);
        }
        else if (entityType == typeof(Tag))
        {
            var tags = await unitOfWork.Tags.GetAllAsync();
            slugs = new HashSet<string>(tags.Select(t => t.Slug).Where(s => !string.IsNullOrWhiteSpace(s))!, StringComparer.OrdinalIgnoreCase);
        }
        else if (entityType == typeof(Author))
        {
            var authors = await unitOfWork.Authors.GetAllAsync();
            slugs = new HashSet<string>(authors.Select(a => a.Slug).Where(s => !string.IsNullOrWhiteSpace(s))!, StringComparer.OrdinalIgnoreCase);
        }

        _logger.LogDebug($"Found {slugs.Count} existing slugs for {entityType.Name}");
        return slugs;
    }

    public void ValidateSlugFormat(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            throw new ArgumentException("Slug cannot be empty", nameof(slug));

        // Slugs should only contain lowercase letters, numbers, and hyphens
        if (!Regex.IsMatch(slug, @"^[a-z0-9]+(?:-[a-z0-9]+)*$"))
        {
            _logger.LogWarning($"Invalid slug format: {slug}");
            throw new BusinessException("Slug must contain only lowercase letters, numbers, and hyphens");
        }

        // Max length 200
        if (slug.Length > 200)
        {
            _logger.LogWarning($"Slug too long: {slug.Length} characters");
            throw new BusinessException("Slug cannot exceed 200 characters");
        }
    }

    public async Task<Guid> ResolveSlugToIdAsync(
        IUnitOfWork unitOfWork,
        string slug,
        Type entityType,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(slug))
            throw new ArgumentException("Slug cannot be empty", nameof(slug));

        _logger.LogInformation($"Resolving slug to ID: {slug} for {entityType.Name}");

        Guid? id = null;

        if (entityType == typeof(Category))
        {
            var categories = await unitOfWork.Categories.GetAllAsync();
            id = categories.FirstOrDefault(c => c.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase))?.Id;
        }
        else if (entityType == typeof(Book))
        {
            var books = await unitOfWork.Books.GetAllAsync();
            id = books.FirstOrDefault(b => b.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase))?.Id;
        }
        else if (entityType == typeof(Tag))
        {
            var tags = await unitOfWork.Tags.GetAllAsync();
            id = tags.FirstOrDefault(t => t.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase))?.Id;
        }
        else if (entityType == typeof(Author))
        {
            var authors = await unitOfWork.Authors.GetAllAsync();
            id = authors.FirstOrDefault(a => a.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase))?.Id;
        }

        if (!id.HasValue || id == Guid.Empty)
        {
            _logger.LogWarning($"Slug not found: {slug} for {entityType.Name}");
            throw new NotFoundException($"No {entityType.Name} found with slug '{slug}'");
        }

        return id.Value;
    }
}
