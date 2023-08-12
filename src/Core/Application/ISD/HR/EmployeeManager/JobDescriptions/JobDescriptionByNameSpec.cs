using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.JobDescriptions;

public class JobDescriptionByNameSpec : Specification<JobDescription>, ISingleResultSpecification<JobDescription>
{
    public JobDescriptionByNameSpec(string name) =>
        Query.Where(p => p.Name.Equals(name));
}