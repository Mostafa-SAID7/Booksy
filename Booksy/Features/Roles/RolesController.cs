using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Booksy.Features.Roles.Commands;
using Booksy.Features.Roles.Queries;
using Booksy.Common.Results;

namespace Booksy.Features.Roles;

/// <summary>
/// Roles management endpoints
/// Thin controller - delegates business logic to CQRS handlers
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Tags("Roles")]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Create a new role
    /// </summary>
    /// <param name="command">Create role command</param>
    /// <returns>Created role</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(typeof(Result<RoleDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<RoleDto>>> CreateRole([FromBody] CreateRoleCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetRoleById), new { roleId = result.Id }, Result<RoleDto>.Ok(result));
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
    /// Update an existing role
    /// </summary>
    /// <param name="roleId">Role ID</param>
    /// <param name="command">Update role command</param>
    /// <returns>Updated role</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("{roleId}")]
    [ProducesResponseType(typeof(Result<RoleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<RoleDto>>> UpdateRole(string roleId, [FromBody] UpdateRoleCommand command)
    {
        try
        {
            command.RoleId = roleId;
            var result = await _mediator.Send(command);
            return Ok(Result<RoleDto>.Ok(result));
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
    /// Delete a role
    /// </summary>
    /// <param name="roleId">Role ID</param>
    /// <returns>No content on success</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{roleId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteRole(string roleId)
    {
        try
        {
            await _mediator.Send(new DeleteRoleCommand { RoleId = roleId });
            return NoContent();
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Assign role to user
    /// </summary>
    /// <param name="command">Assign role command</param>
    /// <returns>No content on success</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("assign")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleToUserCommand command)
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
    /// Get all roles
    /// </summary>
    /// <returns>List of all roles</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Result<List<RoleDetailsDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<List<RoleDetailsDto>>>> GetAllRoles()
    {
        var result = await _mediator.Send(new GetAllRolesQuery());
        return Ok(Result<List<RoleDetailsDto>>.Ok(result));
    }

    /// <summary>
    /// Get a role by ID
    /// </summary>
    /// <param name="roleId">Role ID</param>
    /// <returns>Role details</returns>
    [HttpGet("{roleId}")]
    [ProducesResponseType(typeof(Result<RoleDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<RoleDetailsDto>>> GetRoleById(string roleId)
    {
        try
        {
            var result = await _mediator.Send(new GetRoleByIdQuery { RoleId = roleId });
            return Ok(Result<RoleDetailsDto>.Ok(result));
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Get user roles
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>List of user roles</returns>
    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(Result<List<string>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<List<string>>>> GetUserRoles(string userId)
    {
        try
        {
            var result = await _mediator.Send(new GetUserRolesQuery { UserId = userId });
            return Ok(Result<List<string>>.Ok(result));
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
    }
}
