using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Schedules;

public class ScheduleByNameSpec : Specification<Schedule>, ISingleResultSpecification<Schedule>
{
    public ScheduleByNameSpec(string name) =>
        Query.Where(p => p.Name == name);
}