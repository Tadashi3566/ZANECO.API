using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Accounts;

public class AccountDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public AccountDeleteRequest(Guid id) => Id = id;
}

public class AccountDeleteRequestHandler : IRequestHandler<AccountDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Account> _repository;
    private readonly IStringLocalizer<AccountDeleteRequestHandler> _localizer;

    public AccountDeleteRequestHandler(IRepositoryWithEvents<Account> repository, IStringLocalizer<AccountDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(AccountDeleteRequest request, CancellationToken cancellationToken)
    {
        var account = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = account ?? throw new NotFoundException(_localizer["Account not found."]);

        await _repository.DeleteAsync(account, cancellationToken);

        return request.Id;
    }
}