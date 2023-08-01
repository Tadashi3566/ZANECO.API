using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Accounts;

public class AccountByNameSpec : Specification<Account>, ISingleResultSpecification<Account>
{
    public AccountByNameSpec(string name) => Query.Where(p => p.Name == name);
}