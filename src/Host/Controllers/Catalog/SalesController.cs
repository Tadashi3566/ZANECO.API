using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.Catalog.Sales;

namespace ZANECO.API.Host.Controllers.Catalog;

[EnableRateLimiting("fixed")]
public class SalesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Sandurot)]
    [OpenApiOperation("Search Sales using available filters.", "")]
    public Task<PaginationResponse<SaleDto>> SearchAsync(SaleSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Sandurot)]
    [OpenApiOperation("Get Sale details.", "")]
    public Task<SaleDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new SaleGetRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Sandurot)]
    [OpenApiOperation("Get Sale details via dapper.", "")]
    public Task<SaleDto> GetDapperAsync(DefaultIdType id)
    {
        return Mediator.Send(new SaleGetViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Sandurot)]
    [OpenApiOperation("Create a new Sale.", "")]
    public Task<DefaultIdType> CreateAsync(SaleCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Sandurot)]
    [OpenApiOperation("Update a Sale.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(SaleUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Sandurot)]
    [OpenApiOperation("Delete a Sale.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new SaleDeleteRequest(id));
    }
}