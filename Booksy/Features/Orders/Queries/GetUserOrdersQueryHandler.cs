using AutoMapper;
using Booksy.Core.Interfaces;
using Booksy.Features.Orders.DTOs;
using Booksy.Models.Entities.Orders;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Orders.Queries;

/// <summary>
/// Handler for getting user's orders
/// </summary>
public class GetUserOrdersQueryHandler : IQueryHandler<GetUserOrdersQuery, IEnumerable<OrderResponse>>
{
    private readonly IRepository<OrderItem> _repository;
    private readonly IMapper _mapper;

    public GetUserOrdersQueryHandler(
        IRepository<OrderItem> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderResponse>> Handle(
        GetUserOrdersQuery request,
        CancellationToken cancellationToken)
    {
        // Note: You would need to add IOrderRepository if not exists
        // For now, this is a placeholder implementation
        var orderItems = await _repository.GetAsync();
        var userOrders = orderItems
            .GroupBy(x => x.Order.UserId)
            .Where(g => g.Key == request.UserId)
            .Select(g => g.First().Order)
            .Distinct();

        return _mapper.Map<IEnumerable<OrderResponse>>(userOrders);
    }
}
