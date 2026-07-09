using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Booksy.Features.Orders.Commands;
using Booksy.Features.Orders.Queries;
using Booksy.Features.Orders.DTOs;
using Booksy.Common.Results;

namespace Booksy.Features.Orders;

/// <summary>
/// Order management endpoints
/// Thin controller - delegates business logic to CQRS handlers
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Tags("Orders")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all orders for a user
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>List of user's orders</returns>
    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(Result<IEnumerable<OrderResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<OrderResponse>>>> GetUserOrders(string userId)
    {
        var result = await _mediator.Send(new GetUserOrdersQuery { UserId = userId });
        return Ok(Result<IEnumerable<OrderResponse>>.Ok(result));
    }

    /// <summary>
    /// Get a single order by ID
    /// </summary>
    /// <param name="id">Order ID</param>
    /// <returns>Order details</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Result<OrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<OrderResponse>>> GetById(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetOrderByIdQuery { Id = id });
            return Ok(Result<OrderResponse>.Ok(result));
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Create a new order from cart
    /// </summary>
    /// <param name="command">Order creation command</param>
    /// <returns>Created order</returns>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(Result<OrderResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<OrderResponse>>> Create([FromBody] CreateOrderCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, Result<OrderResponse>.Ok(result));
        }
        catch (Core.Exceptions.BusinessException ex)
        {
            return BadRequest(Result.Fail(ex.Message));
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
    /// Update order status
    /// </summary>
    /// <param name="id">Order ID</param>
    /// <param name="command">Update command</param>
    /// <returns>No content on success</returns>
    [Authorize]
    [HttpPut("{id:guid}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateOrderStatusCommand command)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        command.OrderId = id;
        command.UserId = userId;

        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (Core.Exceptions.AuthorizationException ex)
        {
            return Forbid();
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
    /// Cancel an order
    /// </summary>
    /// <param name="id">Order ID</param>
    /// <returns>No content on success</returns>
    [Authorize]
    [HttpPost("{id:guid}/cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        try
        {
            await _mediator.Send(new CancelOrderCommand { OrderId = id, UserId = userId });
            return NoContent();
        }
        catch (Core.Exceptions.AuthorizationException ex)
        {
            return Forbid();
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
