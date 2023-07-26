using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.CAD.Routes;

namespace ZANECO.API.Host.Controllers.CAD;

[EnableRateLimiting("fixed")]
public class RoutesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.CAD)]
    [OpenApiOperation("Search Routes using available filters.", "")]
    public Task<PaginationResponse<RouteDto>> SearchAsync(RouteSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.CAD)]
    [OpenApiOperation("Get Route details.", "")]
    public Task<RouteDto> GetAsync(Guid id)
    {
        return Mediator.Send(new RouteGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.CAD)]
    [OpenApiOperation("Create a new Route.", "")]
    public Task<Guid> CreateAsync(RouteCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.CAD)]
    [OpenApiOperation("Update a Route.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(RouteUpdateRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.CAD)]
    [OpenApiOperation("Delete a Route.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new RouteDeleteRequest(id));
    }
}