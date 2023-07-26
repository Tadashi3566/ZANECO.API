using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.Catalog.Barcodes;

namespace ZANECO.API.Host.Controllers.Catalog;

[EnableRateLimiting("fixed")]
public class BarcodesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Sandurot)]
    [OpenApiOperation("Search Barcodes using available filters.", "")]
    public Task<PaginationResponse<BarcodeDto>> SearchAsync(BarcodeSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Sandurot)]
    [OpenApiOperation("Get Barcode details.", "")]
    public Task<BarcodeDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new BarcodeGetRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Sandurot)]
    [OpenApiOperation("Get Barcode details via dapper.", "")]
    public Task<BarcodeDto> GetDapperAsync(DefaultIdType id)
    {
        return Mediator.Send(new BarcodeGetViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Sandurot)]
    [OpenApiOperation("Create a new Barcode.", "")]
    public Task<DefaultIdType> CreateAsync(BarcodeCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Sandurot)]
    [OpenApiOperation("Update a Barcode.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(BarcodeUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Sandurot)]
    [OpenApiOperation("Delete a Barcode.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new BarcodeDeleteRequest(id));
    }
}