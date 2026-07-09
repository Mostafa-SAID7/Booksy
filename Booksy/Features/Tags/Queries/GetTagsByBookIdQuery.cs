using Booksy.Core.Interfaces;
using Booksy.Features.Tags.DTOs;

namespace Booksy.Features.Tags.Queries
{
    /// <summary>
    /// Query for retrieving all tags associated with a book
    /// </summary>
    public class GetTagsByBookIdQuery : IQuery<List<TagResponse>>
    {
        /// <summary>
        /// Book ID to retrieve tags for
        /// </summary>
        public Guid BookId { get; set; }

        public GetTagsByBookIdQuery(Guid bookId)
        {
            BookId = bookId;
        }
    }
}
