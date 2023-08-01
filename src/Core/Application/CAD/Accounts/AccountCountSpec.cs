using ZANECO.API.Application.CAD.Ledgers;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Accounts;

public class AccountCountSpec : Specification<Ledger, LedgerDto>, ISingleResultSpecification<Ledger>
{
    public AccountCountSpec(int idCode) => Query.Where(p => p.IdCode == idCode);
}