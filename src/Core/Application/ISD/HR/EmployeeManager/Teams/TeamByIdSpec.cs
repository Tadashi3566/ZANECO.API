using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

public sealed class TeamByIdSpec : Specification<Team, TeamDetailDto>, ISingleResultSpecification<Team>
{
    public TeamByIdSpec(DefaultIdType id) =>
        Query.Where(p => p.Id == id);
}