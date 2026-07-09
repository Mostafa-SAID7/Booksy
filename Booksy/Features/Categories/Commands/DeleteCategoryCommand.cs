using Booksy.Core.Interfaces;

namespace Booksy.Features.Categories.Commands;

/// <summary>
/// Command to delete a category
/// </summary>
public class DeleteCategoryCommand : ICommand
{
    public Guid Id { get; set; }
}
