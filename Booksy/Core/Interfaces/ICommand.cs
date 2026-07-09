using MediatR;

namespace Booksy.Core.Interfaces;

/// <summary>
/// Marker interface for commands that don't return a response
/// </summary>
public interface ICommand : IRequest
{
}

/// <summary>
/// Marker interface for commands that return a response
/// </summary>
/// <typeparam name="TResponse">The response type</typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
