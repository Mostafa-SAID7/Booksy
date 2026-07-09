using Booksy.Core.Exceptions;

using Booksy.Core.Interfaces;

using Booksy.Models.Entities.Users;

using Booksy.Repositories.IRepositories;

using MediatR;


namespace Booksy.Features.Carts.Commands;



/// <summary>

/// Handler for removing an item from cart

/// </summary>

public class RemoveFromCartCommandHandler : ICommandHandler<RemoveFromCartCommand, Unit>

{

    private readonly IUnitOfWork _unitOfWork;



    public RemoveFromCartCommandHandler(IUnitOfWork unitOfWork)

    {

        _unitOfWork = unitOfWork;

    }



    public async Task<Unit> Handle(

        RemoveFromCartCommand request,

        CancellationToken cancellationToken)

    {

        // Get user's cart

        var cart = await _unitOfWork.Carts.GetOneAsync(c => c.UserId == request.UserId);



        if (cart == null)

        {

            throw new NotFoundException($"Cart not found for user {request.UserId}");

        }



        // Find and remove item

        var item = cart.Items.FirstOrDefault(i => i.BookId == request.BookId);

        if (item == null)

        {

            throw new NotFoundException($"Book with ID {request.BookId} not found in cart");

        }



        // Remove the item

        cart.Items.Remove(item);

        _unitOfWork.Carts.Update(cart);

        await _unitOfWork.SaveChangesAsync(cancellationToken);



        return Unit.Value;

    }

}




