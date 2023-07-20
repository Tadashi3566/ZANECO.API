using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Accounts;

public class AccountGetRequest : IRequest<AccountDto>
{
    public DefaultIdType Id { get; set; }

    public AccountGetRequest(Guid id) => Id = id;
}

public class AccountGetRequestHandler : IRequestHandler<AccountGetRequest, AccountDto>
{
    private readonly IRepository<Account> _repository;
    private readonly IStringLocalizer<AccountGetRequestHandler> _localizer;

    public AccountGetRequestHandler(IRepository<Account> repository, IStringLocalizer<AccountGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<AccountDto> Handle(AccountGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new AccountByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException($"Account {request.Id} not found.");
}