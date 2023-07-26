using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.CAD.Ledgers;

namespace ZANECO.API.Host.Controllers.CAD;

[EnableRateLimiting("fixed")]
public class LedgersController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.CAD)]
    [OpenApiOperation("Search Ledgers using available filters.", "")]
    public Task<PaginationResponse<LedgerDto>> SearchAsync(LedgerSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("{id:guid}")]
    [MustHavePermission(FSHAction.Search, FSHResource.CAD)]
    [OpenApiOperation("Load Account Ledger using AccountId", "")]
    public Task<PaginationResponse<LedgerDto>> AccountLedgerAsync(Guid id)
    {
        return Mediator.Send(new AccountLedgerRequest(id));
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.CAD)]
    [OpenApiOperation("Get Ledger details.", "")]
    public Task<LedgerDto> GetAsync(Guid id)
    {
        return Mediator.Send(new LedgerGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.CAD)]
    [OpenApiOperation("Create a new Ledger.", "")]
    public Task<Guid> CreateAsync(LedgerCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.CAD)]
    [OpenApiOperation("Update a Ledger.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(LedgerUpdateRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.CAD)]
    [OpenApiOperation("Delete a Ledger.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new LedgerDeleteRequest(id));
    }
}