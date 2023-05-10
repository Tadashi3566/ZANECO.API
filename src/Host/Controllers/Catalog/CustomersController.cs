using ZANECO.API.Application.Catalog.Customers;

namespace ZANECO.API.Host.Controllers.Catalog;

public class CustomersController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Sandurot)]
    [OpenApiOperation("Search Customers using available filters.", "")]
    public Task<PaginationResponse<CustomerDto>> SearchAsync(CustomerSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Sandurot)]
    [OpenApiOperation("Get Customer details.", "")]
    public Task<CustomerDto> GetAsync(DefaultIdType id)
    {
        return Mediator.Send(new CustomerGetRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Sandurot)]
    [OpenApiOperation("Get Customer details via dapper.", "")]
    public Task<CustomerDto> GetDapperAsync(DefaultIdType id)
    {
        return Mediator.Send(new CustomerGetViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Sandurot)]
    [OpenApiOperation("Create a new Customer.", "")]
    public Task<DefaultIdType> CreateAsync(CustomerCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Sandurot)]
    [OpenApiOperation("Update a Customer.", "")]
    public async Task<ActionResult<DefaultIdType>> UpdateAsync(CustomerUpdateRequest request, DefaultIdType id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Sandurot)]
    [OpenApiOperation("Delete a Customer.", "")]
    public Task<DefaultIdType> DeleteAsync(DefaultIdType id)
    {
        return Mediator.Send(new CustomerDeleteRequest(id));
    }
}