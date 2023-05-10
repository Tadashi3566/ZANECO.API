using ZANECO.API.Application.ISD.HR.PayrollManager.ScheduleDetails;

namespace ZANECO.API.Host.Controllers.ISD.HR.PayrollManager;

public class ScheduleDetailsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Schedules)]
    [OpenApiOperation("Search ScheduleDetail using available filters.", "")]
    public Task<PaginationResponse<ScheduleDetailDto>> SearchAsync(ScheduleDetailSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Schedules)]
    [OpenApiOperation("Get ScheduleDetail details.", "")]
    public Task<ScheduleDetailDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new ScheduleDetailGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Schedules)]
    [OpenApiOperation("Create a new ScheduleDetail.", "")]
    public Task<DefaultIdType> CreateAsync(ScheduleDetailCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Schedules)]
    [OpenApiOperation("Update a ScheduleDetail.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(ScheduleDetailUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Schedules)]
    [OpenApiOperation("Delete a ScheduleDetail.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new ScheduleDetailDeleteRequest(id));
    }
}