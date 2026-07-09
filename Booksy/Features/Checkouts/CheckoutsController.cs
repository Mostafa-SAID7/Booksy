using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Booksy.Features.Checkouts.Commands;
using Booksy.Features.Checkouts.Queries;
using Booksy.Common.Results;
using Booksy.Features.Carts.DTOs;

namespace Booksy.Features.Checkouts;

/// <summary>
/// Checkouts endpoints
/// Thin controller - delegates business logic to CQRS handlers
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Tags("Checkouts")]
public class CheckoutsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CheckoutsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Process checkout
    /// </summary>
    /// <param name="command">Process checkout command</param>
    /// <returns>Checkout result</returns>
    [Authorize]
    [HttpPost("process")]
    [ProducesResponseType(typeof(Result<CheckoutResultDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<CheckoutResultDto>>> ProcessCheckout([FromBody] ProcessCheckoutCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCheckoutCart), new { userId = command.UserId }, Result<CheckoutResultDto>.Ok(result));
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
    /// Apply coupon to checkout
    /// </summary>
    /// <param name="command">Apply coupon command</param>
    /// <returns>Coupon application result</returns>
    [Authorize]
    [HttpPost("apply-coupon")]
    [ProducesResponseType(typeof(Result<ApplyCouponResultDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<ApplyCouponResultDto>>> ApplyCoupon([FromBody] ApplyCouponCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(Result<ApplyCouponResultDto>.Ok(result));
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
    /// Get checkout cart for user
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>Checkout cart</returns>
    [HttpGet("cart/{userId}")]
    [ProducesResponseType(typeof(Result<CheckoutCartDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<CheckoutCartDto>>> GetCheckoutCart(string userId)
    {
        try
        {
            var result = await _mediator.Send(new GetCheckoutCartQuery { UserId = userId });
            return Ok(Result<CheckoutCartDto>.Ok(result));
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Get checkout summary for user
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>Checkout summary</returns>
    [HttpGet("summary/{userId}")]
    [ProducesResponseType(typeof(Result<CheckoutSummaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<CheckoutSummaryDto>>> GetCheckoutSummary(string userId)
    {
        try
        {
            var result = await _mediator.Send(new GetCheckoutSummaryQuery { UserId = userId });
            return Ok(Result<CheckoutSummaryDto>.Ok(result));
        }
        catch (Core.Exceptions.NotFoundException ex)
        {
            return NotFound(Result.Fail(ex.Message));
        }
    }
}
