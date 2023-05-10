using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.Tickets;

public class TicketByNameSpec : Specification<Ticket>, ISingleResultSpecification
{
    public TicketByNameSpec(string name) =>
        Query.Where(p => p.Name == name);
}