using ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

namespace ZANECO.API.Host.Controllers.ISD.HR.EmployeeManager;

public class EmployeesMobileController : VersionedApiController
{
    [HttpPost("search")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Search Employees using available filters.", "")]
    public Task<List<EmployeeMobileDto>> MobileSearchAsync(EmployeeMobileSearchRequest request)
    {
        return Mediator.Send(request);
    }
}