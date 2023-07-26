using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.ISD.HR.PayrollManager.Loans;

namespace ZANECO.API.Host.Controllers.ISD.HR.PayrollManager;

[EnableRateLimiting("fixed")]
public class LoanController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Payroll)]
    [OpenApiOperation("Search Loan using available filters.", "")]
    public Task<PaginationResponse<LoanDto>> SearchAsync(LoanSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Payroll)]
    [OpenApiOperation("Get Employee Payroll details.", "")]
    public Task<LoanDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new LoanGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Payroll)]
    [OpenApiOperation("Create a new Loan.", "")]
    public Task<DefaultIdType> CreateAsync(LoanCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Payroll)]
    [OpenApiOperation("Update a Loan.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(LoanUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Payroll)]
    [OpenApiOperation("Delete a Loan.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new LoanDeleteRequest(id));
    }
}