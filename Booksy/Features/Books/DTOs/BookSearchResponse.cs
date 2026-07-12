namespace Booksy.Features.Books.DTOs;

/// <summary>
/// Flattened book result returned by the Elasticsearch search endpoint.
/// Carries every field stored in the index so callers don't need a follow-up request.
/// </summary>
public class BookSearchResponse
{
    public Guid    Id            { get; set; }
    public string  Title         { get; set; } = string.Empty;
    public string  Slug          { get; set; } = string.Empty;
    public decimal Price         { get; set; }
    public int     Stock         { get; set; }
    public string? CoverImageUrl { get; set; }
    public string? Description   { get; set; }
    public string? Isbn          { get; set; }
    public string  AuthorName    { get; set; } = string.Empty;
    public string  CategoryName  { get; set; } = string.Empty;
    public List<string> TagNames { get; set; } = new();
}
