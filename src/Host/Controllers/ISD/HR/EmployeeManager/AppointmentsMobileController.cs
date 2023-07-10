using ZANECO.API.Application.ISD.HR.EmployeeManager.Appointments;

namespace ZANECO.API.Host.Controllers.ISD.HR.EmployeeManager;

public class AppointmentsMobileController : VersionedApiController
{
    [HttpPost("search")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Search Appointment using available filters.", "")]
    public Task<List<AppointmentDto>> MobileSearchAsync(AppointmentMobileSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Get Appointment details.", "")]
    public Task<AppointmentDto> MobileGetAsync(int id)
    {
        return Mediator.Send(new AppointmentGetRequest(id));
    }

    [HttpPost("create")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Create a new Appointment.", "")]
    public Task<int> MobileCreateAsync(AppointmentCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("mobile-update/{id:int}")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Update an Appointment.", "")]
    public async Task<ActionResult<int>> MobileUpdateAsync(AppointmentUpdateRequest request, int id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpPut("mobile-action")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Appointment Action", "")]
    public async Task<ActionResult<int>> MobileActionAsync(AppointmentActionRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpDelete("delete/{id:int}")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Delete an Appointment.", "")]
    public Task<int> MobileDeleteAsync(int id)
    {
        return Mediator.Send(new AppointmentDeleteRequest(id));
    }
}