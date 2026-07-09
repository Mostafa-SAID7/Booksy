using Booksy.Core.Interfaces;

using MediatR;


namespace Booksy.Features.Books.Commands;



/// <summary>

/// Command to delete a book

/// </summary>

public class DeleteBookCommand : ICommand<Unit>

{

    public Guid Id { get; set; }

}




