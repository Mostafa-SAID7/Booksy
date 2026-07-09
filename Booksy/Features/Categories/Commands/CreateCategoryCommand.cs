using Booksy.Core.Interfaces;
using Booksy.Features.Categories.DTOs;

namespace Booksy.Features.Categories.Commands;

/// <summary>
/// Command to create a new category
/// </summary>
public class CreateCategoryCommand : ICommand<CategoryResponse>
{
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
}
