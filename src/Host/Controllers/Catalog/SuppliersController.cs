using ZANECO.API.Application.Catalog.Suppliers;

namespace ZANECO.API.Host.Controllers.Catalog;

public class SuppliersController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Sandurot)]
    [OpenApiOperation("Search Suppliers using available filters.", "")]
    public Task<PaginationResponse<SupplierDto>> SearchAsync(SupplierSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Sandurot)]
    [OpenApiOperation("Get Supplier details.", "")]
    public Task<SupplierDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new SupplierGetRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Sandurot)]
    [OpenApiOperation("Get Supplier details via dapper.", "")]
    public Task<SupplierDto> GetDapperAsync(DefaultIdType id)
    {
        return Mediator.Send(new SupplierGetViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Sandurot)]
    [OpenApiOperation("Create a new Supplier.", "")]
    public Task<DefaultIdType> CreateAsync(SupplierCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Sandurot)]
    [OpenApiOperation("Update a Supplier.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(SupplierUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Sandurot)]
    [OpenApiOperation("Delete a Supplier.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new SupplierDeleteRequest(id));
    }
}