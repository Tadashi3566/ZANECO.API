using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.JobDescriptions;

public class JobDescriptionByNumberSpec : Specification<JobDescription>, ISingleResultSpecification<JobDescription>
{
    public JobDescriptionByNumberSpec(int number) =>
        Query.Where(p => p.Number.Equals(number));
}