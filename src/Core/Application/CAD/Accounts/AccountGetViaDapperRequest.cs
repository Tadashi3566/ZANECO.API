using Mapster;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Accounts;

public class AccountGetViaDapperRequest : IRequest<AccountDto>
{
    public DefaultIdType Id { get; set; }

    public AccountGetViaDapperRequest(Guid id) => Id = id;
}

public class AccountGetViaDapperRequestHandler : IRequestHandler<AccountGetViaDapperRequest, AccountDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<AccountGetViaDapperRequestHandler> _localizer;

    public AccountGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<AccountGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<AccountDto> Handle(AccountGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var account = await _repository.QueryFirstOrDefaultAsync<Account>(
        $"SELECT * FROM datazaneco.\"Accounts\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = account ?? throw new NotFoundException($"Account {request.Id} not found.");

        return account.Adapt<AccountDto>();
    }
}