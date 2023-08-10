namespace ZANECO.API.Application.ISD.HR.PayrollManager.Calendars;

public class CalendarDto : BaseDto, IDto
{
    public string CalendarType { get; set; } = default!;
    public DateTime CalendarDate { get; set; } = default!;
    public string Day { get; set; } = default!;
    public bool IsNationalHoliday { get; set; } = default!;
}