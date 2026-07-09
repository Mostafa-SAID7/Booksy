using Booksy.Core.Exceptions;

using Booksy.Core.Interfaces;

using Booksy.Models.Entities.Books;

using Booksy.Repositories.IRepositories;

using MediatR;


namespace Booksy.Features.Books.Commands;



/// <summary>

/// Handler for deleting a book

/// </summary>

public class DeleteBookCommandHandler : ICommandHandler<DeleteBookCommand, Unit>

{

    private readonly IUnitOfWork _unitOfWork;



    public DeleteBookCommandHandler(IUnitOfWork unitOfWork)

    {

        _unitOfWork = unitOfWork;

    }



    public async Task<Unit> Handle(

        DeleteBookCommand request,

        CancellationToken cancellationToken)

    {

        // Get existing book

        var book = await _unitOfWork.Books.GetOneAsync(b => b.Id == request.Id);

        if (book == null)

        {

            throw new NotFoundException($"Book with ID {request.Id} not found");

        }



        // Delete book

        _unitOfWork.Books.Delete(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);



        return Unit.Value;

    }

}




