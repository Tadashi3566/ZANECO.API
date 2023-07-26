using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.ISD.HR.EmployeeManager.TimeLogs;

namespace ZANECO.API.Host.Controllers.ISD.HR.EmployeeManager;

[EnableRateLimiting("fixed")]
public class TimeLogsMobileController : VersionedApiController
{
    [HttpPost("create")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Create a new Time Log from Mobile.", "")]
    public Task<DefaultIdType> MobileCreateAsync(TimeLogCreateRequest request)
    {
        return Mediator.Send(request);
    }
}