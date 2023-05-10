using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Loans;

public class LoanByIdSpec : Specification<Loan, LoanDto>, ISingleResultSpecification
{
    public LoanByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}