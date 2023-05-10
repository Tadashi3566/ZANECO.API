using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Powerbills;

public class PowerbillByCommentSpec : Specification<Powerbill>, ISingleResultSpecification
{
    public PowerbillByCommentSpec(string name) =>
        Query.Where(p => p.Name == name);
}