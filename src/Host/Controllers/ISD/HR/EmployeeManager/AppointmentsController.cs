using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.ISD.HR.EmployeeManager.Appointments;

namespace ZANECO.API.Host.Controllers.ISD.HR.EmployeeManager;

[EnableRateLimiting("fixed")]
public class AppointmentsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Appointment)]
    [OpenApiOperation("Search Appointment using available filters.", "")]
    public Task<PaginationResponse<AppointmentDto>> SearchAsync(AppointmentSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:int}")]
    [MustHavePermission(FSHAction.View, FSHResource.Appointment)]
    [OpenApiOperation("Get Appointment details.", "")]
    public Task<AppointmentDto> GetAsync(int id)
    {
        return Mediator.Send(new AppointmentGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Appointment)]
    [OpenApiOperation("Create a new Appointment.", "")]
    public Task<int> CreateAsync(AppointmentCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:int}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Appointment)]
    [OpenApiOperation("Update an Appointment.", "")]
    public async Task<ActionResult<int>> UpdateAsync(AppointmentUpdateRequest request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpPut("action")]
    [MustHavePermission(FSHAction.Update, FSHResource.Appointment)]
    [OpenApiOperation("Appointment Action", "")]
    public async Task<ActionResult<int>> ActionAsync(AppointmentActionRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpDelete("{id:int}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Appointment)]
    [OpenApiOperation("Delete an Appointment.", "")]
    public Task<int> DeleteAsync(int id)
    {
        return Mediator.Send(new AppointmentDeleteRequest(id));
    }
}