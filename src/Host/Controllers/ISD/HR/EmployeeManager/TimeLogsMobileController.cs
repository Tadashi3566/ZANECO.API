using ZANECO.API.Application.ISD.HR.EmployeeManager.TimeLogs;

namespace ZANECO.API.Host.Controllers.ISD.HR.EmployeeManager;

public class TimeLogsMobileController : VersionedApiController
{
    [HttpPost("mobilecreate")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Create a new Time Log from Mobile.", "")]
    public Task<DefaultIdType> CreateFromMobileAsync(TimeLogCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("mobile-create")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Create a new Time Log from Mobile.", "")]
    public Task<DefaultIdType> MobileCreateAsync(TimeLogCreateRequest request)
    {
        return Mediator.Send(request);
    }
}