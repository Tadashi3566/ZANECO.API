using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.JobDescriptions;

public class JobDescriptionByIdSpec : Specification<JobDescription, JobDescriptionDto>, ISingleResultSpecification<JobDescription>
{
    public JobDescriptionByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}