using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Salaries;

public class SalaryByNameSpec : Specification<Salary, SalaryDto>, ISingleResultSpecification
{
    public SalaryByNameSpec(string name) =>
        Query.Where(p => p.Name == name);
}