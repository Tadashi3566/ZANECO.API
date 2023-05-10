using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Calendars;

public class CalendarByDateSpec : Specification<Calendar, CalendarDto>, ISingleResultSpecification
{
    public CalendarByDateSpec(DateTime date) => Query.Where(p => p.CalendarDate == date);
}