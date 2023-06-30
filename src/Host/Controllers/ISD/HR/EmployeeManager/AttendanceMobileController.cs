using ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;

namespace ZANECO.API.Host.Controllers.ISD.HR.EmployeeManager;

public class AttendanceMobileController : VersionedApiController
{
    [HttpPost("mobile-search")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Search Attendance using available filters.", "")]
    public Task<List<AttendanceDto>> MobileSearchAsync(AttendanceDateRangeRequest request)
    {
        return Mediator.Send(request);
    }
}