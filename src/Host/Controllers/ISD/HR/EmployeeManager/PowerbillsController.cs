using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.ISD.HR.EmployeeManager.Powerbills;

namespace ZANECO.API.Host.Controllers.ISD.HR.EmployeeManager;

[EnableRateLimiting("fixed")]
public class PowerbillsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Employees)]
    [OpenApiOperation("Search Powerbills using available filters.", "")]
    public Task<PaginationResponse<PowerbillDto>> SearchAsync(PowerbillSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Employees)]
    [OpenApiOperation("Get Powerbill details.", "")]
    public Task<PowerbillDetailsDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new PowerbillGetRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Employees)]
    [OpenApiOperation("Get Powerbill details via dapper.", "")]
    public Task<PowerbillDto> GetDapperAsync(DefaultIdType id)
    {
        return Mediator.Send(new PowerbillGetViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Employees)]
    [OpenApiOperation("Create a new Powerbill.", "")]
    public Task<DefaultIdType> CreateAsync(PowerbillCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Employees)]
    [OpenApiOperation("Update a Powerbill.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(PowerbillUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Employees)]
    [OpenApiOperation("Delete a Powerbill.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new PowerbillDeleteRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Employees)]
    [OpenApiOperation("Export a Powerbills.", "")]
    public async Task<FileResult> ExportAsync(PowerbillExportRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "PowerbillExports");
    }
}