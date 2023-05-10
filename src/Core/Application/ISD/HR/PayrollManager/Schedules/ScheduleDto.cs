namespace ZANECO.API.Application.ISD.HR.PayrollManager.Schedules;

public class ScheduleDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? Status { get; set; }
}