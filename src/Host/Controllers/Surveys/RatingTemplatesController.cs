using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.Surveys.RatingTemplates;

namespace ZANECO.API.Host.Controllers.Surveys;

[EnableRateLimiting("fixed")]
public class RatingTemplatesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Rating)]
    [OpenApiOperation("Search RatingTemplates using available filters.", "")]
    public Task<PaginationResponse<RatingTemplateDto>> SearchAsync(RatingTemplateSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Rating)]
    [OpenApiOperation("Get RatingTemplate details.", "")]
    public Task<RatingTemplateDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new RatingTemplateGetRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Rating)]
    [OpenApiOperation("Get RatingTemplate details via dapper.", "")]
    public Task<RatingTemplateDto> GetDapperAsync(Guid id)
    {
        return Mediator.Send(new RatingTemplateGetViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Rating)]
    [OpenApiOperation("Create a new RatingTemplate.", "")]
    public Task<Guid> CreateAsync(RatingTemplateCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Rating)]
    [OpenApiOperation("Update a RatingTemplate.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(RatingTemplateUpdateRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Rating)]
    [OpenApiOperation("Delete a RatingTemplate.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new RatingTemplateDeleteRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Rating)]
    [OpenApiOperation("Export a RatingTemplates.", "")]
    public async Task<FileResult> ExportAsync(RatingTemplateExportRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "RatingTemplateExports");
    }
}