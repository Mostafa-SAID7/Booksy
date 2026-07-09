using Booksy.Core.Interfaces;
using Booksy.Features.Books.DTOs;

namespace Booksy.Features.Books.Commands;

/// <summary>
/// Command to create a new book
/// </summary>
public class CreateBookCommand : ICommand<BookResponse>
{
    public string Title { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; } = 0;
    public string? Description { get; set; }
    public Guid CategoryId { get; set; }
    public Guid AuthorId { get; set; }
    public string? CoverImageUrl { get; set; }
    public string ISBN { get; set; } = string.Empty;
}
