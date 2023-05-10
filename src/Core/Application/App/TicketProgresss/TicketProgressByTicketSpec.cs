using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.TicketProgresss;

public class TicketProgressByTicketSpec : Specification<TicketProgress>
{
    public TicketProgressByTicketSpec(Guid TicketId) =>
        Query.Where(p => p.TicketId == TicketId);
}