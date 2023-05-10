using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Loans;

public class LoanByEmployeeAdjustmentSpec : Specification<Loan, LoanDto>, ISingleResultSpecification
{
    public LoanByEmployeeAdjustmentSpec(Guid employeeId, Guid adjustmentId) =>
        Query.Where(p => p.EmployeeId == employeeId && p.AdjustmentId == adjustmentId);
}