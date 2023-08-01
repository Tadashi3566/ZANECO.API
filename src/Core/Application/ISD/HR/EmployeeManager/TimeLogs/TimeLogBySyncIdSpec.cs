using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.TimeLogs;

public sealed class TimeLogBySyncIdSpec : Specification<TimeLog, TimeLogDto>, ISingleResultSpecification<TimeLog>
{
    public TimeLogBySyncIdSpec(DateTime logDate, int syncId) =>
        Query.Where(p => p.LogDate.Equals(logDate) && p.SyncId.Equals(syncId));
}