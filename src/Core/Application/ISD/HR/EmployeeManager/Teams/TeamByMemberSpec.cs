using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

public sealed class TeamByMemberSpec : Specification<Team, TeamDto>, ISingleResultSpecification<Team>
{
    public TeamByMemberSpec(DefaultIdType leaderId, DefaultIdType employeeId) =>
        Query.Where(x => x.LeaderId.Equals(leaderId) && x.EmployeeId.Equals(employeeId));
}