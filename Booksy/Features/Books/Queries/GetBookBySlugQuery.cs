using Booksy.Core.Interfaces;
using Booksy.Features.Books.DTOs;

namespace Booksy.Features.Books.Queries
{
    /// <summary>
    /// Query for retrieving a book by slug
    /// </summary>
    public class GetBookBySlugQuery : IQuery<BookDetailResponse>
    {
        /// <summary>
        /// Book slug to retrieve
        /// </summary>
        public string Slug { get; set; }

        public GetBookBySlugQuery(string slug)
        {
            Slug = slug;
        }
    }
}
