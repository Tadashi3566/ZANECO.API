using ZANECO.API.Application.ISD.HR.PayrollManager.EmployeeAdjustments;

namespace ZANECO.API.Host.Controllers.ISD.HR.PayrollManager;

public class EmployeeAdjustmentsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Payroll)]
    [OpenApiOperation("Search EmployeeAdjustments using available filters.", "")]
    public Task<PaginationResponse<EmployeeAdjustmentDto>> SearchAsync(EmployeeAdjustmentSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Payroll)]
    [OpenApiOperation("Get EmployeeAdjustment details.", "")]
    public Task<EmployeeAdjustmentDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new EmployeeAdjustmentGetRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Payroll)]
    [OpenApiOperation("Get Employee Adjustment details via dapper.", "")]
    public Task<EmployeeAdjustmentDto> GetDapperAsync(DefaultIdType id)
    {
        return Mediator.Send(new EmployeeAdjustmentGetViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Payroll)]
    [OpenApiOperation("Create a new EmployeeAdjustment.", "")]
    public Task<DefaultIdType> CreateAsync(EmployeeAdjustmentCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Payroll)]
    [OpenApiOperation("Update a EmployeeAdjustment.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(EmployeeAdjustmentUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Payroll)]
    [OpenApiOperation("Delete a EmployeeAdjustment.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new EmployeeAdjustmentDeleteRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Payroll)]
    [OpenApiOperation("Export a EmployeeAdjustments.", "")]
    public async Task<FileResult> ExportAsync(EmployeeAdjustmentExportRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "EmployeeAdjustmentExports");
    }
}