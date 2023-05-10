using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Contributions;

public class ContributionBySalariespec : Specification<Contribution, ContributionDto>, ISingleResultSpecification
{
    public ContributionBySalariespec(string contributionType, DateTime? payrollDate, decimal salary) =>
        Query.Where(p => p.ContributionType.Equals(contributionType)
            && p.StartDate <= payrollDate
            && p.EndDate >= payrollDate
            && p.RangeStart <= salary)
        .Take(1)
        .OrderByDescending(p => p.RangeStart);
}