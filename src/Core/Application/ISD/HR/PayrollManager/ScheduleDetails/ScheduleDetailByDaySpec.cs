using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.ScheduleDetails;

public class ScheduleDetailByDaySpec : Specification<ScheduleDetail, ScheduleDetailDto>, ISingleResultSpecification<ScheduleDetail>
{
    public ScheduleDetailByDaySpec(Guid scheduleId, string day) =>
        Query.Where(p => p.ScheduleId == scheduleId
                                && p.Day == day);
}