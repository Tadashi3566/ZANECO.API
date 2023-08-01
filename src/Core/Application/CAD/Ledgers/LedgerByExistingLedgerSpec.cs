using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Ledgers;

public class LedgerByExistingLedgerSpec : Specification<Ledger, LedgerDto>, ISingleResultSpecification<Ledger>
{
    public LedgerByExistingLedgerSpec(int idCode, string billNumber, string billMonth) =>
        Query.Where(l => l.IdCode == idCode && l.BillNumber == billNumber && l.BillMonth == billMonth);
}