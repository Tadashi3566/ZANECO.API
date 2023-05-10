using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Salaries;

public class SalaryByIdSpec : Specification<Salary, SalaryDto>, ISingleResultSpecification
{
    public SalaryByIdSpec(DefaultIdType id) => Query.Where(p => p.Id == id);
}