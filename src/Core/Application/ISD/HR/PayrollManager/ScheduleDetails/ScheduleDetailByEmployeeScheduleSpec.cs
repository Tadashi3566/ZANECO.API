using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.ScheduleDetails;

public class ScheduleDetailByEmployeeScheduleSpec : Specification<ScheduleDetail, ScheduleDetailDto>, ISingleResultSpecification<ScheduleDetail>
{
    public ScheduleDetailByEmployeeScheduleSpec(Guid scheduleId, string day) =>
        Query.Where(p => p.ScheduleId == scheduleId &&
                                p.Day == day);
}