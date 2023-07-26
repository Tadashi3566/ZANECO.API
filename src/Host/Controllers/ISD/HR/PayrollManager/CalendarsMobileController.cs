using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.ISD.HR.PayrollManager.Calendars;

namespace ZANECO.API.Host.Controllers.ISD.HR.PayrollManager;

[EnableRateLimiting("fixed")]
public class CalendarsMobileController : VersionedApiController
{
    [HttpPost("search")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Get Holiday Schdules through mobile client.", "")]
    public Task<List<CalendarDto>> MobileSearchAsync(CalendarMobileSearchRequest request)
    {
        return Mediator.Send(request);
    }
}