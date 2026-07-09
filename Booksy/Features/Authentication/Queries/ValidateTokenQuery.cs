using Booksy.Core.Interfaces;

namespace Booksy.Features.Authentication.Queries;

/// <summary>
/// Query to validate a token
/// </summary>
public class ValidateTokenQuery : IQuery<bool>
{
    public string Token { get; set; } = string.Empty;
}
