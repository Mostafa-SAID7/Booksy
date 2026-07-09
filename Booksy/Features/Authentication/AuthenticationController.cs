using MediatR;
using Microsoft.AspNetCore.Mvc;
using Booksy.Features.Authentication.Commands;
using Booksy.Features.Authentication.Queries;
using Booksy.Features.Authentication.DTOs;
using Booksy.Common.Results;

namespace Booksy.Features.Authentication;

/// <summary>
/// Authentication endpoints
/// Thin controller - delegates business logic to CQRS handlers
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Tags("Authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="command">Registration command</param>
    /// <returns>New user profile</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(Result<UserProfileResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Result<UserProfileResponse>>> Register([FromBody] RegisterCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCurrentUser), new { userId = result.UserId }, Result<UserProfileResponse>.Ok(result));
        }
        catch (Core.Exceptions.ConflictException ex)
        {
            return Conflict(Result.Fail(ex.Message));
        }
        catch (Core.Exceptions.BusinessException ex)
        {
            return BadRequest(Result.Fail(ex.Message));
        }
        catch (Core.Exceptions.ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new Error(e.Key, string.Join(", ", e.Value))).ToList();
            return BadRequest(Result.Fail("Validation failed", errors));
        }
    }

    /// <summary>
    /// Login user
    /// </summary>
    /// <param name="command">Login command</param>
    /// <returns>User profile on success</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(Result<UserProfileResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<UserProfileResponse>>> Login([FromBody] LoginCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(Result<UserProfileResponse>.Ok(result));
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
        catch (Core.Exceptions.BusinessException ex)
        {
            return Unauthorized(Result.Fail(ex.Message));
        }
        catch (Core.Exceptions.ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new Error(e.Key, string.Join(", ", e.Value))).ToList();
            return BadRequest(Result.Fail("Validation failed", errors));
        }
    }

    /// <summary>
    /// Confirm email
    /// </summary>
    /// <param name="command">Email confirmation command</param>
    /// <returns>No content on success</returns>
    [HttpPost("confirm-email")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
        catch (Core.Exceptions.BusinessException ex)
        {
            return BadRequest(Result.Fail(ex.Message));
        }
        catch (Core.Exceptions.ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new Error(e.Key, string.Join(", ", e.Value))).ToList();
            return BadRequest(Result.Fail("Validation failed", errors));
        }
    }

    /// <summary>
    /// Reset password
    /// </summary>
    /// <param name="command">Password reset command</param>
    /// <returns>No content on success</returns>
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
        catch (Core.Exceptions.BusinessException ex)
        {
            return BadRequest(Result.Fail(ex.Message));
        }
        catch (Core.Exceptions.ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new Error(e.Key, string.Join(", ", e.Value))).ToList();
            return BadRequest(Result.Fail("Validation failed", errors));
        }
    }

    /// <summary>
    /// Validate token
    /// </summary>
    /// <param name="query">Token validation query</param>
    /// <returns>True if valid, false otherwise</returns>
    [HttpPost("validate-token")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<bool>>> ValidateToken([FromBody] ValidateTokenQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(Result<bool>.Ok(result));
    }

    /// <summary>
    /// Get current authenticated user
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>User profile</returns>
    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(Result<UserProfileResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<UserProfileResponse>>> GetCurrentUser(string userId)
    {
        try
        {
            var result = await _mediator.Send(new GetCurrentUserQuery { UserId = userId });
            return Ok(Result<UserProfileResponse>.Ok(result));
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
    }
}
