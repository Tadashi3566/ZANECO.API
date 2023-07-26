using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.CAD.PowerConsumptions;

namespace ZANECO.API.Host.Controllers.CAD;

[EnableRateLimiting("fixed")]
public class PowerConsumptionsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.CAD)]
    [OpenApiOperation("Search PowerConsumptions using available filters.", "")]
    public Task<PaginationResponse<PowerConsumptionDto>> SearchAsync(PowerConsumptionSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.CAD)]
    [OpenApiOperation("Get PowerConsumption details.", "")]
    public Task<PowerConsumptionDto> GetAsync(Guid id)
    {
        return Mediator.Send(new PowerConsumptionGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.CAD)]
    [OpenApiOperation("Create a new PowerConsumption.", "")]
    public Task<Guid> CreateAsync(PowerConsumptionCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.CAD)]
    [OpenApiOperation("Update a PowerConsumption.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(PowerConsumptionUpdateRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.CAD)]
    [OpenApiOperation("Delete a PowerConsumption.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new PowerConsumptionDeleteRequest(id));
    }
}