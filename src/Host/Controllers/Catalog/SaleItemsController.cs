using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.Catalog.SaleItems;

namespace ZANECO.API.Host.Controllers.Catalog;

[EnableRateLimiting("fixed")]
public class SaleItemsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Sandurot)]
    [OpenApiOperation("Search SaleItems using available filters.", "")]
    public Task<PaginationResponse<SaleItemDto>> SearchAsync(SaleItemSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Sandurot)]
    [OpenApiOperation("Get SaleItem details.", "")]
    public Task<SaleItemDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new SaleItemGetRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Sandurot)]
    [OpenApiOperation("Get SaleItem details via dapper.", "")]
    public Task<SaleItemDto> GetDapperAsync(DefaultIdType id)
    {
        return Mediator.Send(new SaleItemGetViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Sandurot)]
    [OpenApiOperation("Create a new SaleItem.", "")]
    public Task<DefaultIdType> CreateAsync(SaleItemCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Sandurot)]
    [OpenApiOperation("Update a SaleItem.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(SaleItemUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Sandurot)]
    [OpenApiOperation("Delete a SaleItem.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new SaleItemDeleteRequest(id));
    }
}