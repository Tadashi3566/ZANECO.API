using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Ledgers;

public class AccountLedgerRequest : PaginationFilter, IRequest<PaginationResponse<LedgerDto>>
{
    public DefaultIdType? AccountId { get; set; }

    public AccountLedgerRequest(DefaultIdType? accountId) => AccountId = accountId;
}

public class LedgerByAccountRequestSpec : Specification<Ledger, LedgerDto>
{
    public LedgerByAccountRequestSpec(Guid? accountId) =>
        Query
        .OrderBy(x => x.PostingDate)
        .Where(x => x.AccountId.Equals(accountId));
}

public class LedgerAccountRequestHandler : IRequestHandler<AccountLedgerRequest, PaginationResponse<LedgerDto>>
{
    private readonly IRepository<Ledger> _repository;

    public LedgerAccountRequestHandler(IRepository<Ledger> repository) => _repository = repository;

    public async Task<PaginationResponse<LedgerDto>> Handle(AccountLedgerRequest request, CancellationToken cancellationToken)
    {
        var spec = new LedgerByAccountRequestSpec(request.AccountId);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}