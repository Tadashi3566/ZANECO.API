using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Calendars;

public class CalendarByIdSpec : Specification<Calendar, CalendarDto>, ISingleResultSpecification
{
    public CalendarByIdSpec(DefaultIdType id) => Query.Where(p => p.Id == id);
}