namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class Calendar : AuditableEntity, IAggregateRoot
{
    public Calendar()
    {
    }

    public string CalendarType { get; private set; } = default!; // WORKING HOLIDAY, NON-WORKING HOLIDAY, VACATION LEAVE ETC.
    public DateTime CalendarDate { get; private set; }
    public string Day { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    public Calendar(string calendarType, DateTime calendarDate, string name, string? description = "", string? notes = "")
    {
        CalendarType = calendarType;
        CalendarDate = calendarDate;
        Day = calendarDate.DayOfWeek.ToString().ToUpper();
        Name = name.Trim().ToUpper();

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();
    }

    public Calendar Update(string calendarType, DateTime calendarDate, string name, string? description = "", string? notes = "")
    {
        if (calendarType is not null && !CalendarType.Equals(calendarType)) CalendarType = calendarType;

        if (!CalendarDate.Equals(calendarDate))
        {
            CalendarDate = calendarDate;
            Day = calendarDate!.DayOfWeek.ToString().ToUpper();
        }

        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        return this;
    }
}