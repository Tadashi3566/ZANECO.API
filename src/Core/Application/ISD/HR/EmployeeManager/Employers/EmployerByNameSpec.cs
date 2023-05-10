using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employers;

public class EmployerByCommentSpec : Specification<Employer>, ISingleResultSpecification
{
    public EmployerByCommentSpec(string name) =>
        Query.Where(p => p.Name == name);
}