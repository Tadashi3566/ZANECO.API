namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Calendars;

public class CalendarDto : IDto
{
    public DefaultIdType Id { get; set; }

    public string CalendarType { get; set; } = default!;
    public DateTime CalendarDate { get; set; } = default!;
    public string Day { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}