using ZANECO.API.Application.Common.Exporters;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeeAdjustments;

public class EmployeeAdjustmentExportRequest : BaseFilter, IRequest<Stream>
{
    public Guid? EmployeeId { get; set; }
}

public class EmployeeAdjustmentExportRequestHandler : IRequestHandler<EmployeeAdjustmentExportRequest, Stream>
{
    private readonly IReadRepository<EmployeeAdjustment> _repository;
    private readonly IExcelWriter _excelWriter;

    public EmployeeAdjustmentExportRequestHandler(IReadRepository<EmployeeAdjustment> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(EmployeeAdjustmentExportRequest request, CancellationToken cancellationToken)
    {
        var spec = new EmployeeAdjustmentExportWithEmployeeSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class EmployeeAdjustmentExportWithEmployeeSpecification : EntitiesByBaseFilterSpec<EmployeeAdjustment, EmployeeAdjustmentExportDto>
{
    public EmployeeAdjustmentExportWithEmployeeSpecification(EmployeeAdjustmentExportRequest request)
        : base(request) =>
        Query
            .Include(p => p.Employee)
            .Where(p => p.EmployeeId.Equals(request.EmployeeId!.Value), request.EmployeeId.HasValue);
}