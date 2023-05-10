using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Payrolls;

public class PayrollByNameSpec : Specification<Payroll>, ISingleResultSpecification
{
    public PayrollByNameSpec(string name) =>
        Query.Where(p => p.Name == name);
}