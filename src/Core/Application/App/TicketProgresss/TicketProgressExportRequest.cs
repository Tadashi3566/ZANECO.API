using ZANECO.API.Application.Common.Exporters;
using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.TicketProgresss;

public class TicketProgressExportRequest : BaseFilter, IRequest<Stream>
{
    public Guid? TicketId { get; set; }
}

public class TicketProgressExportRequestHandler : IRequestHandler<TicketProgressExportRequest, Stream>
{
    private readonly IReadRepository<TicketProgress> _repository;
    private readonly IExcelWriter _excelWriter;

    public TicketProgressExportRequestHandler(IReadRepository<TicketProgress> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(TicketProgressExportRequest request, CancellationToken cancellationToken)
    {
        var spec = new TicketProgressExportWithTicketsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class TicketProgressExportWithTicketsSpecification : EntitiesByBaseFilterSpec<TicketProgress, TicketProgressExportDto>
{
    public TicketProgressExportWithTicketsSpecification(TicketProgressExportRequest request)
        : base(request) =>
        Query
            .Include(p => p.Ticket)
            .Where(p => p.TicketId.Equals(request.TicketId!.Value), request.TicketId.HasValue);
}