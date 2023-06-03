using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.TimeLogs;

public class TimeLogByFileNameSpec : Specification<TimeLog, TimeLogDto>, ISingleResultSpecification
{
    public TimeLogByFileNameSpec(string fileName) => Query.Where(p => string.Concat(p.ImagePath).Contains(fileName));
}