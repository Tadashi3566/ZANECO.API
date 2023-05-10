using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.TimeLogs;

public class TimeLogByIdSpec : Specification<TimeLog, TimeLogDto>, ISingleResultSpecification
{
    public TimeLogByIdSpec(DefaultIdType id) => Query.Where(p => p.Id == id);
}