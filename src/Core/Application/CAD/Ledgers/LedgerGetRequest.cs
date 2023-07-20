using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Ledgers;

public class LedgerGetRequest : IRequest<LedgerDto>
{
    public DefaultIdType Id { get; set; }

    public LedgerGetRequest(Guid id) => Id = id;
}

public class LedgerGetRequestHandler : IRequestHandler<LedgerGetRequest, LedgerDto>
{
    private readonly IRepository<Ledger> _repository;
    private readonly IStringLocalizer<LedgerGetRequestHandler> _localizer;

    public LedgerGetRequestHandler(IRepository<Ledger> repository, IStringLocalizer<LedgerGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<LedgerDto> Handle(LedgerGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new LedgerByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException($"Ledger {request.Id} not found.");
}