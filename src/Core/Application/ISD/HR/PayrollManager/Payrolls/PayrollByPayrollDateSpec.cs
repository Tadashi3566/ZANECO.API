using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Payrolls;

public class PayrollByPayrollDateSpec : Specification<Payroll>, ISingleResultSpecification<Payroll>
{
    public PayrollByPayrollDateSpec(DateTime? payrollDate) =>
        Query.Where(p => p.PayrollDate == payrollDate);
}