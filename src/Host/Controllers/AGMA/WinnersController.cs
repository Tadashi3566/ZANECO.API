using ZANECO.API.Application.AGMA.Winners;

namespace ZANECO.API.Host.Controllers.AGMA;

public class WinnersController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Raffles)]
    [OpenApiOperation("Search Winners using available filters.", "")]
    public Task<PaginationResponse<WinnerDto>> SearchAsync(WinnerSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Raffles)]
    [OpenApiOperation("Get Winner details.", "")]
    public Task<WinnerDto> GetAsync(Guid id)
    {
        return Mediator.Send(new WinnerGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Raffles)]
    [OpenApiOperation("Create a new Winner.", "")]
    public Task<Guid> CreateAsync(WinnerCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Raffles)]
    [OpenApiOperation("Update a Winner.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(WinnerUpdateRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Raffles)]
    [OpenApiOperation("Delete a Winner.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new WinnerDeleteRequest(id));
    }
}