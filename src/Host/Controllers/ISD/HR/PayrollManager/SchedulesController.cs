using ZANECO.API.Application.ISD.HR.PayrollManager.Schedules;

namespace ZANECO.API.Host.Controllers.ISD.HR.PayrollManager;

public class SchedulesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Schedules)]
    [OpenApiOperation("Search Schedule using available filters.", "")]
    public Task<PaginationResponse<ScheduleDto>> SearchAsync(ScheduleSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Schedules)]
    [OpenApiOperation("Get Schedule details.", "")]
    public Task<ScheduleDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new ScheduleGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Schedules)]
    [OpenApiOperation("Create a new Schedule.", "")]
    public Task<DefaultIdType> CreateAsync(ScheduleCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Schedules)]
    [OpenApiOperation("Update a Schedule.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(ScheduleUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Schedules)]
    [OpenApiOperation("Delete a Schedule.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new ScheduleDeleteRequest(id));
    }
}