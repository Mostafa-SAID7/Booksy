using Booksy.Core.Interfaces;
using Booksy.Features.Authentication.DTOs;

namespace Booksy.Features.Authors.Queries;

/// <summary>
/// Query to get a single author by ID
/// </summary>
public class GetAuthorByIdQuery : IQuery<AuthorResponse>
{
    public Guid Id { get; set; }
}
