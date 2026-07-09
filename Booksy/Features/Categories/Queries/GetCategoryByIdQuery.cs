using Booksy.Core.Interfaces;
using Booksy.Features.Categories.DTOs;

namespace Booksy.Features.Categories.Queries;

/// <summary>
/// Query to get a category by ID
/// </summary>
public class GetCategoryByIdQuery : IQuery<CategoryResponse>
{
    public Guid Id { get; set; }
}
