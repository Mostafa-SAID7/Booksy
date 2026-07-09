using Booksy.Core.Interfaces;
using Booksy.Features.Authentication.DTOs;

namespace Booksy.Features.Authors.Commands;

/// <summary>
/// Command to create a new author
/// </summary>
public class CreateAuthorCommand : ICommand<AuthorResponse>
{
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public string? Bio { get; set; }
}
