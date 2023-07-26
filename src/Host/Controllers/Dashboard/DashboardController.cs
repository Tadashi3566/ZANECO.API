using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.Dashboard;

namespace ZANECO.API.Host.Controllers.Dashboard;

[EnableRateLimiting("fixed")]
public class DashboardController : VersionedApiController
{
    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.Dashboard)]
    [OpenApiOperation("Get statistics for the dashboard.", "")]
    public Task<StatsDto> GetAsync()
    {
        return Mediator.Send(new GetStatsRequest());
    }
}