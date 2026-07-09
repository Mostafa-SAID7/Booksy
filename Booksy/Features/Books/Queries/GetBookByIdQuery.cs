using Booksy.Core.Interfaces;
using Booksy.Features.Books.DTOs;

namespace Booksy.Features.Books.Queries;

/// <summary>
/// Query to get a single book by ID
/// </summary>
public class GetBookByIdQuery : IQuery<BookResponse>
{
    public Guid Id { get; set; }
}
