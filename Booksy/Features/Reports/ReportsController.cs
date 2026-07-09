using MediatR;
using Microsoft.AspNetCore.Mvc;
using Booksy.Features.Reports.Queries;
using Booksy.Common.Results;

namespace Booksy.Features.Reports;

/// <summary>
/// Reports endpoints
/// Thin controller - delegates business logic to CQRS handlers
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Tags("Reports")]
public class ReportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get top selling books
    /// </summary>
    /// <param name="limit">Number of top books to retrieve</param>
    /// <returns>List of top selling books</returns>
    [HttpGet("top-books")]
    [ProducesResponseType(typeof(Result<List<TopBookDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<List<TopBookDto>>>> GetTopBooks([FromQuery] int limit = 10)
    {
        var result = await _mediator.Send(new GetTopBooksQuery { Limit = limit });
        return Ok(Result<List<TopBookDto>>.Ok(result));
    }

    /// <summary>
    /// Get monthly revenue report
    /// </summary>
    /// <param name="months">Number of months to include</param>
    /// <returns>Monthly revenue data</returns>
    [HttpGet("monthly-revenue")]
    [ProducesResponseType(typeof(Result<List<MonthlyRevenueDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<List<MonthlyRevenueDto>>>> GetMonthlyRevenue([FromQuery] int months = 12)
    {
        var result = await _mediator.Send(new GetMonthlyRevenueQuery { Months = months });
        return Ok(Result<List<MonthlyRevenueDto>>.Ok(result));
    }

    /// <summary>
    /// Get sales report
    /// </summary>
    /// <param name="startDate">Report start date</param>
    /// <param name="endDate">Report end date</param>
    /// <returns>Detailed sales report</returns>
    [HttpGet("sales")]
    [ProducesResponseType(typeof(Result<SalesReportDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<SalesReportDto>>> GetSalesReport([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        var result = await _mediator.Send(new GetSalesReportQuery { StartDate = startDate, EndDate = endDate });
        return Ok(Result<SalesReportDto>.Ok(result));
    }
}
