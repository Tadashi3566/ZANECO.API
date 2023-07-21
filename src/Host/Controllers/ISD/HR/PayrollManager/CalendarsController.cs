using ZANECO.API.Application.ISD.HR.PayrollManager.Calendars;

namespace ZANECO.API.Host.Controllers.ISD.HR.PayrollManager;

public class CalendarsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Calendar)]
    [OpenApiOperation("Search Calendar using available filters.", "")]
    public Task<PaginationResponse<CalendarDto>> SearchAsync(CalendarSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Calendar)]
    [OpenApiOperation("Get Calendar details.", "")]
    public Task<CalendarDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new CalendarGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Calendar)]
    [OpenApiOperation("Create a new Calendar.", "")]
    public Task<DefaultIdType> CreateAsync(CalendarCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Calendar)]
    [OpenApiOperation("Update a Calendar.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(CalendarUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Calendar)]
    [OpenApiOperation("Delete a Calendar.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new CalendarDeleteRequest(id));
    }
}