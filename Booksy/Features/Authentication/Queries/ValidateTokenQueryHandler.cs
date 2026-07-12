using Booksy.Core.Interfaces;
using Booksy.Utility.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Booksy.Features.Authentication.Queries;

/// <summary>
/// Validates a JWT token: checks signature, issuer, audience, and expiry.
/// </summary>
public class ValidateTokenQueryHandler : IQueryHandler<ValidateTokenQuery, bool>
{
    private readonly JwtSettings _jwtSettings;

    public ValidateTokenQueryHandler(IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }

    public Task<bool> Handle(
        ValidateTokenQuery request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Token))
            return Task.FromResult(false);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwtSettings.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            tokenHandler.ValidateToken(request.Token, validationParameters, out _);
            return Task.FromResult(true);
        }
        catch (SecurityTokenException)
        {
            return Task.FromResult(false);
        }
    }
}
