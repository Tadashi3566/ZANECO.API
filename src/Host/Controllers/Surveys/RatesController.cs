using ZANECO.API.Application.Surveys.Rates;

namespace ZANECO.API.Host.Controllers.Surveys;

public class RatesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Rating)]
    [OpenApiOperation("Search Rates using available filters.", "")]
    public Task<PaginationResponse<RateDto>> SearchAsync(RateSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Rating)]
    [OpenApiOperation("Get Rate details.", "")]
    public Task<RateDto> GetAsync(Guid id)
    {
        return Mediator.Send(new RateGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Rating)]
    [OpenApiOperation("Create a new Rate.", "")]
    public Task<Guid> CreateAsync(RateCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Rating)]
    [OpenApiOperation("Update a Rate.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(RateUpdateRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Rating)]
    [OpenApiOperation("Delete a Rate.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new RateDeleteRequest(id));
    }
}