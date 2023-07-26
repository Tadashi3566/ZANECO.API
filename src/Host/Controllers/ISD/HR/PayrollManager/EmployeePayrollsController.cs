using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrolls;

namespace ZANECO.API.Host.Controllers.ISD.HR.PayrollManager;

[EnableRateLimiting("fixed")]
public class EmployeePayrollsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Payroll)]
    [OpenApiOperation("Search EmployeePayroll using available filters.", "")]
    public Task<PaginationResponse<EmployeePayrollDto>> SearchAsync(EmployeePayrollSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Payroll)]
    [OpenApiOperation("Get Employee Payroll details.", "")]
    public Task<EmployeePayrollDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new EmployeePayrollGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Payroll)]
    [OpenApiOperation("Create a new EmployeePayroll.", "")]
    public Task<DefaultIdType> CreateAsync(EmployeePayrollCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Payroll)]
    [OpenApiOperation("Update a EmployeePayroll.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(EmployeePayrollUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Payroll)]
    [OpenApiOperation("Delete a EmployeePayroll.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new EmployeePayrollDeleteRequest(id));
    }
}