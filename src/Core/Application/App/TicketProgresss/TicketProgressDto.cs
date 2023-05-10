namespace ZANECO.API.Application.App.TicketProgresss;

public class TicketProgressDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType TicketId { get; set; }
    public string TicketCode { get; set; } = default!;
    public string TicketName { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? Status { get; set; }
    public string? ImagePath { get; set; }
}