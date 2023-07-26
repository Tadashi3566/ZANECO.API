using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.Surveys.Ratings;

namespace ZANECO.API.Host.Controllers.Surveys;

[EnableRateLimiting("fixed")]
public class RatingsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Surveys)]
    [OpenApiOperation("Search Ratings using available filters.", "")]
    public Task<PaginationResponse<RatingDto>> SearchAsync(RatingSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Surveys)]
    [OpenApiOperation("Get Rating details.", "")]
    public Task<RatingDto> GetAsync(Guid id)
    {
        return Mediator.Send(new RatingGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Surveys)]
    [OpenApiOperation("Create a new Rating.", "")]
    public Task<Guid> CreateAsync(RatingCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Surveys)]
    [OpenApiOperation("Update a Rating.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(RatingUpdateRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Surveys)]
    [OpenApiOperation("Delete a Rating.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new RatingDeleteRequest(id));
    }
}