using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Calendars;

public class CalendarByDateSpec : Specification<Calendar, CalendarDto>, ISingleResultSpecification<Calendar>
{
    public CalendarByDateSpec(DateTime date) =>
        Query.Where(p => p.CalendarDate == date);
}