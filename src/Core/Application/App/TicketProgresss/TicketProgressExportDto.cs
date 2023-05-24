namespace ZANECO.API.Application.App.TicketProgresss;

public class TicketProgressExportDto : IDto
{
    public string TicketCode { get; set; } = default!;
    public string TicketName { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
}