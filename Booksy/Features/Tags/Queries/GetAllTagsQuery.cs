using Booksy.Common.Models;
using Booksy.Core.Interfaces;
using Booksy.Features.Tags.DTOs;

namespace Booksy.Features.Tags.Queries
{
    /// <summary>
    /// Query for retrieving all tags with pagination, search, filter, and sort support
    /// </summary>
    public class GetAllTagsQuery : IPaginatedQuery<PaginatedResponse<TagResponse>>
    {
        /// <summary>
        /// Search, filter, and pagination parameters
        /// </summary>
        public SearchFilter Filter { get; set; }

        public GetAllTagsQuery(SearchFilter? filter = null)
        {
            Filter = filter ?? new SearchFilter();
        }
    }
}
