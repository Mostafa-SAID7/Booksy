using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Booksy.Features.Authors.Commands;
using Booksy.Features.Authors.Queries;
using Booksy.Features.Authentication.DTOs;
using Booksy.Common.Results;
using Booksy.Common.Models;

namespace Booksy.Features.Authors;

/// <summary>
/// Author management endpoints
/// Thin controller - delegates business logic to CQRS handlers
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Tags("Authors")]
public class AuthorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all authors with pagination, search, filtering, and sorting
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(Result<PaginatedResponse<AuthorResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<PaginatedResponse<AuthorResponse>>>> GetAll(
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

            var result = await _mediator.Send(new GetAllAuthorsQuery(filter));
            return Ok(Result<PaginatedResponse<AuthorResponse>>.Ok(result));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(Result.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Get a single author by ID
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Result<AuthorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<AuthorResponse>>> GetById(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetAuthorByIdQuery { Id = id });
            return Ok(Result<AuthorResponse>.Ok(result));
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Create a new author - Requires Admin role
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<AuthorResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<AuthorResponse>>> Create([FromBody] CreateAuthorCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, Result<AuthorResponse>.Ok(result));
        }
        catch (Core.Exceptions.ConflictException ex)
        {
            return Conflict(Result.Fail(ex.Message));
        }
        catch (Core.Exceptions.ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new Error(e.Key, string.Join(", ", e.Value))).ToList();
            return BadRequest(Result.Fail("Validation failed", errors));
        }
    }

    /// <summary>
    /// Update an existing author - Requires Admin role
    /// </summary>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAuthorCommand command)
    {
        command.Id = id;

        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
        catch (Core.Exceptions.ConflictException ex)
        {
            return Conflict(Result.Fail(ex.Message));
        }
        catch (Core.Exceptions.ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new Error(e.Key, string.Join(", ", e.Value))).ToList();
            return BadRequest(Result.Fail("Validation failed", errors));
        }
    }

    /// <summary>
    /// Delete an author - Requires Admin role
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteAuthorCommand { Id = id });
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
