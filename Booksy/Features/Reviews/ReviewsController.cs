using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Booksy.Features.Reviews.Commands;
using Booksy.Features.Reviews.Queries;
using Booksy.Features.Reviews.DTOs;
using Booksy.Common.Results;
using Booksy.Common.Models;

namespace Booksy.Features.Reviews;

/// <summary>
/// Review management endpoints
/// Thin controller - delegates business logic to CQRS handlers
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Tags("Reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReviewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all reviews with pagination, search, and filter support
    /// </summary>
    /// <param name="pageNumber">Page number (default: 1)</param>
    /// <param name="pageSize">Items per page (default: 10, max: 100)</param>
    /// <param name="searchTerm">Search term for comment, book title, or user email</param>
    /// <param name="sortBy">Sort field (e.g., "rating:desc", "createdat")</param>
    /// <returns>Paginated list of reviews</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Result<PaginatedResponse<ReviewDetailResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<PaginatedResponse<ReviewDetailResponse>>>> GetAll(
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

            var result = await _mediator.Send(new GetAllReviewsQuery(filter));
            return Ok(Result<PaginatedResponse<ReviewDetailResponse>>.Ok(result));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(Result.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Get a single review by ID
    /// </summary>
    /// <param name="id">Review ID</param>
    /// <returns>Review details</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Result<ReviewDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<ReviewDetailResponse>>> GetById(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetReviewByIdQuery { Id = id });
            return Ok(Result<ReviewDetailResponse>.Ok(result));
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Get all reviews for a specific book with pagination support
    /// </summary>
    /// <param name="bookId">Book ID</param>
    /// <param name="pageNumber">Page number (default: 1)</param>
    /// <param name="pageSize">Items per page (default: 10, max: 100)</param>
    /// <param name="sortBy">Sort field (e.g., "rating:desc", "createdat")</param>
    /// <returns>Paginated list of reviews for the book</returns>
    [HttpGet("book/{bookId:guid}")]
    [ProducesResponseType(typeof(Result<PaginatedResponse<ReviewDetailResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<PaginatedResponse<ReviewDetailResponse>>>> GetByBookId(
        Guid bookId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? sortBy = null)
    {
        try
        {
            var filter = new SearchFilter(pageNumber, pageSize)
            {
                SortBy = !string.IsNullOrWhiteSpace(sortBy)
                    ? sortBy.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()
                    : null
            };

            var result = await _mediator.Send(new GetBookReviewsQuery(bookId, filter));
            return Ok(Result<PaginatedResponse<ReviewDetailResponse>>.Ok(result));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(Result.Fail(ex.Message));
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Create a new review
    /// </summary>
    /// <param name="command">Review creation command</param>
    /// <returns>Created review</returns>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(Result<ReviewDetailResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<ReviewDetailResponse>>> Create([FromBody] CreateReviewCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, Result<ReviewDetailResponse>.Ok(result));
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
        catch (Core.Exceptions.ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new Error(e.Key, string.Join(", ", e.Value))).ToList();
            return BadRequest(Result.Fail("Validation failed", errors));
        }
    }

    /// <summary>
    /// Update an existing review
    /// </summary>
    /// <param name="id">Review ID</param>
    /// <param name="command">Update command</param>
    /// <returns>No content on success</returns>
    [Authorize]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReviewCommand command)
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
        catch (Core.Exceptions.ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new Error(e.Key, string.Join(", ", e.Value))).ToList();
            return BadRequest(Result.Fail("Validation failed", errors));
        }
    }

    /// <summary>
    /// Delete a review
    /// </summary>
    /// <param name="id">Review ID</param>
    /// <returns>No content on success</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteReviewCommand { Id = id });
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
