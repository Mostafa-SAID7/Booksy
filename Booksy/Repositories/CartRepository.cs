using Booksy.Models.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Booksy.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Cart> _carts;
        private readonly DbSet<CartItem> _cartItems;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
            _carts = context.Set<Cart>();
            _cartItems = context.Set<CartItem>();
        }

        public async Task<Cart?> GetCartByUserIdAsync(string userId)
        {
            return await _carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Book)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<CartItem?> GetCartItemAsync(int cartId, int bookId)
        {
            return await _cartItems.FirstOrDefaultAsync(i => i.CartId == cartId && i.BookId == bookId);
        }

        public async Task AddCartItemAsync(CartItem item)
        {
            await _cartItems.AddAsync(item);
        }

        public void UpdateCartItem(CartItem item)
        {
            _cartItems.Update(item);
        }

        public void RemoveCartItem(CartItem item)
        {
            _cartItems.Remove(item);
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
