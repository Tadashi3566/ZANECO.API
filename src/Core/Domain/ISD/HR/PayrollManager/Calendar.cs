namespace ZANECO.API.Domain.ISD.HR.PayrollManager;

public class Calendar : AuditableEntity, IAggregateRoot
{
    public Calendar()
    {
    }

    public string CalendarType { get; private set; } = default!; // WORKING HOLIDAY, NON-WORKING HOLIDAY, VACATION LEAVE ETC.
    public DateTime CalendarDate { get; private set; }
    public string Day { get; private set; } = default!;

    public bool IsNationalHoliday { get; private set; } = default!;

    public Calendar(string calendarType, DateTime calendarDate, string name, bool isNationalHoliday, string? description = null, string? notes = null)
    {
        CalendarType = calendarType;
        CalendarDate = calendarDate;
        Day = calendarDate.DayOfWeek.ToString().ToUpper();
        Name = name.Trim().ToUpper();
        IsNationalHoliday = isNationalHoliday;

        if (description is not null && Description?.Equals(description) != true) Description = description.Trim();
        if (notes is not null && Notes?.Equals(notes) != true) Notes = notes.Trim();
    }

    public Calendar Update(string calendarType, DateTime calendarDate, string name, bool isNationalHoliday, string? description = null, string? notes = null)
    {
        if (calendarType is not null && !CalendarType.Equals(calendarType)) CalendarType = calendarType;

        if (!CalendarDate.Equals(calendarDate))
        {
            CalendarDate = calendarDate;
            Day = calendarDate!.DayOfWeek.ToString().ToUpper();
        }

        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();
        if (!IsNationalHoliday.Equals(isNationalHoliday)) IsNationalHoliday = isNationalHoliday;

        if (description is not null && Description?.Equals(description) != true) Description = description.Trim();
        if (notes is not null && Notes?.Equals(notes) != true) Notes = notes.Trim();

        return this;
    }
}