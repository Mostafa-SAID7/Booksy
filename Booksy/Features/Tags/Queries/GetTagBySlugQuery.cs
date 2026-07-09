using Booksy.Core.Interfaces;
using Booksy.Features.Tags.DTOs;

namespace Booksy.Features.Tags.Queries
{
    /// <summary>
    /// Query for retrieving a tag by slug
    /// </summary>
    public class GetTagBySlugQuery : IQuery<TagResponse>
    {
        /// <summary>
        /// Tag slug to retrieve
        /// </summary>
        public string Slug { get; set; }

        public GetTagBySlugQuery(string slug)
        {
            Slug = slug;
        }
    }
}
