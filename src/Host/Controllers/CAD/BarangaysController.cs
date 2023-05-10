using ZANECO.API.Application.CAD.Barangays;

namespace ZANECO.API.Host.Controllers.CAD;

public class BarangaysController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.CAD)]
    [OpenApiOperation("Search Barangays using available filters.", "")]
    public Task<PaginationResponse<BarangayDto>> SearchAsync(BarangaySearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.CAD)]
    [OpenApiOperation("Get Barangay details.", "")]
    public Task<BarangayDto> GetAsync(Guid id)
    {
        return Mediator.Send(new BarangayGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.CAD)]
    [OpenApiOperation("Create a new Barangay.", "")]
    public Task<Guid> CreateAsync(BarangayCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.CAD)]
    [OpenApiOperation("Update a Barangay.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(BarangayUpdateRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.CAD)]
    [OpenApiOperation("Delete a Barangay.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new BarangayDeleteRequest(id));
    }
}