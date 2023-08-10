using ZANECO.API.Application.App.Tickets;

namespace ZANECO.API.Application.App.TicketProgresss;

public class TicketProgressDetailsDto : DtoExtension, IDto
{
    public TicketDto Ticket { get; set; } = default!;

}