using Booksy.DataAccess;
using Booksy.Models.Entities.Books;
using Booksy.Models.Entities.Orders;
using Booksy.Models.Entities.Promotions;
using Booksy.Models.Entities.Users;
using Booksy.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Booksy.Repositories;

/// <summary>
/// Unit of Work implementation for coordinating multiple repositories and centralized transaction management
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    // Lazy-loaded repositories
    private IRepository<Category>? _categories;
    private IRepository<Book>? _books;
    private IRepository<Tag>? _tags;
    private IRepository<Author>? _authors;
    private IRepository<Order>? _orders;
    private IRepository<OrderItem>? _orderItems;
    private IRepository<Cart>? _carts;
    private IRepository<CartItem>? _cartItems;
    private IRepository<Promotion>? _promotions;
    private IRepository<ApplicationUser>? _users;
    private IRepository<Booksy.Models.Entities.Books.Review>? _reviews;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Repository for Category entities
    /// </summary>
    public IRepository<Category> Categories
    {
        get
        {
            _categories ??= new Repository<Category>(_context);
            return _categories;
        }
    }

    /// <summary>
    /// Repository for Book entities
    /// </summary>
    public IRepository<Book> Books
    {
        get
        {
            _books ??= new Repository<Book>(_context);
            return _books;
        }
    }

    /// <summary>
    /// Repository for Tag entities
    /// </summary>
    public IRepository<Tag> Tags
    {
        get
        {
            _tags ??= new Repository<Tag>(_context);
            return _tags;
        }
    }

    /// <summary>
    /// Repository for Author entities
    /// </summary>
    public IRepository<Author> Authors
    {
        get
        {
            _authors ??= new Repository<Author>(_context);
            return _authors;
        }
    }

    /// <summary>
    /// Repository for Order entities
    /// </summary>
    public IRepository<Order> Orders
    {
        get
        {
            _orders ??= new Repository<Order>(_context);
            return _orders;
        }
    }

    /// <summary>
    /// Repository for OrderItem entities
    /// </summary>
    public IRepository<OrderItem> OrderItems
    {
        get
        {
            _orderItems ??= new Repository<OrderItem>(_context);
            return _orderItems;
        }
    }

    /// <summary>
    /// Repository for Cart entities
    /// </summary>
    public IRepository<Cart> Carts
    {
        get
        {
            _carts ??= new Repository<Cart>(_context);
            return _carts;
        }
    }

    /// <summary>
    /// Repository for CartItem entities
    /// </summary>
    public IRepository<CartItem> CartItems
    {
        get
        {
            _cartItems ??= new Repository<CartItem>(_context);
            return _cartItems;
        }
    }

    /// <summary>
    /// Repository for Promotion entities
    /// </summary>
    public IRepository<Promotion> Promotions
    {
        get
        {
            _promotions ??= new Repository<Promotion>(_context);
            return _promotions;
        }
    }

    /// <summary>
    /// Repository for ApplicationUser entities
    /// </summary>
    public IRepository<ApplicationUser> Users
    {
        get
        {
            _users ??= new Repository<ApplicationUser>(_context);
            return _users;
        }
    }

    /// <summary>
    /// Repository for Review entities
    /// </summary>
    public IRepository<Booksy.Models.Entities.Books.Review> Reviews
    {
        get
        {
            _reviews ??= new Repository<Booksy.Models.Entities.Books.Review>(_context);
            return _reviews;
        }
    }

    /// <summary>
    /// Save all pending changes to the database
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Number of entities affected</returns>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Begin a new transaction
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    /// <summary>
    /// Commit the current transaction
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await SaveChangesAsync(cancellationToken);
            if (_transaction != null)
            {
                await _transaction.CommitAsync(cancellationToken);
            }
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    /// <summary>
    /// Rollback the current transaction
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync(cancellationToken);
            }
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context?.Dispose();
        GC.SuppressFinalize(this);
    }
}
