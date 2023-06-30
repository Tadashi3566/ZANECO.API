using ZANECO.API.Application.ISD.HR.EmployeeManager.Calendars;

namespace ZANECO.API.Host.Controllers.ISD.HR.EmployeeManager;

public class CalendarsMobileController : VersionedApiController
{
    [HttpPost("mobile-search")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Get Holiday Schdules through mobile client.", "")]
    public Task<List<CalendarDto>> MobileSearchAsync(CalendarMobileSearchRequest request)
    {
        return Mediator.Send(request);
    }
}