using MediatR;
using Microsoft.AspNetCore.Mvc;
using Booksy.Features.Users.Commands;
using Booksy.Features.Users.Queries;
using Booksy.Features.Authentication.DTOs;
using Booksy.Common.Results;
using Booksy.Common.Models;

namespace Booksy.Features.Users;

/// <summary>
/// User management endpoints
/// Thin controller - delegates business logic to CQRS handlers
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Tags("Users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all users with pagination, search, filtering, and sorting
    /// </summary>
    /// <param name="pageNumber">Page number (1-based, default: 1)</param>
    /// <param name="pageSize">Records per page (default: 10, max: 100)</param>
    /// <param name="searchTerm">Search by username, email, or name</param>
    /// <returns>Paginated list of users</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Result<PaginatedResponse<UserProfileResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<PaginatedResponse<UserProfileResponse>>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchTerm = null)
    {
        try
        {
            var filter = new SearchFilter
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SearchTerm = searchTerm
            };

            var result = await _mediator.Send(new GetAllUsersQuery(filter));
            return Ok(Result<PaginatedResponse<UserProfileResponse>>.Ok(result));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(Result.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Get user profile
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>User profile details</returns>
    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(Result<UserProfileResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<UserProfileResponse>>> GetProfile(string userId)
    {
        try
        {
            var result = await _mediator.Send(new GetUserProfileQuery { UserId = userId });
            return Ok(Result<UserProfileResponse>.Ok(result));
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Update user profile
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="command">Update command</param>
    /// <returns>Updated profile</returns>
    [HttpPut("{userId}")]
    [ProducesResponseType(typeof(Result<UserProfileResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<UserProfileResponse>>> UpdateProfile(string userId, [FromBody] UpdateProfileCommand command)
    {
        command.UserId = userId;

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
            return BadRequest(Result.Fail(ex.Message));
        }
        catch (Core.Exceptions.ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new Error(e.Key, string.Join(", ", e.Value))).ToList();
            return BadRequest(Result.Fail("Validation failed", errors));
        }
    }

    /// <summary>
    /// Change user password
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="command">Change password command</param>
    /// <returns>No content on success</returns>
    [HttpPost("{userId}/change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword(string userId, [FromBody] ChangePasswordCommand command)
    {
        command.UserId = userId;

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
}
