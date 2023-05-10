using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.Tickets;

public class TicketsByGroupSpec : Specification<Ticket>
{
    public TicketsByGroupSpec(Guid GroupId) =>
        Query.Where(p => p.GroupId == GroupId);
}