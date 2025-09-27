using Booksy.Models.Entities.Users;

namespace Booksy.Repositories.IRepositories
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserIdAsync(string userId);
        Task<CartItem?> GetCartItemAsync(int cartId, int bookId);
        Task AddCartItemAsync(CartItem item);
        void UpdateCartItem(CartItem item);
        void RemoveCartItem(CartItem item);
        Task CommitAsync();
    }
}
