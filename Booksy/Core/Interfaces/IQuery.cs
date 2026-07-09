using MediatR;

namespace Booksy.Core.Interfaces;

/// <summary>
/// Marker interface for queries that return a response
/// </summary>
/// <typeparam name="TResponse">The response type</typeparam>
public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
