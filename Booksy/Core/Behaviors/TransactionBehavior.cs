using MediatR;
using Microsoft.Extensions.Logging;
using Booksy.Repositories.IRepositories;

namespace Booksy.Core.Behaviors;

/// <summary>
/// MediatR pipeline behavior for automatic transaction management
/// Wraps commands in database transactions for data consistency
/// Priority: 5 (Executes fifth, closest to handler)
/// 
/// Rules:
/// - Only wraps Commands (mutations), NOT Queries (reads)
/// - Provides automatic rollback on failure
/// - Ensures ACID compliance for operations
/// - Thread-safe transaction management
/// 
/// Responsibilities:
/// - Detect command vs query operations
/// - Begin transaction for commands
/// - Commit on success
/// - Rollback on failure
/// - Log transaction lifecycle
/// </summary>
public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBehaviorContext _context;

    public TransactionBehavior(
        ILogger<TransactionBehavior<TRequest, TResponse>> logger,
        IUnitOfWork unitOfWork,
        IBehaviorContext context)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Only apply transactions to Commands (mutation operations)
        // Queries are read-only and don't need transactions
        var requestName = typeof(TRequest).Name;
        var isCommand = requestName.EndsWith("Command", StringComparison.OrdinalIgnoreCase);

        if (!isCommand)
        {
            _logger.LogDebug(
                "Skipping Transaction (Query Operation) | Request: {RequestName}",
                requestName
            );
            return await next();
        }

        _logger.LogDebug(
            "Beginning Transaction | Command: {CommandName}",
            requestName
        );

        _context.Properties["IsTransacted"] = true;
        _context.Properties["TransactionStarted"] = DateTime.UtcNow;

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var response = await next();

            // Commit on success
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            
            _context.Properties["TransactionStatus"] = "Committed";
            _logger.LogDebug(
                "Transaction Committed | Command: {CommandName}",
                requestName
            );

            return response;
        }
        catch (Exception ex)
        {
            // Rollback on failure
            _logger.LogWarning(
                ex,
                "Transaction Rollback | Command: {CommandName} | Reason: {ErrorMessage}",
                requestName,
                ex.Message
            );

            _context.Properties["TransactionStatus"] = "RolledBack";
            _context.Properties["TransactionError"] = ex.Message;

            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
