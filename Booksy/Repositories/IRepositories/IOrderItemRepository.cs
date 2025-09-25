using Booksy.Models;
using System.Linq.Expressions;

namespace Booksy.Repositories.IRepositories
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        Task AddRangeAsync(List<OrderItem> orderItems);
    }
}
