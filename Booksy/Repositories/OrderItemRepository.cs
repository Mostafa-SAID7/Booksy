using Booksy.DataAccess;
using Booksy.Models.Entities.Orders;
using Booksy.Repositories.IRepositories;

namespace Booksy.Repositories
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        private ApplicationDbContext _context;// = new();

        public OrderItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(List<OrderItem> orderItems)
        {
            await _context.OrderItems.AddRangeAsync(orderItems);
        }

    }
}
