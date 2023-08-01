using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.TimeLogs;

public sealed class TimeLogByIdSpec : Specification<TimeLog, TimeLogDto>, ISingleResultSpecification<TimeLog>
{
    public TimeLogByIdSpec(DefaultIdType id) =>
        Query.Where(p => p.Id == id);
}