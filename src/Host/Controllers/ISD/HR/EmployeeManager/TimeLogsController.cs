using ZANECO.API.Application.ISD.HR.EmployeeManager.TimeLogs;

namespace ZANECO.API.Host.Controllers.ISD.HR.EmployeeManager;

public class TimeLogsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Attendance)]
    [OpenApiOperation("Search TimeLogs using available filters.", "")]
    public Task<PaginationResponse<TimeLogDto>> SearchAsync(TimeLogSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Attendance)]
    [OpenApiOperation("Get TimeLogs details.", "")]
    public Task<TimeLogDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new TimeLogGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Attendance)]
    [OpenApiOperation("Create a new Time Log.", "")]
    public Task<DefaultIdType> CreateAsync(TimeLogCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("mobilecreate")]
    //[MustHavePermission(FSHAction.Create, FSHResource.Attendance)]
    [AllowAnonymous]
    [OpenApiOperation("Create a new Time Log from Mobile.", "")]
    public Task<DefaultIdType> CreateFromMobileAsync(TimeLogCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Attendance)]
    [OpenApiOperation("Update a TimeLogs.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(TimeLogUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Attendance)]
    [OpenApiOperation("Delete a TimeLogs.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new TimeLogDeleteRequest(id));
    }
}