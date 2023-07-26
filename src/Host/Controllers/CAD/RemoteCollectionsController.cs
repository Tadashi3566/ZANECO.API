using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.CAD.RemoteCollections;

namespace ZANECO.API.Host.Controllers.CAD;

[EnableRateLimiting("fixed")]
public class RemoteCollectionsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.CAD)]
    [OpenApiOperation("Search RemoteCollections using available filters.", "")]
    public Task<PaginationResponse<RemoteCollectionDto>> SearchAsync(RemoteCollectionSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.CAD)]
    [OpenApiOperation("Get RemoteCollection details.", "")]
    public Task<RemoteCollectionDto> GetAsync(Guid id)
    {
        return Mediator.Send(new RemoteCollectionGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.CAD)]
    [OpenApiOperation("Create a new RemoteCollection.", "")]
    public Task<Guid> CreateAsync(RemoteCollectionCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.CAD)]
    [OpenApiOperation("Update a RemoteCollection.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(RemoteCollectionUpdateRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.CAD)]
    [OpenApiOperation("Delete a RemoteCollection.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new RemoteCollectionDeleteRequest(id));
    }

    [HttpPost("sendsms")]
    [MustHavePermission(FSHAction.Create, FSHResource.CAD)]
    [OpenApiOperation("Send SMS to Account Phone Numbers.", "")]
    public Task<Guid> SendSmsAsync(RemoteCollectionSMSRequest request)
    {
        return Mediator.Send(request);
    }
}