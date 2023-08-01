using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Accounts;

public class AccountByIdSpec : Specification<Account, AccountDto>, ISingleResultSpecification<Account>
{
    public AccountByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}