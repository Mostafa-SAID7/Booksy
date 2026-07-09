using MediatR;
using Microsoft.AspNetCore.Mvc;
using Booksy.Features.Statistics.Queries;
using Booksy.Common.Results;

namespace Booksy.Features.Statistics;

/// <summary>
/// Statistics endpoints
/// Thin controller - delegates business logic to CQRS handlers
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Tags("Statistics")]
public class StatisticsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StatisticsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get dashboard statistics
    /// </summary>
    /// <returns>Dashboard stats</returns>
    [HttpGet("dashboard")]
    [ProducesResponseType(typeof(Result<DashboardStatsDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<DashboardStatsDto>>> GetDashboardStats()
    {
        var result = await _mediator.Send(new GetDashboardStatsQuery());
        return Ok(Result<DashboardStatsDto>.Ok(result));
    }

    /// <summary>
    /// Get user statistics
    /// </summary>
    /// <returns>User stats</returns>
    [HttpGet("users")]
    [ProducesResponseType(typeof(Result<UserStatsDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<UserStatsDto>>> GetUserStats()
    {
        var result = await _mediator.Send(new GetUserStatsQuery());
        return Ok(Result<UserStatsDto>.Ok(result));
    }

    /// <summary>
    /// Get book statistics
    /// </summary>
    /// <returns>Book stats</returns>
    [HttpGet("books")]
    [ProducesResponseType(typeof(Result<BookStatsDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<BookStatsDto>>> GetBookStats()
    {
        var result = await _mediator.Send(new GetBookStatsQuery());
        return Ok(Result<BookStatsDto>.Ok(result));
    }
}
