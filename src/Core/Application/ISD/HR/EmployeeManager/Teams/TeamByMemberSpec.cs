using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

public sealed class TeamByMemberSpec : Specification<Team, TeamDto>, ISingleResultSpecification
{
    public TeamByMemberSpec(DefaultIdType managerId, DefaultIdType memberId) =>
        Query.Where(x => x.ManagerId.Equals(managerId) && x.MemberId.Equals((memberId)));
}