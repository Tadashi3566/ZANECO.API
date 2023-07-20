using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Ledgers;

public class LedgerDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public LedgerDeleteRequest(Guid id) => Id = id;
}

public class LedgerDeleteRequestHandler : IRequestHandler<LedgerDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Ledger> _repository;
    private readonly IStringLocalizer<LedgerDeleteRequestHandler> _localizer;

    public LedgerDeleteRequestHandler(IRepositoryWithEvents<Ledger> repository, IStringLocalizer<LedgerDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(LedgerDeleteRequest request, CancellationToken cancellationToken)
    {
        var ledger = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = ledger ?? throw new NotFoundException($"Ledger {request.Id} not found.");

        await _repository.DeleteAsync(ledger, cancellationToken);

        return request.Id;
    }
}