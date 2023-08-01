using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.Tickets;

public class TicketByIdWithGroupSpec : Specification<Ticket, TicketDetailsDto>, ISingleResultSpecification<Ticket>
{
    public TicketByIdWithGroupSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Group);
}