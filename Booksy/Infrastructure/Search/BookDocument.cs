namespace Booksy.Infrastructure.Search;

/// <summary>
/// Elasticsearch document shape for a book.
/// Kept flat so every searchable field lives at the top level with no nested queries needed.
/// </summary>
public class BookDocument
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Isbn { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? CoverImageUrl { get; set; }

    // Author
    public string AuthorId { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;

    // Category
    public string CategoryId { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;

    // Tags — stored as a flat list so multi-match covers them
    public List<string> TagNames { get; set; } = new();

    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
