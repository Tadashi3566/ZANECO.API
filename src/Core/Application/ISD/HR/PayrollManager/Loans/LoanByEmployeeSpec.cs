using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Loans;

public class LoanByEmployeeSpec : Specification<Loan>
{
    public LoanByEmployeeSpec(Guid EmployeeId) =>
        Query.Where(p => p.EmployeeId == EmployeeId);
}