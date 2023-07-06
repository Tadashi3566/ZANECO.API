using ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

namespace ZANECO.API.Host.Controllers.ISD.HR.EmployeeManager;

public class TeamsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Employees)]
    [OpenApiOperation("Search Team Members using available filters.", "")]
    public Task<PaginationResponse<TeamDto>> SearchAsync(TeamSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("dapper-search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Employees)]
    [OpenApiOperation("Search Team Members using through dapper.", "")]
    public Task<List<TeamDto>> DapperSearchAsync(TeamSearchViaDapperRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Employees)]
    [OpenApiOperation("Get Team details.", "")]
    public Task<TeamDetailDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new TeamGetRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Employees)]
    [OpenApiOperation("Get Team details via dapper.", "")]
    public Task<TeamDto> GetDapperAsync(DefaultIdType id)
    {
        return Mediator.Send(new TeamGetViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Employees)]
    [OpenApiOperation("Create a new Team Member.", "")]
    public Task<DefaultIdType> CreateAsync(TeamCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Employees)]
    [OpenApiOperation("Update a Team Member.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(TeamUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Employees)]
    [OpenApiOperation("Delete a Team Member.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new TeamDeleteRequest(id));
    }

    //[HttpPost("export")]
    //[MustHavePermission(FSHAction.Export, FSHResource.Employees)]
    //[OpenApiOperation("Export a Teams.", "")]
    //public async Task<FileResult> ExportAsync(TeamExportRequest filter)
    //{
    //    var result = await Mediator.Send(filter);
    //    return File(result, "application/octet-stream", "TeamExports");
    //}
}