using ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;

namespace ZANECO.API.Host.Controllers.ISD.HR.EmployeeManager;

public class AttendancesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Attendance)]
    [OpenApiOperation("Search Attendance using available filters.", "")]
    public Task<PaginationResponse<AttendanceDto>> SearchAsync(AttendanceSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("mobilesearch")]
    //[MustHavePermission(FSHAction.Search, FSHResource.Attendance)]
    [OpenApiOperation("Search Attendance using available filters.", "")]
    public Task<List<AttendanceDto>> DateRangeAsync(AttendanceDateRangeRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Attendance)]
    [OpenApiOperation("Get Attendance details.", "")]
    public Task<AttendanceDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new AttendanceGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Attendance)]
    [OpenApiOperation("Create a new Attendance.", "")]
    public Task<DefaultIdType> CreateAsync(AttendanceCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Attendance)]
    [OpenApiOperation("Update a Attendance.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(AttendanceUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Attendance)]
    [OpenApiOperation("Delete a Attendance.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new AttendanceDeleteRequest(id));
    }

    [HttpPost("reschedule")]
    [MustHavePermission(FSHAction.Update, FSHResource.Attendance)]
    [OpenApiOperation("Attendance Reschedule.", "")]
    public Task<bool> RescheduleAsync(AttendanceRescheduleRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("calculate")]
    [MustHavePermission(FSHAction.Update, FSHResource.Attendance)]
    [OpenApiOperation("Calculate an Attendance.", "")]
    public Task<DefaultIdType> CalculateAsync(AttendanceCalculateRequest request)
    {
        return Mediator.Send(request);
    }
}