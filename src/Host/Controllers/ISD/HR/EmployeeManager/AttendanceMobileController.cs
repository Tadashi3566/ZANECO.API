using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;

namespace ZANECO.API.Host.Controllers.ISD.HR.EmployeeManager;

[EnableRateLimiting("fixed")]
public class AttendanceMobileController : VersionedApiController
{
    [HttpPost("search")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Search Attendance using available filters.", "")]
    public Task<List<AttendanceDto>> MobileSearchAsync(AttendanceDateRangeRequest request)
    {
        return Mediator.Send(request);
    }
}