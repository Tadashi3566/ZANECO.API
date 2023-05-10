using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.ScheduleDetails;

public class ScheduleDetailByIdSpec : Specification<ScheduleDetail, ScheduleDetailDto>, ISingleResultSpecification
{
    public ScheduleDetailByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}