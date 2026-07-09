using Booksy.Models.Entities.Books;
using Booksy.Models.Entities.Orders;
using Booksy.Models.Entities.Promotions;
using Booksy.Models.Entities.Users;

namespace Booksy.Repositories.IRepositories;

/// <summary>
/// Unit of Work pattern for coordinating multiple repositories and centralized transaction management
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Repository for Category entities
    /// </summary>
    IRepository<Category> Categories { get; }

    /// <summary>
    /// Repository for Book entities
    /// </summary>
    IRepository<Book> Books { get; }

    /// <summary>
    /// Repository for Tag entities
    /// </summary>
    IRepository<Tag> Tags { get; }

    /// <summary>
    /// Repository for Author entities
    /// </summary>
    IRepository<Author> Authors { get; }

    /// <summary>
    /// Repository for Order entities
    /// </summary>
    IRepository<Order> Orders { get; }

    /// <summary>
    /// Repository for OrderItem entities
    /// </summary>
    IRepository<OrderItem> OrderItems { get; }

    /// <summary>
    /// Repository for Cart entities
    /// </summary>
    IRepository<Cart> Carts { get; }

    /// <summary>
    /// Repository for CartItem entities
    /// </summary>
    IRepository<CartItem> CartItems { get; }

    /// <summary>
    /// Repository for Promotion entities
    /// </summary>
    IRepository<Promotion> Promotions { get; }

    /// <summary>
    /// Repository for ApplicationUser entities
    /// </summary>
    IRepository<ApplicationUser> Users { get; }

    /// <summary>
    /// Repository for Review entities
    /// </summary>
    IRepository<Booksy.Models.Entities.Books.Review> Reviews { get; }

    /// <summary>
    /// Save all pending changes to the database
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Number of entities affected</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begin a new transaction
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Commit the current transaction
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rollback the current transaction
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
