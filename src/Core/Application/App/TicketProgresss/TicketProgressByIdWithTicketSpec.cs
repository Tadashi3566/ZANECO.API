using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.TicketProgresss;

public class TicketProgressByIdWithTicketSpec : Specification<TicketProgress, TicketProgressDetailsDto>, ISingleResultSpecification
{
    public TicketProgressByIdWithTicketSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Ticket);
}