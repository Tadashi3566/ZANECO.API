using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Contributions;

public class ContributionByIdSpec : Specification<Contribution, ContributionDto>, ISingleResultSpecification
{
    public ContributionByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}