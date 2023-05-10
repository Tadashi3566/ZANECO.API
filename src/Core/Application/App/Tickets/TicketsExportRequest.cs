using ZANECO.API.Application.Common.Exporters;
using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.Tickets;

public class TicketExportRequest : BaseFilter, IRequest<Stream>
{
    public Guid? GroupId { get; set; }
}

public class TicketsExportRequestHandler : IRequestHandler<TicketExportRequest, Stream>
{
    private readonly IReadRepository<Ticket> _repository;
    private readonly IExcelWriter _excelWriter;

    public TicketsExportRequestHandler(IReadRepository<Ticket> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(TicketExportRequest request, CancellationToken cancellationToken)
    {
        var spec = new TicketsExportWithGroupsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class TicketsExportWithGroupsSpecification : EntitiesByBaseFilterSpec<Ticket, TicketExportDto>
{
    public TicketsExportWithGroupsSpecification(TicketExportRequest request)
        : base(request) =>
        Query
            .Include(p => p.Group)
            .Where(p => p.GroupId.Equals(request.GroupId!.Value), request.GroupId.HasValue);
}