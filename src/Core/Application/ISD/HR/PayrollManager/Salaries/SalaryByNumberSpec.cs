using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Salaries;

public class SalaryByNumberSpec : Specification<Salary>, ISingleResultSpecification<Salary>
{
    public SalaryByNumberSpec(int number) =>
        Query.Where(p => p.Number == number);
}