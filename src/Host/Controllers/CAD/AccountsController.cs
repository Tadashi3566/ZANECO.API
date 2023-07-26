using Microsoft.AspNetCore.RateLimiting;
using ZANECO.API.Application.CAD.Accounts;

namespace ZANECO.API.Host.Controllers.CAD;

[EnableRateLimiting("fixed")]
public class AccountsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.CAD)]
    [OpenApiOperation("Search Accounts using available filters.", "")]
    public Task<PaginationResponse<AccountDto>> SearchAsync(AccountSearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.CAD)]
    [OpenApiOperation("Get Account details.", "")]
    public Task<AccountDto> GetAsync(Guid id)
    {
        return Mediator.Send(new AccountGetRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.CAD)]
    [OpenApiOperation("Create a new Account.", "")]
    public Task<Guid> CreateAsync(AccountCreateRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.CAD)]
    [OpenApiOperation("Update a Account.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(AccountUpdateRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpPost("migrate-account")]
    [MustHavePermission(FSHAction.Create, FSHResource.CAD)]
    [OpenApiOperation("Migrate Accounts.", "")]
    public Task<Guid> MigrateAccountAsync(AccountMigrateAccountRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("migrate-ledger")]
    [MustHavePermission(FSHAction.Create, FSHResource.CAD)]
    [OpenApiOperation("Migrate Ledger.", "")]
    public Task<Guid> MigrateLedgerAsync(AccountMigrateLedgerRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.CAD)]
    [OpenApiOperation("Delete a Account.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new AccountDeleteRequest(id));
    }
}