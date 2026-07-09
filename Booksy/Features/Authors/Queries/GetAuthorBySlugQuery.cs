using Booksy.Core.Interfaces;
using Booksy.Features.Authentication.DTOs;

namespace Booksy.Features.Authors.Queries
{
    /// <summary>
    /// Query for retrieving an author by slug
    /// </summary>
    public class GetAuthorBySlugQuery : IQuery<AuthorResponse>
    {
        /// <summary>
        /// Author slug to retrieve
        /// </summary>
        public string Slug { get; set; }

        public GetAuthorBySlugQuery(string slug)
        {
            Slug = slug;
        }
    }
}
