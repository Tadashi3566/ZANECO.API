using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Ledgers;

public class LedgerByIdSpec : Specification<Ledger, LedgerDto>, ISingleResultSpecification
{
    public LedgerByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}