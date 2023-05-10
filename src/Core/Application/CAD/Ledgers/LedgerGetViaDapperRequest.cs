using Mapster;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Ledgers;

public class LedgerGetViaDapperRequest : IRequest<LedgerDto>
{
    public DefaultIdType Id { get; set; }

    public LedgerGetViaDapperRequest(Guid id) => Id = id;
}

public class LedgerGetViaDapperRequestHandler : IRequestHandler<LedgerGetViaDapperRequest, LedgerDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<LedgerGetViaDapperRequestHandler> _localizer;

    public LedgerGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<LedgerGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<LedgerDto> Handle(LedgerGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var ledger = await _repository.QueryFirstOrDefaultAsync<Ledger>($"SELECT * FROM datazaneco.\"Ledgers\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = ledger ?? throw new NotFoundException(string.Format(_localizer["Ledger not found."], request.Id));

        return ledger.Adapt<LedgerDto>();
    }
}