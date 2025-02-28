﻿using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.Tickets;

public class TicketByIdSpec : Specification<Ticket>, ISingleResultSpecification<Ticket>
{
    public TicketByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}