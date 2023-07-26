using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.ISD.HR.EmployeeManager.Designations;

namespace ZANECO.API.Host.Controllers.ISD.HR.EmployeeManager;

[EnableRateLimiting("fixed")]
public class DesignationsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Employees)]
    [OpenApiOperation("Search Designations using available filters.", "")]
    public Task<PaginationResponse<DesignationDto>> SearchAsync(DesignationSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Employees)]
    [OpenApiOperation("Get Designation details.", "")]
    public Task<DesignationDetailsDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new DesignationGetRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Employees)]
    [OpenApiOperation("Get Designation details via dapper.", "")]
    public Task<DesignationDto> GetDapperAsync(DefaultIdType id)
    {
        return Mediator.Send(new DesignationGetViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Employees)]
    [OpenApiOperation("Create a new Designation.", "")]
    public Task<DefaultIdType> CreateAsync(DesignationCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Employees)]
    [OpenApiOperation("Update a Designation.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(DesignationUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpPut("current-designation")]
    [MustHavePermission(FSHAction.Update, FSHResource.Employees)]
    [OpenApiOperation("Update Employee current Designation.", "")]
    public Task<bool> DesignationCurrentAsync(DesignationCurrentRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Employees)]
    [OpenApiOperation("Delete a Designation.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new DesignationDeleteRequest(id));
    }
}