using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Orders.DTOs;
using Booksy.Models.Entities.Orders;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Orders.Queries;

/// <summary>
/// Handler for getting an order by ID
/// </summary>
public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderResponse>
{
    private readonly IRepository<OrderItem> _repository;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(
        IRepository<OrderItem> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OrderResponse> Handle(
        GetOrderByIdQuery request,
        CancellationToken cancellationToken)
    {
        // Note: You would need to add IOrderRepository if not exists
        // For now, this retrieves from order items context
        var orderItems = await _repository.GetAsync();
        var order = orderItems
            .Select(x => x.Order)
            .Distinct()
            .FirstOrDefault(o => o.Id == request.Id);

        if (order == null)
        {
            throw new NotFoundException($"Order with ID {request.Id} not found");
        }

        return _mapper.Map<OrderResponse>(order);
    }
}
