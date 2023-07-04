using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

public sealed class TeamByIdSpec : Specification<Team, TeamDto>, ISingleResultSpecification
{
    public TeamByIdSpec(DefaultIdType id) =>
        Query.Where(p => p.Id == id);
}