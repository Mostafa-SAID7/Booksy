using Booksy.Core.Interfaces;

namespace Booksy.Features.Categories.Commands;

/// <summary>
/// Command to update an existing category
/// </summary>
public class UpdateCategoryCommand : ICommand
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
}
