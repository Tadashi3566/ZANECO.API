using ZANECO.API.Application.CAD.PowerRates;

namespace ZANECO.API.Host.Controllers.CAD;

public class PowerRatesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.CAD)]
    [OpenApiOperation("Search PowerRates using available filters.", "")]
    public Task<PaginationResponse<PowerRateDto>> SearchAsync(PowerRateSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.CAD)]
    [OpenApiOperation("Get PowerRate details.", "")]
    public Task<PowerRateDto> GetAsync(Guid id)
    {
        return Mediator.Send(new PowerRateGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.CAD)]
    [OpenApiOperation("Create a new PowerRate.", "")]
    public Task<Guid> CreateAsync(PowerRateCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.CAD)]
    [OpenApiOperation("Update a PowerRate.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(PowerRateUpdateRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.CAD)]
    [OpenApiOperation("Delete a PowerRate.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new PowerRateDeleteRequest(id));
    }
}