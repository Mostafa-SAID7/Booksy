using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Booksy.Features.Promotions.Commands;
using Booksy.Features.Promotions.Queries;
using Booksy.Common.Results;
using Booksy.Features.Reports.DTOs;

namespace Booksy.Features.Promotions;

/// <summary>
/// Promotions management endpoints
/// Thin controller - delegates business logic to CQRS handlers
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Tags("Promotions")]
public class PromotionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PromotionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Create a new promotion
    /// </summary>
    /// <param name="command">Create promotion command</param>
    /// <returns>Created promotion</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(typeof(Result<PromotionResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<PromotionResponse>>> CreatePromotion([FromBody] CreatePromotionCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPromotionById), new { promotionId = result.Id }, Result<PromotionResponse>.Ok(result));
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
    /// Update an existing promotion
    /// </summary>
    /// <param name="command">Update promotion command</param>
    /// <returns>Updated promotion</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("{promotionId:guid}")]
    [ProducesResponseType(typeof(Result<PromotionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<PromotionResponse>>> UpdatePromotion(Guid promotionId, [FromBody] UpdatePromotionCommand command)
    {
        try
        {
            command.PromotionId = promotionId;
            var result = await _mediator.Send(command);
            return Ok(Result<PromotionResponse>.Ok(result));
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
    /// Delete a promotion
    /// </summary>
    /// <param name="promotionId">Promotion ID</param>
    /// <returns>No content on success</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{promotionId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeletePromotion(Guid promotionId)
    {
        try
        {
            await _mediator.Send(new DeletePromotionCommand { PromotionId = promotionId });
            return NoContent();
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Activate or deactivate a promotion
    /// </summary>
    /// <param name="promotionId">Promotion ID</param>
    /// <param name="isActive">Activation status</param>
    /// <returns>No content on success</returns>
    [Authorize(Roles = "Admin")]
    [HttpPatch("{promotionId:guid}/activate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> ActivatePromotion(Guid promotionId, [FromQuery] bool isActive)
    {
        try
        {
            await _mediator.Send(new ActivatePromotionCommand { PromotionId = promotionId, IsActive = isActive });
            return NoContent();
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Get all promotions
    /// </summary>
    /// <returns>List of all promotions</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Result<List<PromotionResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<List<PromotionResponse>>>> GetAllPromotions()
    {
        var result = await _mediator.Send(new GetAllPromotionsQuery());
        return Ok(Result<List<PromotionResponse>>.Ok(result));
    }

    /// <summary>
    /// Get active promotions
    /// </summary>
    /// <returns>List of active promotions</returns>
    [HttpGet("active")]
    [ProducesResponseType(typeof(Result<List<PromotionResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<List<PromotionResponse>>>> GetActivePromotions()
    {
        var result = await _mediator.Send(new GetActivePromotionsQuery());
        return Ok(Result<List<PromotionResponse>>.Ok(result));
    }

    /// <summary>
    /// Get a promotion by ID
    /// </summary>
    /// <param name="promotionId">Promotion ID</param>
    /// <returns>Promotion details</returns>
    [HttpGet("{promotionId:guid}")]
    [ProducesResponseType(typeof(Result<PromotionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<PromotionResponse>>> GetPromotionById(Guid promotionId)
    {
        try
        {
            var result = await _mediator.Send(new GetPromotionByIdQuery { PromotionId = promotionId });
            return Ok(Result<PromotionResponse>.Ok(result));
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
    }
}
