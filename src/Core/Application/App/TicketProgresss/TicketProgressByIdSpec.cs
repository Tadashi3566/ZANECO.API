using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.TicketProgresss;

public class TicketProgressByIdSpec : Specification<TicketProgress, TicketProgressDto>, ISingleResultSpecification<TicketProgress>
{
    public TicketProgressByIdSpec(DefaultIdType id) =>
        Query
            .Where(p => p.Id == id);
}