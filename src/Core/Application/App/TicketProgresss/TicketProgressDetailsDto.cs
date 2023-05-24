using ZANECO.API.Application.App.Tickets;

namespace ZANECO.API.Application.App.TicketProgresss;

public class TicketProgressDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }
    public TicketDto Ticket { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
}