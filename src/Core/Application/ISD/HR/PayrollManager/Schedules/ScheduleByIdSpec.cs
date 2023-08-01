using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Schedules;

public class ScheduleByIdSpec : Specification<Schedule, ScheduleDto>, ISingleResultSpecification<Schedule>
{
    public ScheduleByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}