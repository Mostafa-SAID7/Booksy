namespace Booksy.Core.Interfaces;

/// <summary>
/// Marker interface for paginated queries
/// </summary>
public interface IPaginatedQuery<out TResponse> : IQuery<TResponse>
{
}
