using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrollDetails;

namespace ZANECO.API.Host.Controllers.ISD.HR.PayrollManager;

[EnableRateLimiting("fixed")]
public class EmployeePayrollDetailController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Payroll)]
    [OpenApiOperation("Search EmployeePayrollDetail using available filters.", "")]
    public Task<PaginationResponse<EmployeePayrollDetailDto>> SearchAsync(EmployeePayrollDetailSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Payroll)]
    [OpenApiOperation("Get Employee Payroll details.", "")]
    public Task<EmployeePayrollDetailDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new EmployeePayrollDetailGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Payroll)]
    [OpenApiOperation("Create a new EmployeePayrollDetail.", "")]
    public Task<DefaultIdType> CreateAsync(EmployeePayrollDetailCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("generate")]
    [MustHavePermission(FSHAction.Create, FSHResource.Payroll)]
    [OpenApiOperation("Generate a new Employee Payroll.", "")]
    public Task<bool> GenerateAsync(EmployeePayrollDetailGenerateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Payroll)]
    [OpenApiOperation("Update a EmployeePayrollDetail.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(EmployeePayrollDetailUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Payroll)]
    [OpenApiOperation("Delete a EmployeePayrollDetail.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new EmployeePayrollDetailDeleteRequest(id));
    }
}