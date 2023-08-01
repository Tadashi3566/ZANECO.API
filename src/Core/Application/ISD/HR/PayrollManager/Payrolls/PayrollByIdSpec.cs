using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Payrolls;

public class PayrollByIdSpec : Specification<Payroll, PayrollDto>, ISingleResultSpecification<Payroll>
{
    public PayrollByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}