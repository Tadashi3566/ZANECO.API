using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.ISD.HR.EmployeeManager.Documents;

namespace ZANECO.API.Host.Controllers.ISD.HR.EmployeeManager;

[EnableRateLimiting("fixed")]
public class DocumentsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Documents)]
    [OpenApiOperation("Search Documents using available filters.", "")]
    public Task<PaginationResponse<DocumentDto>> SearchAsync(DocumentSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Documents)]
    [OpenApiOperation("Get Document details.", "")]
    public Task<DocumentDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new DocumentGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Documents)]
    [OpenApiOperation("Create a new Document.", "")]
    public Task<DefaultIdType> CreateAsync(DocumentCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Documents)]
    [OpenApiOperation("Update a Document.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(DocumentUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Documents)]
    [OpenApiOperation("Delete a Document.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new DocumentDeleteRequest(id));
    }
}