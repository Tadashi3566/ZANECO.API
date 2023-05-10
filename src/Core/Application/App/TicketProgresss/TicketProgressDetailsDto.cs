using ZANECO.API.Application.App.Tickets;

namespace ZANECO.API.Application.App.TicketProgresss;

public class TicketProgressDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }
    public TicketDto Ticket { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? Status { get; set; }
}