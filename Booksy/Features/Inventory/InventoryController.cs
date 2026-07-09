using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Booksy.Features.Inventory.Commands;
using Booksy.Features.Inventory.Queries;
using Booksy.Common.Results;
using Booksy.Features.Books.DTOs;

namespace Booksy.Features.Inventory;

/// <summary>
/// Inventory management endpoints
/// Thin controller - delegates business logic to CQRS handlers
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Tags("Inventory")]
public class InventoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public InventoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Update stock for a book
    /// </summary>
    /// <param name="command">Update stock command</param>
    /// <returns>No content on success</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("update-stock")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateStock([FromBody] UpdateStockCommand command)
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
    /// Restock an item
    /// </summary>
    /// <param name="command">Restock command</param>
    /// <returns>No content on success</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("restock")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> RestockItem([FromBody] RestockItemCommand command)
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
    /// Get low stock items
    /// </summary>
    /// <param name="threshold">Stock threshold</param>
    /// <returns>List of low stock books</returns>
    [HttpGet("low-stock")]
    [ProducesResponseType(typeof(Result<List<BookResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<List<BookResponse>>>> GetLowStockItems([FromQuery] int threshold = 10)
    {
        var result = await _mediator.Send(new GetLowStockItemsQuery { Threshold = threshold });
        return Ok(Result<List<BookResponse>>.Ok(result));
    }

    /// <summary>
    /// Get inventory status
    /// </summary>
    /// <returns>Inventory status DTO</returns>
    [HttpGet("status")]
    [ProducesResponseType(typeof(Result<InventoryStatusDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<InventoryStatusDto>>> GetInventoryStatus()
    {
        var result = await _mediator.Send(new GetInventoryStatusQuery());
        return Ok(Result<InventoryStatusDto>.Ok(result));
    }

    /// <summary>
    /// Get inventory for specific item
    /// </summary>
    /// <param name="bookId">Book ID</param>
    /// <returns>Item inventory details</returns>
    [HttpGet("item/{bookId:guid}")]
    [ProducesResponseType(typeof(Result<ItemInventoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<ItemInventoryDto>>> GetItemInventory(Guid bookId)
    {
        try
        {
            var result = await _mediator.Send(new GetItemInventoryQuery { BookId = bookId });
            return Ok(Result<ItemInventoryDto>.Ok(result));
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
    }
}
