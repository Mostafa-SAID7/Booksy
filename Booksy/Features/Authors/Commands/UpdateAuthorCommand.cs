using Booksy.Core.Interfaces;

using MediatR;


namespace Booksy.Features.Authors.Commands;



/// <summary>

/// Command to update an existing author

/// </summary>

public class UpdateAuthorCommand : ICommand<Unit>

{

    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Slug { get; set; }

    public string? Bio { get; set; }

}




