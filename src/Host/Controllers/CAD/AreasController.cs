using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.CAD.Areas;

namespace ZANECO.API.Host.Controllers.CAD;

[EnableRateLimiting("fixed")]
public class AreasController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.CAD)]
    [OpenApiOperation("Search Areas using available filters.", "")]
    public Task<PaginationResponse<AreaDto>> SearchAsync(AreaSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.CAD)]
    [OpenApiOperation("Get Area details.", "")]
    public Task<AreaDto> GetAsync(Guid id)
    {
        return Mediator.Send(new AreaGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.CAD)]
    [OpenApiOperation("Create a new Area.", "")]
    public Task<Guid> CreateAsync(AreaCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.CAD)]
    [OpenApiOperation("Update a Area.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(AreaUpdateRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.CAD)]
    [OpenApiOperation("Delete a Area.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new AreaDeleteRequest(id));
    }
}