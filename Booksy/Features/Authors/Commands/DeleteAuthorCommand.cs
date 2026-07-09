using Booksy.Core.Interfaces;

using MediatR;


namespace Booksy.Features.Authors.Commands;



/// <summary>

/// Command to delete an author

/// </summary>

public class DeleteAuthorCommand : ICommand<Unit>

{

    public Guid Id { get; set; }

}




