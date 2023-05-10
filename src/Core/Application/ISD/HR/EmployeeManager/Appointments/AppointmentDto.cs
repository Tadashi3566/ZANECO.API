namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Appointments;

public class AppointmentDto : IDto
{
    public int Id { get; set; }
    public DefaultIdType EmployeeId { get; set; }
    public string EmployeeName { get; set; } = default!;
    public string AppointmentType { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public DateTime StartDateTime { get; set; } = default!;
    public DateTime EndDateTime { get; set; } = default!;
    public string Location { get; set; } = default!;
    public bool IsAllDay { get; set; } = default!;
    public bool IsReadonly { get; set; } = default!;
    public bool IsBlock { get; set; } = default!;
    public int? CalendarId { get; set; }
    public int? RecurrenceID { get; set; }
    public string? RecurrenceRule { get; set; }
    public string? RecurrenceException { get; set; }
    public string? CssClass { get; set; }

    public DefaultIdType? RecommendedBy { get; set; }
    public DateTime? RecommendedOn { get; set; }
    public DefaultIdType? ApprovedBy { get; set; }
    public DateTime? ApprovedOn { get; set; }

    public string Description { get; set; } = default!;
    public string Notes { get; set; } = default!;
    public string Status { get; set; } = default!;
}