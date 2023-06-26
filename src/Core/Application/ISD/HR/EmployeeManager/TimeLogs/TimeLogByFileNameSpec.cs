using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.TimeLogs;

public sealed class TimeLogByFileNameSpec : Specification<TimeLog, TimeLogDto>, ISingleResultSpecification
{
    public TimeLogByFileNameSpec(string fileName) =>
        Query.Where(p => p.ImagePath.Contains(fileName));
}