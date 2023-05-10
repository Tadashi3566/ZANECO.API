using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Ledgers;

public class LedgerSearchRequest : PaginationFilter, IRequest<PaginationResponse<LedgerDto>>
{
    public Guid? AccountId { get; set; } = default!;
}

public class LedgerBySearchRequestSpec : EntitiesByPaginationFilterSpec<Ledger, LedgerDto>
{
    public LedgerBySearchRequestSpec(LedgerSearchRequest request)
        : base(request) =>
        Query
            .Include(x => x.Account)
            .OrderBy(x => x.PostingDate, !request.HasOrderBy())
            .Where(x => x.AccountId.Equals(request.AccountId!.Value), request.AccountId.HasValue);
}

public class LedgerSearchRequestHandler : IRequestHandler<LedgerSearchRequest, PaginationResponse<LedgerDto>>
{
    private readonly IReadRepository<Ledger> _repository;

    public LedgerSearchRequestHandler(IReadRepository<Ledger> repository) => _repository = repository;

    public async Task<PaginationResponse<LedgerDto>> Handle(LedgerSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new LedgerBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}