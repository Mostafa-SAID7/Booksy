using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Booksy.Common.Results;
using Booksy.Common.Models;
using Booksy.Features.Tags.Commands;
using Booksy.Features.Tags.DTOs;
using Booksy.Features.Tags.Queries;

namespace Booksy.Features.Tags
{
    /// <summary>
    /// Tag management endpoints
    /// Thin controller - delegates business logic to CQRS handlers
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Tags")]
    public class TagsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all tags with pagination, search, and filter support
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Result<PaginatedResponse<TagResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Result<PaginatedResponse<TagResponse>>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? sortBy = null)
        {
            try
            {
                var filter = new SearchFilter(pageNumber, pageSize)
                {
                    SearchTerm = searchTerm,
                    SortBy = !string.IsNullOrWhiteSpace(sortBy)
                        ? sortBy.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()
                        : null
                };

                var result = await _mediator.Send(new GetAllTagsQuery(filter));
                return Ok(Result<PaginatedResponse<TagResponse>>.Ok(result));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(Result.Fail(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(ex.Message));
            }
        }

        /// <summary>
        /// Get a single tag by ID
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Result<TagResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Result<TagResponse>>> GetById(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetTagByIdQuery(id));
                return Ok(Result<TagResponse>.Ok(result));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(Result.Fail(ex.Message));
            }
        }

        /// <summary>
        /// Get a tag by slug (SEO-friendly URL)
        /// </summary>
        [HttpGet("by-slug/{slug}")]
        [ProducesResponseType(typeof(Result<TagResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Result<TagResponse>>> GetBySlug(string slug)
        {
            try
            {
                var result = await _mediator.Send(new GetTagBySlugQuery(slug));
                return Ok(Result<TagResponse>.Ok(result));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(Result.Fail(ex.Message));
            }
        }

        /// <summary>
        /// Get all tags associated with a book
        /// </summary>
        [HttpGet("book/{bookId:guid}")]
        [ProducesResponseType(typeof(Result<List<TagResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Result<List<TagResponse>>>> GetByBookId(Guid bookId)
        {
            try
            {
                var result = await _mediator.Send(new GetTagsByBookIdQuery(bookId));
                return Ok(Result<List<TagResponse>>.Ok(result));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(Result.Fail(ex.Message));
            }
        }

        /// <summary>
        /// Create a new tag - Requires Admin role
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Result<TagResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Result<TagResponse>>> Create([FromBody] CreateTagCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, Result<TagResponse>.Ok(result));
            }
            catch (InvalidOperationException ex)
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
        /// Update an existing tag - Requires Admin role
        /// </summary>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Result<TagResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Result<TagResponse>>> Update(Guid id, [FromBody] UpdateTagCommand command)
        {
            command.Id = id;

            try
            {
                var result = await _mediator.Send(command);
                return Ok(Result<TagResponse>.Ok(result));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(Result.Fail(ex.Message));
            }
            catch (InvalidOperationException ex)
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
        /// Delete a tag - Requires Admin role
        /// </summary>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteTagCommand(id));
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(Result.Fail(ex.Message));
            }
        }
    }
}
