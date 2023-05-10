using ZANECO.API.Application.Catalog.Discounts;

namespace ZANECO.API.Host.Controllers.Catalog;

public class DiscountsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Sandurot)]
    [OpenApiOperation("Search Discounts using available filters.", "")]
    public Task<PaginationResponse<DiscountDto>> SearchAsync(DiscountSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Sandurot)]
    [OpenApiOperation("Get Discount details.", "")]
    public Task<DiscountDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new DiscountGetRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Sandurot)]
    [OpenApiOperation("Get Discount details via dapper.", "")]
    public Task<DiscountDto> GetDapperAsync(DefaultIdType id)
    {
        return Mediator.Send(new DiscountGetViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Sandurot)]
    [OpenApiOperation("Create a new Discount.", "")]
    public Task<DefaultIdType> CreateAsync(DiscountCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Sandurot)]
    [OpenApiOperation("Update a Discount.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(DiscountUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Sandurot)]
    [OpenApiOperation("Delete a Discount.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new DiscountDeleteRequest(id));
    }
}