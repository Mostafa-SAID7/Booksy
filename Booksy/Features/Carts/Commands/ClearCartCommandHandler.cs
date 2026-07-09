using Booksy.Core.Exceptions;

using Booksy.Core.Interfaces;

using Booksy.Models.Entities.Users;

using Booksy.Repositories.IRepositories;

using MediatR;


namespace Booksy.Features.Carts.Commands;



/// <summary>

/// Handler for clearing cart

/// </summary>

public class ClearCartCommandHandler : ICommandHandler<ClearCartCommand, Unit>

{

    private readonly IUnitOfWork _unitOfWork;



    public ClearCartCommandHandler(IUnitOfWork unitOfWork)

    {

        _unitOfWork = unitOfWork;

    }



    public async Task<Unit> Handle(

        ClearCartCommand request,

        CancellationToken cancellationToken)

    {

        // Get user's cart

        var cart = await _unitOfWork.Carts.GetOneAsync(c => c.UserId == request.UserId);



        if (cart == null)

        {

            throw new NotFoundException($"Cart not found for user {request.UserId}");

        }



        // Clear all items

        cart.Items.Clear();

        _unitOfWork.Carts.Update(cart);



        // Save changes

        await _unitOfWork.SaveChangesAsync(cancellationToken);



        return Unit.Value;

    }

}




