using ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

namespace ZANECO.API.Host.Controllers.ISD.HR.EmployeeManager;

public class EmployeesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Employees)]
    [OpenApiOperation("Search Employees using available filters.", "")]
    public Task<PaginationResponse<EmployeeDto>> SearchAsync(EmployeeSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("mobilesearch")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Search Employees using available filters.", "")]
    public Task<List<EmployeeMobileDto>> MobileSearchAsync(EmployeeMobileSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("birthday")]
    [MustHavePermission(FSHAction.Search, FSHResource.Employees)]
    [OpenApiOperation("Search Employees with current Birthday.", "")]
    public Task<PaginationResponse<EmployeeDto>> BirthdayAsync(EmployeeBirthdayRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("anniversary")]
    [MustHavePermission(FSHAction.Search, FSHResource.Employees)]
    [OpenApiOperation("Search Employees with current Anniversary.", "")]
    public Task<PaginationResponse<EmployeeDto>> AnniversaryAsync(EmployeeAnniversaryRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Employees)]
    [OpenApiOperation("Get Employee details.", "")]
    public Task<EmployeeDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new EmployeeGetRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Employees)]
    [OpenApiOperation("Get Employee details via dapper.", "")]
    public Task<EmployeeDto> GetDapperAsync(DefaultIdType id)
    {
        return Mediator.Send(new EmployeeGetViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Employees)]
    [OpenApiOperation("Create a new Employee.", "")]
    public Task<DefaultIdType> CreateAsync(EmployeeCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Employees)]
    [OpenApiOperation("Update an Employee.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(EmployeeUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpPut("set-active-schedule")]
    [MustHavePermission(FSHAction.Update, FSHResource.Employees)]
    [OpenApiOperation("Set Employee Schedule.", "")]
    public async Task<ActionResult<bool>> SetScheduleAsync(EmployeeSetScheduleRequest request, DefaultIdType id)
    {
        return id != request.ScheduleId
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Employees)]
    [OpenApiOperation("Delete a Employee.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new EmployeeDeleteRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Employees)]
    [OpenApiOperation("Export a Employees.", "")]
    public async Task<FileResult> ExportAsync(EmployeeExportRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "EmployeeExports");
    }
}