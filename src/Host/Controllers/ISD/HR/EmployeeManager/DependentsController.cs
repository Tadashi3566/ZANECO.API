using ZANECO.API.Application.ISD.HR.EmployeeManager.Dependents;

namespace ZANECO.API.Host.Controllers.ISD.HR.EmployeeManager;

public class DependentsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Employees)]
    [OpenApiOperation("Search Dependents using available filters.", "")]
    public Task<PaginationResponse<DependentDto>> SearchAsync(DependentSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Employees)]
    [OpenApiOperation("Get Dependent details.", "")]
    public Task<DependentDetailsDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new DependentGetRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Employees)]
    [OpenApiOperation("Get Dependent details via dapper.", "")]
    public Task<DependentDto> GetDapperAsync(DefaultIdType id)
    {
        return Mediator.Send(new DependentGetViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Employees)]
    [OpenApiOperation("Create a new Dependent.", "")]
    public Task<DefaultIdType> CreateAsync(DependentCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Employees)]
    [OpenApiOperation("Update a Dependent.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(DependentUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Employees)]
    [OpenApiOperation("Delete a Dependent.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new DependentDeleteRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Employees)]
    [OpenApiOperation("Export a Dependents.", "")]
    public async Task<FileResult> ExportAsync(DependentExportRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "DependentExports");
    }
}