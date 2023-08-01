using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Calendars;

public class CalendarByNameSpec : Specification<Calendar>, ISingleResultSpecification<Calendar>
{
    public CalendarByNameSpec(DateTime? dt, string name) =>
        Query.Where(p => p.CalendarDate.Equals(dt) && p.Name.Equals(name));
}