using Booksy.Core.Interfaces;
using Booksy.Features.Categories.DTOs;

namespace Booksy.Features.Categories.Queries
{
    /// <summary>
    /// Query for retrieving a category by slug
    /// </summary>
    public class GetCategoryBySlugQuery : IQuery<CategoryResponse>
    {
        /// <summary>
        /// Category slug to retrieve
        /// </summary>
        public string Slug { get; set; }

        public GetCategoryBySlugQuery(string slug)
        {
            Slug = slug;
        }
    }
}
