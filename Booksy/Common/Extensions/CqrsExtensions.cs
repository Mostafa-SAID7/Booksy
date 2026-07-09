using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Booksy.Core.Behaviors;

namespace Booksy.Common.Extensions;

/// <summary>
/// Extension methods for registering CQRS and validation services
/// 
/// Pipeline Architecture:
/// ┌─────────────────────────────────────────────────────────────┐
/// │                    HTTP REQUEST                              │
/// └──────────────────────┬──────────────────────────────────────┘
///                        ↓
/// ┌─────────────────────────────────────────────────────────────┐
/// │  1. ValidationBehavior - Input Validation (Parallel)         │
/// │     • FluentValidation validators                            │
/// │     • Collect validation errors                              │
/// │     • Update context                                         │
/// └──────────────────────┬──────────────────────────────────────┘
///                        ↓
/// ┌─────────────────────────────────────────────────────────────┐
/// │  2. LoggingBehavior - Request/Response Logging               │
/// │     • Log request details                                    │
/// │     • Track execution time                                   │
/// │     • Record metrics                                         │
/// └──────────────────────┬──────────────────────────────────────┘
///                        ↓
/// ┌─────────────────────────────────────────────────────────────┐
/// │  3. ExceptionBehavior - Exception Handling                   │
/// │     • Categorize exceptions                                  │
/// │     • Log appropriately                                      │
/// │     • Preserve stack traces                                  │
/// └──────────────────────┬──────────────────────────────────────┘
///                        ↓
/// ┌─────────────────────────────────────────────────────────────┐
/// │  4. PerformanceBehavior - Performance Monitoring             │
/// │     • Detect slow operations                                 │
/// │     • Log with severity levels                               │
/// │     • Store performance data                                 │
/// └──────────────────────┬──────────────────────────────────────┘
///                        ↓
/// ┌─────────────────────────────────────────────────────────────┐
/// │  5. TransactionBehavior - Transaction Management             │
/// │     • Wrap Commands in transactions                          │
/// │     • Commit on success                                      │
/// │     • Rollback on failure                                    │
/// └──────────────────────┬──────────────────────────────────────┘
///                        ↓
/// ┌─────────────────────────────────────────────────────────────┐
/// │  6. AuthorizationBehavior - Authorization Checks             │
/// │     • Validate user permissions                              │
/// │     • Check role requirements                                │
/// │     • Enforce policies                                       │
/// └──────────────────────┬──────────────────────────────────────┘
///                        ↓
/// ┌─────────────────────────────────────────────────────────────┐
/// │  7. CachingBehavior - Query Caching (Future)                 │
/// │     • Cache query results                                    │
/// │     • Invalidate on updates                                  │
/// │     • Manage TTL                                             │
/// └──────────────────────┬──────────────────────────────────────┘
///                        ↓
/// ┌─────────────────────────────────────────────────────────────┐
/// │              HANDLER EXECUTION (Query/Command)               │
/// └──────────────────────┬──────────────────────────────────────┘
///                        ↓
/// ┌─────────────────────────────────────────────────────────────┐
/// │                  RESPONSE RETURNED                            │
/// └─────────────────────────────────────────────────────────────┘
/// </summary>
public static class CqrsExtensions
{
    /// <summary>
    /// Adds MediatR with all pipeline behaviors and FluentValidation validators
    /// </summary>
    public static IServiceCollection AddCqrsServices(this IServiceCollection services)
    {
        // Register Behavior Context (Scoped - one per request)
        services.AddScoped<IBehaviorContext, BehaviorContext>();

        // Register Behavior Metrics (Scoped - one per request)
        services.AddScoped<IBehaviorMetrics, BehaviorMetrics>();

        // Register MediatR with all handlers from the assembly
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            
            // Register pipeline behaviors in execution order
            // Each behavior wraps the next one, creating a pipeline
            
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
        });

        // Register all FluentValidation validators from assembly
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);

        return services;
    }
}

