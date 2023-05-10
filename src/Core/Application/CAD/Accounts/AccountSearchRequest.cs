using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Accounts;

public class AccountSearchRequest : PaginationFilter, IRequest<PaginationResponse<AccountDto>>
{
}

public class AccountsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Account, AccountDto>
{
    public AccountsBySearchRequestSpec(AccountSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class AccountSearchRequestHandler : IRequestHandler<AccountSearchRequest, PaginationResponse<AccountDto>>
{
    private readonly IReadRepository<Account> _repository;

    public AccountSearchRequestHandler(IReadRepository<Account> repository) => _repository = repository;

    public async Task<PaginationResponse<AccountDto>> Handle(AccountSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new AccountsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}