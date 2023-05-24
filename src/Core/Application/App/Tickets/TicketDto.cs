namespace ZANECO.API.Application.App.Tickets;

public class TicketDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType GroupId { get; set; }
    public string GroupCode { get; set; } = default!;
    public string GroupName { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Impact { get; set; }
    public string? Urgency { get; set; }
    public string? Priority { get; set; }
    public string? RequestedBy { get; set; }
    public string? RequestThrough { get; set; }
    public string? Reference { get; set; }
    public string? AssignedTo { get; set; }
    public string? OpenedBy { get; set; }
    public DateTime? OpenedOn { get; set; }
    public string? ClosedBy { get; set; }
    public DateTime? ClosedOn { get; set; }
    public string? ApprovedBy { get; set; }
    public DateTime? ApprovedOn { get; set; }
    public string? Status { get; set; }
    public string? ImagePath { get; set; }
}