using Booksy.Models;
using System.Linq.Expressions;

namespace Booksy.Repositories.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task AddRangeAsync(List<Product> products);
    }
}
