using Booksy.Core.Interfaces;
using Booksy.Features.Tags.DTOs;

namespace Booksy.Features.Tags.Queries
{
    /// <summary>
    /// Query for retrieving a tag by ID
    /// </summary>
    public class GetTagByIdQuery : IQuery<TagResponse>
    {
        /// <summary>
        /// Tag ID to retrieve
        /// </summary>
        public Guid Id { get; set; }

        public GetTagByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
