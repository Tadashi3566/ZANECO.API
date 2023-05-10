using ZANECO.API.Application.App.Groups;

namespace ZANECO.API.Host.Controllers.App;

public class GroupsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Groups)]
    [OpenApiOperation("Search Groups using available filters.", "")]
    public Task<PaginationResponse<GroupDto>> SearchAsync(GroupSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Groups)]
    [OpenApiOperation("Get Group details.", "")]
    public Task<GroupDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GroupGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Groups)]
    [OpenApiOperation("Create a new Group.", "")]
    public Task<Guid> CreateAsync(GroupCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Groups)]
    [OpenApiOperation("Update a Group.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(GroupUpdateRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Groups)]
    [OpenApiOperation("Delete a Group.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new GroupDeleteRequest(id));
    }
}