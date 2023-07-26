using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.ISD.Members;

namespace ZANECO.API.Host.Controllers.ISD;

[EnableRateLimiting("fixed")]
public class MembersController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.CAD)]
    [OpenApiOperation("Search Members using available filters.", "")]
    public Task<PaginationResponse<MemberDto>> SearchAsync(MemberSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.CAD)]
    [OpenApiOperation("Get Member details.", "")]
    public Task<MemberDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new MemberGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.CAD)]
    [OpenApiOperation("Create a new Member.", "")]
    public Task<DefaultIdType> CreateAsync(MemberCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.CAD)]
    [OpenApiOperation("Update a Member.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(MemberUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.CAD)]
    [OpenApiOperation("Delete a Member.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new MemberDeleteRequest(id));
    }
}