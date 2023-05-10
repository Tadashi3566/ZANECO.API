using ZANECO.API.Application.Common.Exporters;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Powerbills;

public class PowerbillExportRequest : BaseFilter, IRequest<Stream>
{
    public Guid? EmployeeId { get; set; }
}

public class PowerbillExportRequestHandler : IRequestHandler<PowerbillExportRequest, Stream>
{
    private readonly IReadRepository<Powerbill> _repository;
    private readonly IExcelWriter _excelWriter;

    public PowerbillExportRequestHandler(IReadRepository<Powerbill> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(PowerbillExportRequest request, CancellationToken cancellationToken)
    {
        var spec = new PowerbillExportWithEmployeeSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class PowerbillExportWithEmployeeSpecification : EntitiesByBaseFilterSpec<Powerbill, PowerbillExportDto>
{
    public PowerbillExportWithEmployeeSpecification(PowerbillExportRequest request)
        : base(request) =>
        Query
            .Include(p => p.Employee)
            .Where(p => p.EmployeeId.Equals(request.EmployeeId!.Value), request.EmployeeId.HasValue);
}